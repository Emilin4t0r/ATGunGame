using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public Animator anim;
    public bool dying;
    public GameObject barrelEnd;
    public GameObject mzlFlash;

    CapsuleCollider col;
    float randDieRot;
    Vector3 dieRot;
    void Start() {
        col = transform.GetComponent<CapsuleCollider>();
        randDieRot = Random.Range(-45, 45);
        dieRot = new Vector3(0, transform.localEulerAngles.y + randDieRot, 0);
    }

    private void FixedUpdate() {
        if (dying) {
            col.enabled = false;
            anim.SetTrigger("Die");
            transform.localEulerAngles = dieRot;
        }
        int rand = Random.Range(1, 300);
        float dist = Vector3.Distance(transform.position, new Vector3(0, 30, 0));
        if (rand == 1 && dist < 150) {
            GameObject flash = Instantiate(mzlFlash, barrelEnd.transform.position, Quaternion.identity, barrelEnd.transform);
            //AudioFW.PlayRandomPitch("EnemyRifle");
            Destroy(flash, 0.2f);
        }
    }
}
