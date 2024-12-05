using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionMgr_AlphaSH : MonoBehaviourPunCallbacks
{
    // InputNickName
    public TMP_InputField inputNickName;

    // BtnConnect
    public Button btnConnect;

    void Start()
    {
        // inputNickName�� ������ ����� �� ȣ��Ǵ� �Լ� ���
        inputNickName.onValueChanged.AddListener(OnValueChanged);
    }

    void Update()
    {

    }

    void OnValueChanged(string s)
    {
        btnConnect.interactable = s.Length > 0;

        //// ���࿡ s�� ���̰� 0���� ũ��
        //if (s.Length > 0)
        //{
        //    // ���� ��ư�� Ȱ��ȭ
        //    btnConnect.interactable = true;

        //}
        //// �׷��� ������(s�� ���̰� 0)
        //else
        //{
        //    // ���� ��ư�� ��Ȱ��ȭ
        //    btnConnect.interactable = false;
        //}
    }

    public void OnClickConnect()
    {
        // ������ ������ ���� �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    // ������ ������ ���� �����ϸ� ȣ��Ǵ� �Լ� // MonoBehaviourPunCallbacks ����� �ʿ��ϴ�.
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        // �г��� ����
        PhotonNetwork.NickName = inputNickName.text;
        // �κ������ ��ȯ
        PhotonNetwork.LoadLevel("LobbyScene_Alpha_SH");
    }
}
