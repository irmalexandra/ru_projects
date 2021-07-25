using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public PlayerController controller;
    public Animator animator;
    public float runSpeed = 40f;
    private float horizontalMove;
    private bool jump;
    private bool crouch;
    private bool canMove;

    void Update()
    {
        canMove = controller._alive;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouch = false;
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Aiming", true);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("Aiming", false);
        }
        
        animator.SetBool("Nervous", controller._nervous); 
           
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        }
        jump = false;
        //animator.SetBool("Jumping", false);
        if (controller.grounded){
            animator.SetBool("Jumping", false);
        }
        
        animator.SetBool("Dead", !controller._alive);
        animator.SetBool("HasKey", controller.hasFinalKey);
            
    }

}
