using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoWorld : MonoBehaviourPun
{
    private PhotonView photonView1;

    void Awake()
    {
        // PhotonView ������Ʈ�� ã�� �ʱ�ȭ
        photonView1 = GetComponent<PhotonView>();

        if (photonView == null)
        {
            Debug.LogError("PhotonView ������Ʈ�� �� ���� ������Ʈ�� �����ϴ�.");
        }
    }

    void Update()
    {
        // K Ű�� ������ �� ��� �÷��̾ Kyoto_Beta ������ �̵�
        if (Input.GetKeyDown(KeyCode.K) && PhotonNetwork.IsMasterClient)
        {
            if (photonView != null)
            {
                photonView.RPC("ChangeScene", RpcTarget.All, "Kyoto_Beta");
            }
        }

        // U Ű�� ������ �� ��� �÷��̾ UnderWater_Beta ������ �̵�
        if (Input.GetKeyDown(KeyCode.U) && PhotonNetwork.IsMasterClient)
        {
            if (photonView != null)
            {
                photonView.RPC("ChangeScene", RpcTarget.All, "UnderWater_Beta");
            }
        }

        // Esc Ű�� ������ �� ��� �÷��̾ ���� ������ �̵�
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsMasterClient)
        {
            if (photonView != null)
            {
                photonView.RPC("ChangeScene", RpcTarget.All, "CesiumGoogleMapsTiles_Beta_Final_X");
            }
        }
    }

    [PunRPC]
    void ChangeScene(string sceneName)
    {
        // ��� �÷��̾�� �� ����
        PhotonNetwork.LoadLevel(sceneName);
    }
}