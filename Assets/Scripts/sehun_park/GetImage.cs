using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetImage : MonoBehaviourPun
{
    public string url;              // ��û�� URL
    public Renderer targetRenderer; // �̹����� ������ 3D ������Ʈ�� Renderer
    public Material defaultMaterial; // �ʱ� ������ �⺻ Material (���� ����)

    private void Update()
    {
        // ���� 9 Ű�� ������ �� GetImage1 �޼ҵ� ȣ��
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GetImage1();
        }
    }

    public void GetImage1()
    {
        StartCoroutine(GetImageRequest(url));
    }

    // �̹��� ������ Get���� �޴� �Լ�
    IEnumerator GetImageRequest(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // �̹����� �ٿ�ε��Ͽ� byte[]�� ��ȯ
                byte[] imageData = request.downloadHandler.data;

                // Photon RPC�� ����Ͽ� ��� Ŭ���̾�Ʈ�� �̹��� ���� ��û
                photonView.RPC("ApplyTexture", RpcTarget.AllBuffered, imageData);
            }
            else
            {
                Debug.LogError(request.error);
            }
        }
    }

    [PunRPC]
    void ApplyTexture(byte[] textureData)
    {
        // byte[]�� Texture2D�� ��ȯ
        Texture2D newTexture = new Texture2D(2, 2);
        newTexture.LoadImage(textureData);

        // Renderer�� material�� Texture2D�� ����
        if (targetRenderer != null)
        {
            // �⺻ Material�� �ʿ��� ��� ����� �� �ֽ��ϴ�.
            Material material = new Material(defaultMaterial);
            material.mainTexture = newTexture;
            targetRenderer.material = material;
        }
    }
}