using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SimpleConnectionMgr : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photon 환경설정을 기반으로 마스터 서버에 접속을 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        
    }

    // 마스터 서버에 접속이 되면 호출되는 함수
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("마스터 서버에 접속");

        // 로비 접속
        JoinLobby();
    }

    public void JoinLobby()
    {
        // 닉네임 설정
        PhotonNetwork.NickName = "김현진" + Random.Range(1, 1000);
        // 기본 Lobby 입장
        PhotonNetwork.JoinLobby();
    }

    // 로비에 참여가 성공하면 호출되는 함수
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("로비 입장 완료");

        JoinOrCreateRoom();
    }

    // Room 을 참여하자. 만약에 해당 Room 이 없으면 Room 만들겠다.
    public void JoinOrCreateRoom()
    {
        // 방 생성 옵션
        RoomOptions roomOption = new RoomOptions();
        // 방에 들어 올 수 있는 최대 인원 설정
        roomOption.MaxPlayers = 3;
        // 로비에 방을 보이게 할것이니?
        roomOption.IsVisible = true;
        // 방에 참여를 할 수 있니?
        roomOption.IsOpen = true;

        // Room 참여 or 생성
        PhotonNetwork.JoinOrCreateRoom("meta_unity_room012340", roomOption, TypedLobby.Default);
    }

    // 방 생성 성공 했을 때 호출되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("방 생성 완료");
    }

    // 방 입장 성공 했을 때 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("방 입장 완료");

        // 멀티플레이 컨텐츠 즐길 수 있는 상태
        // GameScene으로 이동!
        PhotonNetwork.LoadLevel("GameScene");
    }
}
