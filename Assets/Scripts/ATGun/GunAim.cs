using UnityEngine;

public class GunAim : MonoBehaviour
{
    float x, y;
    public float sensitivity;
    public float controlSensitivity = 50f;
    public Transform horizontal, vertical;
    public Transform hControl, vControl;
    GunOperating go;

    private void Start()
    {
        go = GunOperating.instance;
    }

    private void Update()
    {
        if (!go.gunLoadedAndAiming)
            return;

        x += Input.GetAxis("Mouse X") * sensitivity;
        y += Input.GetAxis("Mouse Y") * sensitivity;

        x = Mathf.Clamp(x, -15, 15);
        vertical.localEulerAngles = new Vector3(0, x ,0);
        y = Mathf.Clamp(y, -15, 15);
        horizontal.localEulerAngles = new Vector3(-y ,0, 0);

        Vector3 xTargetEul = hControl.transform.localEulerAngles;
        xTargetEul.y = x * controlSensitivity;
        hControl.transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(xTargetEul), Time.fixedDeltaTime);
        Vector3 vTargetEul = vControl.transform.localEulerAngles;
        vTargetEul.y = y * controlSensitivity * 2;
        vControl.transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(vTargetEul), Time.fixedDeltaTime);
    }
}
