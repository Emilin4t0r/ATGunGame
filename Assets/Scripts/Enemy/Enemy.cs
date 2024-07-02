using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public Animator anim;
    public GameObject barrelEnd;
    public GameObject mzlFlash;
    bool dying;

    CapsuleCollider col;
    void Start() {
        col = transform.GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate() {
        int rand = Random.Range(1, 300);
        float dist = Vector3.Distance(transform.position, new Vector3(0, 30, 0));
        if (rand == 1 && dist < 600) {
            GameObject flash = Instantiate(mzlFlash, barrelEnd.transform.position, Quaternion.identity, barrelEnd.transform);
            Sounds.Spawn(transform.position, transform, SoundLibrary.GetClip("enemyRifle"));
            Destroy(flash, 0.2f);
        }
    }

    public void Die()
    {
        if (dying)
            return;
        dying = true;
        StartCoroutine(DeathTimeRandomizer());
    }

    IEnumerator DeathTimeRandomizer()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.2f));
        GetComponent<NavMeshAgent>().speed = 0;
        col.enabled = false;
        int r = Random.Range(0, 2);
        if (r == 0)
            anim.SetTrigger("Die");
        else
            anim.SetTrigger("Die2");
        anim.speed = Random.Range(0.4f, 0.8f);
        transform.localEulerAngles += new Vector3(0, Random.Range(0, 360), 0);
        EnemyManager.enemiesLeft--;
        GameManager.instance.kills++;
        Clipboard.instance.UpdateKills();
        Destroy(gameObject, 2f);
    }
}
