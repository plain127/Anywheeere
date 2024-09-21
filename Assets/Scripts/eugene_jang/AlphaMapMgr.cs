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
    public GameObject LVCPanel;



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
        LVCPanel.SetActive(false);
    }
    public void CountryBtnOnClick(string str)
    {
        print(str);
        if(str == "USA")
        {
            CountryPanel.SetActive(false);
            USAPanel.SetActive(true);
        }else if (str == "Italy")
        {
            CountryPanel.SetActive(false);
            ItalyPanel.SetActive(true);
        }else if (str == "Japan")
        {
            CountryPanel.SetActive(false);
            JapanPanel.SetActive(true);
        }
    }

    public void SelectCity(string city)
    {
        print(city);
        if(city == "NewYork") 
        { 
            NYCPanel.SetActive(true); 
            USAPanel.SetActive(false);
        }
    }

    public void SelectMonument(string monument)
    {
        print(monument);
    }
}
