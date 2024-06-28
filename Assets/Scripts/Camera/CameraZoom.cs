using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Camera cam;
    float fovOnLoad;
    public float zoomFov;
    GunOperating go;

    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        fovOnLoad = cam.fieldOfView;
        go = GunOperating.instance;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ToggleScope(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ToggleScope(false);
        }
    }

    void ToggleScope(bool isZooming)
    {
        if (!go.gunLoadedAndAiming)
            return;

        // Change camera position to scope (make a parent obj)
        // Set scope image active for camera's canvas
        if (isZooming)
            cam.fieldOfView = zoomFov;
        else
            cam.fieldOfView = fovOnLoad;
    }
}
