using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class VideoUIController : MonoBehaviour
{
    public RawImage rawImage;  // Raw Image�� ���� ���� ���
    public VideoPlayer videoPlayer;  // Video Player ������Ʈ
    public GameObject talkingSignal;

    public Outline ts; 

    void Start()
    {
        // ���� �÷��̾� �غ� �Ϸ� �� �̺�Ʈ ���
        videoPlayer.prepareCompleted += OnVideoPrepared;

        // ���� �غ� �� ���
        PrepareVideo();

       ts = talkingSignal.GetComponent<Outline>();
    }

    void PrepareVideo()
    {
        // ���� �÷��̾� �غ� ����
        videoPlayer.Prepare();
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        // ���� �÷��̾ �غ�Ǿ��� �� ������ ����ϰ� Raw Image�� �ؽ�ó ����
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }

    void Update()
    {
        // ������ ��� ���̰�, �ؽ�ó�� �������� �ʾҴٸ� ����
        if (videoPlayer.isPrepared && rawImage.texture == null)
        {
            rawImage.texture = videoPlayer.texture;
        }

        if (rawImage.IsActive())
        {
            ts.enabled = true;
        } 
        
        if ( !rawImage.IsActive())
        {
            ts.enabled = false;   
        }
        
    }
}
