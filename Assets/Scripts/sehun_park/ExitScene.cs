using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ExitScene : MonoBehaviour
{
    private PhotonView photonView;

    void Awake()
    {
        // PhotonView 컴포넌트를 찾고 초기화
        photonView = GetComponent<PhotonView>();

        // photonView가 null인지 확인
        if (photonView == null)
        {
            Debug.LogError("PhotonView 컴포넌트가 이 게임 오브젝트에 없습니다.");
        }
    }

    void Update()
    {
        // B 키가 눌렸는지 확인
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsMasterClient)
        {
            // 커서 고정 해제 및 커서 표시
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // 모든 플레이어에게 씬 변경 요청
            if (photonView != null)
            {
                photonView.RPC("ChangeScene", RpcTarget.All, "CesiumGoogleMapsTiles_Beta_Sehun");
            }
        }
    }

    [PunRPC]
    void ChangeScene(string sceneName)
    {
        // 씬 로드
        PhotonNetwork.LoadLevel(sceneName);
    }
}