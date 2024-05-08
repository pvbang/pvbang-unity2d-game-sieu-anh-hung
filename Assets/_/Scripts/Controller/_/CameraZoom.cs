using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float ZoomChange;
    public float SmoothChange;
    public float MinZoom;
    public float MaxZoom;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            cam.orthographicSize -= ZoomChange * Time.deltaTime + SmoothChange;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            cam.orthographicSize += ZoomChange * Time.deltaTime + SmoothChange;
        }

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, MinZoom, MaxZoom);
    }
}
