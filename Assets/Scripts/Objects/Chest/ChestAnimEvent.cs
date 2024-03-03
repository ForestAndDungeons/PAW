using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimEvent : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;
    
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ANIM_Event(string param)
    {
        if(param == "idle")
        {
            _audioSource.PlayOneShot(_audioClip[0]);
        }

        if (param == "opening")
        {
            _audioSource.PlayOneShot(_audioClip[1]);
        }
    }
}
