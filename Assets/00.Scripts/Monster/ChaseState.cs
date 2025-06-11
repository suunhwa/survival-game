using UnityEngine;

public class ChaseState:IMonsterState
{
    public void Enter(MonsterAI monster)
    {
        monster.animator.SetBool("Run Forward", true);
        Debug.Log("����: Chase");
    }

    public void Execute(MonsterAI monster)
    {
        float distance = Vector3.Distance(monster.transform.position, monster.player.position);

        monster.agent.SetDestination(monster.player.position);

        if(distance <= monster.attackRange)
        {
            monster.ChangeState(new AttackState());
        }
        else if(distance > monster.detectionRange + 3f)
        {
            monster.ChangeState(new IdleState());
        }
    }

    public void Exit(MonsterAI monster)
    {
        monster.animator.SetBool("Run Forward", false);
        Debug.Log("Chase -> �ٸ� ���·� ����");
    }
}
