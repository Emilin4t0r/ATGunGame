using UnityEngine;

public class Shot : MonoBehaviour
{
    // 0 = HE, 1 = AP
    public int shotType;
    public GameObject smoke;
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
        if (collision.gameObject.CompareTag("Terrain"))
        {
            if (shotType == 0)
            {
                Detonate(shotType, rot);
            }
        } else if (collision.gameObject.CompareTag("Metal"))
        {
            Tank tank = collision.gameObject.GetComponent<Tank>();
            if (shotType == 1)
            {
                tank.TakeDamage(2);
            } else if (shotType == 0)
            {
                tank.TakeDamage(1);
            }
            Detonate(shotType, rot);
        }
    }

    void Detonate(int type, Vector3 rot)
    {
        if (type == 0)
            EZCameraShake.CameraShaker.Instance.ShakeOnce(10, 10, 0, 0.7f);
        GameObject exp = type == 0 ? expGrnd : expTank;
        var ex = Instantiate(exp, transform.position, Quaternion.identity, null);
        ex.transform.eulerAngles = rot;
        smoke.transform.parent = null;
        Destroy(smoke.gameObject, 0.2f);
        Destroy(ex.gameObject, 5);
        Destroy(transform.gameObject);
    }
}
