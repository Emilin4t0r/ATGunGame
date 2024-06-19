using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    float aliveTime;

    // Update is called once per frame
    void FixedUpdate()
    {
        aliveTime += Time.fixedDeltaTime;
        if (aliveTime > 3f)
        {
            GoStatic();
        }
    }

    void GoStatic()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetChild(0).gameObject.isStatic = true;
        gameObject.isStatic = true;
    }
}
