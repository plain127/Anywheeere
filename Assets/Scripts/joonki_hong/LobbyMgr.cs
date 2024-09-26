using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMgr : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    // Input Room Name
    public TMP_InputField inputRoomName;
    // Input Max Player
    public TMP_InputField inputMaxPlayer;
    // Create Button
    public Button btnCreate;
    // Join Button
    public Button btnJoin;

    Dictionary<string, RoomInfo> allRoomInfo = new Dictionary<string, RoomInfo>();
    void Start()
    {
        // 로비 진입
        PhotonNetwork.JoinLobby();

        // inputRoomName의 내용이 변경될 때 호출되는 함수 등록
        inputRoomName.onValueChanged.AddListener(OnValueChangedRoomName);
        // inputMaxPlayer의 내용이 변경될 때 호출되는 함수 등록
        inputMaxPlayer.onValueChanged.AddListener(OnValueChangedMaxPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Join & Create 버튼을 활성화 / 비활성화
    void OnValueChangedRoomName(string roomName)
    {
        // join 버튼 활성화 / 비활성하
        btnJoin.interactable = roomName.Length > 0;

        // create 버튼 활성화 / 비활성화
        btnCreate.interactable = roomName.Length > 0 && inputMaxPlayer.text.Length > 0;

    }
    // Create 버튼을 활성화 / 비활성화
    void OnValueChangedMaxPlayer(string maxPlayer)
    {
        btnCreate.interactable = maxPlayer.Length > 0 && inputRoomName.text.Length > 0;
    }

    public void CreateRoom()
    {
        // 방 옵션 설정
        RoomOptions option = new RoomOptions();
        // 최대 인원 설정
        option.MaxPlayers = int.Parse(inputMaxPlayer.text);
        // 방 옵션을 기반으로 방을 생성
        PhotonNetwork.CreateRoom(inputRoomName.text, option);

    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("방 생성 완료");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("방 생성 실패 : " + message);
    }

    public void JoinRoom()
    {
        // 방 입장 요청
        PhotonNetwork.JoinRoom(inputRoomName.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("방 입장 완료");
        PhotonNetwork.LoadLevel("CesiumGoogleMapsTiles_Beta_JK");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("방 입장 실패: " + message);
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("로비 진입 성공!");
    }

    // 로비에 있을 때 방에 대한 정보들이 변경되면 호출되는 함수
    // roomList : 전체 방 목록X, 변경된 방 정보
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        // 방 목록 UI를 전체 삭제
        RemoveRoomList();

        // 전체 방 정보를 갱신
        UpdateRoomList(roomList);

        // allRoomInfo를 기반으로 방목록 UI를 만들자
        CreateRoomList();

        //for(int i = 0; i < roomList.Count; i++)
        //{
        //    print(roomList[i].Name + "," + roomList[i].PlayerCount + ", " + roomList[i].RemovedFromList);
        //}
    }

    void UpdateRoomList(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            // allRoomInfo에 roomList의 i번째 정보가 있니?
            // (=
            if (allRoomInfo.ContainsKey(roomList[i].Name))
            {
                // allRoomInfo 수정 or 삭제
                // 삭제된 방이니?
                if (roomList[i].RemovedFromList == true)
                {
                    allRoomInfo.Remove(roomList[i].Name);
                }
                else
                {
                    allRoomInfo[roomList[i].Name] = roomList[i];
                }

            }
            else
            {
                // allRooomInfo 추가
                allRoomInfo[roomList[i].Name] = roomList[i];
            }
        }
    }

    // RoomItem의 Prefab
    public GameObject roomItemFactory;

    // ScrollView의 Content transform
    public RectTransform trContent;
    void CreateRoomList()
    {
        foreach(RoomInfo info in allRoomInfo.Values)
        {
            // roomItem Prefab을 이용해서 roomItem을 만든다
            GameObject go = Instantiate(roomItemFactory, trContent);
            Debug.Log("GameObject go = Instantiate(roomItemFactory, trContent);가 실행됨");
            // 만들어진 roomItem의 내용을 변경
            // Text 컴포넌트 가져오자
            Text roomItem = go.GetComponentInChildren<Text>();
            // 가져온 컴포넌트에 정보를 입력
            roomItem.text = info.Name + " ( " + info.PlayerCount + " / " + info.MaxPlayers + " )";
        }
    }

    void RemoveRoomList()
    {
        for(int i = 0; i < trContent.childCount; i++)
        {
            Destroy(trContent.GetChild(i).gameObject);
        }
    }
}
