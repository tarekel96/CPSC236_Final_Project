// Tarek El Hajjaoui, Nina Valdez, Joshua Wisdom
// CPSC 236-03
// elhaj102@mail.chapman.edu, divaldez@chapman.edu, jowisdom@chapman.edu
// Final Project: Untitled Platformer
// This is our own work, we did not cheat on this assignment

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script controls general player movement: Idle, run, dash
/// </summary>


public class NinaPlayerMovement : MonoBehaviour
{
    public Level2_CharacterController2D controller;
    public Rigidbody2D rb;
    public Animator animator;
    public AudioClip jumpClip;

    private bool isAttacking = false;
    public float runSpeed = 25f;

    public bool hasJumpPotion = false;
    public bool hasSpeedPotion = false;
    public int potionModAmount = 0;
    private float horizontalMove = 0f;
    private bool jumpFlag = false;
    private bool jump = false;

    public float dashSpeed;
    public float startDashTime;
    private float dashTime;
    private int direction;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Jump Movement
        if (jumpFlag)
        {
            jumpFlag = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log("I am jumping");
            AudioSource.PlayClipAtPoint(jumpClip, transform.position);
            animator.SetBool("IsJumping", true);
            jump = true;
        }

        // Dash Feature 
        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction = 2;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                direction = 1;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if (direction == 2)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
                else if (direction == 1)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                }
            }
        }
    }

    // Landing after jump
    public void OnLanding()
    {
        jump = false;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        if (jump)
        {
            jumpFlag = true;
        }
    }
}