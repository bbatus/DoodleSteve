using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetTrigger("isJumping");
        }
        // else {
        //      animator.SetBool("isJumping", false);
        // }
    }
}
