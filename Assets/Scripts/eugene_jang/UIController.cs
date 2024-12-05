using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DocentManager;

// ui ������ ����
// ���� ���� > ���� ���� > ��ȭ�� ����
// 1. ���� ���ý� Audio ���
// 2. ���� ���ý� Audio ���
// 3. ��ȭ�� ���ý� Audio ���..
// ... �̰� ����Ʈ�� �Ǿ� ������ .. ���� �̸� �غ� ����.. 
// AI���� �ǽð� ������ ��� �� ���ΰ�? 
// �߰� ����� �ճ�.. ?
// �ϴ��� �غ� �غ���... 


[Serializable]
enum PlayStates
{
    map,
    region,
    landmark
}

public class UIController : MonoBehaviour
{
    public GameObject CesiumMap;
    public SkyboxManager skyboxManager;

    public string url;
    // ���õ� ���÷� �̵��Ѵ�.
    // ���ÿ� ����Ʈ �����͸� AI ���Լ� �޾ƿ´�.
    // �޾ƿ� �����͸� �����صд�. text / audio

    public string docent;

    public string audioAddress;
    public AudioClip docentAudio;

    string jsonData;

    public GameObject docentUI;
    public Text docentText;
    // UI�� Ȱ��ȭ �Ǿ������� ���� 

    // �ϴ� ���� > ���� > ��ȭ�� UI �����
    public GameObject mapUI;

    public GameObject countryPanel;
    public GameObject cityPanel;
    public GameObject monumentPanel;

    public Button[] countryButtons;
    public Button[] cityButtons;
    public Button[] monumentButtons;

    private string selectedCountry;
    private string selectedCity;

    private Dictionary<string, List<string>> countryToCities = new Dictionary<string, List<string>>()
    {
        { "USA", new List<string> { "New York", "San Francisco", "Las Vegas" } },
        { "Italy", new List<string> { "Rome", "Venice" } },
        { "Japan", new List<string> { "Tokyo", "Kyoto" } }
    };

    private Dictionary<string, List<string>> cityToMonuments = new Dictionary<string, List<string>>()
    {
        // USA
        { "New York", new List<string> { "Statue of Liberty", "Times Square", "Central Park", "Empire State Building", "Brooklyn Bridge" } },
        { "San Francisco", new List<string> { "Golden Gate Bridge", "Beach" } },
        { "Las Vegas", new List<string> { "The Sphere", "Las Vegas Strip", "Bellagio Fountains", "Caesars Palace", "Venetian Hotel" } },

        // Italy
        { "Rome", new List<string> { "Pantheon", "Colosseum", "St. Peter's Basilica" } },
        { "Venice", new List<string> { "St. Mark's Basilica", "Rialto Bridge", "Grand Canal" } },

        // Japan
        { "Tokyo", new List<string> { "Tokyo Tower", "Tokyo Skytree", "Asakusa" } },
        { "Kyoto", new List<string> { "Kinkaku-ji", "Fushimi Inari Shrine", "Yasaka Pagoda", "Arashiyama Bamboo Grove" } }
    };


    void Start()
    {
        // ������ UI�� Ȱ��ȭ
        // map mode ����
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("NYC");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(1);
            // �������� �̵� �� ���� 
            jsonData = "{\"text\":\"Italy\"}";

            GetDocent();
            
            //docentText.text = "�׽�Ʈ USA ����Ʈ ����";
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("Statue of Liberty");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(2);
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("Rome");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(4);
            jsonData = "{\"text\":\"�ݷμ���\"}";
            GetDocent();

            docentText.text = "�ݷμ��� ����Ʈ ����";
        }

        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("Colosseum");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(5);
        }

        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }


            print("Paris");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(7);
            jsonData = "{\"text\":\"����ž\"}";
            GetDocent();

            docentText.text = "����ž ����Ʈ ����";
        }

        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("Eiffel Tower");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(8);
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            if (docentUI.activeInHierarchy)
            {
                docentUI.SetActive(false);
            }
            else if (!docentUI.activeInHierarchy)
            {
                docentUI.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapUI.activeInHierarchy)
            {
                mapUI.SetActive(false);
            }
            else if (!mapUI.activeInHierarchy)
            {
                mapUI.SetActive(true);
                //ShowCountryPanel();
            }

        }

    }
    
    void SetupCountryButtons()
    {
        string[] countries = new string[] { "USA", "Italy", "Japan" };

        for (int i = 0; i < countryButtons.Length; i++)
        {
            if(i < countries.Length)
            {
                string country = countries[i];
                countryButtons[i].gameObject.SetActive(true);
                countryButtons[i].transform.GetChild(0).GetComponent<Text>().text = country;
                countryButtons[i].onClick.RemoveAllListeners();
                countryButtons[i].onClick.AddListener(() => ShowCityPanel(country));
            }
            else 
            {
                countryButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void ShowCountryPanel()
    {
        countryPanel.SetActive(true);
        cityPanel.SetActive(false);
        monumentPanel.SetActive(false);
    }

    public void ShowCityPanel(string country)
    {
        selectedCity = country;
        countryPanel.SetActive(false);
        cityPanel.SetActive(true);
        monumentPanel.SetActive(false);
        UpdateCityButtons();
    }

    public void ShowMonumentPanel(string city)
    {
        selectedCity = city;
        countryPanel.SetActive(false);
        cityPanel.SetActive(false);
        monumentPanel.SetActive(true);
        UpdateMonumentButtons();
    }

    void UpdateCityButtons()
    {
        List<string> cities = countryToCities[selectedCountry];

        for (int i = 0; i < cityButtons.Length; i++)
        {
            if(i < cities.Count)
            {
                string city = cities[i];
                cityButtons[i].gameObject.SetActive(true);
                print(i);
                cityButtons[i].transform.GetChild(0).GetComponent<Text>().text = city;

                cityButtons[i].onClick.RemoveAllListeners();
                cityButtons[i].onClick.AddListener(() => ShowMonumentPanel(city));
            }
            else
            {
                cityButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void UpdateMonumentButtons()
    {
        List<string> monuments = cityToMonuments[selectedCity];

        for (int i = 0; i < monumentButtons.Length; i ++)
        {
            if (i < monuments.Count)
            {
                string monument = monuments[i];
                monumentButtons[i].gameObject.SetActive(true);
                monumentButtons[i].GetComponentInChildren<Text>().text = monuments[i];

                monumentButtons[i].onClick.RemoveAllListeners();
                monumentButtons[i].onClick.AddListener(() => OnMonumentSelected(monument));
            }
            else
            {
                monumentButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnCountrySelected(string country)
    {
        selectedCountry = country;
        Debug.Log($"Selected Country : {country}");
        ShowCityPanel(country);
    }

    public void OnMonumentSelected(string monument)
    {
        Debug.Log($"Selected Monument: {monument}");
    }



    // ������� �Լ��� ��

    #region ������� ��� �Լ���
    public void GetDocent()
    {
        StartCoroutine(GetDocentFromAI(url, jsonData));
    }

    // �̵��ϴ� ���� ������ �����ϰ� docent �ؽ�Ʈ�� audio �ּҸ� �޾� ��
    IEnumerator GetDocentFromAI(string url, string jsonData)
    {   
        using (UnityWebRequest www = UnityWebRequest.Get(url+"docent"))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            DocentResponse responseData = JsonUtility.FromJson<DocentResponse>(www.downloadHandler.text);

            print(responseData.audio);
            docentText.text = responseData.docent;
            //�޾ƿ� �ؽ�Ʈ(json���� ��) ����Ʈ �κ��� string ������ ���� �� ��

            // �޾ƿ� json���� docent �κа� audio �ּ� �κ��� �и��� �־� ��
            // ����� ����Ʈ �޴� �Լ� ����
            // docentAudio�� �޾ƿ� AudioClip ���� 
        }
    }

    // docent audio �� �޾Ƽ� AudioClip�� ���� �ϴ� �Լ�
    IEnumerator GetDecentAudioFromAI(string url)
    {
        jsonData = "{\"path\":\"./output.wav\"}";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            DownloadHandlerAudioClip downloadHandler = www.downloadHandler as DownloadHandlerAudioClip;
            docentAudio = downloadHandler.audioClip;

            // docentAudio�� ���� �ؾ� �� �� ���� �Ѵ�.(��� �־� ������~?)
        }
    }


    // ����ȯ�濡�� �޾ƿ� docent�� UI�� ���� ��Ų��.( Audio�� ���� ���� // ����� ���� ��ư�� ?)
    void ViewDocent()
    {

    }


    #endregion




}
