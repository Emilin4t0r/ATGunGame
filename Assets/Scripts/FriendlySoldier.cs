using UnityEngine;

public class FriendlySoldier : MonoBehaviour
{
    public string animTrigger;
    public Animator anim;

    private void Start()
    {
        anim.SetTrigger(animTrigger);
    }
}
