using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrgotLaserState : EnemyState<UrgotStateEnum>
{
    private float _upTime;
    private float _upCoolTime = 3.5f;

    public UrgotLaserState(Enemy enemyBase, EnemyStateMachine<UrgotStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.StopImmediately(false);
        _upTime = Time.time;
    }

    public override void Exit()
    {
        _enemyBase.battleTime = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (CanDown())
        {
            _stateMachine.ChangeState(UrgotStateEnum.Down);
        }
    }

    private bool CanDown()
    {
        return Time.time >= _upTime + _upCoolTime;
    }
}
