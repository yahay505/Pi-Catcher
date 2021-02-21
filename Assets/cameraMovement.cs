﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    Vector3 lastpos, lastposcroll;
    public float speedWASD = 0.05f;
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 300f;
    [SerializeField] private float zoomLerpSpeed = 10f;
    RoverManager rover;

    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
        rover = GameObject.FindObjectOfType<RoverManager>();
    }

    void Update()
    {
        rover = rover ?? GameObject.FindObjectOfType<RoverManager>();
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.transform.position=new Vector3 (rover.transform.position.x, rover.transform.position.y, transform.position.z);
        }
        if (rover.isZoom)
        {
            transform.position = new Vector3(rover.transform.position.x, rover.transform.position.y, transform.position.z);
            cam.orthographicSize = 3f;
        }
        if (Input.mouseScrollDelta!=Vector2.zero)
        {
            lastposcroll = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + (Input.mouseScrollDelta.y * zoomLerpSpeed*-1f),3f,float.PositiveInfinity);
            transform.position += lastposcroll - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);




        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");
        if (Camera.current != null)
        {
            Camera.current.transform.Translate(new Vector3(xAxisValue*speedWASD, yAxisValue*speedWASD, 0.0f));
        }

    }

    public void LateUpdate()
    {
        if (Input.GetMouseButton(2))
        {
            transform.position += lastpos - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        lastpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }



}
