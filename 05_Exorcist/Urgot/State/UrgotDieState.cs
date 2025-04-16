using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UrgotDieState : EnemyState<UrgotStateEnum>
{
    private EnemyUrgot _enemy;

    public UrgotDieState(Enemy enemyBase, EnemyStateMachine<UrgotStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = _enemyBase as EnemyUrgot;
    }

    public override void Enter()
    {
        base.Enter();
        EazySoundManager.PlaySound(_enemy.audioList[0]);
        _enemy.OnDead?.Invoke();
    }

    public override void Exit()
    {
        Debug.Log("죽음");
        GameObject.Destroy(_enemyBase.gameObject);
        base.Exit();
    }
}
