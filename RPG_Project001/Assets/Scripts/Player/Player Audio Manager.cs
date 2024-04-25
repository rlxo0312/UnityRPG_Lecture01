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
    public void playAllStop() //소리를 즉각적으로 꺼주는 함수인데 이를 비활성화 시켜서
    {                         //잔음 ex)진동음 등을 표현하는데 이용할 수 도 있다. 
        audioSource.Stop();
    }
}
