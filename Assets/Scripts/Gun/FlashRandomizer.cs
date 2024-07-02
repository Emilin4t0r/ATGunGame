using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashRandomizer : MonoBehaviour {

    void FixedUpdate() {
        transform.localEulerAngles = new Vector3(0, 180, Random.Range(0, 360));
        float randScale = Random.Range(16, 20);
        transform.localScale = new Vector3 (randScale, randScale, randScale);

        int randomShow = Random.Range(0, 10);
        if (randomShow == 1) {
            gameObject.SetActive(false);
        }
    }
}
