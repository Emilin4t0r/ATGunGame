using UnityEngine;

public class MGBurstRandomizer : MonoBehaviour
{
    public GameObject mgEffect;
    public float minBurstLength, maxBurstLength;
    float timeToEndBurst, timeToStartBurst;
    bool isFiring;
    GameObject mgSound;

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
        mgSound = Sounds.SpawnLoop(transform.position, transform, SoundLibrary.GetClip("tank_mg2"));
    }

    void EndBurst()
    {
        float rand = Random.Range(minBurstLength * 2, maxBurstLength * 2);
        timeToStartBurst = Time.time + rand;
        mgEffect.SetActive(false);
        isFiring = false;
        Sounds.EndLoop(mgSound);
        Sounds.Spawn(transform.position, transform, SoundLibrary.GetClip("tank_mg_tail"));
    }
}
