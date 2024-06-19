using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    [Tooltip("0 = HE, 1 = AP")]
    public int ammoType;
    void OnMouseDown()
    {
        Sounds.Spawn(transform.position, transform, SoundLibrary.GetClip("shellGrab"));
        GunOperating.instance.SetShellType(ammoType);
        PlayerLookPoints.instance.NextView();
    }
}
