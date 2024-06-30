using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EZCameraShake;

public class Vehicle : MonoBehaviour {
    public int lives;
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
        lives = 2;
    }

    private void FixedUpdate() {
        if (lives < 2 && !damaged) {
            damaged = true;
            agent.speed = damagedSpeed;
            //AudioFW.Play("TruckDamaged");
        }
        if (lives < 1) {
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
            GameObject deadTruck = Instantiate(vehicleDeath, transform.position, transform.rotation);
            Destroy(deadTruck, 5f);
            //AudioFW.Play("TruckDie");
        }

        if (damaged) {
            flames.SetActive(true);
        }
    }

    public void TakeDamage(int dmg)
    {
        lives -= dmg;
    }
}
