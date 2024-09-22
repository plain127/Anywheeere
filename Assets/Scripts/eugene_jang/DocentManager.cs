using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class DocentManager : MonoBehaviour
{
    public string url;
    // 선택된 도시로 이동한다.
    // 동시에 도슨트 데이터를 AI 에게서 받아온다.
    // 받아온 데이터를 저장해둔다. text / audio

    public string docent;

    public string audioAddress;
    public AudioClip docentAudio;

    // 도슨트를 실행하면 저장되어 있던 text는 UI 형태로 표시한다.
    // audio는 AudioClip 으로 실행 한다.

    // 응답 받은 json 데이터를 이용 하기 위한 class 
    [Serializable]
    public class DocentResponse
    {
        public string docent;
        public string audio;
    }


    void Start()
    {
            
    }

    void Update()
    {
        
    }

    public void GetDocent()
    {
        StartCoroutine(GetDocentFromAI(url));
    }

    // 이동하는 도시 정보를 전달하고 docent 텍스트와 audio 주소를 받아 옴
    IEnumerator GetDocentFromAI(string url)
    {
        string jsonData = "{\"text\":\"자유의여신상\"}";
        using (UnityWebRequest www = UnityWebRequest.Get(url + "docent"))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            // 받아온 text를 json 에서 DocentResponse class 로 변경
            DocentResponse responseData = JsonUtility.FromJson<DocentResponse>(www.downloadHandler.text);

            // 받아온 json에서 docent 부분과 audio 주소 부분을 분리해 둬야 함
            print(responseData.docent);
            print(responseData.audio);
            docent = responseData.docent;
            audioAddress = responseData.audio;

            // 알파 때.. 받으면 할 것 덜

            // 오디오 도슨트 받는 함수 실행
            // docentAudio에 받아온 AudioClip 저장

            // 미리 받아놓은 오디오의 재생
            // 전체 플로우 관리는 어디서 하는가.. ?
            StartCoroutine(GetDecentAudioFromAI(url + "audio"));
        }
    }

    // docent audio 를 받아서 AudioClip에 저장 하는 함수
    // 알파때 조질 예정
    IEnumerator GetDecentAudioFromAI(string url)
    {
        string jsonData = "{\"path\":\"./output.wav\"}";
        using(UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
        {
            print("audio 함수 실행");
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            DownloadHandlerAudioClip downloadHandler = www.downloadHandler as DownloadHandlerAudioClip;
            docentAudio = downloadHandler.audioClip;
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = docentAudio;
            audioSource.Play();
            // docentAudio를 실행 해야 할 때 실행 한다.(어디에 넣어 놓을까~?)
        }


    }





    // 몰입환경에서 받아온 docent를 UI에 노출 시킨다.( Audio도 같이 실행 // 오디오 끄는 버튼도 ?)
    void ViewDocent()
    {

    }

    

}
