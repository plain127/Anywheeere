using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ResultManager : MonoBehaviourPun
{
    public static ResultManager instance;

    public Text resultText;  // 결과를 표시할 텍스트

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 타이머 종료 후 결과 출력 함수
    public void ShowResults()
    {
        // ScoreManager에서 점수 가져오기
        int finalScore = ScoreManager.instance.score;
        Debug.Log("최종 점수: " + finalScore);  // 디버그로 최종 점수 확인

        // 결과 텍스트 업데이트
        resultText.text = "Game Over\nResult: " + finalScore + " points";

        // 콘솔에도 출력
        Debug.Log("Result: " + finalScore);
    }
}