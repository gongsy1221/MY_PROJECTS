using UnityEngine;

public class UrgotUltimateState : EnemyState<UrgotStateEnum>
{
    public UrgotUltimateState(Enemy enemyBase, EnemyStateMachine<UrgotStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.StopImmediately(false);
    }

    public override void Exit()
    {
        _enemyBase.battleTime = Time.time;
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
