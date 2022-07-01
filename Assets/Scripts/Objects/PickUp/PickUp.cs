using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] protected ItemUI _itemUI;
    [SerializeField] protected string _title;
    [SerializeField] protected string _description;
    [SerializeField] protected bool _isConsumable;


    protected Renderer _renderer;
    protected Collider _collider;
    protected ParticleSystem _particleSystem;

    public abstract void Pick(PlayerBase playerBase);

    private void Awake()
    {
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

        if (!_isConsumable)
        {
            _itemUI.ActiveInterface(_title, _description);
        }

        Destroy(this.gameObject, 1f);
    }
}