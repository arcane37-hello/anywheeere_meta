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
        #region json �����͸� ����� �����ϱ�
        //// ����ü �ν��Ͻ��� �����.
        //UserData user1 = new UserData("�ڿ���", 44, "����", true);
        ////user1.name = "�ڿ���";
        ////user1.age = 44;
        ////user1.job = "����";
        ////user1.isMan = true;

        //// ����ü �����͸� Json ���·� �����Ѵ�.
        //string jsonUser1 = JsonUtility.ToJson(user1, true);

        //print(jsonUser1);
        //result_text.text = jsonUser1;

        //SaveJsonData(jsonUser1, Application.dataPath, "�ڿ���.json");
        //Application.streamingAssetsPath ������� ��� ��� ���
        #endregion

        //string readString = ReadJsonData(Application.dataPath, "�ڿ���json");
        //print(readString);
    }

    // text �����͸� ���Ϸ� �����ϱ�
    public void SaveJsonData(string json, string path, string fileName)
    {
        // 1. ���� ��Ʈ���� ���� ���·� ����.
        //string fullPath = path + "/" + fileName;
        string fullPath = Path.Combine(path, fileName);
        FileStream fs = new FileStream(fullPath ,FileMode.OpenOrCreate, FileAccess.Write);
        // 2. ��Ʈ���� json �����͸� ����� �����Ѵ�.
        byte[] jsonBinary = Encoding.UTF8.GetBytes(json);
        fs.Write(jsonBinary);

        // 3. ��Ʈ���� �ݾ� �ش�.
        fs.Close();
    }

    // text ������ �о����
    //public string ReadJsonData(string path, string fileName)
    //{
    //    string readText;

    //    //1. ���� ��Ʈ���� �б� ���� ����
    //    string fullPath = Path.Combine(path, fileName);
    //    FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

    //    // 2. ��Ʈ�����κ��� ������(byte)�� �о� �´�.
    //    StreamReader sr = new StreamReader(fs, Encoding.UTF8);
    //    readText = sr.ReadToEnd();

    //    // 3. ���� �����͸� string���� ��ȯ�ؼ� ��ȯ�Ѵ�.
    //    return readText;
    //}
}
