using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SetChild : MonoBehaviour
{
    // 현재 오브젝트를 붙일 카메라의 이름
    private const string cameraName = "DynamicCamera";

    // 플레이어의 위치를 마스터 플레이어 주위에 배치할 반경
    public float radius = 5.0f;

    void Start()
    {
        // 방에 참가한 후 플레이어 위치 설정
        SetPlayerPositions();
    }

    private void SetPlayerPositions()
    {
        // 태그가 "MainCamera"인 오브젝트를 찾습니다.
        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");

        if (cameraObject != null)
        {
            // 카메라 오브젝트를 찾았으면, 현재 오브젝트를 카메라의 자식으로 설정합니다.
            if (cameraObject.name == cameraName)
            {
                // 방의 모든 플레이어를 순회하면서 위치를 설정합니다.
                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    // 마스터 클라이언트 식별
                    if (player.ActorNumber == PhotonNetwork.MasterClient.ActorNumber)
                    {
                        // 마스터 플레이어는 (0, 0, 6) 위치에 배치합니다.
                        if (PhotonNetwork.LocalPlayer.ActorNumber == player.ActorNumber)
                        {
                            transform.SetParent(cameraObject.transform);
                            transform.localPosition = new Vector3(0, 0, 6);
                            transform.localRotation = Quaternion.identity;
                            print("마스터 플레이어 위치 설정 완료");
                        }
                    }
                    else
                    {
                        // 마스터 클라이언트가 아닌 플레이어의 위치를 설정합니다.
                        if (PhotonNetwork.LocalPlayer.ActorNumber == player.ActorNumber)
                        {
                            transform.SetParent(cameraObject.transform);

                            // 마스터 플레이어의 위치를 기준으로 원형 배치
                            Vector3 masterPosition = new Vector3(0, 0, 6);
                            float angle = Random.Range(0f, 360f);
                            float x = masterPosition.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
                            float z = masterPosition.z + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
                            Vector3 playerPosition = new Vector3(x, 0, z);

                            transform.localPosition = playerPosition;
                            transform.localRotation = Quaternion.identity;
                            print("마스터가 아닌 플레이어 위치 설정 완료");
                        }
                    }
                }
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
    }
}