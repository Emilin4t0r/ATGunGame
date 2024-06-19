using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Camera cam;
    float fovOnLoad;
    public float zoomFov;
    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        fovOnLoad = cam.fieldOfView;
    }
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ToggleZoom(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ToggleZoom(false);
        }
    }

    void ToggleZoom(bool isZooming)
    {
        if (isZooming)
            cam.fieldOfView = zoomFov;
        else
            cam.fieldOfView = fovOnLoad;
    }
}
