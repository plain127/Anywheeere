using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 point;
    void Start()
    {
        PhotonNetwork.Instantiate("Player1", point, Quaternion.identity);
    }
    void Update()
    {

    }
}