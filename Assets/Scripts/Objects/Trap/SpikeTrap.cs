using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] Animator _trapAnim;
    [SerializeField] BoxCollider _hitbox;
    [SerializeField] ParticleSystem _particles;
    [SerializeField] TrapSound _trapSound;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;

    private void Start()
    {
        _trapSound = new TrapSound(_audioSource, _audioClip);
    }


    private void OnTriggerEnter(Collider other)
    {
        _trapAnim.SetTrigger("TrapTriggered");
    }

    private void OnTriggerStay(Collider other)
    {
        _trapAnim.SetTrigger("TrapTriggered");
    } 

    //Estos metodos los activa eventos en la animacion
    void SpikeTremble()
    {
        _particles.Play();
        _trapSound.TremblePlay();
    }
    void SpikeAttack()
    {
        _hitbox.enabled = true;
        _particles.Stop();
        _trapSound.AudioStop();
        _trapSound.AttackPlay();
    }
    void SpikeRetract()
    {
        _hitbox.enabled = false;
        _trapAnim.ResetTrigger("TrapTriggered");
    }
}