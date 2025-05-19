using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackRange = 2f;
    public LayerMask monsterLayer;

    private Animator animator;
    private float attackCooldown = 0.8f;
    private float lastAttackTime = -999f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("attack");
            TryAttack();
        }
    }

    void TryAttack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, monsterLayer);

        foreach(var hit in hits)
        {
            Monster monster = hit.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(attackDamage);
                Debug.Log($"{monster.name} 에게 {attackDamage} 데미지!");
            }
        }
    }
}
