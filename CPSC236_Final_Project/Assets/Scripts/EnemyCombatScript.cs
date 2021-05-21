using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    public Animator animator;

    public Transform player;

    public Transform attackPoint;
    public float attackRange = 3f;
    public LayerMask playerLayer;

    private int attackDamage = 15;
    private float attackRate = .5f;
    private float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        //if player gets too close...
        if ((Mathf.Abs(this.gameObject.transform.position.x - player.transform.position.x) < 2f))
        {
            if (Time.time >= nextAttackTime)
            {
                Debug.Log("Attacking player!");
                Attack();

                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        //Play attack animation
        animator.SetTrigger("Attack");

        //Detect enemies in range of attack
        //Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        //Damage enemies
        //foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerHealthScript>().TakeDamage(attackDamage);

            Debug.Log("Player hit!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
