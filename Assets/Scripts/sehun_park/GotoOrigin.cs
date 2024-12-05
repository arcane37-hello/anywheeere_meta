using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoOrigin : MonoBehaviourPun
{
    void Update()
    {
        // Esc 키를 눌렀을 때 원래 씬으로 돌아감
        if (Input.GetKeyDown(KeyCode.Escape) && photonView.IsMine)
        {
            PhotonNetwork.LoadLevel("CesiumGoogleMapsTiles_Beta_X");  // 원래 씬으로 이동
        }
    }
}