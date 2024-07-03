using System.Collections;
using UnityEngine;
//using System.

public class MaximGun : MonoBehaviour
{
    public float rotationSpeed = 2.0f;
    public float accuracy = 0.05f;
    public float shootForce = 10;
    public float fireRate;
    float timeToFire;
    public GameObject mFlash, mgShot;
    public Transform muzzle;
    private Transform target;

    public GameObject muzzleParticle;
    public float minBurstLength, maxBurstLength;
    float timeToEndBurst, timeToStartBurst;
    public bool isFiring;
    GameObject mgSound;

    void Start()
    {
        FindNewTarget();
    }

    void FixedUpdate()
    {
        if (target)
        {
            // Rotate towards the target
            Vector3 direction = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);

            if (isFiring)
            {
                if (Time.time > timeToFire)
                {
                    Shoot();
                }
                if (Time.time > timeToEndBurst)
                {
                    EndBurst();
                }
            }
            else
            {
                if (Time.time > timeToStartBurst)
                {
                    StartBurst();
                }
            }
        }
        else if (EnemyManager.enemiesLeft > 0)
        {           
            // Try to find a new target
            FindNewTarget();
        } else
        {
            if (isFiring)
                EndBurst();
        }

        // Ensure the local Z-axis rotation is always 0
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.z = 0;
        transform.rotation = Quaternion.Euler(eulerAngles);
    }

    void StartBurst()
    {
        float rand = Random.Range(minBurstLength, maxBurstLength);
        timeToEndBurst = Time.time + rand;        
        isFiring = true;
        mgSound = Sounds.SpawnLoop(transform.position, transform, SoundLibrary.GetClip("kk_fire2"), 0, false);
    }

    void EndBurst()
    {
        float rand = Random.Range(minBurstLength, maxBurstLength);
        timeToStartBurst = Time.time + rand;
        muzzleParticle.SetActive(false);
        isFiring = false;
        Sounds.EndLoop(mgSound);
        Sounds.Spawn(transform.position, transform, SoundLibrary.GetClip("kk_tail"));
    }

    void Shoot()
    {
        //Bullet spread calculations
        Vector3 deviation3D = Random.insideUnitCircle * accuracy;
        Quaternion rot = Quaternion.LookRotation(Vector3.forward + deviation3D);
        Vector3 fwd = transform.rotation * rot * Vector3.forward;

        GameObject newShot = Instantiate(mgShot, muzzle.position, transform.parent.rotation);
        newShot.GetComponent<Rigidbody>().AddForce(fwd * shootForce, ForceMode.Impulse);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                StartCoroutine(EnemyHit(newShot, hit));

                //blood splatter effect
                /*GameObject impact = Instantiate(bodyImpact, hit.transform.position, bodyImpact.transform.rotation);
                float randScale = Random.Range(2f, 3f);
                impact.transform.localScale = (new Vector3(randScale, randScale, randScale));
                Destroy(impact, 1f);*/
            }            
        } else
        {
            Destroy(newShot, 1f);
        }
        

        muzzleParticle.SetActive(true);
        GameObject flash = Instantiate(mFlash, muzzle.position, Quaternion.identity);
        Destroy(flash, 0.02f);

        timeToFire = Time.time + fireRate;
    }

    IEnumerator EnemyHit(GameObject shot, RaycastHit hit)
    {
        hit.transform.GetComponent<CapsuleCollider>().enabled = false;
        hit.transform.tag = "Untagged";
        target = null;
        yield return new WaitForSeconds(0.25f);
        Destroy(shot);
        hit.transform.GetComponent<Enemy>().Die();
    }

    void FindNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        target = null;

        foreach (GameObject enemy in enemies)
        {
            Transform enemyTransform = enemy.transform;
            Vector3 directionToEnemy = enemyTransform.position - muzzle.position;
            float distanceToEnemy = directionToEnemy.magnitude;

            // Perform a raycast from the shooter to the enemy
            RaycastHit hit;
            if (Physics.Raycast(muzzle.position, directionToEnemy, out hit, distanceToEnemy))
            {
                // Check if the raycast hit the enemy
                if (hit.transform == enemyTransform)
                {
                    target = enemyTransform;
                    break;  // Exit the loop as we've found a valid target
                }
            }
        }        
    }
}
