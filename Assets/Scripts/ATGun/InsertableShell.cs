using UnityEngine;
using System.Collections;

public class InsertableShell : MonoBehaviour
{
    bool isHolding = false;
    bool pushingStarted = false;
    public float sensitivity = 2.5f;
    public float min, max;
    public float finishedThreshold = 0.01f;
    float value;
    GunOperating go;
    PlayerLookPoints plp;

    private void Start()
    {
        go = GunOperating.instance;
        plp = PlayerLookPoints.instance;
        value = min;
    }

    void FixedUpdate()
    {        
        if (isHolding)
        {
            float y = Input.GetAxis("Mouse Y") * sensitivity;
            value += y;
            value = Mathf.Clamp(value, min, max);
            if (value >= max - finishedThreshold)
            {
                // Op finished!
                value = max;
                isHolding = false;
                pushingStarted = false;
                Cursor.lockState = CursorLockMode.None;
                Sounds.Spawn(transform.position, transform, SoundLibrary.GetClip("shellLoadFull"));

                //Animate breech block closing
                StartCoroutine(BreechBlockCloseAnim());
            }
            go.insertableShellDistance = value;
        }
    }

    IEnumerator BreechBlockCloseAnim()
    {
        bool closing = true;
        while (closing)
        {
            go.breechBlockHandleRot += Time.deltaTime * 1000f;
            if (go.breechBlockHandleRot >= 174)
            {
                go.breechBlockHandleRot = 175;                
                closing = false;
                yield return new WaitForSeconds(0.2f);
                go.SetShellType(-1);
                go.insertableShellDistance = min;
                go.gunLoadedAndAiming = true;
                Cursor.lockState = CursorLockMode.Locked;
                plp.NextView();
            }
            yield return null;
        }        
    }

    void OnMouseDown()
    {
        if (plp.currentView != 3)
            return;
        Sounds.Spawn(transform.position, transform, SoundLibrary.GetClip("shellLoadHalf"));
        if (Input.GetMouseButton(0))
        {
            if (!pushingStarted)
            {
                value = min;
                pushingStarted = true;
            }
            isHolding = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void OnMouseUp()
    {
        if (isHolding)
        {
            isHolding = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
