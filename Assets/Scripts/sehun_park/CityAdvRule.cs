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

    // 카운트다운을 출력할 Text
    public Text countdownText;

    // 타이머가 실행 중인지 여부
    private bool timerRunning = false;

    private void Start()
    {
        // 마스터 클라이언트에서만 카운트다운 시작
        if (PhotonNetwork.IsMasterClient)
        {
            // 5초 동안 카운트다운 후 타이머 시작
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
            // 카운트다운 텍스트 업데이트
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        // 카운트다운 텍스트 파괴
        Destroy(countdownText.gameObject);

        // 타이머 시작
        StartTimer();
    }

    // 타이머 시작을 위한 RPC 호출 (모든 클라이언트에서 타이머 시작)
    private void StartTimer()
    {
        photonView.RPC(nameof(StartTimerRPC), RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void StartTimerRPC()
    {
        // 타이머 시작 설정
        timerRunning = true;
        // 매 1초마다 UpdateTimer() 호출
        InvokeRepeating(nameof(UpdateTimer), 1.0f, 1.0f);
    }

    // 매 초마다 타이머를 업데이트
    private void UpdateTimer()
    {
        if (!timerRunning) return;

        // 초가 0이면 분을 줄이고, 초를 59로 설정
        if (seconds == 0)
        {
            if (minutes == 0)
            {
                // 타이머가 종료되면
                timerRunning = false;
                CancelInvoke(nameof(UpdateTimer));

                // 시간이 다 되었을 때
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

        // 타이머 UI 업데이트
        UpdateTimerUI();
    }

    // 타이머 UI를 갱신하는 함수
    private void UpdateTimerUI()
    {
        string timeText = string.Format("{0}:{1:00}", minutes, seconds);
        timerText.text = timeText;
    }

    // 시간이 다 되었을 때 호출되는 함수
    private void TimeUp()
    {
        // 시간 종료 로그 출력
        Debug.Log("시간 종료");
    }
}