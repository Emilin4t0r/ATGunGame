using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashRandomizer : MonoBehaviour {

    void FixedUpdate() {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        float randScale = Random.Range(5, 7);
        transform.localScale = new Vector3 (randScale, randScale, randScale);

        int randomShow = Random.Range(0, 4);
        if (randomShow == 1) {
            gameObject.SetActive(false);
        }
    }
}
