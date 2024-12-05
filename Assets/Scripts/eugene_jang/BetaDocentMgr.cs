using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DocentManager;


public class BetaDocentMgr : MonoBehaviour
{
    public static BetaDocentMgr Instance;

    public GameObject CesiumMap;

    public string url;

    public AudioSource audioSource;
    public Text docentText;

    public Text countryText;
    public Text cityText;

    public AudioClip[] effectSound;

    public AudioSource docentAudio;
    public AudioSource bgm;

    public GameObject mapPanel;

    public int idx;
    string jsonData;

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

    private void Awake()
    {
        if (Instance == null)
        {
        Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        idx = 33;
        audioSource.volume = 0.5f;
    }
       

    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.L))
        {
            audioSource.clip = effectSound[1];
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            audioSource.Play();
        }

        if (idx < 0)
        {
            countryText.text = " - ";
        }
        if (idx < 3)
        {
            cityText.text = " - ";
        }
        // index�� ����...


    }
    private void OnDisable()
    {
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
        countryText.text = country;
        CountryPanel.SetActive(false);
        if      (country == "USA")   { idx = 0; USAPanel.SetActive(true); docentText.text = "��, ���� �̱��� �����߽��ϴ�! �� ����� �� ������ ������ �پ��� ��ȭ�� �� ���迡�� ����ް� �ֽ��ϴ�. ������ ��¡�� ����, ������ ���� �������ý���, �׸��� ������ �������θ�Ʈ�� �������� �󽺺���������, �̱��� ������ ���ο� ������ �����մϴ�. ���� �������� ��� �ɱ��?";  }
        else if (country == "Italy") { idx = 1; ItalyPanel.SetActive(true); docentText.text = "�츮�� ���� ��Ż���ƿ� �����߽��ϴ�! ��� �θ��� ������ ��� �� ���� �̰��� ������ ������ �����Դϴ�. �θ��� �Ŵ��� ������, ����ġ���� ����, �׸��� ��Ż���� �������� �������� �Ƹ��ٿ� ǳ��� ���ı���, ��Ż���ƴ� Ž���ϴ� ��ſ��� �����մϴ�. �غ�Ǽ̳���?"; }
        else if (country == "Japan") { idx = 2; JapanPanel.SetActive(true); docentText.text = "�Ϻ��� �����߽��ϴ�! �� ����� ����� ���밡 ��ȭ�Ӱ� ��췯�� ������, ���� ����� ÷�� ����� �����ϴ� Ư���� �ŷ��� ������ �ֽ��ϴ�. ������ ������� �Ż�� ������ ȭ���� ��ī�̶���, �׸��� �Ϻ� �������� �������� ��Ư�� ��ȭ�� ǳ���� ����� ��ٸ��� �ֽ��ϴ�."; }

        print(idx);
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
    }

    public void SelectCity(string city)
    {
        cityText.text = city;
        if      (city == "NewYork")         { idx = 3; NYCPanel.SetActive(true); docentText.text = "���� ���忡 �����߽��ϴ�! ������ �߽��̶�� �Ҹ��� �� ���ô� ���� ������ ���Ӿ��� �������� Ư¡�Դϴ�. ������ ���Ż�� Ÿ�ӽ�����, �׸��� ��Ʈ����ũ����, ������ ������ ��̷ο� ���Դϴ�."; }
        else if (city == "San Francisco")   { idx = 4; SFPanel.SetActive(true); docentText.text = "�������ý��ڿ� �����߽��ϴ�! ����� �ٴ�, �׸��� ��¡���� �ݹ����� ����� �ݰ��ݴϴ�. ��Ư�� ��ȭ�� �پ��� ������� ��췯���� �� ���ô� Ž���� ��ġ�� ��Ĩ�ϴ�. ���� �ٶ��� ������ ���ø� �ѷ��� �غ� �Ǽ̳���?"; }
        else if (city == "Las Vegas")       { idx = 5; LVPanel.SetActive(true); docentText.text = "�󽺺������Դϴ�! ȭ���� �Һ��� ������ �������θ�Ʈ�� �������� �� ���ÿ����� �� ������ ��̷ӽ��ϴ�. ī����� ��, �׸��� �������� �м� ���� ���� ��ҵ��� �츮�� ��ٸ��� �ֳ׿�. �׷� �󽺺������� ��ܺ����?"; }
        USAPanel.SetActive(false);

        if      (city == "Rome")   { idx = 6;  RomePanel.SetActive(true); docentText.text = "�θ��� �̵��մϴ�! ����� ������ ��� �� ���ô� �� ���迡�� ���� ���� ���� ����� ��ȭ�� �ڶ��մϴ�. �ݷμ���, ���׿�, �׸��� �� ����� �뼺�����, �̰����� ������ �ð� ������ ����� ������. �θ��� �Ÿ��� ������ �� ���縦 ���������?"; }
        else if (city == "Venice") { idx = 7; VenicePanel.SetActive(true); docentText.text = "����ġ�ƿ� �����߽��ϴ�! �� ���� �� �ִ� �� ���ô� ����Ͽ� �ﵹ��� �����ϸ�, �� ��Ư�� �ŷ��� �������Գ� ���� �� ���� ������ �����մϴ�. ������ �ٸ��� �� ������ ������ ������, ����ġ���� ������ ������� ������."; }
        ItalyPanel.SetActive(false);

        if      (city == "Kyoto") { idx = 8; KyotoPanel.SetActive(true); docentText.text = "����� �Խ��ϴ�! �Ϻ��� ����� ���簡 ��� �� ���� �̰������� ������� ������ �Ż簡 ���� ������ �ڸ��ϰ� �ֽ��ϴ�. �ݰ���� �Ľù� �̳��� �Ż�, �׸��� �볪�� �� ���� ������ ������� ������ �� �ִ� �̰��� ���� �� ���� ������ �����մϴ�."; }
        else if (city == "Tokyo") { idx = 9; TokyoPanel.SetActive(true); docentText.text = "���쿡 �����߽��ϴ�! ����� ������ �����ϴ� �� ���ô� ������ �������� ���� ǳ������ �����մϴ�. ���� Ÿ���� ��ī��Ʈ������ �ٶ󺸴� ���� ������ �������� �Ƹ�����. �� Ȱ���� ���ÿ��� ���ο� ������ ������ ���ô�."; }
        JapanPanel.SetActive(false);

        print(idx);
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
    }

    public void SelectMonument(string monument)
    {
        print(monument);
        if      (monument == "������ ���Ż�") { idx = 10; docentText.text = "������ ��¡�� ������ ���Ż� �����߽��ϴ�. �̹����� ���� ȯ�濡�� ���� �����ϰ� ������ �����?"; }
        else if (monument == "Ÿ�� ������") { idx = 11; docentText.text = "�츮�� ������ ȭ���� Ÿ�ӽ���� �Խ��ϴ�. ���� ȯ�濡�� �� ��ä�Ӱ� ü���� �����?"; }
        else if (monument == "��Ʈ�� ��ũ") { idx = 12; docentText.text = "������ ���ƽý�, ��Ʈ����ũ�� �����߾��. ���� ���� ȯ�濡�� �ڿ��� ������ ���ô�."; }
        else if (monument == "���Ŭ�� �긴��") { idx = 13; docentText.text = "�츮�� ������ �������� ���Ŭ�� �긴���� �����߽��ϴ�. �̹����� ���� ȯ�濡�� �ٸ� ���� �ɾ���?"; }

        else if (monument == "��� ����Ʈ �긴��") { idx = 14; docentText.text = "�������ý����� ������, �ݹ����� �Խ��ϴ�. ���� ȯ�濡�� ���� �ǰ� ���� �ѷ������?"; }
        else if (monument == "�丮 ����") { idx = 15; docentText.text = "�츮�� �������ý����� �丮 ������ �����߽��ϴ�. �̹����� ���� ȯ�濡�� �� �������� �ǹ��� Ȱ���� ������ �ѷ������?"; }
        else if (monument == "�������ý��� �غ�") { idx = 16; docentText.text = "�������ý����� �Ƹ��ٿ� �غ��� �����߾��. ��ư�� ���� �غ����� ���ѳ��̸� ��ܺ��ô�."; }

        else if (monument == "�� ���Ǿ�") { idx = 17; docentText.text = "�󽺺������� �ֽ� ���, The Sphere�� �����߽��ϴ�. ���� ȯ�濡�� �� Ư������ ������ �����?"; }
        else if (monument == "�������� �м�") { idx = 18; docentText.text = "�󽺺������� ���� �������� �м��� �����߾��. ���� ȯ�濡�� ǳ���� ������ �����?"; }
        else if (monument == "������ �縮��") { idx = 19; docentText.text = "�󽺺������� ��¡���� ������ �Ӹ����� �Խ��ϴ�. ���� ȯ�濡�� �� �������� ü���� ���ô�."; }
        else if (monument == "����ġ�� ȣ��") { idx = 20; docentText.text = "�츮�� �󽺺������� ����ġ�� ȣ�ڿ� �����߽��ϴ�. �̹����� ���� ȯ�濡�� ��Ż������ �����⸦ ���������?"; }

        else if (monument == "���׿� ����") { idx = 21; docentText.text = "�θ��� �������� ���׿¿� �Խ��ϴ�. ���� ȯ�濡�� �� �ź� Ž���� �����?"; }
        else if (monument == "�ݷμ���") { idx = 22; docentText.text = "�츮�� �θ��� ��¡, �ݷμ��� �����߽��ϴ�. �̹����� ���� ȯ�濡�� ����� ������ ���������?"; }
        else if (monument == "�� ����� �뼺��") { idx = 23; docentText.text = "�θ��� ������ �� ����� �뼺�翡 �Ծ��. ���� ȯ�濡�� �� �Ƹ��ٿ��� ������ ���ô�."; }

        else if (monument == "�� ������ �뼺��") { idx = 24; docentText.text = "����ġ���� ����, �� ������ �뼺�翡 �����߽��ϴ�. ���� ���� ȯ�濡�� ���θ� ���캼���?"; }
        else if (monument == "������ �ٸ�") { idx = 25; docentText.text = "�츮�� ����ġ���� ��¡���� ������ �ٸ��� �Խ��ϴ�. ���� ȯ�濡�� ������ ǳ���� ��ܺ����?"; }
        else if (monument == "���Ͻ� �����") { idx = 26; docentText.text = "����ġ���� ����Ͽ� �����߾��. �̹����� ���� ȯ�濡�� �� �Ƹ��ٿ��� ������ ���ô�."; }

        else if (monument == "�ݰ���") { idx = 27; docentText.text = "������ ����� �ݰ��翡 �Խ��ϴ�. ���� ȯ�濡�� �� Ȳ�ݺ��� ���������?"; }
        else if (monument == "�߻�ī�� ž") { idx = 28; docentText.text = "������ �߻�ī�� ž�� �Ծ��. ���� ȯ�濡�� ������ �Ƹ��ٿ��� ������ ���ô�."; }
        else if (monument == "ŰŸ�� �ٸ���") { idx = 29; docentText.text = "�츮�� ������ ŰŸ�� �ٸ����� �����߽��ϴ�. �̹����� ���� ȯ�濡�� �� �Ż��� �Ƹ��ٿ� ������ ���縦 �Բ� ���������?"; }

        else if (monument == "����Ÿ��") { idx = 30; docentText.text = "������ ��¡�� ���� Ÿ���� �Խ��ϴ�. ���� ȯ�濡�� ������ ������ ��ܺ����?"; }
        else if (monument == "���� ��ī�� Ʈ��") { idx = 31; docentText.text = "�츮�� ������ ���帶ũ, ���� ��ī��Ʈ���� �����߽��ϴ�. �̹����� ������ ȯ�濡�� �ϴ÷� �ö� ���ô�."; }
        else if (monument == "����� ����") { idx = 32; docentText.text = "�츮�� ���� ����Ϸ��忡 �����߽��ϴ�. �̹����� ���� ȯ�濡 �� ���� ���� �׸���ũ �ӿ��� ȯ������ ü���� �����غ����?"; }

        print(idx);
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
        GetDocent(monument);
        GetAudioDocent();
    }

    public void GetDocent(string target)
    {
        jsonData = "{\"text\":\"" + target + "\"}";
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
            docentText.text = responseData.docent;
        }
    }

    public void GetAudioDocent()
    {
        StartCoroutine(GetAudioDocentFromAI());
    }

    IEnumerator GetAudioDocentFromAI()
    {
        string jsonData = "{\"path\":\"./output.wav\"}";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip( url + "audio", AudioType.WAV))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                DownloadHandlerAudioClip downloadHandler = www.downloadHandler as DownloadHandlerAudioClip;
                PlayAudioClip(downloadHandler.audioClip);
            }
        }
    }

    void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void OnClickCountryDepth()
    {
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

        idx = -1;
    }

    public void OnClickCityDepth()
    {
        if (cityText.text != " - ")
        {
            CountryPanel.SetActive(false);
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

            if (cityText.text == "NewYork" || cityText.text == "San Francisco" || cityText.text == "Las Vegas")
            {
                USAPanel.SetActive(true);
            }
            else if(cityText.text == "Rome" || cityText.text == "Venice")
            {
                ItalyPanel.SetActive(true);
            }else if (cityText.text == "Kyoto" || cityText.text == "Tokyo")
            {
                JapanPanel.SetActive(true);
            }
        }

        // idx �� ���� text ox   
    }

    

}
