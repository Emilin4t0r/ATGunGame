using UnityEngine;

public class Shot : MonoBehaviour
{
    // 0 = HE, 1 = AP
    public int shotType;
    public float effectiveArea;
    [Range(0, 100)]
    public float deadliness;
    public GameObject visuals;
    public GameObject expGrnd, expTank;
    Rigidbody rb;
    float vel;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10);
        
        if (shotType == 1)
        {
            vel = rb.linearVelocity.magnitude;
            if (vel < 50 && vel != 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 rot = collision.GetContact(0).normal;
        string tag = collision.gameObject.tag;
        if (shotType == 0)
        {
            if (tag == "Vehicle")
            {
                Vehicle v = collision.gameObject.GetComponent<Vehicle>();
                v.TakeDamage(50);
            }
            Detonate(rot);
        }
        else if (shotType == 1)
        {
            if (tag == "Vehicle")
            {
                Vehicle v = collision.gameObject.GetComponent<Vehicle>();
                v.TakeDamage(vel);
                Detonate(rot);
            }
            if (tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().Die();
                Detonate(rot);
            }
        }
    }

    void Detonate(Vector3 rot)
    {
        GameObject exObj = shotType == 0 ? expGrnd : expTank;
        var ex = Instantiate(exObj, transform.position, Quaternion.identity, null);
        ex.transform.eulerAngles = rot;
        visuals.transform.parent = null;

        if (shotType == 0)
        {
            EZCameraShake.CameraShaker.GetInstance(CamerasController.instance.activeCam).ShakeOnce(10, 10, 0, 0.7f);
            Sounds.Spawn(transform.position, ex.transform, SoundLibrary.GetClip("shot_expl_grnd"));
            Collider[] cols = Physics.OverlapSphere(transform.position, effectiveArea);
            for (int i = 0; i < cols.Length; ++i)
            {
                if (cols[i].gameObject.CompareTag("Enemy"))
                {
                    float dist = Vector3.Distance(transform.position, cols[i].transform.position);
                    float enemyLuck = Random.Range(0f, 50);
                    enemyLuck += dist / 2;
                    if (enemyLuck < deadliness)
                    {
                        cols[i].GetComponent<Enemy>().Die();
                    }
                }
            }
        }
        else if (shotType == 1)
        {
            EZCameraShake.CameraShaker.GetInstance(CamerasController.instance.activeCam).ShakeOnce(6, 10, 0, 0.35f);
            Sounds.Spawn(transform.position, ex.transform, SoundLibrary.GetClip("shot_expl_tank"));
        }

        Destroy(visuals.gameObject, 0.2f);
        Destroy(ex.gameObject, 3);
        Destroy(transform.gameObject);
    }
}
