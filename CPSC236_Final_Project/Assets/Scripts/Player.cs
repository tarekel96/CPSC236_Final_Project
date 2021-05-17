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
        //firstClickTime = 0f;
        //timeBetweenClicks = 0.2f;
        //clickCounter = 0;
        //coroutineAllowed = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        //animator.SetFloat("Speed", (Mathf.Abs(targetPosition.x - gameObject.transform.position.x)));

        //// set the mouse position
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    clickCounter++;
        //    targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //}
        //if (clickCounter == 1 && coroutineAllowed)
        //{
        //    moveSpeed = 6;
        //    firstClickTime = Time.time;
        //    StartCoroutine(DoubleClickDetection());
        //}


        //// relative poistion of the target based upon the current position
        //relativePosition = new Vector2(
        //    targetPosition.x - gameObject.transform.position.x,
        //    targetPosition.y - gameObject.transform.position.y);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    //private IEnumerator DoubleClickDetection()
    //{
    //    coroutineAllowed = false;
    //    while (Time.time < firstClickTime + timeBetweenClicks)
    //    {
    //        if (clickCounter == 2)
    //        {
    //            Debug.Log("Double Click");
    //            moveSpeed = 12;
    //            yield return new WaitForSeconds(1.5f); // give player 2x speed for 1.5 seconds
    //        }
    //        else
    //        {
    //            yield return new WaitForEndOfFrame();
    //        }

    //    }
    //    clickCounter = 0;
    //    firstClickTime = 0f;
    //    coroutineAllowed = true;
    //}


    //void FixedUpdate()
    //{
    //    Move();
    //}

    //void Move()
    //{
    //    transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    //}

}