using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var hp = other.GetComponent<PlayerHealth>();
            if (hp != null)
                hp.TakeDamage(damage);
        }
    }
}
