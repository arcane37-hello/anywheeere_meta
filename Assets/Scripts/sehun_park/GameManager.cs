using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Vector3 point;

    // �÷��̾� �г����� ǥ���� 3���� TextMeshPro UI ���
    public TMP_Text[] playerNickNameTexts = new TMP_Text[3];

    // �÷��̾���� PhotonVoiceView ������Ʈ
    public PhotonVoiceView[] playerVoiceViews = new PhotonVoiceView[3];

    void Start()
    {
        PhotonNetwork.Instantiate("Player1", point, Quaternion.identity);
        UpdatePlayerList(); // ���� ���� �� �÷��̾� ����Ʈ ������Ʈ
    }

    // ���ο� �÷��̾ �濡 ���� �� ȣ��Ǵ� �Լ�
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        UpdatePlayerList(); // ���ο� �÷��̾ ������ ����Ʈ ������Ʈ
    }

    // �÷��̾ ���� ������ �� ȣ��Ǵ� �Լ�
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        UpdatePlayerList(); // �÷��̾ ������ ����Ʈ ������Ʈ
    }

    // �÷��̾� ����Ʈ UI�� ������Ʈ�ϴ� �Լ�
    void UpdatePlayerList()
    {
        Player[] players = PhotonNetwork.PlayerList;

        // TextMeshPro UI ��Ҹ� ��ȯ�ϸ� �г��� �Ҵ�
        for (int i = 0; i < playerNickNameTexts.Length; i++)
        {
            if (i < players.Length)
            {
                // �� �÷��̾��� �г����� �ش� Text�� �Ҵ�
                playerNickNameTexts[i].text = players[i].NickName;

                // �÷��̾��� PhotonVoiceView ����
                playerVoiceViews[i] = players[i].TagObject as PhotonVoiceView;
            }
            else
            {
                // �� ������ "Waiting..."���� ǥ��
                playerNickNameTexts[i].text = "Waiting...";
            }
        }
    }
}
