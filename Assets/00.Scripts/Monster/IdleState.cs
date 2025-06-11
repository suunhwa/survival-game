using UnityEngine;

public class IdleState:IMonsterState
{
    private float enterTime;
    private float idleDuration = 3f;

    public void Enter(MonsterAI monster)
    {
        Debug.Log("상태: IDLE");
        enterTime = Time.time;

        monster.agent.isStopped = true;
        monster.agent.ResetPath();

        monster.animator.SetBool("WalkForward", false);
        monster.animator.SetBool("Run Forward", false);
        monster.animator.SetBool("Idle", true);
    }

    public void Execute(MonsterAI monster)
    {
        float distance = Vector3.Distance(monster.transform.position, monster.player.position);

        if (distance < monster.detectionRange)
        {
            monster.ChangeState(new ChaseState());
            return;
        }

        if(Time.time - enterTime >= idleDuration)
        {
            monster.ChangeState(new PatrolState());
        }
    }

    public void Exit(MonsterAI monster)
    {
        monster.animator.SetBool("Idle", false);
        Debug.Log("Idle -> 다른 상태로 변경");
    }
}
