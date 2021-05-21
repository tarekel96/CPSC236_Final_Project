using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public List<WaypointScript> Waypoints = new List<WaypointScript>();
    public float Speed = 1.7f;
    public int DestinationWaypoint = 1;
    public Transform player;

    public Animator animator;

    public int maxHealth = 100;
    public bool isDead = false;


    private int currentHealth;
    private Vector3 Destination;
    private bool Forwards = true;
    private float TimePassed = 0f;
    private float chaseSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        this.Destination = this.Waypoints[DestinationWaypoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StopAllCoroutines();

        if (IsPlayerClose())
        {
            //ChasePlayer();
            //IsPlayerCaptured();
        }
        else
            StartCoroutine(MoveTo());
    }

    IEnumerator MoveTo()
    {
        while ((transform.position - this.Destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                this.Destination, this.Speed * Time.deltaTime);
            yield return null;
        }

        if ((transform.position - this.Destination).sqrMagnitude <= 0.01f)
        {
            if (this.Waypoints[DestinationWaypoint].IsSentry)
            {
                while (this.TimePassed < this.Waypoints[DestinationWaypoint].PauseTime)
                {
                    this.TimePassed += Time.deltaTime;
                    yield return null;
                }

                this.TimePassed = 0f;
            }

            GetNextWaypoint();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Current enemy health: " + currentHealth);

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject);
    }

    void GetNextWaypoint()
    {
        if (this.Waypoints[DestinationWaypoint].IsEndpoint)
        {
            if (this.Forwards)
                this.Forwards = false;
            else
                this.Forwards = true;
        }

        if (this.Forwards)
            ++DestinationWaypoint;
        else
            --DestinationWaypoint;

        if (DestinationWaypoint >= this.Waypoints.Count)
            DestinationWaypoint = 0;

        this.Destination = this.Waypoints[DestinationWaypoint].transform.position;
    }

    private bool IsPlayerClose()
    {
        if ((Mathf.Abs(this.gameObject.transform.position.x - player.transform.position.x) < 4f) &&
            (Mathf.Abs(this.gameObject.transform.position.y - player.transform.position.y) < 4f))
        {
            return true;
        }
        else
            return false;
    }

    //void ChasePlayer()
    //{
    //    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * chaseSpeed);
    //}

    //private bool IsPlayerCaptured()
    //{
    //    if ((Mathf.Abs(this.gameObject.transform.position.x - player.transform.position.x) < .5f) &&
    //        (Mathf.Abs(this.gameObject.transform.position.y - player.transform.position.y) < .5f))
    //    {
    //        Debug.Log("Restart at " + Time.time);
    //        SceneManager.LoadScene("AvoiderGame");
    //        return true;
    //    }
    //    else
    //        return false;
    //}
}