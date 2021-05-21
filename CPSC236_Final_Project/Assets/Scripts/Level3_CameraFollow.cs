
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    private Vector3 smoothPosition;
    private float smoothSpeed = 0.5f;
    public GameObject cameraLeftBorder;
    public GameObject cameraRightBorder;

    private float cameraHalfWidth;


    // Start is called before the first frame update
    void Start()
    {
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void FixedUpdate()
    {
        if(followTransform == null)
        {
            return;
        }

        float borderLeft = cameraLeftBorder.transform.position.x + cameraHalfWidth;
        float borderRight = cameraRightBorder.transform.position.x - cameraHalfWidth;

        smoothPosition = Vector3.Lerp(this.transform.position,
            new Vector3(Mathf.Clamp(followTransform.position.x, borderLeft, borderRight),
            this.transform.position.y,
            this.transform.position.z), smoothSpeed);

        this.transform.position = smoothPosition;
    }
}