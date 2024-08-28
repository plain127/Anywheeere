using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviourPun
{
    public GameObject cubeFactory;
    void Start()
    {

    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // 1번 키 누르면
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("키 눌림");
                // 카메라의 앞 방향으로 5만큼 떨어진 위치를 구하자.
                Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 10;
                // 큐브 공장에서 큐브를 생성, 위치, 회전
                PhotonNetwork.Instantiate("Cube", pos, Quaternion.identity);
            }
        }
    }
}
