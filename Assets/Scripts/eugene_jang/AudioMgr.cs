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
        
    }

    void Update()
    {
        int curIdx = BetaDocentMgr.Instance.idx;

        if (preIdx != curIdx)
        {
            preIdx = curIdx;
            PlayFixedAudio(audios, curIdx);
        }
    }
    // idx의 Audio resource가 null 이 아니라면 
    // Audio를 재생
    // bgm 먼저 깔리고.. 
    // 도슨트 재생.. 

    public void PlayFixedAudio(AudioClip[] clips, int idx)
    {

        if (audioSource != null)
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                Destroy(audioSource);
            }
            print("잘 자요");
        } 

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clips[idx];
        audioSource.Play();
    }


}
