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
        // index에 따라...


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
        if      (country == "USA")   { idx = 0; USAPanel.SetActive(true); docentText.text = "자, 이제 미국에 도착했습니다! 이 나라는 그 광대한 대지와 다양한 문화로 전 세계에서 사랑받고 있습니다. 자유의 상징인 뉴욕, 혁신의 도시 샌프란시스코, 그리고 끝없는 엔터테인먼트가 펼쳐지는 라스베가스까지, 미국은 언제나 새로운 경험을 제공합니다. 다음 목적지는 어디가 될까요?";  }
        else if (country == "Italy") { idx = 1; ItalyPanel.SetActive(true); docentText.text = "우리는 이제 이탈리아에 도착했습니다! 고대 로마의 영광이 살아 숨 쉬는 이곳은 예술과 역사의 보고입니다. 로마의 거대한 유적들, 베네치아의 운하, 그리고 이탈리아 전역에서 펼쳐지는 아름다운 풍경과 음식까지, 이탈리아는 탐험하는 즐거움이 가득합니다. 준비되셨나요?"; }
        else if (country == "Japan") { idx = 2; JapanPanel.SetActive(true); docentText.text = "일본에 도착했습니다! 이 나라는 전통과 현대가 조화롭게 어우러진 곳으로, 깊은 역사와 첨단 기술이 공존하는 특별한 매력을 가지고 있습니다. 교토의 고즈넉한 신사와 도쿄의 화려한 스카이라인, 그리고 일본 전역에서 느껴지는 독특한 문화와 풍경이 당신을 기다리고 있습니다."; }

        print(idx);
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
    }

    public void SelectCity(string city)
    {
        cityText.text = city;
        if      (city == "NewYork")         { idx = 3; NYCPanel.SetActive(true); docentText.text = "이제 뉴욕에 도착했습니다! 세계의 중심이라고 불리는 이 도시는 높은 빌딩과 끊임없는 에너지가 특징입니다. 자유의 여신상과 타임스퀘어, 그리고 센트럴파크까지, 뉴욕은 언제나 흥미로운 곳입니다."; }
        else if (city == "San Francisco")   { idx = 4; SFPanel.SetActive(true); docentText.text = "샌프란시스코에 도착했습니다! 언덕과 바다, 그리고 상징적인 금문교가 당신을 반겨줍니다. 독특한 문화와 다양한 사람들이 어우러지는 이 도시는 탐험할 가치가 넘칩니다. 이제 바람을 맞으며 도시를 둘러볼 준비가 되셨나요?"; }
        else if (city == "Las Vegas")       { idx = 5; LVPanel.SetActive(true); docentText.text = "라스베가스입니다! 화려한 불빛과 끝없는 엔터테인먼트가 펼쳐지는 이 도시에서는 매 순간이 흥미롭습니다. 카지노와 쇼, 그리고 벨라지오 분수 같은 멋진 명소들이 우리를 기다리고 있네요. 그럼 라스베가스를 즐겨볼까요?"; }
        USAPanel.SetActive(false);

        if      (city == "Rome")   { idx = 6;  RomePanel.SetActive(true); docentText.text = "로마로 이동합니다! 고대의 영광이 깃든 이 도시는 전 세계에서 가장 유서 깊은 역사와 문화를 자랑합니다. 콜로세움, 판테온, 그리고 성 베드로 대성당까지, 이곳에서 느끼는 시간 여행을 기대해 보세요. 로마의 거리를 걸으며 그 역사를 느껴볼까요?"; }
        else if (city == "Venice") { idx = 7; VenicePanel.SetActive(true); docentText.text = "베네치아에 도착했습니다! 물 위에 떠 있는 이 도시는 대운하와 곤돌라로 유명하며, 그 독특한 매력은 누구에게나 잊을 수 없는 경험을 선사합니다. 리알토 다리와 산 마르코 광장을 지나며, 베네치아의 낭만에 빠져들어 보세요."; }
        ItalyPanel.SetActive(false);

        if      (city == "Kyoto") { idx = 8; KyotoPanel.SetActive(true); docentText.text = "교토로 왔습니다! 일본의 전통과 역사가 살아 숨 쉬는 이곳에서는 고즈넉한 사찰과 신사가 도시 곳곳에 자리하고 있습니다. 금각사와 후시미 이나리 신사, 그리고 대나무 숲 속을 걸으며 고요함을 느껴볼 수 있는 이곳은 잊을 수 없는 경험을 선사합니다."; }
        else if (city == "Tokyo") { idx = 9; TokyoPanel.SetActive(true); docentText.text = "도쿄에 도착했습니다! 현대와 전통이 공존하는 이 도시는 끝없는 에너지와 놀라운 풍경으로 가득합니다. 도쿄 타워와 스카이트리에서 바라보는 도시 전경은 숨막히게 아름답죠. 이 활기찬 도시에서 새로운 모험을 시작해 봅시다."; }
        JapanPanel.SetActive(false);

        print(idx);
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
    }

    public void SelectMonument(string monument)
    {
        print(monument);
        if      (monument == "자유의 여신상") { idx = 10; docentText.text = "뉴욕의 상징인 자유의 여신상에 도착했습니다. 이번에는 몰입 환경에서 더욱 생생하게 감상해 볼까요?"; }
        else if (monument == "타임 스퀘어") { idx = 11; docentText.text = "우리는 뉴욕의 화려한 타임스퀘어에 왔습니다. 몰입 환경에서 더 다채롭게 체험해 볼까요?"; }
        else if (monument == "센트럴 파크") { idx = 12; docentText.text = "뉴욕의 오아시스, 센트럴파크에 도착했어요. 이제 몰입 환경에서 자연을 만끽해 봅시다."; }
        else if (monument == "브루클린 브릿지") { idx = 13; docentText.text = "우리는 뉴욕의 역사적인 브루클린 브릿지에 도착했습니다. 이번에는 몰입 환경에서 다리 위를 걸어볼까요?"; }

        else if (monument == "골든 게이트 브릿지") { idx = 14; docentText.text = "샌프란시스코의 아이콘, 금문교에 왔습니다. 몰입 환경에서 더욱 실감 나게 둘러볼까요?"; }
        else if (monument == "페리 빌딩") { idx = 15; docentText.text = "우리는 샌프란시스코의 페리 빌딩에 도착했습니다. 이번에는 몰입 환경에서 이 역사적인 건물과 활기찬 시장을 둘러볼까요?"; }
        else if (monument == "샌프란시스코 해변") { idx = 16; docentText.text = "샌프란시스코의 아름다운 해변에 도착했어요. 버튼을 눌러 해변에서 물총놀이를 즐겨봅시다."; }

        else if (monument == "더 스피어") { idx = 17; docentText.text = "라스베가스의 최신 명소, The Sphere에 도착했습니다. 몰입 환경에서 그 특별함을 경험해 볼까요?"; }
        else if (monument == "벨라지오 분수") { idx = 18; docentText.text = "라스베가스의 명물인 벨라지오 분수에 도착했어요. 몰입 환경에서 풍경을 감상해 볼까요?"; }
        else if (monument == "시저스 펠리스") { idx = 19; docentText.text = "라스베가스의 상징적인 시저스 팰리스에 왔습니다. 몰입 환경에서 그 웅장함을 체험해 봅시다."; }
        else if (monument == "베네치안 호텔") { idx = 20; docentText.text = "우리는 라스베가스의 베네치안 호텔에 도착했습니다. 이번에는 몰입 환경에서 이탈리아의 분위기를 느껴볼까요?"; }

        else if (monument == "판테온 신전") { idx = 21; docentText.text = "로마의 역사적인 판테온에 왔습니다. 몰입 환경에서 그 신비를 탐험해 볼까요?"; }
        else if (monument == "콜로세움") { idx = 22; docentText.text = "우리는 로마의 상징, 콜로세움에 도착했습니다. 이번에는 몰입 환경에서 고대의 영광을 느껴볼까요?"; }
        else if (monument == "성 베드로 대성당") { idx = 23; docentText.text = "로마의 웅장한 성 베드로 대성당에 왔어요. 몰입 환경에서 그 아름다움을 감상해 봅시다."; }

        else if (monument == "산 마르코 대성당") { idx = 24; docentText.text = "베네치아의 보물, 산 마르코 대성당에 도착했습니다. 이제 몰입 환경에서 세부를 살펴볼까요?"; }
        else if (monument == "리알토 다리") { idx = 25; docentText.text = "우리는 베네치아의 상징적인 리알토 다리에 왔습니다. 몰입 환경에서 운하의 풍경을 즐겨볼까요?"; }
        else if (monument == "베니스 대운하") { idx = 26; docentText.text = "베네치아의 대운하에 도착했어요. 이번에는 몰입 환경에서 그 아름다움을 감상해 봅시다."; }

        else if (monument == "금각사") { idx = 27; docentText.text = "교토의 명소인 금각사에 왔습니다. 몰입 환경에서 그 황금빛을 느껴볼까요?"; }
        else if (monument == "야사카의 탑") { idx = 28; docentText.text = "교토의 야사카의 탑에 왔어요. 몰입 환경에서 전통의 아름다움을 감상해 봅시다."; }
        else if (monument == "키타노 텐만구") { idx = 29; docentText.text = "우리는 교토의 키타노 텐만구에 도착했습니다. 이번에는 몰입 환경에서 이 신사의 아름다운 정원과 역사를 함께 느껴볼까요?"; }

        else if (monument == "도쿄타워") { idx = 30; docentText.text = "도쿄의 상징인 도쿄 타워에 왔습니다. 몰입 환경에서 도시의 전망을 즐겨볼까요?"; }
        else if (monument == "도쿄 스카이 트리") { idx = 31; docentText.text = "우리는 도쿄의 랜드마크, 도쿄 스카이트리에 도착했습니다. 이번에는 몰입형 환경에서 하늘로 올라가 봅시다."; }
        else if (monument == "디즈니 랜드") { idx = 32; docentText.text = "우리는 도쿄 디즈니랜드에 도착했습니다. 이번에는 몰입 환경에 들어가 마법 같은 테마파크 속에서 환상적인 체험을 시작해볼까요?"; }

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

        // idx 에 따라서 text ox   
    }

    

}
