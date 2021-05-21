// Tarek El Hajjaoui, Nina Valdez, Joshua Wisdom
// CPSC 236-03
// elhaj102@mail.chapman.edu, divaldez@chapman.edu, jowisdom@chapman.edu
// Final Project: Untitled Platformer
// This is our own work, we did not cheat on this assignment

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Script is used to have camera follow direction of player 
/// </summary>

public class CameraMovement : MonoBehaviour
{
    public Transform followTransform;
    private Vector3 smoothPos;
    private float smoothSpeed = 0.5f;

    public GameObject cameraLeftBoarder;
    public GameObject cameraRightBoarder;

    private float cameraHalfWidth;

    // Start is called before the first frame update
    void Start()
    {
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float boarderLeft = cameraLeftBoarder.transform.position.x + cameraHalfWidth;
        float boarderRight = cameraRightBoarder.transform.position.x - cameraHalfWidth;

        smoothPos = Vector3.Lerp(this.transform.position,
            new Vector3(Mathf.Clamp(followTransform.position.x, boarderLeft, boarderRight),
            this.transform.position.y,
            this.transform.position.z), smoothSpeed);

        this.transform.position = smoothPos;

    }
}
