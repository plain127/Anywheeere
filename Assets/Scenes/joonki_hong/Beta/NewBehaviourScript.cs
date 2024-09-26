using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("CesiumGoogleMapsTiles_Beta_JK");
            }
        }
    }
}
