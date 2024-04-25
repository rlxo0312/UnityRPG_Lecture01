using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void playFootStepSFX()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(SoundManager.Instance.footStepSFX);
        }
    }
    public void PlayJumpSFX()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(SoundManager.Instance.CandleSFX);
        }
    }
    public void playAllStop() //�Ҹ��� �ﰢ������ ���ִ� �Լ��ε� �̸� ��Ȱ��ȭ ���Ѽ�
    {                         //���� ex)������ ���� ǥ���ϴµ� �̿��� �� �� �ִ�. 
        audioSource.Stop();
    }
}
