using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionMgr_JK : MonoBehaviourPunCallbacks
{
    public static string savedNickname;  // �Էµ� �г����� ������ ����

    // InputNickName
    public TMP_InputField inputNickName;

    // BtnConnect
    public Button btnConnect;

    void Start()
    {
        // inputNickName�� ������ ����� �� ȣ��Ǵ� �Լ� ���
        inputNickName.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(string s)
    {
        btnConnect.interactable = s.Length > 0;
    }

    public void OnClickConnect()
    {
        // ������ ������ ���� �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        // �г��� ����
        PhotonNetwork.NickName = inputNickName.text;

        // �г��� ���� (�ٸ� ������ ����ϱ� ����)
        savedNickname = inputNickName.text;

        // �κ������ ��ȯ
        PhotonNetwork.LoadLevel("LobbyScene_Beta");
    }
}