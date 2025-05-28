using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public float detectionRange = 12f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public float patrolRadius = 5f;
    public float patrolWaitTime = 3f;

    private enum State { Patrol, Chase, Attack }
    private State currentState = State.Patrol;

    private float lastAttackTime;
    private float patrolTimer;
    private Vector3 patrolTarget;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private Monster monster;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        monster = GetComponent<Monster>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        ChooseNewPatrolTarget();
    }

    void Update()
    {
        if (monster == null || monster.IsDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                PatrolLogic();
                if (distance <= detectionRange)
                    currentState = State.Chase;
                break;

            case State.Chase:
                agent.SetDestination(player.position);
                animator.SetBool("WalkForward", false);
                animator.SetBool("Run Forward", true);

                if (distance <= attackRange)
                    currentState = State.Attack;
                else if (distance > detectionRange + 3f)
                    currentState = State.Patrol;
                break;

            case State.Attack:
                agent.ResetPath();
                animator.SetBool("WalkForward", false);
                animator.SetBool("Run Forward", false);
                TryAttack();

                if (distance > attackRange + 1f)
                    currentState = State.Chase;
                break;
        }
    }

    void PatrolLogic()
    {
        animator.SetBool("Run Forward", false);
        animator.SetBool("WalkForward", true);
        agent.SetDestination(patrolTarget);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                ChooseNewPatrolTarget();
                patrolTimer = 0f;
            }
        }
    }

    void ChooseNewPatrolTarget()
    {
        Vector3 randomDir = Random.insideUnitSphere * patrolRadius;
        randomDir += transform.position;

        if (NavMesh.SamplePosition(randomDir, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
            patrolTarget = hit.position;
        else
            patrolTarget = transform.position;
    }

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("Attack1"); 
        }
    }
}
