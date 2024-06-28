using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    GunOperating go;
    public Transform plrMoveSpot, plrLookSpot;

    void Start()
    {
        go = GunOperating.instance;
    }

    void FixedUpdate()
    {
        if (go.gunLoadedAndAiming && !PlayerLookPoints.instance.movementInProgress)
        {
            transform.position = plrMoveSpot.position;
            transform.LookAt(plrLookSpot.position);
        }
    }
}
