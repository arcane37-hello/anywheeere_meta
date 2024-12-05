using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Manager : MonoBehaviourPunCallbacks
{
    public static Game2Manager instance;

    // Spawn ��ġ�� ��Ƴ��� ����
    public Vector3[] spawnPos;
    public Transform spawnCenter;

    // ��� Player �� PhotonView ������ �ִ� ����
    public PhotonView[] allPlayer;

    // GameScene ���� �Ѿ�� Player �� ����
    int enterPlayerCnt;

    // ���� ���� �� �� �ִ� player Idx
    int turnIdx = -1;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        SetSpawnPos();

        // RPC ������ �� ����
        PhotonNetwork.SendRate = 60;
        // OnPhotonSerializeView ������ �ް� �ϴ� �� ����
        PhotonNetwork.SerializationRate = 60;

        // ���� ��ġ�ؾ� �ϴ� idx �� �˾ƿ���. (���� ���� ���� �ִ� �ο� �� )
        int idx = ProjectMgr.Get().orderInRoom;

        // �÷��̾ ���� (���� Room �� ���� �Ǿ��ִ� ģ���鵵 ���̰�)
        PhotonNetwork.Instantiate("Player3", spawnPos[idx], Quaternion.identity);

        // ��� �÷��̾� ���� ���� ���� �Ҵ�
        allPlayer = new PhotonView[PhotonNetwork.CurrentRoom.MaxPlayers];

        // �� �̻� �̹濡 ������ ���� ���ϰ� ����.

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // ���� ������
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        // ���忡 ���ؼ� Scene ��ȯ�Ǵ� �ɼ� ��Ȱ��
        PhotonNetwork.AutomaticallySyncScene = false;
        // ���콺 Ŀ�� Lock ��� Ǯ��
        Cursor.lockState = CursorLockMode.None;

        // �ڵ����� Master Server �� ���� �õ�
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        // LobbyScene ���� ��ȯ
        PhotonNetwork.LoadLevel("LobbyScene");
    }


    void SetSpawnPos()
    {
        // maxPlayer �� ���� ���� �ִ� �ο����� ����
        int maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers;

        // �ִ� �ο� ��ŭ spawnPos �� ������ �Ҵ�
        spawnPos = new Vector3[maxPlayer];

        // spawnPos ���� ����(����)
        float angle = 360.0f / maxPlayer;

        // maxPlayer ��ŭ �ݺ�
        for (int i = 0; i < maxPlayer; i++)
        {
            // spawnCenter ȸ�� (i * angle) ��ŭ
            spawnCenter.eulerAngles = new Vector3(0, i * angle, 0);
            // spawnCenter �չ������� 2��ŭ ������ ��ġ ������.
            spawnPos[i] = spawnCenter.position + spawnCenter.forward * 2;
            //// ť�� �ϳ� ����
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //// ������ ť�긦 ������ ���� ��ġ�� ����.
            //cube.transform.position = spawnPos[i];
        }
    }

    public void AddPlayer(PhotonView pv, int order)
    {
        enterPlayerCnt++;

        allPlayer[order] = pv;

        // ���࿡ ��� Player �� �� ���Դٸ�
        if (enterPlayerCnt == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            // �����϶���
            if (PhotonNetwork.IsMasterClient)
            {
                // �� ����!
                ChangeTurn();
            }
        }
    }

    // ����� �� �Ѱ���
    public void ChangeTurn()
    {
        photonView.RPC(nameof(RpcChangeTurn), RpcTarget.MasterClient);

    }

    // ���忡�� ȣ��ȴ�.
    [PunRPC]
    void RpcChangeTurn()
    {
        // turnIdx �� �ִ��ο� ������ �۰� ������.
        turnIdx = ++turnIdx % allPlayer.Length;

        print("���� �� : " + turnIdx);

        PlayerFire pf = allPlayer[turnIdx].GetComponent<PlayerFire>();
        pf.ChangeTurn(true);
    }
}
