using CesiumForUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CesiumTest : MonoBehaviour
{
    //public CesiumGeoreference cesiumGeoreference;

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))  // 마우스 클릭을 통해 위치 확인
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        if (Physics.Raycast(ray, out RaycastHit hit))
    //        {
    //            // Raycast로 얻은 세계 좌표
    //            Vector3 worldPosition = hit.point;

    //            // 세계 좌표를 위도/경도로 변환
    //            Cartographic cartographic = cesiumGeoreference.TransformUnityWorldToCartographic(worldPosition);

    //            double latitude = cartographic.latitude * Mathf.Rad2Deg;
    //            double longitude = cartographic.longitude * Mathf.Rad2Deg;

    //            Debug.Log("Latitude: " + latitude + ", Longitude: " + longitude);
    //        }
    //    }
    //}
}