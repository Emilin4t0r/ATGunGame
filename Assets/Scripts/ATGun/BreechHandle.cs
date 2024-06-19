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
                var es = Instantiate(emptyShell, shellSpitOutSpot.position, shellSpitOutSpot.rotation, null);
                Rigidbody esRb = es.GetComponent<Rigidbody>();
                esRb.AddForce(-es.transform.forward * 10, ForceMode.Impulse);
                StartCoroutine(ApplyTorqueToESDelayed(esRb));
                Sounds.Spawn(transform.position, transform, SoundLibrary.GetClip("openLeverFull"));
                StartCoroutine(NextViewWaiter());
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
