using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIK : MonoBehaviour
{
    Animator anim;

    // 바라볼 대상
    public Transform trLook;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        // 해당 위치를 바라보게 설정
        anim.SetLookAtPosition(trLook.position);
        // 바라보게 하는 IK 기능의 가중치를 주자
        anim.SetLookAtWeight(1, 1);
    }
}
