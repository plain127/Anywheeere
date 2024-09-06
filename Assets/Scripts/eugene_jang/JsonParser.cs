using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;

[System.Serializable]
public struct UserData
{
    public string name;
    public int age;
    public string job;
    public bool isMan;

    public UserData(string name, int age, string job, bool isMan)
    {
        this.name = name;
        this.age = age; 
        this.job = job;
        this.isMan = isMan;

    }
}
[System.Serializable]
public struct PointedPlace
{
    float latitude;
    float longitude;
    string city;
    string landmark;
    public PointedPlace(float latitude, float longitude, string city, string landmark)
    {
        this.latitude = latitude;
        this.longitude = longitude;
        this.city = city;  
        this.landmark = landmark;
    }
}

public class JsonParser : MonoBehaviour
{
    public Text result_text;

    void Start()
    {
        #region json 데이터를 만들고 저장하기
        //// 구조체 인스턴스를 만든다.
        //UserData user1 = new UserData("박원석", 44, "강사", true);
        ////user1.name = "박원석";
        ////user1.age = 44;
        ////user1.job = "강사";
        ////user1.isMan = true;

        //// 구조체 데이터를 Json 형태로 변경한다.
        //string jsonUser1 = JsonUtility.ToJson(user1, true);

        //print(jsonUser1);
        //result_text.text = jsonUser1;

        //SaveJsonData(jsonUser1, Application.dataPath, "박원석.json");
        //Application.streamingAssetsPath 모바일의 경우 상대 경로
        #endregion

        //string readString = ReadJsonData(Application.dataPath, "박원석json");
        //print(readString);
    }

    // text 데이터를 파일로 저장하기
    public void SaveJsonData(string json, string path, string fileName)
    {
        // 1. 파일 스트림을 쓰기 형태로 연다.
        //string fullPath = path + "/" + fileName;
        string fullPath = Path.Combine(path, fileName);
        FileStream fs = new FileStream(fullPath ,FileMode.OpenOrCreate, FileAccess.Write);
        // 2. 스트림에 json 데이터를 쓰기로 전달한다.
        byte[] jsonBinary = Encoding.UTF8.GetBytes(json);
        fs.Write(jsonBinary);

        // 3. 스트림을 닫아 준다.
        fs.Close();
    }

    // text 파일을 읽어오기
    //public string ReadJsonData(string path, string fileName)
    //{
    //    string readText;

    //    //1. 파일 스트림을 읽기 모드로 연다
    //    string fullPath = Path.Combine(path, fileName);
    //    FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

    //    // 2. 스트림으로부터 데이터(byte)를 읽어 온다.
    //    StreamReader sr = new StreamReader(fs, Encoding.UTF8);
    //    readText = sr.ReadToEnd();

    //    // 3. 읽은 데이터를 string으로 변환해서 반환한다.
    //    return readText;
    //}
}
