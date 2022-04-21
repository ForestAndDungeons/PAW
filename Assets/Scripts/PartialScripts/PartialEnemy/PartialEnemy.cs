using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartialEnemy : MonoBehaviour
{
    [SerializeField] private Transform _target;
    public float speed = 0.2f;
    public Rigidbody rb;
    


    void OnTriggerStay(Collider player)
    {
        if (player.tag == "Player")
        {
            if (_target == null)
            {
                _target = player.transform;
            }
            
            if (_target)
            {
                FollowPlayer();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _target = null;
    }

    void FollowPlayer()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position , _target.position, speed * Time.deltaTime);
        rb.MovePosition(pos);
        transform.LookAt(_target);
    }

}
