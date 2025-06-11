using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IMonsterState
{
    private Vector3 patrolTarget;
    private float patrolTimer;

   public void Enter(MonsterAI monster)
   {
        monster.agent.isStopped = false;
        monster.animator.SetBool("WalkForward", true);
        patrolTimer = 0f;
        SetNewPatrolTarget(monster);
        Debug.Log("상태: Patrol");
    }

    public void Execute(MonsterAI monster)
    {
        float distance = Vector3.Distance(monster.transform.position, monster.player.position);

        if (distance <= monster.detectionRange)
        {
            monster.ChangeState(new ChaseState());
            return;
        }

        monster.agent.SetDestination(patrolTarget);

        if (!monster.agent.pathPending && (monster.agent.remainingDistance < 0.5f || monster.agent.velocity.magnitude < 0.1f))
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= monster.patrolWaitTime)
            {
                SetNewPatrolTarget(monster);
                patrolTimer = 0f;
            }
        }
    }

    public void Exit(MonsterAI monster)
    {
        monster.animator.SetBool("WalkForward", false);
        Debug.Log("Patrol -> 다른 상태로 변경");
    }

    private void SetNewPatrolTarget(MonsterAI monster)
    {
        Vector3 randomDir = Random.insideUnitSphere * monster.patrolRadius;
        randomDir.y = 0f;
        Vector3 target = monster.transform.position + randomDir;

        if (NavMesh.SamplePosition(target, out NavMeshHit hit, monster.patrolRadius, NavMesh.AllAreas))
        {
            patrolTarget = hit.position;
        }
        else
        {
            patrolTarget = monster.transform.position + monster.transform.forward * 5f;
        }

        Debug.Log($"New Patrol Target: {patrolTarget}");
    }
}
