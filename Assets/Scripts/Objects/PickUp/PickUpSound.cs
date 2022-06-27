using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSound : SoundsManager
{
    public PickUpSound(AudioSource audioSource, AudioClip[] audioClip)
    {
        _audioSource = audioSource;
        _audioClip = audioClip;
    }
    public override void playOnCollision(AudioSource audioSource, AudioClip _audioClip)
    {
        Debug.Log("Ejecuto sonido PickUp");
        audioSource.PlayOneShot(_audioClip);
    }
}
