using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Manager : MonoBehaviourPunCallbacks
{
    public static Game2Manager instance;

    // Spawn 위치를 담아놓을 변수
    public Vector3[] spawnPos;
    public Transform spawnCenter;

    // 모든 Player 의 PhotonView 가지고 있는 변수
    public PhotonView[] allPlayer;

    // GameScene 으로 넘어온 Player 의 갯수
    int enterPlayerCnt;

    // 현재 총을 쏠 수 있는 player Idx
    int turnIdx = -1;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        SetSpawnPos();

        // RPC 보내는 빈도 설정
        PhotonNetwork.SendRate = 60;
        // OnPhotonSerializeView 보내고 받고 하는 빈도 설정
        PhotonNetwork.SerializationRate = 60;

        // 내가 위치해야 하는 idx 를 알아오자. (현재 방의 들어와 있는 인원 수 )
        int idx = ProjectMgr.Get().orderInRoom;

        // 플레이어를 생성 (현재 Room 에 접속 되어있는 친구들도 보이게)
        PhotonNetwork.Instantiate("Player3", spawnPos[idx], Quaternion.identity);

        // 모든 플레이어 담을 변수 공간 할당
        allPlayer = new PhotonView[PhotonNetwork.CurrentRoom.MaxPlayers];

        // 더 이상 이방에 접속을 하지 못하게 하자.

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // 방을 떠나자
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        // 방장에 의해서 Scene 전환되는 옵션 비활성
        PhotonNetwork.AutomaticallySyncScene = false;
        // 마우스 커서 Lock 모드 풀자
        Cursor.lockState = CursorLockMode.None;

        // 자동으로 Master Server 에 접속 시도
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        // LobbyScene 으로 전환
        PhotonNetwork.LoadLevel("LobbyScene");
    }


    void SetSpawnPos()
    {
        // maxPlayer 를 현재 방의 최대 인원으로 설정
        int maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers;

        // 최대 인원 만큼 spawnPos 의 공간을 할당
        spawnPos = new Vector3[maxPlayer];

        // spawnPos 간의 간격(각도)
        float angle = 360.0f / maxPlayer;

        // maxPlayer 만큼 반복
        for (int i = 0; i < maxPlayer; i++)
        {
            // spawnCenter 회전 (i * angle) 만큼
            spawnCenter.eulerAngles = new Vector3(0, i * angle, 0);
            // spawnCenter 앞방향으로 2만큼 떨어진 위치 구하자.
            spawnPos[i] = spawnCenter.position + spawnCenter.forward * 2;
            //// 큐브 하나 생성
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //// 생성된 큐브를 위에서 구한 위치에 놓자.
            //cube.transform.position = spawnPos[i];
        }
    }

    public void AddPlayer(PhotonView pv, int order)
    {
        enterPlayerCnt++;

        allPlayer[order] = pv;

        // 만약에 모든 Player 가 다 들어왔다면
        if (enterPlayerCnt == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            // 방장일때만
            if (PhotonNetwork.IsMasterClient)
            {
                // 턴 시작!
                ChangeTurn();
            }
        }
    }

    // 방장아 턴 넘겨줘
    public void ChangeTurn()
    {
        photonView.RPC(nameof(RpcChangeTurn), RpcTarget.MasterClient);

    }

    // 방장에서 호출된다.
    [PunRPC]
    void RpcChangeTurn()
    {
        // turnIdx 를 최대인원 값보다 작게 만들자.
        turnIdx = ++turnIdx % allPlayer.Length;

        print("현재 턴 : " + turnIdx);

        PlayerFire pf = allPlayer[turnIdx].GetComponent<PlayerFire>();
        pf.ChangeTurn(true);
    }
}
