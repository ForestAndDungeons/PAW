using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float radius = 0.1f;

    public bool IsGrounded;

    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius, GroundLayer);

        if (colliders.Length > 0)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
