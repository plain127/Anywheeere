using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DocentManager;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;

public class AlphaMapMgr : MonoBehaviour
{
    public GameObject CesiumMap;
    public SkyboxManager skyboxManager;

    public string url;


    public AudioSource audioSource;
    public Text docentText;

    public GameObject mapPanel;

    int idx;
    string jsonData;

    public Button btn_USA;
    public Button btn_Italy;
    public Button btn_Japan;

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
        if (Input.GetKeyDown(KeyCode.T))
        {
            print("눌렸어요!");
            TestAudioFromAI();
        }

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
        if(audioSource != null)
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                Destroy(audioSource);
            }
            print("잘 자요");
        }
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
        print(country);
        // country 로 이동
        // country 의 docent audio 받아오기 ?? !! 응 됨
        
        CountryPanel.SetActive(false);
        if(country == "USA")
        {
            USAPanel.SetActive(true);
            idx = 0;
        }
        else if (country == "Italy")
        {
            ItalyPanel.SetActive(true);
            idx = 1;
        }
        else if (country == "Japan")
        {
            JapanPanel.SetActive(true);
            idx = 2;
        }

        //GetDocent(country);
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
    }

    public void SelectCity(string city)
    {
        print(city);
        // city 로 이동
        // city 의 docent audio 받아오기 ?? !! 되겠지 ? ㅇㅇ 

        if(city == "NewYork") 
        { 
            idx = 3;
            NYCPanel.SetActive(true); 
        } else if(city == "San Francisco")
        {
            idx = 4;
            SFPanel.SetActive(true);
        } else if (city == "Las Vegas")
        {
            idx = 5;
            LVPanel.SetActive(true);
        }
        USAPanel.SetActive(false);

        if (city == "Rome")
        {
            idx = 6;
            RomePanel.SetActive(true);
        }else if (city == "Venice")
        {
            idx = 7;
            VenicePanel.SetActive(true);
        }
        ItalyPanel.SetActive(false);

        if (city == "Tokyo")
        {
            idx = 8;
            TokyoPanel.SetActive(true);
        }else if(city == "Kyoto")
        {
            idx = 9;
            KyotoPanel.SetActive(true);
        }
        JapanPanel.SetActive(false);

        //GetDocent(city);
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
    }


    public void SelectMonument(string monument)
    {
        print(monument);
        if (monument == "자유의 여신상") { idx = 10; }
        else if (monument == "타임 스퀘어") { idx = 11;}
        else if (monument == "센트럴 파크") { idx = 12; }
        else if (monument == "엠파이어 스테이트 빌딩") { idx = 13; }
        else if (monument == "브루클린 브릿지") { idx = 14; }
        else if (monument == "골든 게이트 브릿지") { idx = 15; }
        else if (monument == "샌프란시스코 해변") { idx = 16; }
        else if (monument == "더 스피어") { idx = 17; }
        else if (monument == "라스베가스 스트립") { idx = 18; }
        else if (monument == "벨라지오 분수") { idx = 19; }
        else if (monument == "시저스 펠리스") { idx = 20; }
        else if (monument == "베네치안 호텔") { idx = 21; }
        else if (monument == "판테온 신전") { idx = 22; }
        else if (monument == "콜로세움") { idx = 23; }
        else if (monument == "성 베드로 대성당") { idx = 24; }
        else if (monument == "산 마르코 대성당") { idx = 25; }
        else if (monument == "리알토 다리") { idx = 26; }
        else if (monument == "베니스 대운하") { idx = 27; }
        else if (monument == "도쿄타워") { idx = 28; }
        else if (monument == "도쿄 스카이 트리") { idx = 29; }
        else if (monument == "아사쿠사") { idx = 30; }
        else if (monument == "금각사") { idx = 31; }
        else if (monument == "후시미 이나리 신사") { idx = 32; }
        else if (monument == "야사카의 탑") { idx = 33; }
        else if (monument == "아라시야마 대나무 숲") { idx = 34; }
        GetDocent(monument);
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
    }


    public void GetDocent(string target)
    {
        jsonData = "{\"text\":\""+target+"\"}";
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

            print(responseData.docent);
            print(responseData.audio);
            docentText.text = responseData.docent;
            //받아온 텍스트(json형태 임) 도슨트 부분은 string 변수에 저장 해 둠

            // 받아온 json에서 docent 부분과 audio 주소 부분을 분리해 둬야 함
            // 오디오 도슨트 받는 함수 실행
            // docentAudio에 받아온 AudioClip 저장 
            
        }
    }


        //오디오 테스트 함수
        public void TestAudioFromAI()
    {
        StartCoroutine(TestAudioRequest());
    }

    IEnumerator TestAudioRequest()
    {
        string jsonData = "{\"path\":\"./output.wav\"}";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("http://metaai.iptime.org:9000/audio", AudioType.WAV))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if ( www.result == UnityWebRequest.Result.Success)
            {
                DownloadHandlerAudioClip downloadHandler = www.downloadHandler as DownloadHandlerAudioClip;
                PlayAudioClip(downloadHandler.audioClip);
            }
        }
    }
    void PlayAudioClip(AudioClip clip)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
