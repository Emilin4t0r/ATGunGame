using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Sounds : MonoBehaviour
{
    public GameObject soundPrefab;
    public static GameObject staticSoundPrefab;

    static AudioSource sourceToFadeOut;

    private void Awake()
    {
        staticSoundPrefab = soundPrefab;
    }

    private void Update()
    {
        if (sourceToFadeOut)
        {
            sourceToFadeOut.volume *= 0.5f;
        }
    }

    public static void EndLoop(GameObject soundObj)
    {
        if (soundObj)
        {
            sourceToFadeOut = soundObj.GetComponent<AudioSource>();
            Destroy(sourceToFadeOut.gameObject, 0.5f);
        }
        else
        {
            return;
        }
    }

    public static GameObject Spawn(Vector3 pos, Transform parent, AudioClip clip, float spatialBlend = 0, bool randomPitch = true, float volume = 0.75f)
    {
        var sound = Instantiate(staticSoundPrefab, pos, parent.rotation, parent);
        var aSource = sound.GetComponent<AudioSource>();
        if (randomPitch)
            aSource.pitch = Random.Range(0.95f, 1.05f);
        aSource.spatialBlend = spatialBlend;
        aSource.clip = clip;
        aSource.volume = volume;
        aSource.Play();
        Destroy(sound, 5);
        return sound;
    }

    public static void SpawnSoundWithPitch(Vector3 pos, Transform parent, AudioClip clip, float spatialBlend = 0, float pitch = 1, float volume = 0.75f)
    {
        var sound = Instantiate(staticSoundPrefab, pos, parent.rotation, parent);
        var aSource = sound.GetComponent<AudioSource>();
        aSource.spatialBlend = spatialBlend;
        aSource.pitch = pitch;
        aSource.clip = clip;
        aSource.volume = volume;
        aSource.Play();
        Destroy(sound, 5);
    }

    public static GameObject SpawnLoop(Vector3 pos, Transform parent, AudioClip clip, float spatialBlend = 0, bool randomPitch = true, float volume = 0.75f)
    {
        var sound = Instantiate(staticSoundPrefab, pos, parent.rotation, parent);
        var aSource = sound.GetComponent<AudioSource>();
        if (randomPitch)
            aSource.pitch = Random.Range(0.9f, 1.1f);
        aSource.clip = clip;
        aSource.spatialBlend = spatialBlend;
        aSource.loop = true;
        aSource.volume = volume;
        aSource.Play();
        return sound;
    }
}