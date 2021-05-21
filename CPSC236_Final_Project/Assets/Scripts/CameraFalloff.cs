// Joshua Wisdom
// 2313991
// CPSC 236-03
// jowisdom@chapman.edu
// Project 05: 2D Platformer
// This is my own work, and I did not cheat on this assignment.

// No known errors.

// Completed alongside provided videos with provided materials.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFalloff : MonoBehaviour
{
    public Transform followTransform;

    private Vector3 smoothPos;
    private float smoothSpeed = 0.5f;

    public GameObject cameraLeftBorder;
    public GameObject cameraRightBorder;

    private float cameraHalfWidth;

    // Start is called before the first frame update
    void Start()
    {
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float borderLeft = cameraLeftBorder.transform.position.x + cameraHalfWidth;
        float borderRight = cameraRightBorder.transform.position.x - cameraHalfWidth;

        smoothPos = Vector3.Lerp(this.transform.position,
            new Vector3(Mathf.Clamp(followTransform.position.x, borderLeft, borderRight),
            this.transform.position.y,
            this.transform.position.z), smoothSpeed);

        this.transform.position = smoothPos;
    }
}
