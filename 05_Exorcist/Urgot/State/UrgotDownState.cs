using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrgotDownState : EnemyState<UrgotStateEnum>
{
    private EnemyUrgot _enemy;

    public UrgotDownState(Enemy enemyBase, EnemyStateMachine<UrgotStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = _enemyBase as EnemyUrgot;
    }

    public override void Enter()
    {
        base.Enter();
        EazySoundManager.PlaySound(_enemy.audioList[5]);

        _enemyBase.StopImmediately(false);
    }

    public override void Exit()
    {
        _enemyBase.ReturnDefaultSpeed();

        _enemyBase.battleTime = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_triggerCalled)
        {
            _enemyBase.ColliderCompo.enabled = true;
            _enemyBase.RigidbodyCompo.gravityScale = 1;

            _stateMachine.ChangeState(UrgotStateEnum.Battle);
        }
    }
}
