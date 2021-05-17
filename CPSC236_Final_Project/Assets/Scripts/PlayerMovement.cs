using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public CharacterController2D controller;
    public Rigidbody2D rb;
    public Animator animator;
    private bool isAttacking = false;
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttacking = true;
            StartCoroutine(ToggleAttackAnimation());
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;


        if (jumpFlag)
        {
            jumpFlag = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
                jump = true;
        }
    }

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

    private IEnumerator ToggleAttackAnimation()
    {
        if (isAttacking)
        {
            isAttacking = true;
            animator.SetBool("Attack", true);
            yield return new WaitForSeconds(0.9f);
        }
        animator.SetBool("Attack", false);
        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Is colliding");

            if (Input.GetKeyDown(KeyCode.Mouse0) || isAttacking)
            {
                Debug.Log("Is colliding and attacking");
                Destroy(collision.gameObject);
            }
            else
            {
                Vector2 bounceVector = new Vector2(-10, 10);
                rb.AddForce(bounceVector, ForceMode2D.Impulse);
            }
        }
    }
}
