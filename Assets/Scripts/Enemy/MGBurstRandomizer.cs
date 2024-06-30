using UnityEngine;

public class MGBurstRandomizer : MonoBehaviour
{
    public GameObject mgEffect;
    public float minBurstLength, maxBurstLength;
    float timeToEndBurst, timeToStartBurst;
    bool isFiring;

    private void Start()
    {
        StartBurst();
    }

    private void FixedUpdate()
    {
        if (isFiring)
        {
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

    void StartBurst()
    {
        float rand = Random.Range(minBurstLength, maxBurstLength);
        timeToEndBurst = Time.time + rand;
        mgEffect.SetActive(true);
        isFiring = true;
    }

    void EndBurst()
    {
        float rand = Random.Range(minBurstLength * 2, maxBurstLength * 2);
        timeToStartBurst = Time.time + rand;
        mgEffect.SetActive(false);
        isFiring = false;
    }
}
