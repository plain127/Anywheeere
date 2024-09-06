using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


[Serializable]
enum PlayStates
{
    map,
    region,
    landmark
}

public class UIController : MonoBehaviour
{
    public GameObject CesiumMap;
    public SkyboxManager skyboxManager;

    public string url;
    // 선택된 도시로 이동한다.
    // 동시에 도슨트 데이터를 AI 에게서 받아온다.
    // 받아온 데이터를 저장해둔다. text / audio

    public string docent;

    public string audioAddress;
    public AudioClip docentAudio;

    string jsonData;

    public GameObject docentUI;
    public Text docentText;
    // UI가 활성화 되어있을때 조작 
    void Start()
    {
        // 선택지 UI를 활성화
        // map mode 변경
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("NYC");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(1);
            // 뉴욕으로 이동 할 꺼임 
            jsonData = "{\"text\":\"자유의여신상\"}";
            GetDocent();
            
            docentText.text = "자유의 여신상 도슨트 블라라";
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("Statue of Liberty");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(2);
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("Rome");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(4);
            jsonData = "{\"text\":\"콜로세움\"}";
            GetDocent();

            docentText.text = "콜로세움 도슨트 블라라";
        }

        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("Colosseum");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(5);
        }

        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }


            print("Paris");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(7);
            jsonData = "{\"text\":\"에펠탑\"}";
            GetDocent();

            docentText.text = "에펠탑 도슨트 블라라";
        }

        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("Eiffel Tower");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(8);
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            if (docentUI.activeInHierarchy)
            {
                docentUI.SetActive(false);
            }
            else if (!docentUI.activeInHierarchy)
            {
                docentUI.SetActive(true);
            }
        }

    }
    // 프로토용 함수들 임

    #region 프로토용 통신 함수들
    public void GetDocent()
    {
        StartCoroutine(GetDocentFromAI(url, jsonData));
    }

    // 이동하는 도시 정보를 전달하고 docent 텍스트와 audio 주소를 받아 옴
    IEnumerator GetDocentFromAI(string url, string jsonData)
    {   
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            docentText.text = www.downloadHandler.text;
            //받아온 텍스트(json형태 임) 도슨트 부분은 string 변수에 저장 해 둠

            // 받아온 json에서 docent 부분과 audio 주소 부분을 분리해 둬야 함
            // 오디오 도슨트 받는 함수 실행
            // docentAudio에 받아온 AudioClip 저장 
        }
    }

    // docent audio 를 받아서 AudioClip에 저장 하는 함수
    IEnumerator GetDecentAudioFromAI(string url)
    {
        jsonData = "{\"path\":\"./result.wav\"}";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            DownloadHandlerAudioClip downloadHandler = www.downloadHandler as DownloadHandlerAudioClip;
            docentAudio = downloadHandler.audioClip;

            // docentAudio를 실행 해야 할 때 실행 한다.(어디에 넣어 놓을까~?)
        }
    }





    // 몰입환경에서 받아온 docent를 UI에 노출 시킨다.( Audio도 같이 실행 // 오디오 끄는 버튼도 ?)
    void ViewDocent()
    {

    }


    #endregion


}
