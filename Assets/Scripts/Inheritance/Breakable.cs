using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] AudioSource _breakSound;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] GameObject _whole;
    [SerializeField] GameObject _broken;

    public void Break()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
        _whole.SetActive(false);
        _broken.SetActive(true);

        _particleSystem.Play();
        _breakSound.Play();
    }
}
