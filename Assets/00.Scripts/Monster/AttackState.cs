using UnityEngine;

public class AttackState:IMonsterState
{
    private float lastAttackTime;

    public void Enter(MonsterAI monster)
    {
        monster.agent.ResetPath();
        Debug.Log("상태: Attack");
    }

    public void Execute(MonsterAI monster)
    {
        float distance = Vector3.Distance(monster.transform.position, monster.player.position);

        if(distance > monster.attackRange + 1f)
        {
            monster.ChangeState(new ChaseState());
            return;
        }

        if(Time.time - lastAttackTime >= monster.attackCooldown)
        {
            lastAttackTime = Time.time;

            int rand = Random.Range(0, 4);
            string[] attackAnims = { "Attack1", "Attack2", "Attack3", "Attack5" };

            monster.animator.SetTrigger(attackAnims[rand]);
        }
    }

    public void Exit(MonsterAI monster)
    {
        Debug.Log("Attack -> 다른 상태로 변경");
    }
}
