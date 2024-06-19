
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour {

    public GameObject enemy;
    public GameObject truck;
    public Clipboard clipBoard;
    public GameObject[] spawnPoints;
    public GameObject[] goalPoints;
    public static int enemiesLeft = 0;
    int lastAmt;
    public int currentWave;
    float nextWaveTimer = 0;

    private void Start() {
        lastAmt = 3;
        currentWave = 0;
        clipBoard.ChangeWave(currentWave.ToString());
        nextWaveTimer = 0;
    }
    void FixedUpdate() {
        if (enemiesLeft == 0 && !GameManager.instance.gameLost) {
            if (nextWaveTimer > 5) {
                int nextAmt = lastAmt + Random.Range(2, 5);
                currentWave++;
                clipBoard.ChangeWave(currentWave.ToString());
                int trucks;
                if (currentWave % 3 == 0) {
                    trucks = 1;
                } else if (currentWave % 10 == 0) {
                    trucks = 2;
                } else {
                    trucks = 0;
                }
                GenerateWave(nextAmt, trucks);
                nextWaveTimer = 0;
                clipBoard.ChangeActiveInfo("game");
            } else {
                nextWaveTimer += Time.deltaTime;
                clipBoard.ChangeWaveTimerTime((5 - nextWaveTimer).ToString("0"));
                if (clipBoard.gameInfo.activeSelf == true) {
                    clipBoard.ChangeActiveInfo("nextwave");
                }
            }
        }
    }

    void GenerateWave(int enemies, int trucks) {
        for (int i = 0; i < enemies; ++i) {
            int randSpawn = Random.Range(0, spawnPoints.Length);
            int randGoal = Random.Range(0, goalPoints.Length);
            GameObject newEnemy = Instantiate(enemy, spawnPoints[randSpawn].transform.position, Quaternion.identity);
            NavMeshAgent agent = newEnemy.GetComponent<NavMeshAgent>();
            float randSpeed = Random.Range(6.5f, 10.5f);
            agent.destination = goalPoints[randGoal].transform.position;
            agent.speed = randSpeed;
            newEnemy.transform.GetChild(0).GetComponent<Animator>().SetFloat("RunSpeed", randSpeed / 8);
            enemiesLeft = enemies;
            lastAmt = enemies;
        }
        if (trucks > 0) {
            for (int i = 0; i < trucks; i++) {
                int randSpawn = Random.Range(0, spawnPoints.Length);
                int randGoal = Random.Range(0, goalPoints.Length);
                GameObject newTruck = Instantiate(truck, spawnPoints[randSpawn].transform.position, Quaternion.identity);
                NavMeshAgent agent = newTruck.GetComponent<NavMeshAgent>();
                agent.destination = goalPoints[randGoal].transform.position;
                enemiesLeft++;
                lastAmt++;
            }
        }
        if (trucks == 0) {
            //AudioFW.Play("NewWave");
        } else {
            //AudioFW.Play("NewWaveTruck");
        }
    }
}
