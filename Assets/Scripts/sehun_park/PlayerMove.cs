using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{
    public float moveSpeed = 5.0f;
    CharacterController cc;

    // 중력
    float gravity = -9.81f;
    // y 속력
    float yVelocity;
    // 점프 초기 속력
    public float jumpPower = 3;

    public GameObject cam;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        // 내 것일 때만 카메라를 활성화하자.
        cam.SetActive(photonView.IsMine);
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // 1. 키보드 WASD 키 입력을 받자
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            // 2. 방향을 정하자.
            Vector3 dirH = transform.right * h;
            Vector3 dirV = transform.forward * v;
            Vector3 dir = dirH + dirV;

            dir.Normalize();

            // 만약에 땅에 있으면 yVelocity를 0으로 초기화
            if (cc.isGrounded)
            {
                yVelocity = 0;
            }

            // 만약에 Space 바를 누르면
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // yVelocity 를 jumpPower로 설정
                yVelocity = jumpPower;
            }

            // yVelocity 값을 중력에 의해서 변경시키자.
            yVelocity += gravity * Time.deltaTime;
            // dir.y에 yVelocity 값을 세팅
            dir.y = yVelocity;

            // 자신의 방향을 기준으로 dir 변경
            // dir = transform.TransformDirection(dir); ;

            // 3, 그 방향으로 움직이자.
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }

    }
}