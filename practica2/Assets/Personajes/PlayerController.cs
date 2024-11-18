using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rigidBody;
    public float speedOut = 5f;
    public float walkspeed = 1f;
    public float jumpForce = 5f;
    private bool isJumping = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        if (Input.GetKey(KeyCode.W))
            rigidBody.velocity = forward * speedOut * walkspeed + new Vector3(0, rigidBody.velocity.y, 0);
        if (Input.GetKey(KeyCode.S))
            rigidBody.velocity = -forward * speedOut * walkspeed + new Vector3(0, rigidBody.velocity.y, 0);
        if (Input.GetKey(KeyCode.A))
            rigidBody.velocity = -right * speedOut * walkspeed + new Vector3(0, rigidBody.velocity.y, 0);
        if (Input.GetKey(KeyCode.D))
            rigidBody.velocity = right * speedOut * walkspeed + new Vector3(0, rigidBody.velocity.y, 0);

        if (rigidBody.velocity.magnitude > 0)
        {
            anim.SetBool("isRunning", Input.GetKey(KeyCode.LeftShift));
            anim.SetBool("isWalking", !Input.GetKey(KeyCode.LeftShift));
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            anim.SetBool("isJumping", true);
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
            isJumping = false;
        }
    }
}
