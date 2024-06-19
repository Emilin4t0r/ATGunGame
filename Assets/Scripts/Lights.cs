using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour {

    public static Lights instance;

    public GameObject[] lights;
    public Material lightOn, lightOff;
    int lightsActive = 3;

    private void Start() {
        instance = this;
    }

    public void ShutNextLight() {
        if (lightsActive == 3) {
            lights[0].transform.GetChild(0).GetComponent<Light>().enabled = false;
            lights[0].transform.GetChild(1).GetComponent<MeshRenderer>().material = lightOff;
            lightsActive--;
        } else if (lightsActive == 2) {
            lights[1].transform.GetChild(0).GetComponent<Light>().enabled = false;
            lights[1].transform.GetChild(1).GetComponent<MeshRenderer>().material = lightOff;
            lightsActive--;
        } else if (lightsActive == 1) {
            lights[2].transform.GetChild(0).GetComponent<Light>().enabled = false;
            lights[2].transform.GetChild(1).GetComponent<MeshRenderer>().material = lightOff;
            lightsActive--;
        }        
    }
}
