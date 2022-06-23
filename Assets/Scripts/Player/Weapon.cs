using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float _attackPower;
    [SerializeField] Collider _myColliderAttack;
    [SerializeField] Collider _myColliderSpecial;
    [SerializeField] Collider _myColliderBlock;
    Player _player;

    [SerializeField] float _slowMotion;
    [SerializeField] ParticleSystem _particleSystem;

    private void Start()
    {
        _player = this.gameObject.GetComponentInParent<Player>();
    }

    public void OnTriggerEnter(Collider other)
    {
        var breakable = other.GetComponent<Breakable>();

        if (breakable != null)
            breakable.Break();

        _attackPower = _player._playerBase.GetAttackPower();
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

    public void activateColliderAttack()
    {
        _myColliderAttack.enabled = true;
    }

    public void deactivateColliderAttack()
    {
        _myColliderAttack.enabled = false;
    }

    public void activateColliderSpecial()
    {
        _myColliderSpecial.enabled = true;
    }

    public void deactivateColliderSpecial()
    {
        _myColliderSpecial.enabled = false;
    }

    public void activateColliderBlock()
    {
        _myColliderBlock.enabled = true;
    }

    public void deactivateColliderBlock()
    {
        _myColliderBlock.enabled = false;
    }

    public IEnumerator SlowMotion()
    {
        yield return new WaitForSeconds(_slowMotion);
        Time.timeScale = 1f;
    }
}