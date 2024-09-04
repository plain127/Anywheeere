using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            print("NYC");
            //CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(8);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            print("Rome");
            //CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(9);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            print("Paris");
            //CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(10);
        }

    }
}
