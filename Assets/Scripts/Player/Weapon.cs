using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float _attackPower;
    Collider _myCollider;
    Player _player;

    [SerializeField] float _slowMotion;
    [SerializeField] ParticleSystem _particleSystem;

    private void Start()
    {
        _player = this.gameObject.GetComponentInParent<Player>();
        _myCollider = GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        var breakable = other.GetComponent<Breakable>();

        if (breakable != null)
            breakable.Break();

        _attackPower = _player._playerBase.attackPowerGetter();
        _particleSystem.Play();

        if (other != null)
        {
            var enemy = other.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy._enemyBase.onDamage(_attackPower);

                Time.timeScale = 0.5f;
                StartCoroutine(SlowMotion());
            }

            _player.SoundHit();
        }
    }

    public void activateCollider()
    {
        _myCollider.enabled = true;
    }

    public void deactivateCollider()
    {
        _myCollider.enabled = false;
    }

    public IEnumerator SlowMotion()
    {
        yield return new WaitForSeconds(_slowMotion);
        Time.timeScale = 1f;
    }
}