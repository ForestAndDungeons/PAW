using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSound : SoundsManager
{
    public TrapSound(AudioSource audioSource, AudioClip[] audioClip)
    {
        _audioSource = audioSource;
        _audioClip = audioClip;
    }

    public void TremblePlay()
    {
        _audioSource.PlayOneShot(_audioClip[0]);
    }

    public void AttackPlay()
    {
        _audioSource.PlayOneShot(_audioClip[1]);
    }
    public void AudioStop()
    {
        _audioSource.Stop();
    }
}
