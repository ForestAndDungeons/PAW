using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IDamage
{
    [SerializeField] AudioSource _breakSound;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] GameObject _whole;
    [SerializeField] GameObject _broken;
    [SerializeField] GameObject _coin;

    public void onDamage(float damage)
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
        _whole.SetActive(false);
        _broken.SetActive(true);

        _particleSystem.Play();
        _breakSound.Play();

        Instantiate(_coin, _broken.transform.position, Quaternion.Euler(0f,0f,90f));
    }
}
