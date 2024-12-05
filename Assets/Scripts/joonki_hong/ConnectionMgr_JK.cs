using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionMgr_JK : MonoBehaviourPunCallbacks
{
    public static string savedNickname;  // 입력된 닉네임을 저장할 변수

    // InputNickName
    public TMP_InputField inputNickName;

    // BtnConnect
    public Button btnConnect;

    void Start()
    {
        // inputNickName의 내용이 변경될 때 호출되는 함수 등록
        inputNickName.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(string s)
    {
        btnConnect.interactable = s.Length > 0;
    }

    public void OnClickConnect()
    {
        // 마스터 서버에 접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        // 닉네임 설정
        PhotonNetwork.NickName = inputNickName.text;

        // 닉네임 저장 (다른 씬에서 기억하기 위함)
        savedNickname = inputNickName.text;

        // 로비씬으로 전환
        PhotonNetwork.LoadLevel("LobbyScene_Beta");
    }
}