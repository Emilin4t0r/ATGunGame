using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject sandImpact;
    public GameObject metalImpact;

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag("Terrain")) {
            GameObject impact = Instantiate(sandImpact, transform.position, sandImpact.transform.rotation);
            float randScale = Random.Range(0.25f, 1.5f);
            impact.transform.localScale = (new Vector3(randScale, randScale, randScale));
            Destroy(impact, 1f);        
        } else if (collision.transform.CompareTag("Truck") || collision.transform.CompareTag("Metal")) {
            //spark hit effect
            GameObject mImpact = Instantiate(metalImpact, transform.position, metalImpact.transform.rotation);
            float randScale = Random.Range(1f, 3f);
            mImpact.transform.localScale = (new Vector3(randScale, randScale, randScale));
            Destroy(mImpact, 1f);
        }
    }
}
