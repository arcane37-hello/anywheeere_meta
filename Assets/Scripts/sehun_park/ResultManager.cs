using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ResultManager : MonoBehaviourPun
{
    public static ResultManager instance;

    public Text resultText;  // ����� ǥ���� �ؽ�Ʈ

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Ÿ�̸� ���� �� ��� ��� �Լ�
    public void ShowResults()
    {
        // ScoreManager���� ���� ��������
        int finalScore = ScoreManager.instance.score;
        Debug.Log("���� ����: " + finalScore);  // ����׷� ���� ���� Ȯ��

        // ��� �ؽ�Ʈ ������Ʈ
        resultText.text = "Game Over\nResult: " + finalScore + " points";

        // �ֿܼ��� ���
        Debug.Log("Result: " + finalScore);
    }
}