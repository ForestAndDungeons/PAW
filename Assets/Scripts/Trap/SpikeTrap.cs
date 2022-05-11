using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] Animator _trapAnim;
    [SerializeField] BoxCollider _hitbox;
    [SerializeField] ParticleSystem _particles;

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
    }
    void SpikeAttack()
    {
        _hitbox.enabled = true;
        _particles.Stop();
    }
    void SpikeRetract()
    {
        _hitbox.enabled = false;
    }
 
}
