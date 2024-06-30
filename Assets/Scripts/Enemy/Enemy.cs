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
    void Start() {
        col = transform.GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate() {
        if (dying) {
            col.enabled = false;
            anim.SetTrigger("Die");
        }
        int rand = Random.Range(1, 300);
        float dist = Vector3.Distance(transform.position, new Vector3(0, 30, 0));
        if (rand == 1 && dist < 600) {
            GameObject flash = Instantiate(mzlFlash, barrelEnd.transform.position, Quaternion.identity, barrelEnd.transform);
            //AudioFW.PlayRandomPitch("EnemyRifle");
            Destroy(flash, 0.2f);
        }
    }

    public void Die()
    {
        StartCoroutine(DeathTimeRandomizer());        
    }

    IEnumerator DeathTimeRandomizer()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.15f));
        dying = true;
        transform.localEulerAngles += new Vector3(0, Random.Range(0, 360), 0);
        EnemyManager.enemiesLeft--;
        GameManager.instance.kills++;
        Clipboard.instance.UpdateKills();
        Destroy(gameObject, 0.5f);
    }
}
