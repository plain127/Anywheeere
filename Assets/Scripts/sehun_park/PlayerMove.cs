using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerMove : MonoBehaviourPun, IPunObservable
{
    // 캐릭터 컨트롤러
    CharacterController cc;

    // 이동 속력
    public float moveSpeed = 5;

    // 중력
    float gravity = -9.81f;
    // y 속력
    float yVelocity;
    // 점프 초기 속력
    public float jumpPower = 3;

    // 대쉬 관련 변수
    public float dashDistance = 10f;  // 대쉬 거리
    public float dashDuration = 0.2f; // 대쉬 시전 시간
    public float dashCooldown = 5f;   // 대쉬 쿨타임
    private bool canDash = true;      // 대쉬 가능 여부
    private bool isDashing = false;   // 대쉬 중인지 여부
    private Vector3 dashDirection;    // 대쉬 방향
    private float dashTimeLeft;       // 남은 대쉬 시간

    // 카메라 
    public GameObject cam;

    // 서버에서 넘어오는 위치값
    Vector3 receivePos;
    // 서버에서 넘어오는 회전값
    Quaternion receiveRot;
    // 보정 속력
    public float lerpSpeed = 50;

    // animator
    Animator anim;

    // AD 키 입력 받을 변수
    float h;
    // WS 키 입력 받을 변수
    float v;

    // LookPos
    public Transform lookPos;

    // 닉네임 UI
    public TMP_Text nickName;

    [PunRPC]
    void RpcAddPlayer(int order)
    {
        // GameManger 에게 photonView 를 넘겨주자
        Game2Manager.instance.AddPlayer(photonView, order);
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            // 마우스 잠그자.
            Cursor.lockState = CursorLockMode.Locked;

            // 내가 방에 들어온 순서를 모두에게 알려주자.
            photonView.RPC(nameof(RpcAddPlayer), RpcTarget.AllBuffered, ProjectMgr.Get().orderInRoom);

        }


        // 캐릭터 컨트롤러 가져오자.
        cc = GetComponent<CharacterController>();
        // 내 것일 때만 카메라를 활성화자
        cam.SetActive(photonView.IsMine);
        // Animator 가져오자
        anim = GetComponentInChildren<Animator>();

        // 닉네임 UI 에 해당캐릭터의 주인의 닉네임 설정
        nickName.text = photonView.Owner.NickName;
    }

    void Update()
    {
        // 내 것일 때만 컨트롤 하자!
        if (photonView.IsMine)
        {
            // 마우스의 lockMode 가 None 이면 (마우스 포인터가 활성화 되어 있다면) 함수를 나가자.
            if (Cursor.lockState == CursorLockMode.None)
                return;

            // 1 .키보드 WASD 키 입력을 받자
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            // 2. 방향을 정하자.
            Vector3 dirH = transform.right * h;
            Vector3 dirV = transform.forward * v;
            Vector3 dir = dirH + dirV;

            dir.Normalize();

            // 만약에 땅에 있으면 yVelocity 를 0 으로 초기화
            if (cc.isGrounded)
            {
                yVelocity = 0;
            }

            // 만약에 Space 바를 누르면
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // yVelocity 를 jumpPower 로 설정
                yVelocity = jumpPower;
            }

            // yVelocity 값을 중력에 의해서 변경시키자.
            yVelocity += gravity * Time.deltaTime;

            #region 물리적인 점프 아닌것
            // dir.y 에 yVelocity 값을 셋팅
            dir.y = yVelocity;

            // 3. 그 방향으로 움직이자.
            cc.Move(dir * moveSpeed * Time.deltaTime);
            #endregion

            #region 물리적인 점프
            //dir = dir * moveSpeed;
            //dir.y = yVelocity;
            //cc.Move(dir * Time.deltaTime);
            #endregion

            // 대쉬 처리
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartDash(dir);  // 대쉬 시작
            }

            else
            {
                DashMove();
            }
        }

        // 나의 Player 아니라면
        else
        {
            // 위치 보정
            transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * lerpSpeed);
            // 회전 보정
            transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, Time.deltaTime * lerpSpeed);
        }

        // anim 을 이용해서 h, v 값을 전달
        anim.SetFloat("DirH", h);
        anim.SetFloat("DirV", v);
    }
    // 대쉬 시작 메서드
    private void StartDash(Vector3 dir)
    {
        dashDirection = dir;
        dashTimeLeft = dashDuration;
        isDashing = true;
        canDash = false;
        Invoke(nameof(ResetDash), dashCooldown);  // 쿨타임 시작
    }

    // 대쉬 이동 메서드
    private void DashMove()
    {
        if (dashTimeLeft > 0)
        {
            cc.Move(dashDirection * (dashDistance / dashDuration) * Time.deltaTime);
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
        }
    }

    // 대쉬 쿨타임 초기화 메서드
    private void ResetDash()
    {
        canDash = true;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 만약에 내가 데이터를 보낼 수 있는 상태라면 (내 것이라면)
        if (stream.IsWriting)
        {
            // 나의 위치값을 보낸다.
            stream.SendNext(transform.position);
            // 나의 회전값을 보낸다.
            stream.SendNext(transform.rotation);
            // 나의 h 값
            stream.SendNext(h);
            // 나의 v 값
            stream.SendNext(v);
            // LookPos 의 위치값을 보낸다.
            stream.SendNext(lookPos.position);
        }
        // 데이터를 받을 수 있는 상태라면 (내 것이 아나라면)
        else if (stream.IsReading)
        {
            // 위치값을 받자.
            receivePos = (Vector3)stream.ReceiveNext();
            // 회전값을 받자.
            receiveRot = (Quaternion)stream.ReceiveNext();
            // 서버에서 전달 되는 h 값 받자.
            h = (float)stream.ReceiveNext();
            // 서버에서 전달 되는 v 값 받자.
            v = (float)stream.ReceiveNext();
            // LookPos 의 위치값을 받자.
            lookPos.position = (Vector3)stream.ReceiveNext();
        }
    }
}
