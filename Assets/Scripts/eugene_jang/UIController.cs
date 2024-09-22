using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DocentManager;

// ui 종류와 역할
// 나라 선택 > 도시 선택 > 문화재 선택
// 1. 나라 선택시 Audio 재생
// 2. 도시 선택시 Audio 재생
// 3. 문화재 선택시 Audio 재생..
// ... 이거 리스트업 되어 있으면 .. 전부 미리 준비 가능.. 
// AI와의 실시간 소통은 어떻게 할 것인가? 
// 추가 방안이 잇나.. ?
// 일단은 준비 해보자... 


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

    // 일단 나라 > 도시 > 문화재 UI 만들기
    public GameObject mapUI;

    public GameObject countryPanel;
    public GameObject cityPanel;
    public GameObject monumentPanel;

    public Button[] countryButtons;
    public Button[] cityButtons;
    public Button[] monumentButtons;

    private string selectedCountry;
    private string selectedCity;

    private Dictionary<string, List<string>> countryToCities = new Dictionary<string, List<string>>()
    {
        { "USA", new List<string> { "New York", "San Francisco", "Las Vegas" } },
        { "Italy", new List<string> { "Rome", "Venice" } },
        { "Japan", new List<string> { "Tokyo", "Kyoto" } }
    };

    private Dictionary<string, List<string>> cityToMonuments = new Dictionary<string, List<string>>()
    {
        // USA
        { "New York", new List<string> { "Statue of Liberty", "Times Square", "Central Park", "Empire State Building", "Brooklyn Bridge" } },
        { "San Francisco", new List<string> { "Golden Gate Bridge", "Beach" } },
        { "Las Vegas", new List<string> { "The Sphere", "Las Vegas Strip", "Bellagio Fountains", "Caesars Palace", "Venetian Hotel" } },

        // Italy
        { "Rome", new List<string> { "Pantheon", "Colosseum", "St. Peter's Basilica" } },
        { "Venice", new List<string> { "St. Mark's Basilica", "Rialto Bridge", "Grand Canal" } },

        // Japan
        { "Tokyo", new List<string> { "Tokyo Tower", "Tokyo Skytree", "Asakusa" } },
        { "Kyoto", new List<string> { "Kinkaku-ji", "Fushimi Inari Shrine", "Yasaka Pagoda", "Arashiyama Bamboo Grove" } }
    };


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
            jsonData = "{\"text\":\"Italy\"}";

            GetDocent();
            
            //docentText.text = "테스트 USA 도슨트 블라라";
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

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapUI.activeInHierarchy)
            {
                mapUI.SetActive(false);
            }
            else if (!mapUI.activeInHierarchy)
            {
                mapUI.SetActive(true);
                //ShowCountryPanel();
            }

        }

    }
    
    void SetupCountryButtons()
    {
        string[] countries = new string[] { "USA", "Italy", "Japan" };

        for (int i = 0; i < countryButtons.Length; i++)
        {
            if(i < countries.Length)
            {
                string country = countries[i];
                countryButtons[i].gameObject.SetActive(true);
                countryButtons[i].transform.GetChild(0).GetComponent<Text>().text = country;
                countryButtons[i].onClick.RemoveAllListeners();
                countryButtons[i].onClick.AddListener(() => ShowCityPanel(country));
            }
            else 
            {
                countryButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void ShowCountryPanel()
    {
        countryPanel.SetActive(true);
        cityPanel.SetActive(false);
        monumentPanel.SetActive(false);
    }

    public void ShowCityPanel(string country)
    {
        selectedCity = country;
        countryPanel.SetActive(false);
        cityPanel.SetActive(true);
        monumentPanel.SetActive(false);
        UpdateCityButtons();
    }

    public void ShowMonumentPanel(string city)
    {
        selectedCity = city;
        countryPanel.SetActive(false);
        cityPanel.SetActive(false);
        monumentPanel.SetActive(true);
        UpdateMonumentButtons();
    }

    void UpdateCityButtons()
    {
        List<string> cities = countryToCities[selectedCountry];

        for (int i = 0; i < cityButtons.Length; i++)
        {
            if(i < cities.Count)
            {
                string city = cities[i];
                cityButtons[i].gameObject.SetActive(true);
                print(i);
                cityButtons[i].transform.GetChild(0).GetComponent<Text>().text = city;

                cityButtons[i].onClick.RemoveAllListeners();
                cityButtons[i].onClick.AddListener(() => ShowMonumentPanel(city));
            }
            else
            {
                cityButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void UpdateMonumentButtons()
    {
        List<string> monuments = cityToMonuments[selectedCity];

        for (int i = 0; i < monumentButtons.Length; i ++)
        {
            if (i < monuments.Count)
            {
                string monument = monuments[i];
                monumentButtons[i].gameObject.SetActive(true);
                monumentButtons[i].GetComponentInChildren<Text>().text = monuments[i];

                monumentButtons[i].onClick.RemoveAllListeners();
                monumentButtons[i].onClick.AddListener(() => OnMonumentSelected(monument));
            }
            else
            {
                monumentButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnCountrySelected(string country)
    {
        selectedCountry = country;
        Debug.Log($"Selected Country : {country}");
        ShowCityPanel(country);
    }

    public void OnMonumentSelected(string monument)
    {
        Debug.Log($"Selected Monument: {monument}");
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
        using (UnityWebRequest www = UnityWebRequest.Get(url+"docent"))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            DocentResponse responseData = JsonUtility.FromJson<DocentResponse>(www.downloadHandler.text);

            print(responseData.audio);
            docentText.text = responseData.docent;
            //받아온 텍스트(json형태 임) 도슨트 부분은 string 변수에 저장 해 둠

            // 받아온 json에서 docent 부분과 audio 주소 부분을 분리해 둬야 함
            // 오디오 도슨트 받는 함수 실행
            // docentAudio에 받아온 AudioClip 저장 
        }
    }

    // docent audio 를 받아서 AudioClip에 저장 하는 함수
    IEnumerator GetDecentAudioFromAI(string url)
    {
        jsonData = "{\"path\":\"./output.wav\"}";
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
