using UnityEngine;

public class MaximGun : MonoBehaviour
{
    public float rotationSpeed = 2.0f;
    public float accuracy = 0.05f;
    public float shootForce = 10;
    public GameObject mFlash, mgShot;
    public Transform muzzle;
    private Transform target;

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

            Shoot();
        }
        else if (EnemyManager.enemiesLeft > 0)
        {           
            // Try to find a new target
            FindNewTarget();
        }

        // Ensure the local Z-axis rotation is always 0
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.z = 0;
        transform.rotation = Quaternion.Euler(eulerAngles);
    }

    void Shoot()
    {
        //Bullet spread calculations
        Vector3 deviation3D = Random.insideUnitCircle * accuracy;
        Quaternion rot = Quaternion.LookRotation(Vector3.forward + deviation3D);
        Vector3 fwd = transform.rotation * rot * Vector3.forward;

        bool enemyHit = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Destroy(hit.transform.gameObject, 0.5f);
                hit.transform.GetComponent<Enemy>().dying = true;
                enemyHit = true;

                //blood splatter effect
                /*GameObject impact = Instantiate(bodyImpact, hit.transform.position, bodyImpact.transform.rotation);
                float randScale = Random.Range(2f, 3f);
                impact.transform.localScale = (new Vector3(randScale, randScale, randScale));
                Destroy(impact, 1f);*/
            }            
        }
        GameObject newShot = Instantiate(mgShot, transform.position, transform.parent.rotation);
        newShot.GetComponent<Rigidbody>().AddForce(fwd * shootForce, ForceMode.Impulse);
        if (enemyHit)
        {
            Destroy(newShot, 0.1f);
            hit.transform.GetComponent<Enemy>().Die();
        }
        else
        {
            Destroy(newShot, 1f);
        }

        GameObject flash = Instantiate(mFlash, muzzle.position, Quaternion.identity);
        Destroy(flash, 0.02f);
    }

    void FindNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            target = enemies[Random.Range(0, enemies.Length)].transform;
        }
        else
        {
            target = null;
        }
        print("Target: " + target);
    }
}
