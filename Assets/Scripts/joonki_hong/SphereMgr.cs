using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SphereManager : MonoBehaviour
{
    public Material sphereMaterial;  // 셰이더에 연결된 머티리얼
    public float duration = 3.0f;    // 값이 서서히 변화하는 시간
    public Texture2D[] textures = new Texture2D[33]; // 퍼블릭으로 선언된 텍스처 리스트
    private int currentTextureIndex = 0; // 현재 텍스처 인덱스

    private bool isIncreasing = true; // 값이 0에서 1로 증가 중인지 여부
    private float startValue = 0f;   // 셰이더의 초기 Value 값
    private float endValue = 0.6f;     // 셰이더의 목표 Value 값
    private float currentTime = 0f;  // 시간이 경과하는 타이머

    private bool isRunning = false;  // 코루틴 실행 중인지 여부

    public int preIdx;

    void Start()
    {
        //첫 번째 텍스처를 기본 텍스처로 설정
        if (textures.Length > 0)
        {
            sphereMaterial.SetTexture("_Texture2D", textures[currentTextureIndex]);
        }
    }

    void Update()
    {
        int curIdx = BetaDocentMgr.Instance.idx;

        if (preIdx != curIdx)
        {
            preIdx = curIdx;
            
            print("sphere 인덱스가 바뀌었어요"+curIdx);
            // sphere의 texture2D를 sphere textuers[idx]의 texture2D로 변경 
            ChangeTexture(curIdx);
        }

        // space바를 눌렀을 때 값 변화 시작/반전
        if (Input.GetKeyDown(KeyCode.I) && !isRunning)
        {
            // 코루틴을 시작하고 방향을 설정
            if (isIncreasing)
            {
                StartCoroutine(ChangeShaderValue(0f, 0.6f)); // 0에서 1로 증가
            }
            else
            {
                StartCoroutine(ChangeShaderValue(0.6f, 0f)); // 1에서 0으로 감소
            }

            // 방향을 반전
            isIncreasing = !isIncreasing;
        }
    }

    // 텍스처를 변경하는 함수
    void ChangeTexture(int idx)
    {
        currentTextureIndex = idx;

        // 새로운 텍스처를 머티리얼에 적용
        sphereMaterial.SetTexture("_Texture2D", textures[currentTextureIndex]);
    }

    // 값이 변화하는 코루틴 (0에서 1 또는 1에서 0으로)
    IEnumerator ChangeShaderValue(float start, float end)
    {
        isRunning = true; // 코루틴이 실행 중임을 표시
        currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newValue = Mathf.Lerp(start, end, currentTime / duration);
            sphereMaterial.SetFloat("_Value", newValue);
            yield return null;
        }

        // 정확히 목표 값으로 설정
        sphereMaterial.SetFloat("_Value", end);

        isRunning = false; // 코루틴 종료 후 실행 중 상태 해제
    }
}
