using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrgotStingState : EnemyState<UrgotStateEnum>
{
    private EnemyUrgot _enemy;

    public UrgotStingState(Enemy enemyBase, EnemyStateMachine<UrgotStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = _enemyBase as EnemyUrgot;
    }

    public override void Enter()
    {
        base.Enter();

        _enemyBase.StopImmediately(false);
        EazySoundManager.PlaySound(_enemy.audioList[2]);
    }

    public override void Exit()
    {
        _enemyBase.lastTimeAttacked = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_triggerCalled)
        {
            _stateMachine.ChangeState(UrgotStateEnum.Battle);
        }
    }
}
