using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterBase
{
    public void onDamage(float damage);

    public void onAttack(Collision other);

    public void HealthUp(float healing);
}
