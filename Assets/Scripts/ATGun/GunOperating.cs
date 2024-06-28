using UnityEngine;
using EZCameraShake;
using System.Collections;

public class GunOperating : MonoBehaviour
{
    public static GunOperating instance;

    public GameObject shotPrefab;
    public float shootForce;
    [Space(10)]
    public GameObject breechBlockHandle;
    public GameObject breechBlock;
    public GameObject insertableShell;
    public GameObject heShell, apShell;
    public GameObject muzzle, muzzleFlash;
    public Animator barrelAnim;

    public float breechBlockHandleRot = 175;
    public float insertableShellDistance = -0.7f;
    public bool gunLoadedAndAiming = false;
    bool shotFired = false;

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
            if (shotFired)
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    gunLoadedAndAiming = false;
                    shotFired = false;
                }
            }
        }
    }

    void Shoot()
    {                
        CameraShaker.Instance.ShakeOnce(10, 15, 0, 1.5f);
        Sounds.Spawn(transform.position, transform, SoundLibrary.GetClip("atGunFire"), 0, true, 1);
        var mf = Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation, null);
        Destroy(mf, 5);
        barrelAnim.SetTrigger("Fire");        
        Cursor.lockState = CursorLockMode.None;

        var shot = Instantiate(shotPrefab, muzzle.transform.position, muzzle.transform.rotation, null);
        shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * shootForce, ForceMode.Impulse);
        Destroy(shot, 3f);
        StartCoroutine(NextViewWaiter());
        shotFired = true;
    }

    IEnumerator NextViewWaiter()
    {
        yield return new WaitForSeconds(1f);
        while (Input.GetKey(KeyCode.Mouse0))
        {
            yield return null;
        }
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
