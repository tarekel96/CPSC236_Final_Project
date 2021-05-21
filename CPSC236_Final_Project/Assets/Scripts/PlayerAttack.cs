// Tarek El Hajjaoui, Nina Valdez, Joshua Wisdom
// CPSC 236-03
// elhaj102@mail.chapman.edu, divaldez@chapman.edu, jowisdom@chapman.edu
// Final Project: Untitled Platformer
// This is our own work, we did not cheat on this assignment

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script controls how player attacks enemy 
/// </summary>


public class PlayerAttack : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;

    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayers;

    // Update is called once per frame
    // Player Attack
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAttack>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
