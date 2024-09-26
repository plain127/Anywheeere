using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVideoMgr : MonoBehaviourPunCallbacks
{
    // 1st, 2nd, 3rd 플레이어의 RawImage (각 플레이어의 비디오 표시)
    public RawImage firstPlayerVideo;
    public RawImage secondPlayerVideo;
    public RawImage thirdPlayerVideo;

    // 플레이어들의 PhotonVoiceView 컴포넌트
    public PhotonVoiceView[] playerVoiceViews = new PhotonVoiceView[3];

    void Start()
    {
        // 처음에는 모든 비디오 비활성화
        firstPlayerVideo.enabled = false;
        secondPlayerVideo.enabled = false;
        thirdPlayerVideo.enabled = false;

        // 자신이 로컬 플레이어라면 PhotonVoiceView 할당
        AssignVoiceViewForPlayer(PhotonNetwork.LocalPlayer);
    }

    void Update()
    {
        // 각 플레이어가 말하고 있는지 확인하여 비디오 활성화/비활성화
        if (playerVoiceViews[0] != null && playerVoiceViews[0].IsSpeaking)
        {
            firstPlayerVideo.enabled = true;
        }
        else
        {
            firstPlayerVideo.enabled = false;
        }

        Debug.LogError(playerVoiceViews[1].IsSpeaking);
        Debug.LogError(playerVoiceViews[1].IsRecording);

        if (playerVoiceViews[1] != null && playerVoiceViews[1].IsSpeaking)
        {
            secondPlayerVideo.enabled = true;
            //Debug.Log(playerVoiceViews[1]);
            //Debug.Log(playerVoiceViews[1].IsSpeaking);

        }
        else
        {
            secondPlayerVideo.enabled = false;
            //Debug.Log(playerVoiceViews[1]);
            //Debug.Log(playerVoiceViews[1].IsSpeaking);
        }

        if (playerVoiceViews[2] != null && playerVoiceViews[2].IsSpeaking)
        {
            thirdPlayerVideo.enabled = true;

        }
        else
        {
            thirdPlayerVideo.enabled = false;
        }
    }

    // 플레이어가 방에 들어올 때 호출되는 함수
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom 실행됨." + newPlayer);

        base.OnPlayerEnteredRoom(newPlayer);

        // 새 플레이어가 입장할 때 해당 플레이어 오브젝트를 찾고 PhotonVoiceView 할당
        AssignVoiceViewForPlayer(newPlayer);
    }

    // 새로 들어온 플레이어의 PhotonVoiceView를 찾고 할당하는 함수
    void AssignVoiceViewForPlayer(Player player)
    {
        Debug.Log("-1번구간");

        // 네트워크에서 해당 플레이어의 PhotonView를 찾음
        foreach (PhotonView view in FindObjectsOfType<PhotonView>())
        {
            Debug.Log("0번구간");

            PhotonVoiceView voiceView = view.GetComponent<PhotonVoiceView>();
            Debug.Log("1번구간");
            if (voiceView != null)
            {
                Debug.Log("2번구간");
                // 빈 자리에 플레이어의 VoiceView를 할당
                for (int i = 0; i < playerVoiceViews.Length; i++)
                {
                    if (playerVoiceViews[i] == null)
                    {
                        Debug.Log("3번구간");
                        playerVoiceViews[i] = voiceView;
                        break;
                    }
                }
            }
            if (view.Owner == player)
            {
            }
        }
    }
}
