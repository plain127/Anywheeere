using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DocentManager;

public class BetaDocentMgr : MonoBehaviour
{
    public GameObject CesiumMap;

    public string url;

    public AudioSource audioSource;
    public Text docentText;

    public GameObject bgmBox;
    public GameObject docentBox;

    public GameObject mapPanel;

    public int idx;
    string jsonData;

    public GameObject CountryPanel;
    public GameObject USAPanel;
    public GameObject ItalyPanel;
    public GameObject JapanPanel;

    public GameObject NYCPanel;
    public GameObject SFPanel;
    public GameObject LVPanel;

    public GameObject RomePanel;
    public GameObject VenicePanel;

    public GameObject TokyoPanel;
    public GameObject KyotoPanel;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapPanel.activeInHierarchy)
            {
                mapPanel.SetActive(false);
                OnDisable();
            }
            else if (!mapPanel.activeInHierarchy)
            {
                mapPanel.SetActive(true);
            }

        }


    }
    private void OnDisable()
    {
        CountryPanel.SetActive(true);
        USAPanel.SetActive(false);
        ItalyPanel.SetActive(false);
        JapanPanel.SetActive(false);
        NYCPanel.SetActive(false);
        SFPanel.SetActive(false);
        LVPanel.SetActive(false);
        RomePanel.SetActive(false);
        VenicePanel.SetActive(false);
        TokyoPanel.SetActive(false);
        KyotoPanel.SetActive(false);
    }

    public void CountryBtnOnClick(string country)
    {
        CountryPanel.SetActive(false);
        if      (country == "USA")   { idx = 0; USAPanel.SetActive(true); }
        else if (country == "Italy") { idx = 1; ItalyPanel.SetActive(true);  }
        else if (country == "Japan") { idx = 2; JapanPanel.SetActive(true);  }

        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
        // 국가 BGM 과 설정된 Audio 재생
    }

    public void SelectCity(string city)
    {
        if      (city == "NewYork")         { idx = 3; NYCPanel.SetActive(true); }
        else if (city == "San Francisco")   { idx = 4; SFPanel.SetActive(true); }
        else if (city == "Las Vegas")       { idx = 5; LVPanel.SetActive(true); }
        USAPanel.SetActive(false);

        if      (city == "Rome")   { idx = 6;  RomePanel.SetActive(true); }
        else if (city == "Venice") { idx = 7; VenicePanel.SetActive(true); }
        ItalyPanel.SetActive(false);

        if      (city == "Kyoto") { idx = 8; KyotoPanel.SetActive(true); }
        else if (city == "Tokyo") { idx = 9; TokyoPanel.SetActive(true); }
        JapanPanel.SetActive(false);

        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
        // 도시 BGM 과 설정된 Audio 재생
    }

    public void SelectMonument(string monument)
    {
        print(monument);
        if      (monument == "자유의 여신상") { idx = 10; }
        else if (monument == "타임 스퀘어") { idx = 11; }
        else if (monument == "센트럴 파크") { idx = 12; }
        else if (monument == "브루클린 브릿지") { idx = 13; }
        else if (monument == "골든 게이트 브릿지") { idx = 14; }
        else if (monument == "페리 빌딩") { idx = 15; }
        else if (monument == "샌프란시스코 해변") { idx = 16; }
        else if (monument == "더 스피어") { idx = 17; }
        else if (monument == "벨라지오 분수") { idx = 18; }
        else if (monument == "시저스 펠리스") { idx = 19; }
        else if (monument == "베네치안 호텔") { idx = 20; }
        else if (monument == "판테온 신전") { idx = 21; }
        else if (monument == "콜로세움") { idx = 22; }
        else if (monument == "성 베드로 대성당") { idx = 23; }
        else if (monument == "산 마르코 대성당") { idx = 24; }
        else if (monument == "리알토 다리") { idx = 25; }
        else if (monument == "베니스 대운하") { idx = 26; }
        else if (monument == "금각사") { idx = 27; }
        else if (monument == "야사카의 탑") { idx = 28; }
        else if (monument == "키타노 텐만구") { idx = 29; }
        else if (monument == "도쿄타워") { idx = 30; }
        else if (monument == "도쿄 스카이 트리") { idx = 31; }
        else if (monument == "디즈니 랜드") { idx = 32; }

        // 랜드마크 Audio 재생 ( BGM 따로 없음. 도시 BGM 계속 플레이)
        GetDocent(monument);
        GetAudioDocent();
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
    }

    public void GetDocent(string target)
    {
        jsonData = "{\"text\":\"" + target + "\"}";
        print(idx);
        StartCoroutine(GetDocentFromAI(url, jsonData));
    }

    IEnumerator GetDocentFromAI(string url, string jsonData)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + "docent"))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            DocentResponse responseData = JsonUtility.FromJson<DocentResponse>(www.downloadHandler.text);
            docentText.text = responseData.docent;
        }
    }

    public void GetAudioDocent()
    {
        StartCoroutine(GetAudioDocentFromAI());
    }

    IEnumerator GetAudioDocentFromAI()
    {
        string jsonData = "{\"path\":\"./output.wav\"}";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip( url + "audio", AudioType.WAV))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                DownloadHandlerAudioClip downloadHandler = www.downloadHandler as DownloadHandlerAudioClip;
                PlayAudioClip(downloadHandler.audioClip);
            }
        }
    }

    void PlayAudioClip(AudioClip clip)
    {

        //if (audioSource != null)
        //{
        //    AudioSource[] audioSources = GetComponents<AudioSource>();
        //    foreach (AudioSource audioSource in audioSources)
        //    {
        //        Destroy(audioSource);
        //    }
        //    print("잘 자요");
        //}

        // 지울 것들은 지우고 
        // 필요한 오디오 재생
    }



}
