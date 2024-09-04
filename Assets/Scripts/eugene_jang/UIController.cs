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
        //    if (Input.GetKeyUp(KeyCode.Alpha1))
        //    {
        //        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(0);
        //    }
        //    if (Input.GetKeyUp(KeyCode.Alpha2))
        //    {
        //        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(1);
        //    }
        //    if (Input.GetKeyUp(KeyCode.Alpha3))
        //    {
        //        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(2);
        //    }
        //    if (Input.GetKeyUp(KeyCode.Alpha4))
        //    {
        //        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(3);
        //    }
        //    if (Input.GetKeyUp(KeyCode.Alpha5))
        //    {
        //        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(4);
        //    }
        //    if (Input.GetKeyUp(KeyCode.Alpha6))
        //    {
        //        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(5);
        //    }

    }
}
