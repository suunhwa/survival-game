using UnityEngine;

public class WeaponColliderHandler : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("몬스터 공격!");
            other.GetComponent<Monster>()?.TakeDamage(damage);
        }
    }
}
