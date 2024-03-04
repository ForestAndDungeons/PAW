using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] Collider _AttackCollider;

    public void ColliderActivateEvent()
    {
        if (_AttackCollider!=null)
        {
            _AttackCollider.enabled = true;
        }
    }

    public void ColliderDesactivateEvent()
    {
        if (_AttackCollider != null)
        {
            _AttackCollider.enabled = false;
        }
    }

}
