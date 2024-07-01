using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EZCameraShake;

public class Vehicle : MonoBehaviour {
    public float startHealth;
    float health;
    public GameObject vehicleDeath;
    NavMeshAgent agent;
    bool damaged;
    public GameObject flames;

    public float startSpeed, damagedSpeed;
    GameManager gm;

    void Start() {
        agent = transform.GetComponent<NavMeshAgent>();
        agent.speed = startSpeed;
        gm = GameManager.instance;
        health = startHealth;
    }

    private void FixedUpdate() {
        if (health < startHealth / 2 && !damaged) {
            damaged = true;
            agent.speed = damagedSpeed;
            //AudioFW.Play("TruckDamaged");
        }
        if (health <= 0) {
            EnemyManager.enemiesLeft--;

            gm.kills++;
            Clipboard.instance.ChangeKills(gm.kills.ToString());
            Destroy(gameObject);
            Collider[] cols = Physics.OverlapSphere(transform.position, 25);
            foreach(Collider col in cols)
            {
                if (col.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemy = col.GetComponent<Enemy>();
                    EnemyManager.enemiesLeft--;
                    gm.kills++;
                    Clipboard.instance.ChangeKills(gm.kills.ToString());
                    Destroy(enemy.gameObject, 0.5f);
                    enemy.dying = true;
                    gm.CallShowHitMark(false);
                }
            }
            GameObject deadVeh = Instantiate(vehicleDeath, transform.position, transform.GetChild(0).rotation);
            Destroy(deadVeh, 8f);
            //AudioFW.Play("TruckDie");
        }

        if (damaged) {
            flames.SetActive(true);
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }
}
