using UnityEngine;
using EZCameraShake;
using System.Collections;

public class GunOperating : MonoBehaviour
{
    public static GunOperating instance;

    public GameObject breechBlockHandle;
    public GameObject breechBlock;
    public GameObject insertableShell;
    public GameObject heShell, apShell;
    public GameObject muzzle, muzzleFlash;

    public float breechBlockHandleRot = 175;
    public float insertableShellDistance = -0.7f;
    public bool gunLoadedAndAiming = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        heShell.SetActive(false);
        apShell.SetActive(false);
        breechBlockHandleRot = breechBlockHandle.transform.localEulerAngles.x;
        insertableShellDistance = insertableShell.transform.localPosition.z;
        gunLoadedAndAiming = false;
    }

    private void Update()
    {
        if (gunLoadedAndAiming)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {                
                Shoot();                
            }
        }
    }

    void Shoot()
    {
        gunLoadedAndAiming = false;
        StartCoroutine(NextViewWaiter());
        CameraShaker.Instance.ShakeOnce(8, 12, 0, 1.5f);
        Sounds.Spawn(transform.position, transform, SoundLibrary.GetClip("atGunFire"), 0, true, 1);
        var mf = Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation, null);
        Destroy(mf, 5);
    }

    IEnumerator NextViewWaiter()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerLookPoints.instance.NextView();
    }

    private void FixedUpdate()
    {
        // BREECH BLOCK
        breechBlockHandle.transform.localEulerAngles = new Vector3(breechBlockHandleRot, 0, 90);
        breechBlock.transform.localPosition = new Vector3(0, (breechBlockHandleRot - 75) * 0.00121f, 0);

        // SHELL
        insertableShell.transform.localPosition = new Vector3(0, 0, insertableShellDistance);        
    }

    public void SetShellType(int t)
    {
        if (t == 0)
        {
            heShell.SetActive(true);
            apShell.SetActive(false);
        } else if (t == 1)
        {
            heShell.SetActive(false);
            apShell.SetActive(true);
        } else
        {
            heShell.SetActive(false);
            apShell.SetActive(false);
        }
    }
}
