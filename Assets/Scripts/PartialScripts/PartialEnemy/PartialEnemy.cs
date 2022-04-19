using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartialEnemy : MonoBehaviour
{
    [SerializeField]private Transform _target;
    public float speed = 0.2f;
    public Rigidbody rb;

    void Start()
    {
        if (_target == null)
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                _target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
    }

    void OnTriggerStay(Collider player)
    {
        if (_target)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
        rb.MovePosition(pos);
        transform.LookAt(_target);
    }

}
