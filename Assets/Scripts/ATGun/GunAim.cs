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

    private void FixedUpdate()
    {
        if (!go.gunLoadedAndAiming)
            return;

        x += Input.GetAxis("Mouse X");
        y += Input.GetAxis("Mouse Y");

        x = Mathf.Clamp(x, -10, 10);
        vertical.localEulerAngles = new Vector3(0, x * sensitivity, 0);
        y = Mathf.Clamp(y, -10, 10);
        horizontal.localEulerAngles = new Vector3(-y * sensitivity, 0, 0);

        Vector3 xTargetEul = hControl.transform.localEulerAngles;
        xTargetEul.y = x * controlSensitivity;
        hControl.transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(xTargetEul), Time.fixedDeltaTime * 125);
        Vector3 vTargetEul = vControl.transform.localEulerAngles;
        vTargetEul.y = y * controlSensitivity * 2;
        vControl.transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(vTargetEul), Time.fixedDeltaTime * 125);
    }
}
