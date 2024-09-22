using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviourPun
{
    // 큐브 Prefab
    public GameObject cubeFactory;

    // Impact Prefab
    public GameObject impactFactory;

    // RigidBody 로 움직이는 총알 Prefab
    public GameObject bulletFactory;
    // 총알 Prefab
    public GameObject rpcBulletFactory;
    // 총구 Transform
    public Transform firePos;
    // Animator
    Animator anim;

    // 스킬 중심점
    public Transform skillCenter;

    // 나의 턴 이니?
    public bool isMyTurn;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        // HPSystem 가져오자.
        HPSystem hpSystem = GetComponentInChildren<HPSystem>();
        // onDie 변수에 OnDie 함수 설정
        hpSystem.onDie = OnDie;
    }

    void Update()
    {
        // 만약에 내 것이 아니라면
        if (photonView.IsMine == false) return;

        // 마우스의 lockMode 가 None 이면 (마우스 포인터가 활성화 되어 있다면) 함수를 나가자.
        if (Cursor.lockState == CursorLockMode.None)
            return;

        // HP 0 이 되었으면 총 쏘지 못하게
        if (isDie) return;

        // 내 턴이 아니라면 함수를 나가자
        // if (!isMyTurn) return;

        // 마우스 왼쪽 버튼 누르면
        if (Input.GetMouseButtonDown(0))
        {
            // 총쏘는 애니메이션 실행 (Fire 트리거 발생)
            photonView.RPC(nameof(SetTrigger), RpcTarget.All, "Fire");
            // 총알공장에서 총알을 생성, 총구위치 셋팅, 총구회전 셋팅
            PhotonNetwork.Instantiate("Bullet2", firePos.position, firePos.rotation);

            Debug.Log("총알 발사됨");
        }
        // 마우스 가운데 휠 버튼 눌렀을때
        //if (Input.GetMouseButtonDown(2))
        //{
        //    photonView.RPC(nameof(CreateBullet), RpcTarget.All, firePos.position, Camera.main.transform.rotation);
        //}

        //// 마우스 오른쪽 버튼 누르면
        //if (Input.GetMouseButtonDown(1))
        //{
        //    // 카메라 위치, 카메라 앞방향으로 된 Ray를 만들자.
        //    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //    // 만들어진 Ray 를 이용해서 Raycast 하자.
        //    RaycastHit hit;
        //    // 만약 부딪힌 지점이 있으면
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        // 폭발효과를 생성하고 부딪힌 위치에 놓자.
        //        //CreateImpact(hit.point);
        //        photonView.RPC(nameof(CreateImpact), RpcTarget.All, hit.point);

        //        // 부딪힌 놈의 데미지를 주자.
        //        HPSystem hpSystem = hit.transform.GetComponentInChildren<HPSystem>();
        //        if (hpSystem != null)
        //        {
        //            hpSystem.UpdateHP(-1);
        //        }
        //    }

        //    // 내 턴을 끝내자
        //    isMyTurn = false;
        //    // GameManger 에게 턴 넘겨달라고 요청
        //    Game2Manager.instance.ChangeTurn();
        //}

        // 1 번키 누르면
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 카메라의 앞방향으로 5만큼 떨어진 위치를 구하자.
            Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 5;
            // 큐브공장에서 큐브를 생성, 위치, 회전
            PhotonNetwork.Instantiate("Cube", pos, Quaternion.identity);
            //photonView.RPC(nameof(CreateCube), RpcTarget.All, pos);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            int maxBulletCnt = 10;
            float angle = 360.0f / maxBulletCnt;

            for (int i = 0; i < maxBulletCnt; i++)
            {
                #region 싱글플레이 모드
                //// 총알 생성
                //GameObject bullet = Instantiate(bulletFactory);
                //// skillCenter 를 (angle * i) 만큼 회전
                //skillCenter.localEulerAngles = new Vector3(0, angle * i, 0);

                //// 생성된 총알을 skillCenter 의 앞방으로 2 만큼 떨어진 위치에 놓자.
                //bullet.transform.position = skillCenter.position + skillCenter.forward * 2;

                //// 생성된 총알의 up 방향을 skillcenter 의 forward 로 하자
                //bullet.transform.up = skillCenter.forward;
                #endregion

                #region 멀티플레이 모드
                // skillCenter 를 (angle * i) 만큼 회전
                skillCenter.localEulerAngles = new Vector3(0, angle * i, 0);
                Vector3 pos = skillCenter.position + skillCenter.forward * 2;
                Quaternion rot = Quaternion.LookRotation(Vector3.down, skillCenter.forward);
                PhotonNetwork.Instantiate(bulletFactory.name, pos, rot);
                #endregion
            }
        }
    }

    [PunRPC]
    void SetTrigger(string parameter)
    {
        anim.SetTrigger(parameter);
    }

    [PunRPC]
    void CreateBullet(Vector3 position, Quaternion rotation)
    {
        Instantiate(rpcBulletFactory, position, rotation);
    }

    [PunRPC]
    void CreateImpact(Vector3 position)
    {
        GameObject impact = Instantiate(impactFactory);
        impact.transform.position = position;
    }


    [PunRPC]
    void CreateCube(Vector3 position)
    {
        Instantiate(cubeFactory, position, Quaternion.identity);
    }

    // 죽었니?
    bool isDie;
    public void OnDie()
    {
        isDie = true;
    }

    public void ChangeTurn(bool turn)
    {
        photonView.RPC(nameof(RpcChangeTurn), photonView.Owner, turn);
    }

    // isMyTurn 을 변경해주는 함수
    [PunRPC]
    void RpcChangeTurn(bool turn)
    {
        isMyTurn = turn;
    }
}