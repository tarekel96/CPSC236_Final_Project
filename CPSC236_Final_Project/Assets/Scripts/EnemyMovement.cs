using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    // vars for spawning enemies
    public float timerMin = 5f;
    public float timerMax = 25f;

    // player ~ user
    public Player player;

    // vars for enemy movement
    private Vector2 relativePosition;
    private float moveSpeed = 1.6f;
    private Vector2 enemyMovement;
    private float enemyDetectionRadius = 5f;
    // vars for rand enemy movement
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;

    public bool canMove = true;

    public bool canAttackPlayer = false;
    bool shouldFlip = false;

    // Start is called before the first frame update
    void Start()
    {
        // calculations for enemy movement
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, moveSpeed);
        canAttackPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this == null || player == null)
        {
            return;
        }

        if (!canMove)
        {
            return;
        }

        if (player != null)
        {
            // relative poistion of the target (player) based upon the current position
            relativePosition = new Vector2(
                player.GetComponent<Rigidbody2D>().position.x - gameObject.transform.position.x,
                player.GetComponent<Rigidbody2D>().position.y - gameObject.transform.position.y);
        }

        if (relativePosition.x < enemyDetectionRadius && relativePosition.y < enemyDetectionRadius)
        {
            canAttackPlayer = true;
        }
        else
        {
            canAttackPlayer = false;
            //if the changeTime was reached, calculate a new movement vector
            if (Time.time - latestDirectionChangeTime > directionChangeTime)
            {
                latestDirectionChangeTime = Time.time;
                calcuateNewMovementVector();
            }
        }

        if (canAttackPlayer)
        {
            if ((player.GetComponent<Rigidbody2D>().position.x - gameObject.transform.position.x) > 0 && this.transform.localScale.x > 0)
            {
                shouldFlip = true;
            }
            else if ((player.GetComponent<Rigidbody2D>().position.x - gameObject.transform.position.x) < 0 && this.transform.localScale.x < 0)
            {
                shouldFlip = true;
            }
            else
            {
                shouldFlip = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if(this == null)
        {
            return;
        }

        if (!canMove)
        {
            return;
        }

        if (shouldFlip)
        {
            Flip();
        }
        if (player != null && canAttackPlayer)
        {
            MoveTowardsPlayer();
        }
        else if (player != null && !canAttackPlayer)
        {
            RandomMove();
        }
    }

    public void DisableMove()
    {
        canMove = false;
    }


    void MoveTowardsPlayer()
    {
        if (moveSpeed * Time.deltaTime >= Mathf.Abs(relativePosition.x))
        {
            enemyMovement.x = relativePosition.x;
        }
        else
        {
            enemyMovement.x = moveSpeed * Mathf.Sign(relativePosition.x);
        }

        rb.velocity = enemyMovement;
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), 0).normalized;
        movementPerSecond = movementDirection * moveSpeed;
    }

    void RandomMove()
    {
        //move enemy: 
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));

    }

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}