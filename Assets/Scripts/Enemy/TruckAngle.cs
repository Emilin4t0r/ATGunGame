using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckAngle : MonoBehaviour {

    public LayerMask terrainLayer;

    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, -transform.parent.up, out hit, 2, terrainLayer)) {
            Quaternion slopeRotation = Quaternion.FromToRotation(transform.up, hit.normal);
            transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation * transform.rotation, 2 * Time.deltaTime);
            float x, z;
            x = transform.localEulerAngles.x;
            z = transform.localEulerAngles.z;
            transform.localEulerAngles = new Vector3(x, 0, z);
        }
    }
}
