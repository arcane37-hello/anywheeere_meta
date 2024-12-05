using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoWorld : MonoBehaviourPun
{
    private PhotonView photonView1;

    void Awake()
    {
        // PhotonView 컴포넌트를 찾고 초기화
        photonView1 = GetComponent<PhotonView>();

        if (photonView == null)
        {
            Debug.LogError("PhotonView 컴포넌트가 이 게임 오브젝트에 없습니다.");
        }
    }

    void Update()
    {
        // K 키를 눌렀을 때 모든 플레이어를 Kyoto_Beta 씬으로 이동
        if (Input.GetKeyDown(KeyCode.K) && PhotonNetwork.IsMasterClient)
        {
            if (photonView != null)
            {
                photonView.RPC("ChangeScene", RpcTarget.All, "Kyoto_Beta");
            }
        }

        // U 키를 눌렀을 때 모든 플레이어를 UnderWater_Beta 씬으로 이동
        if (Input.GetKeyDown(KeyCode.U) && PhotonNetwork.IsMasterClient)
        {
            if (photonView != null)
            {
                photonView.RPC("ChangeScene", RpcTarget.All, "UnderWater_Beta");
            }
        }

        // Esc 키를 눌렀을 때 모든 플레이어를 원래 씬으로 이동
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
        // 모든 플레이어에게 씬 변경
        PhotonNetwork.LoadLevel(sceneName);
    }
}