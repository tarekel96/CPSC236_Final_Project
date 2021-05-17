using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //public Rigidbody2D rb;
    //public float moveSpeed = 6f;
    //public Animator animator;

    //public Vector2 targetPosition; // cursor clicked position
    //public Vector2 relativePosition;  // relativePosition - character relative position
    ////private Vector2 movement;  // movement - var that stores movement position after a click

    //private float firstClickTime, timeBetweenClicks;
    //private bool coroutineAllowed;
    //private int clickCounter;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(this == null)
        {
            return;
        }


        if(currentHealth == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    // logic handling player-enemy collisions
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        Vector2 bounceVector = new Vector2(-10, 10);
    //        rb.AddForce(bounceVector, ForceMode2D.Impulse);
    //    }
    //}
}