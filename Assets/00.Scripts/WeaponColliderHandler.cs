using UnityEngine;

public class WeaponColliderHandler : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("���� ����!");
            other.GetComponent<Monster>()?.TakeDamage(damage);
        }
    }
}
