using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    [SerializeField] AudioSource _myAudioSource;
    [SerializeField] AudioClip[] _audioClips;
    [SerializeField] Animator _transition;

    private void OnTriggerEnter(Collider other)
    {
        _transition.SetTrigger("fadeIn");
        _myAudioSource.PlayOneShot(_audioClips[1]);
    }

    private void OnTriggerExit(Collider other)
    {
        _transition.SetTrigger("fadeIn");
        _myAudioSource.PlayOneShot(_audioClips[0]);
    }
}
