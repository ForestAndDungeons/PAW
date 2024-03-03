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
    [SerializeField] int _coinChance;

    public void onDamage(float damage)
    {
        var chance = Random.Range(0, 100);

        this.gameObject.GetComponent<Collider>().enabled = false;
        _whole.SetActive(false);
        _broken.SetActive(true);

        _particleSystem.Play();
        _breakSound.Play();

        if(chance <= _coinChance)
            Instantiate(_coin, _broken.transform.position, Quaternion.Euler(0f,0f,90f));
    }
}
