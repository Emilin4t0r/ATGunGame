using UnityEngine;
using System.Collections;

public class BreechHandle : MonoBehaviour
{
    bool isHolding = false;
    public float sensitivity = 2.5f;
    public float min, max;
    public float finishedThreshold = 1;
    public GameObject emptyShell;
    public Transform shellSpitOutSpot;
    float value;
    GunOperating go;
    PlayerLookPoints plp;

    GameObject tempEmptyShell;

    private void Start()
    {
        go = GunOperating.instance;
        plp = PlayerLookPoints.instance;
    }

    void FixedUpdate()
    {
        if (isHolding)
        {
            float y = Input.GetAxis("Mouse Y") * sensitivity;
            value += y;
            value = Mathf.Clamp(value, min, max);
            print(value);
            if (value <= min + finishedThreshold)
            {
                // Op finished!
                value = min;
                isHolding = false;
                Cursor.lockState = CursorLockMode.None;

                //Animate shell ejection
                
                Rigidbody esRb = tempEmptyShell.GetComponent<Rigidbody>();
                esRb.isKinematic = false;
                esRb.AddForce(-tempEmptyShell.transform.forward * 10, ForceMode.Impulse);
                tempEmptyShell.GetComponent<Case>().enabled = true;
                StartCoroutine(ApplyTorqueToESDelayed(esRb));
                Sounds.Spawn(transform.position, transform, SoundLibrary.GetClip("openLeverFull"));
                StartCoroutine(NextViewWaiter());
                tempEmptyShell = null;
            }
            go.breechBlockHandleRot = value;
        }
    }

    IEnumerator ApplyTorqueToESDelayed(Rigidbody rb)
    {        
        float torqueAmount = 1f;
        Vector3 randomTorque = new Vector3(
            Random.Range(-torqueAmount, torqueAmount),
            Random.Range(-torqueAmount, torqueAmount),
            0
        );
        yield return new WaitForSeconds(0.2f);
        rb.AddTorque(randomTorque, ForceMode.Impulse);
    }

    IEnumerator NextViewWaiter()
    {
        yield return new WaitForSeconds(0.2f);
        plp.NextView();
    }

    void OnMouseDown()
    {
        if (plp.currentView != 1)
            return;
        Sounds.Spawn(transform.position, transform, SoundLibrary.GetClip("openLeverHalf"));
        if (tempEmptyShell == null)
        {
            tempEmptyShell = Instantiate(emptyShell, shellSpitOutSpot.position, shellSpitOutSpot.rotation, null);            
            tempEmptyShell.GetComponent<Rigidbody>().isKinematic = true;
            tempEmptyShell.GetComponent<Case>().enabled = false;
        }
        if (Input.GetMouseButton(0))
        {
            isHolding = true;
            if (value == max)
            {
                value = max - finishedThreshold * 2;
            }
            else
            {
                value = go.breechBlockHandleRot;
            }
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
