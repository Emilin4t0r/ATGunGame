using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    public static CamerasController instance;
    GunOperating go;
    public GameObject mainCam, scopeCam;
    public string activeCam;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        go = GunOperating.instance;
        activeCam = mainCam.name;
    }
    void Update()
    {
        if (activeCam == scopeCam.name)
        {
            scopeCam.GetComponent<Camera>().fieldOfView -= Input.mouseScrollDelta.y;
            Mathf.Clamp(scopeCam.GetComponent<Camera>().fieldOfView, 10, 23);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!go.gunLoadedAndAiming)
                return;
            ToggleScope(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ToggleScope(false);
        }
    }

    public void ToggleScope(bool scopedIn)
    {
        if (scopedIn)
        {
            mainCam.SetActive(false);
            scopeCam.SetActive(true);
            activeCam = scopeCam.name;
        }
        else
        {
            scopeCam.SetActive(false);
            mainCam.SetActive(true);
            activeCam = mainCam.name;
        }
    }
}
