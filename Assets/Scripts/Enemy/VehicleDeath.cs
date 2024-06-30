using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDeath : MonoBehaviour {

    public GameObject[] parts;
    public float explosionForce;

    private void Start() {
      foreach (GameObject part in parts) {
            Vector3 randomDir = new Vector3(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360));
            part.GetComponent<Rigidbody>().AddForce(randomDir * Random.Range(explosionForce / 2, explosionForce), ForceMode.Impulse);
            part.GetComponent<Rigidbody>().AddTorque(randomDir * Random.Range(explosionForce / 2, explosionForce), ForceMode.Impulse);
        }  
    }
}
