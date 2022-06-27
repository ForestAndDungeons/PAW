using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    protected PickUpSound _pickUpSound;
    protected Renderer _renderer;
    protected Collider _collider;
    protected ParticleSystem _particleSystem;

    public abstract void Pick(PlayerBase playerBase);

    public void onPickUp()
    {
        _renderer = this.GetComponent<MeshRenderer>();
        _collider = this.GetComponent<Collider>();
        _particleSystem = this.GetComponent<ParticleSystem>();

        _particleSystem.Play();

        _renderer.enabled = false;
        _collider.enabled = false;

        Destroy(this.gameObject, 1f);
    }
}