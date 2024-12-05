using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GotoCityAdv : MonoBehaviour
{
    private PhotonView photonView;

    void Awake()
    {
        // PhotonView ������Ʈ�� ã�� �ʱ�ȭ
        photonView = GetComponent<PhotonView>();

        // photonView�� null���� Ȯ��
        if (photonView == null)
        {
            Debug.LogError("PhotonView ������Ʈ�� �� ���� ������Ʈ�� �����ϴ�.");
        }
    }

    void Update()
    {
        // B Ű�� ���ȴ��� Ȯ��
        if (Input.GetKeyDown(KeyCode.B) && PhotonNetwork.IsMasterClient)
        {
            // ��� �÷��̾�� �� ���� ��û
            if (photonView != null)
            {
                photonView.RPC("ChangeScene", RpcTarget.All, "03_CesiumSanFrancisco_Beta");
            }
        }
    }

    [PunRPC]
    void ChangeScene(string sceneName)
    {
        // �� �ε�
        PhotonNetwork.LoadLevel(sceneName);
    }
}