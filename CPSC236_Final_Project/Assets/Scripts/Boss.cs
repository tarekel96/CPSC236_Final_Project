using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
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
        if (currentHealth < 0)
        {
            return;
        }

        currentHealth -= damage;

        // play hurt animation
        animator.SetTrigger("Monster-Boss-Hurt");

        if (currentHealth <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        // dead state
        animator.SetBool("Monster-Boss-Dead", true);

        // disable colliding of dead body
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BossMovement>().DisableMove();
        this.enabled = false;

        StartCoroutine(DestoryBoss());
    }

    IEnumerator DestoryBoss()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}