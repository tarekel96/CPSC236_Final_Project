using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_PlayerMovement : MonoBehaviour
{
    
    public Level3_CharacterController2D controller;
    public Rigidbody2D rb;

    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public int attackDamage = 100;
    public float attackRange = 2.5f;

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
            Attack();
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

    void Attack()
    {
        // play attack animation
        animator.SetTrigger("AttackTrigger");

        // detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        if(hitEnemies == null)
        {
            return;
        }

        // damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Level3_Enemy>())
            {
                enemy.GetComponent<Level3_Enemy>().TakeDamage(attackDamage);
            }
            else
            {
                enemy.GetComponent<Boss>().TakeDamage(attackDamage);
                //StartCoroutine(DisableCollider(enemy));
                //boss.GetComponent<Collider2D>().enabled = false;
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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

    //IEnumerator DisableCollider(Boss boss)
    //{
    //    boss.GetComponent<Collider2D>().enabled = false;
    //    yield return new WaitForSeconds(1.5f);
    //}

}
