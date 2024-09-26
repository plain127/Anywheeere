using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    // 점수 변수
    public int score = 0;

    // 점수 표시할 텍스트
    public Text scoreText;

    private void Awake()
    {
        // 싱글톤 패턴으로 하나의 인스턴스만 존재하도록 설정
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 점수 추가 함수
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("점수 추가됨. 현재 점수: " + score);  // 점수 추가 로그
        UpdateScoreUI();
    }

    // 점수 UI 업데이트 함수
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            Debug.LogWarning("ScoreText가 설정되지 않았습니다.");
        }
    }
}