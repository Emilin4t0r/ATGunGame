using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy") || other.CompareTag("Vehicle")) {
            Destroy(other.gameObject);
            EnemyManager.enemiesLeft--;
            GameManager.instance.lives--;
            //Lights.instance.ShutNextLight();
            if (GameManager.instance.lives >= 0) {
                //AudioFW.Play("LoseLife");
            }
        }
    }
}
