using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CityAdvRule : MonoBehaviourPun
{
    // Ÿ�̸� �ð� (1��)
    private int minutes = 1;
    private int seconds = 0;

    // Ÿ�̸� UI ����� Text
    public Text timerText;
    public Text countdownText; // ī��Ʈ�ٿ� �ؽ�Ʈ

    public GameObject showImage;
    public GameObject timeOverImage;

    // Ÿ�̸Ӱ� ���� ������ ����
    private bool timerRunning = false;

    private void Start()
    {
        if (showImage != null)
        {
            showImage.SetActive(false);
        }

        if (timeOverImage != null)
        {
            timeOverImage.SetActive(false);
        }
        // ������ Ŭ���̾�Ʈ������ ī��Ʈ�ٿ� ����
        if (PhotonNetwork.IsMasterClient)
        {
            // 5�� ī��Ʈ�ٿ� �� Ÿ�̸� ����
            photonView.RPC(nameof(StartCountdownRPC), RpcTarget.AllBuffered);
        }
    }

    // ī��Ʈ�ٿ� ������ ���� RPC ȣ��
    [PunRPC]
    private void StartCountdownRPC()
    {
        StartCoroutine(StartCountdown());
    }

    // 5�� ī��Ʈ�ٿ� �ڷ�ƾ
    private IEnumerator StartCountdown()
    {
        int countdown = 5;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString(); // ī��Ʈ�ٿ� �ؽ�Ʈ ������Ʈ
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = ""; // ī��Ʈ�ٿ��� ������ �ؽ�Ʈ �����

        // ī��Ʈ�ٿ��� ������ �� �̹����� �����ֱ� ���� RPC ȣ��
        photonView.RPC(nameof(ShowImageRPC), RpcTarget.AllBuffered);

        StartTimer(); // Ÿ�̸� ����
    }

    [PunRPC]
    private void ShowImageRPC()
    {
        StartCoroutine(ShowImage());
    }

    private IEnumerator ShowImage()
    {
        if (showImage != null)
        {
            showImage.SetActive(true); // �̹��� Ȱ��ȭ
            yield return new WaitForSeconds(1f);
            showImage.SetActive(false); // 1�� �� �̹��� ��Ȱ��ȭ
        }
    }

    // Ÿ�̸� ������ ���� RPC ȣ��
    private void StartTimer()
    {
        photonView.RPC(nameof(StartTimerRPC), RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void StartTimerRPC()
    {
        if (!timerRunning)
        {
            timerRunning = true;
            InvokeRepeating(nameof(UpdateTimer), 1.0f, 1.0f);
        }
    }

    // �� �ʸ��� Ÿ�̸Ӹ� ������Ʈ
    private void UpdateTimer()
    {
        if (!timerRunning) return;

        if (seconds == 0)
        {
            if (minutes == 0)
            {
                timerRunning = false;
                CancelInvoke(nameof(UpdateTimer));
                TimeUp();
                return;
            }
            else
            {
                minutes--;
                seconds = 59;
            }
        }
        else
        {
            seconds--;
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    private void TimeUp()
    {
        Debug.Log("�ð� ����");

        // Ÿ�� ���� �̹����� �����ֱ� ���� RPC ȣ��
        photonView.RPC(nameof(ShowTimeOverImageRPC), RpcTarget.AllBuffered);

        ResultManager.instance.ShowResults();  // ��� ���
    }

    // Ÿ�� ���� �̹����� �����ֱ� ���� RPC
    [PunRPC]
    private void ShowTimeOverImageRPC()
    {
        StartCoroutine(ShowTimeOverImage());
    }

    // 2�� ���� Ÿ�� ���� �̹����� �����ִ� �ڷ�ƾ
    private IEnumerator ShowTimeOverImage()
    {
        if (timeOverImage != null)
        {
            timeOverImage.SetActive(true); // Ÿ�� ���� �̹��� Ȱ��ȭ
            yield return new WaitForSeconds(2f);
            timeOverImage.SetActive(false); // 2�� �� Ÿ�� ���� �̹��� ��Ȱ��ȭ
        }
    }
}