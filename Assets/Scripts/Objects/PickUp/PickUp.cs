using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [Header("Text")]
    protected ItemUI _itemUI;
    [SerializeField] protected string _title;
    [SerializeField] protected string _description;
    [SerializeField] protected float _speedUP;
    [SerializeField] protected bool _isConsumable;

    [Header("Audio")]
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected AudioClip[] _audioClip;

    protected PickUpSound _pickUpSound;
    protected Renderer _renderer;
    protected Collider _collider;
    protected ParticleSystem _particleSystem;

    public abstract void Pick(PlayerBase playerBase);

    private void Start()
    {
        _pickUpSound = new PickUpSound(_audioSource, _audioClip);
        _itemUI = FindObjectOfType<ItemUI>();
    }

    public void OnPickUp()
    {
        _renderer = this.GetComponent<MeshRenderer>();
        _collider = this.GetComponent<Collider>();
        _particleSystem = this.GetComponent<ParticleSystem>();

        _particleSystem.Play();

        _renderer.enabled = false;
        _collider.enabled = false;

        _pickUpSound.playOnCollision(_audioSource, _audioClip[0]);

        if (!_isConsumable)
        {
            _itemUI.ActiveInterface(_title, _description);
        }

        Destroy(this.gameObject, 1f);
    }
}