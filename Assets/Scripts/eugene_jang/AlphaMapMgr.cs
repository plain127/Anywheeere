using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaMapMgr : MonoBehaviour
{
    public GameObject MapPanel;

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
        print(country);
        // country 로 이동
        // country 의 docent audio 받아오기 

        CountryPanel.SetActive(false);
        if(country == "USA")
        {
            USAPanel.SetActive(true);
        }else if (country == "Italy")
        {
            ItalyPanel.SetActive(true);
        }else if (country == "Japan")
        {
            JapanPanel.SetActive(true);
        }
    }

    public void SelectCity(string city)
    {
        print(city);
        // city 로 이동
        // city 의 docent audio 받아오기 
        if(city == "NewYork") 
        { 
            NYCPanel.SetActive(true); 
        } else if(city == "San Francisco")
        {
            SFPanel.SetActive(true);
        } else if (city == "Las Vegas")
        {
            LVPanel.SetActive(true);
        }
        USAPanel.SetActive(false);

        if (city == "Rome")
        {
            RomePanel.SetActive(true);
        }else if (city == "Venice")
        {
            VenicePanel.SetActive(true);
        }
        ItalyPanel.SetActive(false);

        if (city == "Tokyo")
        {
            TokyoPanel.SetActive(true);
        }else if(city == "Kyoto")
        {
            KyotoPanel.SetActive(true);
        }
        JapanPanel.SetActive(false);
    }

    public void SelectMonument(string monument)
    {
        print(monument);
        // monument 로 이동
        // monument 의 docent 받아오기 + Audio 재생
    }
}
