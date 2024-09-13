using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Game2Manager : MonoBehaviour
{

    public Vector3 var;

    void Start()
    {
        PhotonNetwork.Instantiate("Player3", var, Quaternion.identity);
    }

    void Update()
    {

    }
}
