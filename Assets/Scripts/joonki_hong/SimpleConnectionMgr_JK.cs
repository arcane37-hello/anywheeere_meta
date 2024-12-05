using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SimpleConnectionMgr_JK : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photon ȯ�漳���� ������� ������ ������ ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        
    }

    // ������ ������ ������ �Ǹ� ȣ��Ǵ� �Լ�
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("������ ������ ����");

        // �κ� ����
        JoinLobby();
    }

    public void JoinLobby()
    {
        // �г��� ����
        PhotonNetwork.NickName = "User_" + Random.Range(1, 1000);
        // �⺻ Lobby ����
        PhotonNetwork.JoinLobby();
    }

    // �κ� ������ �����ϸ� ȣ��Ǵ� �Լ�
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("�κ� ���� ����");

        JoinOrCreateRoom();
    }

    // Room�� ��������. ���࿡ �ش� Room�� ������ Room�� ����ڴ�.
    public void JoinOrCreateRoom()
    {
        // �� ���� �ɼ�
        RoomOptions roomOption = new RoomOptions();
        // �濡 ��� �� �� �ִ� �ִ� �ο� ����
        roomOption.MaxPlayers = 20;
        // �κ� ���� ���̰� �� ���ΰ�?
        roomOption.IsVisible = true;
        // �濡 ���� �����Ѱ�?
        roomOption.IsOpen = true;

        // Room ���� or ����
        PhotonNetwork.JoinOrCreateRoom("earrrth", roomOption, TypedLobby.Default);
    }

    // �� ���� �������� �� ȣ��Ǵ� �Լ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("�� ���� �Ϸ�");
    }

    // �� ���� �������� �� ȣ��Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("�� ���� �Ϸ�");

        // ��Ƽ�÷��� ������ ��� �� �ִ� ����
        // GameScene���� �̵�
        PhotonNetwork.LoadLevel("LobbyScene");
        // PhotonNetwork.LoadLevel("MapTestScene");
    }
}
