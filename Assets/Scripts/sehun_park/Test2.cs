
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public float rotSpeed = 200.0f;

    // 회전 값
    float rotX;
    float rotY;

    // 회전 가능 여부
    public bool useRotX;
    public bool useRotY;
    void Start()
    {

    }


    void Update()
    {

            // 마우스의 움직임을 받아오자.
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");

            // 회전 값을 변경 (누적)
            if (useRotX) rotX += my * rotSpeed * Time.deltaTime;
            if (useRotY) rotY += mx * rotSpeed * Time.deltaTime;

            // rotX의 값 제한
            rotX = Mathf.Clamp(rotX, -80, 80);

            // 구해진 회전
            transform.localEulerAngles = new Vector3(-rotX, rotY, 0);

        
    }
}