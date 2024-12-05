using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVideoMgr : MonoBehaviourPunCallbacks
{
    // 1st, 2nd, 3rd �÷��̾��� RawImage (�� �÷��̾��� ���� ǥ��)
    //public RawImage firstPlayerVideo;
    //public RawImage secondPlayerVideo;
    //public RawImage thirdPlayerVideo;

    public RawImage [] playerVideo;

    // �÷��̾���� PhotonVoiceView ������Ʈ
    public PhotonVoiceView[] playerVoiceViews = new PhotonVoiceView[3];

    public void UpdatePlayerVideo(int idx, bool isEnable)
    {
        playerVideo[idx].enabled = isEnable;
    }

    void Start()
    {
        // ó������ ��� ���� ��Ȱ��ȭ
        //firstPlayerVideo.enabled = false;
        //secondPlayerVideo.enabled = false;
        //thirdPlayerVideo.enabled = false;

        // �ڽ��� ���� �÷��̾��� PhotonVoiceView �Ҵ�
        AssignVoiceViewForPlayer(PhotonNetwork.LocalPlayer);

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {

       // // �� �÷��̾ ���ϰ� �ִ��� Ȯ���Ͽ� ���� Ȱ��ȭ/��Ȱ��ȭ
       // if (playerVoiceViews[0] != null && playerVoiceViews[0].IsSpeaking)
       // {
       //     firstPlayerVideo.enabled = true;
       // }
       // else
       // {
       //     firstPlayerVideo.enabled = false;
       // }

       //// Debug.LogError(playerVoiceViews[1].IsSpeaking);
       //// Debug.LogError(playerVoiceViews[1].IsRecording);

       // if (playerVoiceViews[1] != null && playerVoiceViews[1].IsSpeaking)
       // {
       //     secondPlayerVideo.enabled = true;
       //     //Debug.Log(playerVoiceViews[1]);
       //     //Debug.Log(playerVoiceViews[1].IsSpeaking);

       // }
       // else
       // {
       //     secondPlayerVideo.enabled = false;
       //     //Debug.Log(playerVoiceViews[1]);
       //     //Debug.Log(playerVoiceViews[1].IsSpeaking);
       // }

       // if (playerVoiceViews[2] != null && playerVoiceViews[2].IsSpeaking)
       // {
       //     thirdPlayerVideo.enabled = true;

       // }
       // else
       // {
       //     thirdPlayerVideo.enabled = false;
       // }
    }

    // �÷��̾ �濡 ���� �� ȣ��Ǵ� �Լ�
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom �����." + newPlayer);

        base.OnPlayerEnteredRoom(newPlayer);

        // �� �÷��̾ ������ �� �ش� �÷��̾� ������Ʈ�� ã�� PhotonVoiceView �Ҵ�
        AssignVoiceViewForPlayer(newPlayer);
    }

    // ���� ���� �÷��̾��� PhotonVoiceView�� ã�� �Ҵ��ϴ� �Լ�
    void AssignVoiceViewForPlayer(Player player)
    {
        Debug.Log("-1������");

        // ��Ʈ��ũ���� �ش� �÷��̾��� PhotonView�� ã��
        foreach (PhotonView view in FindObjectsOfType<PhotonView>())
        {
            Debug.Log("0������");

            PhotonVoiceView voiceView = view.GetComponent<PhotonVoiceView>();
            Debug.Log("1������");
            if (voiceView != null)
            {
                Debug.Log("2������");
                // �� �ڸ��� �÷��̾��� VoiceView�� �Ҵ�
                for (int i = 0; i < playerVoiceViews.Length; i++)
                {
                    if (playerVoiceViews[i] == null)
                    {
                        Debug.Log("3������");
                        playerVoiceViews[i] = voiceView;
                        break;
                    }
                }
            }
            if (view.Owner == player)
            {
            }
        }
    }
}
