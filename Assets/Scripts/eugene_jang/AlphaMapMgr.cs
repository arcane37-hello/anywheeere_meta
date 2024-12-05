using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DocentManager;
// using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;

public class AlphaMapMgr : MonoBehaviour
{
    public GameObject CesiumMap;
    public SkyboxManager skyboxManager;

    public string url;


    public AudioSource audioSource;
    public Text docentText;

    public GameObject mapPanel;

    int idx;
    string jsonData;

    public Button btn_USA;
    public Button btn_Italy;
    public Button btn_Japan;

    public GameObject CountryPanel;
    public GameObject USAPanel;
    public GameObject ItalyPanel;
    public GameObject JapanPanel;

    public GameObject NYCPanel;
    public GameObject SFPanel;
    public GameObject LVPanel;

    public GameObject RomePanel;
    public GameObject VenicePanel;

    public GameObject TokyoPanel;
    public GameObject KyotoPanel;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            print("���Ⱦ��!");
            TestAudioFromAI();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapPanel.activeInHierarchy)
            {
                mapPanel.SetActive(false);
                OnDisable();
            }
            else if (!mapPanel.activeInHierarchy)
            {
                mapPanel.SetActive(true);
            }

        } 

    }
    
    private void OnDisable()
    {
        if(audioSource != null)
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                Destroy(audioSource);
            }
            print("�� �ڿ�");
        }
        CountryPanel.SetActive(true);
        USAPanel.SetActive(false);
        ItalyPanel.SetActive(false);
        JapanPanel.SetActive(false);
        NYCPanel.SetActive(false);
        SFPanel.SetActive(false);
        LVPanel.SetActive(false);
        RomePanel.SetActive(false);
        VenicePanel.SetActive(false);
        TokyoPanel.SetActive(false);
        KyotoPanel.SetActive(false);

    }
    public void CountryBtnOnClick(string country)
    {
        print(country);
        // country �� �̵�
        // country �� docent audio �޾ƿ��� ?? !! �� ��
        
        CountryPanel.SetActive(false);
        if(country == "USA")
        {
            USAPanel.SetActive(true);
            idx = 0;
        }
        else if (country == "Italy")
        {
            ItalyPanel.SetActive(true);
            idx = 1;
        }
        else if (country == "Japan")
        {
            JapanPanel.SetActive(true);
            idx = 2;
        }

        //GetDocent(country);
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
    }

    public void SelectCity(string city)
    {
        print(city);
        // city �� �̵�
        // city �� docent audio �޾ƿ��� ?? !! �ǰ��� ? ���� 

        if(city == "NewYork") 
        { 
            idx = 3;
            NYCPanel.SetActive(true); 
        } else if(city == "San Francisco")
        {
            idx = 4;
            SFPanel.SetActive(true);
        } else if (city == "Las Vegas")
        {
            idx = 5;
            LVPanel.SetActive(true);
        }
        USAPanel.SetActive(false);

        if (city == "Rome")
        {
            idx = 6;
            RomePanel.SetActive(true);
        }else if (city == "Venice")
        {
            idx = 7;
            VenicePanel.SetActive(true);
        }
        ItalyPanel.SetActive(false);

        if (city == "Tokyo")
        {
            idx = 8;
            TokyoPanel.SetActive(true);
        }else if(city == "Kyoto")
        {
            idx = 9;
            KyotoPanel.SetActive(true);
        }
        JapanPanel.SetActive(false);

        //GetDocent(city);
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
    }


    public void SelectMonument(string monument)
    {
        print(monument);
        if (monument == "������ ���Ż�") { idx = 10; }
        else if (monument == "Ÿ�� ������") { idx = 11;}
        else if (monument == "��Ʈ�� ��ũ") { idx = 12; }
        else if (monument == "�����̾� ������Ʈ ����") { idx = 13; }
        else if (monument == "���Ŭ�� �긴��") { idx = 14; }
        else if (monument == "��� ����Ʈ �긴��") { idx = 15; }
        else if (monument == "�������ý��� �غ�") { idx = 16; }
        else if (monument == "�� ���Ǿ�") { idx = 17; }
        else if (monument == "�󽺺����� ��Ʈ��") { idx = 18; }
        else if (monument == "�������� �м�") { idx = 19; }
        else if (monument == "������ �縮��") { idx = 20; }
        else if (monument == "����ġ�� ȣ��") { idx = 21; }
        else if (monument == "���׿� ����") { idx = 22; }
        else if (monument == "�ݷμ���") { idx = 23; }
        else if (monument == "�� ����� �뼺��") { idx = 24; }
        else if (monument == "�� ������ �뼺��") { idx = 25; }
        else if (monument == "������ �ٸ�") { idx = 26; }
        else if (monument == "���Ͻ� �����") { idx = 27; }
        else if (monument == "����Ÿ��") { idx = 28; }
        else if (monument == "���� ��ī�� Ʈ��") { idx = 29; }
        else if (monument == "�ƻ����") { idx = 30; }
        else if (monument == "�ݰ���") { idx = 31; }
        else if (monument == "�Ľù� �̳��� �Ż�") { idx = 32; }
        else if (monument == "�߻�ī�� ž") { idx = 33; }
        else if (monument == "�ƶ�þ߸� �볪�� ��") { idx = 34; }
        GetDocent(monument);
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
    }


    public void GetDocent(string target)
    {
        jsonData = "{\"text\":\""+target+"\"}";
        print(idx);
        StartCoroutine(GetDocentFromAI(url, jsonData));
    }

    IEnumerator GetDocentFromAI(string url, string jsonData)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + "docent"))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            DocentResponse responseData = JsonUtility.FromJson<DocentResponse>(www.downloadHandler.text);

            print(responseData.docent);
            print(responseData.audio);
            docentText.text = responseData.docent;
            //�޾ƿ� �ؽ�Ʈ(json���� ��) ����Ʈ �κ��� string ������ ���� �� ��

            // �޾ƿ� json���� docent �κа� audio �ּ� �κ��� �и��� �־� ��
            // ����� ����Ʈ �޴� �Լ� ����
            // docentAudio�� �޾ƿ� AudioClip ���� 
            
        }
    }


        //����� �׽�Ʈ �Լ�
        public void TestAudioFromAI()
    {
        StartCoroutine(TestAudioRequest());
    }

    IEnumerator TestAudioRequest()
    {
        string jsonData = "{\"path\":\"./output.wav\"}";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("http://metaai.iptime.org:9000/audio", AudioType.WAV))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if ( www.result == UnityWebRequest.Result.Success)
            {
                DownloadHandlerAudioClip downloadHandler = www.downloadHandler as DownloadHandlerAudioClip;
                PlayAudioClip(downloadHandler.audioClip);
            }
        }
    }
    void PlayAudioClip(AudioClip clip)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
