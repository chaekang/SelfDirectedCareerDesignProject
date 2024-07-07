using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip ADChannel;  // 채널
    public AudioClip ADPoisonChannel;  // 독 채널
    public AudioClip ADSynapse; // 시냅스
    public AudioClip ADStart; // q버튼
    public AudioClip ADDirection;    // 방향 전환
    public AudioClip ADUIButton;  // 모든 UI 버튼

    public AudioSource audioSource;

    // 오디오 믹서 그룹 추가
    public AudioMixerGroup mixerGroup;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string action)
    {
        GameObject go = new GameObject("AD" + action);
        audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixerGroup; // 믹서 그룹 설정
        DontDestroyOnLoad(go);

        switch (action)
        {
            case "Channel":
                audioSource.clip = ADChannel; break;
            case "PoisonChannel":
                audioSource.clip = ADPoisonChannel; break;
            case "Synapse":
                audioSource.clip = ADSynapse; break;
            case "Start":
                audioSource.clip = ADStart; break;
            case "Direction":
                audioSource.clip = ADDirection; break;
            case "UIButton":
                audioSource.clip = ADUIButton; break;
        }
        audioSource.Play();
        Destroy(go, audioSource.clip.length);
    }
}
