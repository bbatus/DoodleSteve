using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public PlayerData playerData;
    public bool isGrounded;
    public LayerMask groundLayer; 
    public float groundCheckDistance = 0.2f;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGroundStatus();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    void CheckGroundStatus()
    {
        isGrounded = IsGrounded();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0, 0) * playerData.movementSpeed;
        playerRb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        playerRb.AddForce(Vector3.up * playerData.jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //     }
    // }

    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = false;
    //     }
    // }
}
