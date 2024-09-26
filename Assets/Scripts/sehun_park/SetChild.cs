using Photon.Pun;
using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChild : MonoBehaviourPun
{
    // 현재 오브젝트를 붙일 카메라의 이름
    private const string cameraName = "DynamicCamera";

    public int enterOrder = 0;
    PhotonVoiceView pvv;
    PlayerVideoMgr playerVideoMgr;

    void Start()
    {
        // 태그가 "MainCamera"인 오브젝트를 찾습니다.
        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");

        if (cameraObject != null)
        {
            // 카메라 오브젝트를 찾았으면, 현재 오브젝트를 카메라의 자식으로 설정합니다.
            if (cameraObject.name == cameraName)
            {
                transform.SetParent(cameraObject.transform);

                // 자식 오브젝트의 위치를 카메라 오브젝트의 위치에 상대적으로 맞추도록 설정합니다.
                transform.localPosition = new Vector3(0, 0, 6);
                transform.localRotation = Quaternion.identity;
            }
            else
            {
                Debug.LogWarning($"태그가 'MainCamera'인 오브젝트가 있지만, 이름이 '{cameraName}'이 아닙니다.");
            }
        }
        else
        {
            Debug.LogWarning("태그가 'MainCamera'인 오브젝트를 찾을 수 없습니다.");
        }

        playerVideoMgr = GameObject.Find("GameManager").GetComponent<PlayerVideoMgr>();
        pvv = GetComponent<PhotonVoiceView>();
        enterOrder = PhotonNetwork.CurrentRoom.PlayerCount - 1;
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            playerVideoMgr.UpdatePlayerVideo(enterOrder, pvv.IsRecording);

        }
        else
        {
            playerVideoMgr.UpdatePlayerVideo(enterOrder, pvv.IsSpeaking);
        }
    }
}