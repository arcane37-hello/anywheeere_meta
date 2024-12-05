using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    // ���� ����
    public int score = 0;

    // ���� ǥ���� �ؽ�Ʈ
    public Text scoreText;

    private void Awake()
    {
        // �̱��� �������� �ϳ��� �ν��Ͻ��� �����ϵ��� ����
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���� �߰� �Լ�
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("���� �߰���. ���� ����: " + score);  // ���� �߰� �α�
        UpdateScoreUI();
    }

    // ���� UI ������Ʈ �Լ�
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text =  score.ToString();
        }
        else
        {
            Debug.LogWarning("ScoreText�� �������� �ʾҽ��ϴ�.");
        }
    }
}