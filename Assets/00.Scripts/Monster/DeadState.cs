using UnityEngine;

public class DeadState:IMonsterState
{
    public void Enter(MonsterAI monster)
    {
        monster.monster.Die();
        Debug.Log("����: Dead");
    }

    public void Execute(MonsterAI monster)
    {

    }

    public void Exit(MonsterAI monster)
    {

    }
}
