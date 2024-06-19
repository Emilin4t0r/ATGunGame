using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckDeath : MonoBehaviour {

    public GameObject[] parts = new GameObject[6];
    private void Start() {
      foreach (GameObject part in parts) {
            Vector3 randomDir = new Vector3(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360));
            part.GetComponent<Rigidbody>().AddForce(randomDir * Random.Range(2f, 3f), ForceMode.Impulse);
        }  
    }
}
