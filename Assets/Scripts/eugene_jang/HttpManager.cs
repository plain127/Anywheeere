using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;  // http 통신을 위한 네임 스페이스
using System.Text;      // json, csv 같은 문서 형태의 인코딩  (UTF-8)을 위한 네임 스페이스

public class HttpManager : MonoBehaviour
{
    public string url;

    void Start()
    {
        StartCoroutine(GetRequest(url));
    }

    // Get 통신 코루틴 함수
    IEnumerator GetRequest(string url)
    {
        // http Get 통신 준비를 한다.
        UnityWebRequest request = UnityWebRequest.Get(url);

        // 서버에 Get 요청을 하고, 서버로 부터 응답이 올 때 까지 대기한다. 
        yield return request.SendWebRequest();

        // 만일, 서버로부터 온 응답이 성공(200)이라면...
        if(request.result == UnityWebRequest.Result.Success)
        {
            // 응답받은 데이터를 출력한다.
            string response = request.downloadHandler.text;
            print(response);
        }
        // 그렇지 않다면...(400,404 etc)
        else
        {
            // 에러 내용을 출력한다.
            print(request.error);
        }



    }

}
