using UnityEngine;

public class Monster : MonoBehaviour
{
    public int maxHealth = 100;
    public int damage = 10;
    public string dropItemName = "Meat";

    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int d)
    {
        if (isDead)
            return;

        currentHealth -= d;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        Invoke(nameof(DropItem), 1.5f);
        Destroy(gameObject, 3f);
    }

    private void DropItem()
    {
        Vector3 dropPos = transform.position + Vector3.up * 0.5f;
        ItemPoolManager.Instance.Spawn(dropItemName, "Food", 1, dropPos);
    }
}
