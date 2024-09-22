using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;  // http 통신을 위한 네임 스페이스
using System.Text;      // json, csv 같은 문서 형태의 인코딩  (UTF-8)을 위한 네임 스페이스
using UnityEngine.UI;
using System;


public class HttpManager : MonoBehaviour
{
    public string url;
    public Text text_response;
    public Text text_Docent;
    public RawImage img_response;

    public List<InputField> userInputs = new List<InputField>();
    public Toggle freeUser;
    public AudioClip docentAudio;

    private string apiUrl = "http://39.121.152.94:3000/api/data";
    public void Get()
    {
        StartCoroutine(GetRequest(url));
    }

    // Get 통신 코루틴 함수
    #region 수업 내용 Get
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
            text_response.text = response;

        }
        // 그렇지 않다면...(400,404 etc)
        else
        {
            // 에러 내용을 출력한다.
            print(request.error);
            text_response.text = request.error;
        }

#endregion
    #region 수업 내용 Get Image

    }
    public void GetImage()
    {
        StartCoroutine(GetImageRequest(url));
    }

    // 이미지 파일을 Get으로 받는 함수
    IEnumerator GetImageRequest(string url)
    {
        // get(Texture) 통신을 준비한다.
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        // 서버에 요청을 하고 , 응답이 있을 때까지 기다린다.
        yield return request.SendWebRequest();

        // 만일, 응답이 성공이라면...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 받은 텍스쳐 데이터를 Textur2D 변수에 받아 놓는다.
            Texture2D response = DownloadHandlerTexture.GetContent(request);
            // Texture2D 이미지를 img_response의 texture 값으로 넣어둔다.
            img_response.texture = response;

            // text_response에 성공 코드 번호를 출력한다.
            text_response.text = "성공 - " + request.responseCode.ToString();

        }else
        {
            print(request.error);
            text_response.text = request.error;
        }
    }
    #endregion

    #region 수업 Get Json
    public void GetJson()
    {

    }

    IEnumerator GetJsonImageRequest(string url)
    {
        // url로 부터 Get으로 요청을 준비한다. 
        UnityWebRequest request = UnityWebRequest.Get(url);
        // 준비된 요청을 서버에 전달하고 응답이 올때까지 기다린다.
        yield return request.SendWebRequest();
        // 만일, 응답이 성공이라면...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 텍스트를 받는다.
            string result = request.downloadHandler.text;
            
            //응답받은 json 데이터를 RequestImage 구조체 형태로 파싱한다.
            RequestImage reqImageData = JsonUtility.FromJson<RequestImage>(result);

            byte[] binaries = Encoding.UTF8.GetBytes(reqImageData.img);
            byte[] imageBytes = Convert.FromBase64String(reqImageData.img);

            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(imageBytes))
            {
                img_response.texture = texture;

                text_response.text = "이미지 로드 성공";
            }
            else
            {
                //이미지 로드 실패 시 오류 메시지 출력
                text_response.text = "이미지 로드 실패";
            }

            //if(binaries.Length >0)
            //{
            //    Texture2D texture = new Texture2D(2, 2);
                
            //    // byte 배열로 된 raw 데이터를 텍스쳐 형태로 변환해서 texture2D 인스턴스로 변환한다.
            //    texture.LoadRawTextureData(binaries);
            //    texture.EncodeToJPG();

            //    img_response.texture = texture;
                
            //}

        }
        // 그렇지 않다면...
        else
        {
        // 에러 내용을 text_response에 전달한다.
        text_response.text = request.responseCode + " : " + request.error;
            Debug.LogError(request.responseCode + " : " + request.error);
        }

    }
    #endregion

    #region 수업 내용 Post
    // 서버에 Json 데이터를 Post하는 함수
    public void PostJson()
    {
        StartCoroutine(PostJsonRequest(url));
    }
    IEnumerator PostJsonRequest(string url)
    {
        // 사용자의 입력 정보를 Json 데이터로 변환하기
        JoinUserData userData = new JoinUserData();
        userData.id = Convert.ToInt32(userInputs[0].text);
        userData.password = userInputs[1].text;
        userData.nickName = userInputs[2].text;
        userData.freeAccount = freeUser.isOn;
        string userJsonData = JsonUtility.ToJson(userData, true);
        byte[] jsonBins = Encoding.UTF8.GetBytes(userJsonData);

        // POST를 하기 위한 준비를 한다.
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(jsonBins);
        request.downloadHandler = new DownloadHandlerBuffer();

        // 서버에 Post를 전송하고 응답이 올 때 까지 기다린다.
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            // 다운로드 핸들러에서 텍스트 값을 받아서 UI에 출력한다.
            string response = request.downloadHandler.text;
            text_response.text = response;
            Debug.LogError(text_response.text);
        }
        else
        {
            text_response.text = request.error;
            Debug.LogError(request.error);
        }
    }

    #endregion


    public void PostJsonToAI()
    {
        StartCoroutine(PostPointJsonRequest(url));
    }
    IEnumerator PostPointJsonRequest(string url)
    {
        string jsonData = "{\"text\":\"자유의여신상\"}";       
        using(UnityWebRequest www = UnityWebRequest.Get("http://metaai.iptime.org:9000/docent"))
        {
            byte [] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");   
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.downloadHandler.text);
                text_response.text = www.downloadHandler.text;
                text_Docent.text = www.downloadHandler.text;

}
            else
            {
                Debug.LogError(www.error);
            }
        }

        #region 망한 post요청
        //    PointedPlace testData = new PointedPlace(40.6893f, -74.0448f, "NewYork", "StatueOfLiberty");
        //string pointJsonData = JsonUtility.ToJson(testData, true);
        //byte[] jsonBins = System.Text.Encoding.UTF8.GetBytes(pointJsonData);

        //UnityWebRequest request = new UnityWebRequest(url, "POST");
        //request.SetRequestHeader("Content-Type", "application/json");
        ////request.uploadHandler = new UploadHandlerRaw(jsonBins);
        //request.uploadHandler = new UploadHandlerRaw(jsonBins);
        //request.downloadHandler = new DownloadHandlerBuffer();

        //yield return request.SendWebRequest();

        //if (request.result == UnityWebRequest.Result.Success)
        //{
        //    // 다운로드 핸들러에서 텍스트 값을 받아서 UI에 출력한다.
        //    string response = request.downloadHandler.text;
        //    text_response.text = response;
        //    Debug.LogError(text_response.text);
        //}
        //else
        //{
        //    text_response.text = request.error;
        //    Debug.LogError(request.error);
        //}
        #endregion
    }

    public void GetAudioFromAI()
    {
        StartCoroutine(GetAudioRequest(url));
    }

    IEnumerator GetAudioRequest(string url)
    {
        // 보낼 json 데이터를 준비함
        string jsonData = "{\"path\":\"./result.wav\"}";
        // url로 요청을 보내고 받을 파일을 지정함? // Json으로 받는거 아닌가.. ?
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("http://metaai.iptime.org:9000/audio", AudioType.WAV ))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            // 요청이 가는 것 까지는 확인 Clear!

            // 받는 것은 어케 하누.. ㅠ ?
            //www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            

            if (www.result == UnityWebRequest.Result.Success)
            {
                DownloadHandlerAudioClip downloadHandler = www.downloadHandler as DownloadHandlerAudioClip;
                PlayAudioClip(downloadHandler.audioClip);
                
                print("여기까지 됨");

                // 텍스트는 오지 않아... 기다리지 마...
                //string responseText = downloadHandler.text;
                //docentAudio = DownloadHandlerAudioClip.GetContent(www);

                if (docentAudio != null)
                {
                    PlayAudioClip(docentAudio);
                }

                // 텍스트가 포함 되어 있지 않음
                //Debug.LogError(www.downloadHandler.text);
                //text_response.text = www.downloadHandler.text;

            }
            else
            {
                Debug.LogError(www.error);
            }
        }
    }
    
    void PlayAudioClip(AudioClip clip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();


    }


    public void GetNodeAPI()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("API 응답: "+ jsonResponse);
            text_response.text = jsonResponse;
        }
        else
        {
            Debug.LogError("API 요청 실패: " + request.error);
        }
    }

}


#region 수업에서 사용한 구조체

[System.Serializable]
public struct RequestImage
{
    public string img;
}

[System.Serializable]
public struct JoinUserData
{
    public int id;
    public string password;
    public string nickName;
    public bool freeAccount;
}
#endregion