using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip ADChannel;  // ä��
    public AudioClip ADPoisonChannel;  // �� ä��
    public AudioClip ADSynapse; // �ó���
    public AudioClip ADStart; // q��ư
    public AudioClip ADDirection;    // ���� ��ȯ
    public AudioClip ADUIButton;  // ��� UI ��ư

    public AudioSource audioSource;

    // ����� �ͼ� �׷� �߰�
    public AudioMixerGroup mixerGroup;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string action)
    {
        GameObject go = new GameObject("AD" + action);
        audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixerGroup; // �ͼ� �׷� ����
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
