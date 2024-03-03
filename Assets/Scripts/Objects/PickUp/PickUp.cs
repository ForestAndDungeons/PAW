using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [Header("Text")]
    protected ItemUI _itemUI;
    [SerializeField] protected float _value;

    [SerializeField] protected string _title;
    public string title { get { return _title; } }

    [SerializeField] protected string _description;

    [SerializeField] protected float _shopPrice;
    public float shopPrice { get { return _shopPrice; } }

    [SerializeField] protected bool _isConsumable;

    [SerializeField] protected bool _isPurchasable;
    public bool isPurchasable { get { return _isPurchasable; } }

    AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;

    protected delegate void PickUpDelegate(float number);
    protected PickUpDelegate _pickUpDelegate;

    [SerializeField] protected GameObject _model;
    [SerializeField] protected Collider _collider;
    [SerializeField] protected Collider _colliderTrigger;
    protected Rigidbody _rigidBody;
    protected Animator _animator;
    protected Renderer _renderer;
    protected ParticleSystem _particleSystem;

    public abstract void Pick(PlayerBase playerBase);

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        _renderer = GetComponentInChildren<MeshRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();

        _itemUI = FindObjectOfType<ItemUI>();
        
        _pickUpDelegate = null;
    }

    void Start()
    {
        if(!_isPurchasable)
        {
            _rigidBody.AddForce(new Vector3(Random.Range(-2f, 2f), Random.Range(4f, 6f), Random.Range(-2f, 2f)), ForceMode.Impulse);
        }
    }

    public void OnPickUp(PlayerBase playerBase)
    {
        if (_isPurchasable)
        {
            var money = playerBase.coins;

            if (money >= _shopPrice)
            {
                playerBase.coins -= _shopPrice;
                _audioSource.PlayOneShot(_audioClip);
                _pickUpDelegate(_value);
                playerBase.player._infoUI.text = null;
                Activate();
            }
        }
        else
        {
            _audioSource.PlayOneShot(_audioClip);
            _pickUpDelegate(_value);
            Activate();
        }
    }

    public void Activate()
    {
        _collider.enabled = false;
        _colliderTrigger.enabled = false;
        _model.SetActive(false);
        _particleSystem.Play();

        if (!_isConsumable)
        {
            _itemUI.ActiveInterface(_title, _description);
        }

        Destroy(this.gameObject, 1f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _rigidBody.isKinematic = true;
            _collider.enabled = false;
        }        
    }
}