using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [Header("Text")]
    protected ItemUI _itemUI;
    [SerializeField] protected string _title;
    [SerializeField] protected string _description;

    [SerializeField] protected bool _isConsumable;
    [SerializeField] protected bool _isPurchasable;

    [SerializeField] protected float _number;
    [SerializeField] protected float _price;

    AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;

    protected delegate void PickUpDelegate(float number);
    protected PickUpDelegate _pickUpDelegate;

    protected Renderer _renderer;
    protected Collider _collider;
    protected ParticleSystem _particleSystem;

    public abstract void Pick(PlayerBase playerBase);

    private void Awake()
    {
        _itemUI = FindObjectOfType<ItemUI>();
        _audioSource = GetComponent<AudioSource>();
        _pickUpDelegate = null;
    }

    public void OnPickUp(PlayerBase playerBase)
    {
        if (_isPurchasable)
        {
            var money = playerBase.money;

            if (money >= _price)
            {
                playerBase.money -= _price;
                _audioSource.PlayOneShot(_audioClip);
                _pickUpDelegate(_number);
                Activate();
            }
        }
        else
        {
            _audioSource.PlayOneShot(_audioClip);
            _pickUpDelegate(_number);
            Activate();
        }
    }

    public void Activate()
    {
        _renderer = this.GetComponent<MeshRenderer>();
        _collider = this.GetComponent<Collider>();
        _particleSystem = this.GetComponent<ParticleSystem>();

        _particleSystem.Play();

        _renderer.enabled = false;
        _collider.enabled = false;

        if (!_isConsumable)
        {
            _itemUI.ActiveInterface(_title, _description);
        }

        Destroy(this.gameObject, 1f);
    }

    public string GetTitle()
    {
        return _title;
    }
    public float GetPrice()
    {
        return _price;
    }
    public bool GetIsPurchasable()
    {
        return _isPurchasable;
    }
}