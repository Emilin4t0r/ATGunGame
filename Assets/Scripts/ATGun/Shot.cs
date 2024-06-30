using UnityEngine;

public class Shot : MonoBehaviour
{
    // 0 = HE, 1 = AP
    public int shotType;
    public float effectiveArea;
    public GameObject visuals;
    public GameObject expGrnd, expTank;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(shotType + " " + collision.gameObject.tag);
        Vector3 rot = collision.GetContact(0).normal;
        string tag = collision.gameObject.tag;
        if (shotType == 0)
        {
            if (tag == "Vehicle")
            {
                Vehicle v = collision.gameObject.GetComponent<Vehicle>();
                v.TakeDamage(1);
            }
            Detonate(rot);
        } else if (shotType == 1)
        {
            if (tag == "Vehicle")
            {
                Vehicle v = collision.gameObject.GetComponent<Vehicle>();
                v.TakeDamage(2);
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
            foreach (var c in cols)
            {
                if (c.gameObject.CompareTag("Enemy"))
                    c.GetComponent<Enemy>().Die();
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
