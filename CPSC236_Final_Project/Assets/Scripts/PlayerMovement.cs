using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    //public Animator animator;
    public float runSpeed = 25f;
    public bool hasJumpPotion = false;
    public bool hasSpeedPotion = false;
    public int potionModAmount = 0;

    public AudioClip jumpClip;

    private float horizontalMove = 0f;
    private bool jumpFlag = false;
    private bool jump = false;

    // Update is called once per frame
    void Update()
    {
        if(this == null)
        {
            return;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        //animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (jumpFlag)
        {
            //animator.SetBool("IsJumping", true);
            jumpFlag = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("I am jumping");
            //if (animator.GetBool("IsJumping") == false)
            //{
            //    AudioSource.PlayClipAtPoint(jumpClip, transform.position);
            //    animator.SetBool("IsJumping", true);
                jump = true;
            //}
        }
    }

    public void OnLanding()
    {
        Debug.Log("stop jumping...");
        //animator.SetBool("IsJumping", false);
        jump = false;
    }

    void FixedUpdate()
    {
        //if (hasJumpPotion)
        //{
        //    controller.m_JumpForceMod = potionModAmount;
        //}
        //else
        //{
        //    controller.m_JumpForceMod = 0;
        //    hasJumpPotion = false;
        //}

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        if (jump)
        {
            jumpFlag = true;
        }
    }
}
