using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject hitbox;

    public void EnableHitbox()
    {
        hitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        hitbox.SetActive(false);
    }
}
