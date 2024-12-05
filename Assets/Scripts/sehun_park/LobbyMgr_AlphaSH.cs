using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMgr_AlphaSH : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    // Input Room Name
    public TMP_InputField inputRoomName;
    // Input Max Player
    public TMP_InputField inputMaxPlayer;
    // Create Button
    public Button btnCreate;
    // Join Button
    public Button btnJoin;

    Dictionary<string, RoomInfo> allRoomInfo = new Dictionary<string, RoomInfo>();
    void Start()
    {
        // �κ� ����
        PhotonNetwork.JoinLobby();

        // inputRoomName�� ������ ����� �� ȣ��Ǵ� �Լ� ���
        inputRoomName.onValueChanged.AddListener(OnValueChangedRoomName);
        // inputMaxPlayer�� ������ ����� �� ȣ��Ǵ� �Լ� ���
        inputMaxPlayer.onValueChanged.AddListener(OnValueChangedMaxPlayer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Join & Create ��ư�� Ȱ��ȭ / ��Ȱ��ȭ
    void OnValueChangedRoomName(string roomName)
    {
        // join ��ư Ȱ��ȭ / ��Ȱ����
        btnJoin.interactable = roomName.Length > 0;

        // create ��ư Ȱ��ȭ / ��Ȱ��ȭ
        btnCreate.interactable = roomName.Length > 0 && inputMaxPlayer.text.Length > 0;

    }
    // Create ��ư�� Ȱ��ȭ / ��Ȱ��ȭ
    void OnValueChangedMaxPlayer(string maxPlayer)
    {
        btnCreate.interactable = maxPlayer.Length > 0 && inputRoomName.text.Length > 0;
    }

    public void CreateRoom()
    {
        // �� �ɼ� ����
        RoomOptions option = new RoomOptions();
        // �ִ� �ο� ����
        option.MaxPlayers = int.Parse(inputMaxPlayer.text);
        // �� �ɼ��� ������� ���� ����
        PhotonNetwork.CreateRoom(inputRoomName.text, option);

    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("�� ���� �Ϸ�");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("�� ���� ���� : " + message);
    }

    public void JoinRoom()
    {
        // �� ���� ��û
        PhotonNetwork.JoinRoom(inputRoomName.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("�� ���� �Ϸ�");
        PhotonNetwork.LoadLevel("CesiumGoogleMapsTiles_Alpha_SH");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("�� ���� ����: " + message);
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("�κ� ���� ����!");
    }

    // �κ� ���� �� �濡 ���� �������� ����Ǹ� ȣ��Ǵ� �Լ�
    // roomList : ��ü �� ���X, ����� �� ����
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        // �� ��� UI�� ��ü ����
        RemoveRoomList();

        // ��ü �� ������ ����
        UpdateRoomList(roomList);

        // allRoomInfo�� ������� ���� UI�� ������
        CreateRoomList();

        //for(int i = 0; i < roomList.Count; i++)
        //{
        //    print(roomList[i].Name + "," + roomList[i].PlayerCount + ", " + roomList[i].RemovedFromList);
        //}
    }

    void UpdateRoomList(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            // allRoomInfo�� roomList�� i��° ������ �ִ�?
            // (=
            if (allRoomInfo.ContainsKey(roomList[i].Name))
            {
                // allRoomInfo ���� or ����
                // ������ ���̴�?
                if (roomList[i].RemovedFromList == true)
                {
                    allRoomInfo.Remove(roomList[i].Name);
                }
                else
                {
                    allRoomInfo[roomList[i].Name] = roomList[i];
                }

            }
            else
            {
                // allRooomInfo �߰�
                allRoomInfo[roomList[i].Name] = roomList[i];
            }
        }
    }

    // RoomItem�� Prefab
    public GameObject roomItemFactory;

    // ScrollView�� Content transform
    public RectTransform trContent;
    void CreateRoomList()
    {
        foreach (RoomInfo info in allRoomInfo.Values)
        {
            // roomItem Prefab�� �̿��ؼ� roomItem�� �����
            GameObject go = Instantiate(roomItemFactory, trContent);
            Debug.Log("GameObject go = Instantiate(roomItemFactory, trContent);�� �����");
            // ������� roomItem�� ������ ����
            // Text ������Ʈ ��������
            Text roomItem = go.GetComponentInChildren<Text>();
            // ������ ������Ʈ�� ������ �Է�
            roomItem.text = info.Name + " ( " + info.PlayerCount + " / " + info.MaxPlayers + " )";
        }
    }

    void RemoveRoomList()
    {
        for (int i = 0; i < trContent.childCount; i++)
        {
            Destroy(trContent.GetChild(i).gameObject);
        }
    }
}