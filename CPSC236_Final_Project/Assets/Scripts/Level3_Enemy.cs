using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3_Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(currentHealth < 0)
        {
            return;
        }

        currentHealth -= damage;

        // play hurt animation

        if(currentHealth <= 0)
        {
            Die();
            
        }
    }

    void Die()
    {
        // die animation
        animator.SetTrigger("Monster-Trash-Die");


        // disable colliding of dead body
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Level3_EnemyMovement>().DisableMove();
        this.enabled = false;

        StartCoroutine(DestoryEnemy());
    }

    IEnumerator DestoryEnemy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}