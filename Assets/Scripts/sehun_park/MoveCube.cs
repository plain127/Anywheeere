using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오른쪽으로 정확하게 5 만큼 움직이고 싶다.

public class MoveCube : MonoBehaviour
{
    // 이동거리
    float moveDist = 0;
    // 움직일 수 있니?
    bool isMove = true;
    void Start()
    {
        
    }

    void Update()
    {
        if(isMove == true)
        {
            // 1. 오른쪽으로 움직이자.
            transform.position += Vector3.right * 5 * Time.deltaTime;
            // 이동거리 누적
            moveDist += 5 * Time.deltaTime;
            // 2. 만약에 움직인 거리가 5 보다 커지면
            if (moveDist >= 5)
            {
                // 3. 멈추자.
                isMove = false;
                // 4. 오버 된 이동거리 만큼 왼쪽으로 이동시키자.
                float overDist = moveDist - 5;
                transform.position += Vector3.left * overDist;
            }
        }        
    }
}
