using UnityEngine;

public interface IMonsterState
{
    void Enter(MonsterAI monster);
    void Execute(MonsterAI monster);
    void Exit(MonsterAI monster);

}
