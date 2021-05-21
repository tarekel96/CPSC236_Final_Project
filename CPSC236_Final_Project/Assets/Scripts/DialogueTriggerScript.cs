using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerScript : MonoBehaviour
{
    public Dialogue dialogue;

    public Animator animator;

    //public void TriggerDialogue()
    //{
    //    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    //}

    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerCombatScript>().enabled = false;
        animator.SetFloat("Speed", 0);

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
