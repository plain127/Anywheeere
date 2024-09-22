using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GotoCityAdv : MonoBehaviour
{
    void Update()
    {
        // B 키가 눌렸는지 확인
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Photon 네트워크를 통해 씬을 로드
            PhotonNetwork.LoadLevel("03_CesiumSanFrancisco");
        }
    }
}