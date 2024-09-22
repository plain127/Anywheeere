using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Delegate : 함수를 담는 변수를 만드는 자료형
public delegate void CallBack();
public delegate void CallBack2(string s);

public class DelegateStudy : MonoBehaviour
{
    CallBack callBack;
    Action action;

    CallBack2 callBack2;
    Action<string> action2;

    void Start()
    {
        callBack = () =>
        {
            print(gameObject.name);
        };


        action = PrintName;

        PrintName();
        callBack();


        callBack2 = (string s) =>
        {
            print(s + " : " + gameObject.name);
        };

        callBack2 += PrintName3;

        //PrintName2("직접 호출");
        callBack2("딜리게이트로 호출");
    }

    void Update()
    {
        
    }

    void PrintName()
    {
        print(gameObject.name);
    }
    void PrintName2(string s)
    {
        print(s + " : " + gameObject.name);
    }

    void PrintName3(string s)
    {
        print(s);
    }
}
