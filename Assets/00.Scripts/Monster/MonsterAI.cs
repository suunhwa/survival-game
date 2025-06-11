using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public float detectionRange = 12f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public float patrolRadius = 30f;
    public float patrolWaitTime = 3f;

    public Transform player;
    public NavMeshAgent agent;
    public Animator animator;
    public Monster monster;

    private IMonsterState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        monster = GetComponent<Monster>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        ChangeState(new PatrolState());
    }

    void Update()
    {
        if (monster.IsDead || player == null) return;
        currentState?.Execute(this);
    }

    public void ChangeState(IMonsterState state)
    {
        currentState?.Exit(this);  
        currentState = state;
        currentState.Enter(this);
    }
}
