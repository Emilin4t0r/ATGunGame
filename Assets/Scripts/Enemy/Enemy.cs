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
            int r = Random.Range(0, 2);
            if (r == 0)
                anim.SetTrigger("Die");
            else
                anim.SetTrigger("Die2");
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
        yield return new WaitForSeconds(Random.Range(0, 0.2f));
        anim.speed = Random.Range(0.4f, 0.8f);
        print("TOD: " + Time.time);
        dying = true;
        transform.localEulerAngles += new Vector3(0, Random.Range(0, 360), 0);
        EnemyManager.enemiesLeft--;
        GameManager.instance.kills++;
        Clipboard.instance.UpdateKills();
        Destroy(gameObject, 2f);
    }
}
