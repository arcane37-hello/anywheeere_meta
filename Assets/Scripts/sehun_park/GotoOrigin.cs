using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoOrigin : MonoBehaviourPun
{
    void Update()
    {
        // Esc Ű�� ������ �� ���� ������ ���ư�
        if (Input.GetKeyDown(KeyCode.Escape) && photonView.IsMine)
        {
            PhotonNetwork.LoadLevel("CesiumGoogleMapsTiles_Beta_X");  // ���� ������ �̵�
        }
    }
}