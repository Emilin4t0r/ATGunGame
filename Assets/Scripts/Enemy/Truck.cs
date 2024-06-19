using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EZCameraShake;

public class Truck : MonoBehaviour {
    public int lives;
    public GameObject truckDeath;
    NavMeshAgent agent;
    bool damaged;
    public GameObject flames;

    public float startSpeed, damagedSpeed;
    public GameObject[] wheels = new GameObject[4];
    GameManager gm;

    MeshCollider col;
    void Start() {
        col = transform.GetComponent<MeshCollider>();
        agent = transform.GetComponent<NavMeshAgent>();
        agent.speed = startSpeed;
        gm = GameManager.instance;
        lives = 25;
    }

    private void FixedUpdate() {

        RotateWheels(350);

        if (lives < 15 && !damaged) {
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
            GameObject deadTruck = Instantiate(truckDeath, transform.position, transform.rotation);
            Destroy(deadTruck, 5f);
            CameraShaker.GetInstance("Main Camera").ShakeOnce(7.5f, 5f, 0.0f, 2.5f);
            //AudioFW.Play("TruckDie");
        }

        if (damaged) {
            flames.SetActive(true);
        }
    }

    void RotateWheels(float speed) {
        foreach (GameObject wheel in wheels) {
            wheel.transform.Rotate(-Time.deltaTime * speed, 0, 0);
        }
    }
}
