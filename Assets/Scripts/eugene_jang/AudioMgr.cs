using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    
    public AudioSource audioSource;

    public int preIdx;

    public AudioClip[] audios = new AudioClip[33];
    void Start()
    {
        audioSource.volume = 0.5f;
    }

    void Update()
    {
        int curIdx = BetaDocentMgr.Instance.idx;

        if (preIdx != curIdx)
        {
            preIdx = curIdx;
            if (curIdx >= 0)
            {
                PlayFixedAudio(audios, curIdx);
            }
        }
    }
    // idx�� Audio resource�� null �� �ƴ϶�� 
    // Audio�� ���
    // bgm ���� �򸮰�.. 
    // ����Ʈ ���.. 

    public void PlayFixedAudio(AudioClip[] clips, int idx)
    {
        audioSource.Stop();

        //if (audioSource != null)
        //{
        //    AudioSource[] audioSources = GetComponents<AudioSource>();
        //    foreach (AudioSource audioSource in audioSources)
        //    {
        //        Destroy(audioSource);
        //    }
        //    print("�� �ڿ�");
        //} 

        //audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clips[idx];
        audioSource.Play();
    }


}
