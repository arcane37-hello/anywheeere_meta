using Photon.Pun;
using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChild : MonoBehaviourPun
{
    // ���� ������Ʈ�� ���� ī�޶��� �̸�
    private const string cameraName = "DynamicCamera";

    public int enterOrder = 0;
    PhotonVoiceView pvv;
    PlayerVideoMgr playerVideoMgr;

    void Start()
    {
        // �±װ� "MainCamera"�� ������Ʈ�� ã���ϴ�.
        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");

        if (cameraObject != null)
        {
            // ī�޶� ������Ʈ�� ã������, ���� ������Ʈ�� ī�޶��� �ڽ����� �����մϴ�.
            if (cameraObject.name == cameraName)
            {
                transform.SetParent(cameraObject.transform);

                // �ڽ� ������Ʈ�� ��ġ�� ī�޶� ������Ʈ�� ��ġ�� ��������� ���ߵ��� �����մϴ�.
                transform.localPosition = new Vector3(0, 0, 6);
                transform.localRotation = Quaternion.identity;
            }
            else
            {
                Debug.LogWarning($"�±װ� 'MainCamera'�� ������Ʈ�� ������, �̸��� '{cameraName}'�� �ƴմϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("�±װ� 'MainCamera'�� ������Ʈ�� ã�� �� �����ϴ�.");
        }

        playerVideoMgr = GameObject.Find("GameManager").GetComponent<PlayerVideoMgr>();
        pvv = GetComponent<PhotonVoiceView>();
        enterOrder = photonView.Owner.ActorNumber - 1;
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            playerVideoMgr.UpdatePlayerVideo(enterOrder, pvv.IsRecording);

        }
        else
        {
            playerVideoMgr.UpdatePlayerVideo(enterOrder, pvv.IsSpeaking);
        }
    }
}