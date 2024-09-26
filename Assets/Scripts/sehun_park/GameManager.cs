using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Vector3 point;

    // 플레이어 닉네임을 표시할 3개의 TextMeshPro UI 요소
    public TMP_Text[] playerNickNameTexts = new TMP_Text[3];

    // 플레이어들의 PhotonVoiceView 컴포넌트
    public PhotonVoiceView[] playerVoiceViews = new PhotonVoiceView[3];

    void Start()
    {
        PhotonNetwork.Instantiate("Player1", point, Quaternion.identity);
        UpdatePlayerList(); // 게임 시작 시 플레이어 리스트 업데이트
    }

    // 새로운 플레이어가 방에 들어올 때 호출되는 함수
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        UpdatePlayerList(); // 새로운 플레이어가 들어오면 리스트 업데이트
    }

    // 플레이어가 방을 나갔을 때 호출되는 함수
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        UpdatePlayerList(); // 플레이어가 나가면 리스트 업데이트
    }

    // 플레이어 리스트 UI를 업데이트하는 함수
    void UpdatePlayerList()
    {
        Player[] players = PhotonNetwork.PlayerList;

        // TextMeshPro UI 요소를 순환하며 닉네임 할당
        for (int i = 0; i < playerNickNameTexts.Length; i++)
        {
            if (i < players.Length)
            {
                // 각 플레이어의 닉네임을 해당 Text에 할당
                playerNickNameTexts[i].text = players[i].NickName;

                // 플레이어의 PhotonVoiceView 설정
                playerVoiceViews[i] = players[i].TagObject as PhotonVoiceView;
            }
            else
            {
                // 빈 슬롯은 "Waiting..."으로 표시
                playerNickNameTexts[i].text = "Waiting...";
            }
        }
    }
}
