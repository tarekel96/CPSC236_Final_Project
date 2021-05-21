// Tarek El Hajjaoui, Nina Valdez, Joshua Wisdom
// CPSC 236-03
// elhaj102@mail.chapman.edu, divaldez@chapman.edu, jowisdom@chapman.edu
// Final Project: Untitled Platformer
// This is our own work, we did not cheat on this assignment

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls player health, when enemy collides with player, and when player dies
/// </summary

public class Player : MonoBehaviour
{
    public Animator animator;
    public HealthBar healthBar;

    public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // impact on player health 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player Impact!");
            TakeDamage(20);
            return;
        }
    }

    // reload scene after player death
    void Update()
    {
        if (currentHealth == 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Player Died! Try Again!");
        }
    }

    // View Health on Health bar
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}