using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class DocentManager : MonoBehaviour
{
    public string url;
    // ���õ� ���÷� �̵��Ѵ�.
    // ���ÿ� ����Ʈ �����͸� AI ���Լ� �޾ƿ´�.
    // �޾ƿ� �����͸� �����صд�. text / audio

    public string docent;

    public string audioAddress;
    public AudioClip docentAudio;

    // ����Ʈ�� �����ϸ� ����Ǿ� �ִ� text�� UI ���·� ǥ���Ѵ�.
    // audio�� AudioClip ���� ���� �Ѵ�.

    // ���� ���� json �����͸� �̿� �ϱ� ���� class 
    [Serializable]
    public class DocentResponse
    {
        public string docent;
        public string audio;
    }


    void Start()
    {
            
    }

    void Update()
    {
        
    }

    public void GetDocent()
    {
        StartCoroutine(GetDocentFromAI(url));
    }

    // �̵��ϴ� ���� ������ �����ϰ� docent �ؽ�Ʈ�� audio �ּҸ� �޾� ��
    IEnumerator GetDocentFromAI(string url)
    {
        string jsonData = "{\"text\":\"�����ǿ��Ż�\"}";
        using (UnityWebRequest www = UnityWebRequest.Get(url + "docent"))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            // �޾ƿ� text�� json ���� DocentResponse class �� ����
            DocentResponse responseData = JsonUtility.FromJson<DocentResponse>(www.downloadHandler.text);

            // �޾ƿ� json���� docent �κа� audio �ּ� �κ��� �и��� �־� ��
            print(responseData.docent);
            print(responseData.audio);
            docent = responseData.docent;
            audioAddress = responseData.audio;

            // ���� ��.. ������ �� �� ��

            // ����� ����Ʈ �޴� �Լ� ����
            // docentAudio�� �޾ƿ� AudioClip ����

            // �̸� �޾Ƴ��� ������� ���
            // ��ü �÷ο� ������ ��� �ϴ°�.. ?

            StartCoroutine(GetDecentAudioFromAI(url + "audio"));
        }
    }

    // docent audio �� �޾Ƽ� AudioClip�� ���� �ϴ� �Լ�
    // ���Ķ� ���� ����
    IEnumerator GetDecentAudioFromAI(string url)
    {
        string jsonData = "{\"path\":\"./output.wav\"}";
        using(UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
        {
            print("audio �Լ� ����");
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            DownloadHandlerAudioClip downloadHandler = www.downloadHandler as DownloadHandlerAudioClip;
            docentAudio = downloadHandler.audioClip;
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = docentAudio;
            audioSource.Play();
            // docentAudio�� ���� �ؾ� �� �� ���� �Ѵ�.(��� �־� ������~?)
        }
    }





    // ����ȯ�濡�� �޾ƿ� docent�� UI�� ���� ��Ų��.( Audio�� ���� ���� // ����� ���� ��ư�� ?)
    void ViewDocent()
    {

    }

    

}
