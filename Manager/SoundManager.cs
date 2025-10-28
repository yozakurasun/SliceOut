using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField] private AudioSource _sourceBGM;
    [SerializeField] private AudioSource _sourceSE;

    public void PlayBGM(SoundType soundType)
    {
        AudioClip clip = GetAudioClip(soundType);
        _sourceBGM.clip = clip;
        _sourceBGM.Play();
    }

    public void PlaySE(SoundType soundType) => _sourceSE.PlayOneShot(GetAudioClip(soundType));

    public AudioClip GetAudioClip(SoundType soundType) => GameDataManager.GetSoundData(soundType);
}
