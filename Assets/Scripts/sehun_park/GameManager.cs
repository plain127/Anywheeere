using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{

    public Vector3 var;

    void Start()
    {
        PhotonNetwork.Instantiate("Player", var, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
