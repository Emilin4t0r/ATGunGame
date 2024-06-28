using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    [Tooltip("0 = HE, 1 = AP")]
    public int ammoType;
    GunOperating go;

    private void Start()
    {
        go = GunOperating.instance;
    }

    void OnMouseDown()
    {
        Sounds.Spawn(transform.position, transform, SoundLibrary.GetClip("shellGrab"));
        go.SetShellType(ammoType);
        go.loadedShotType = ammoType;
        PlayerLookPoints.instance.NextView();
    }
}
