using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]  int speed;
    [SerializeField]  int jumpForce;

    public Animator myAnimator;

    Rigidbody myRigidBody;

    Vector3 verticalInput;
    Vector3 horizontalInput;
    Vector3 movement;

    void Start()
    {
        speed = 5;
        jumpForce = 5;
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (groundSensor.IsGrounded)
            {
                myRigidBody.AddForce(transform.up * jumpForce);
            }
        }

        myAnimator.SetFloat("vertical", Input.GetAxis("Vertical"));
        myAnimator.SetFloat("horizontal", Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Fire1"))
        {
            myAnimator.SetBool("IsAttacking", true);
        }

        verticalInput = Input.GetAxis("Vertical") * transform.forward * speed * Time.fixedDeltaTime;
        horizontalInput = Input.GetAxis("Horizontal") * transform.right * speed * Time.fixedDeltaTime;
        movement = horizontalInput + verticalInput;

        movement.y = myRigidBody.velocity.y;
        myRigidBody.velocity = movement;
    }
}
