using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CityAdvRule : MonoBehaviourPun
{
    // 타이머 시간 (1분)
    private int minutes = 1;
    private int seconds = 0;

    // 타이머 UI 출력할 Text
    public Text timerText;
    public Text countdownText; // 카운트다운 텍스트

    public GameObject showImage;
    public GameObject timeOverImage;

    // 타이머가 실행 중인지 여부
    private bool timerRunning = false;

    private void Start()
    {
        if (showImage != null)
        {
            showImage.SetActive(false);
        }

        if (timeOverImage != null)
        {
            timeOverImage.SetActive(false);
        }
        // 마스터 클라이언트에서만 카운트다운 시작
        if (PhotonNetwork.IsMasterClient)
        {
            // 5초 카운트다운 후 타이머 시작
            photonView.RPC(nameof(StartCountdownRPC), RpcTarget.AllBuffered);
        }
    }

    // 카운트다운 시작을 위한 RPC 호출
    [PunRPC]
    private void StartCountdownRPC()
    {
        StartCoroutine(StartCountdown());
    }

    // 5초 카운트다운 코루틴
    private IEnumerator StartCountdown()
    {
        int countdown = 5;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString(); // 카운트다운 텍스트 업데이트
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = ""; // 카운트다운이 끝나면 텍스트 지우기

        // 카운트다운이 끝났을 때 이미지를 보여주기 위한 RPC 호출
        photonView.RPC(nameof(ShowImageRPC), RpcTarget.AllBuffered);

        StartTimer(); // 타이머 시작
    }

    [PunRPC]
    private void ShowImageRPC()
    {
        StartCoroutine(ShowImage());
    }

    private IEnumerator ShowImage()
    {
        if (showImage != null)
        {
            showImage.SetActive(true); // 이미지 활성화
            yield return new WaitForSeconds(1f);
            showImage.SetActive(false); // 1초 후 이미지 비활성화
        }
    }

    // 타이머 시작을 위한 RPC 호출
    private void StartTimer()
    {
        photonView.RPC(nameof(StartTimerRPC), RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void StartTimerRPC()
    {
        if (!timerRunning)
        {
            timerRunning = true;
            InvokeRepeating(nameof(UpdateTimer), 1.0f, 1.0f);
        }
    }

    // 매 초마다 타이머를 업데이트
    private void UpdateTimer()
    {
        if (!timerRunning) return;

        if (seconds == 0)
        {
            if (minutes == 0)
            {
                timerRunning = false;
                CancelInvoke(nameof(UpdateTimer));
                TimeUp();
                return;
            }
            else
            {
                minutes--;
                seconds = 59;
            }
        }
        else
        {
            seconds--;
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    private void TimeUp()
    {
        Debug.Log("시간 종료");

        // 타임 오버 이미지를 보여주기 위한 RPC 호출
        photonView.RPC(nameof(ShowTimeOverImageRPC), RpcTarget.AllBuffered);

        ResultManager.instance.ShowResults();  // 결과 출력
    }

    // 타임 오버 이미지를 보여주기 위한 RPC
    [PunRPC]
    private void ShowTimeOverImageRPC()
    {
        StartCoroutine(ShowTimeOverImage());
    }

    // 2초 동안 타임 오버 이미지를 보여주는 코루틴
    private IEnumerator ShowTimeOverImage()
    {
        if (timeOverImage != null)
        {
            timeOverImage.SetActive(true); // 타임 오버 이미지 활성화
            yield return new WaitForSeconds(2f);
            timeOverImage.SetActive(false); // 2초 후 타임 오버 이미지 비활성화
        }
    }
}