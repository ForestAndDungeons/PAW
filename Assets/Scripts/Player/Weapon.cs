using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float _attackPower;
    [SerializeField] float _slowMotion;
    [SerializeField] Collider _myColliderAttack;
    [SerializeField] Collider _myColliderSpecial;
    [SerializeField] Collider _myColliderBlock;
    [SerializeField] ParticleSystem _particleSystem;

    private void Start()
    {
        //_entity = this.GetComponentInParent<Player>();

        //_attackPower = _entity.GetAttackPower();
    }

    public void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponent<IDamage>();

        if (entity != null)
        {
            entity.onDamage(_attackPower);
            Time.timeScale = 0.5f;
            StartCoroutine(SlowMotion());
        }
    }

    public void SetAttackPower(float attackPower)
    {
        _attackPower = attackPower;
    }

    public void AttackUp(float add)
    {
        _attackPower += add;
    }

    public void ActivateColliderAttack(bool activate)
    {
        _myColliderAttack.enabled = activate;
    }

    public void ActivateColliderSpecial(bool activate)
    {
        _myColliderSpecial.enabled = activate;
    }

    public void ActivateColliderBlock(bool activate)
    {
        _myColliderBlock.enabled = activate;
    }

    public IEnumerator SlowMotion()
    {
        yield return new WaitForSeconds(_slowMotion);
        Time.timeScale = 1f;
    }
}