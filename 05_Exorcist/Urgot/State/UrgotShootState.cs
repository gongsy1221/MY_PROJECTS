using Hellmade.Sound;
using UnityEngine;

public class UrgotShootState : EnemyState<UrgotStateEnum>
{
    private EnemyUrgot _enemy;

    public UrgotShootState(Enemy enemyBase, EnemyStateMachine<UrgotStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = _enemyBase as EnemyUrgot;
    }

    public override void Enter()
    {
        base.Enter();
        EazySoundManager.PlaySound(_enemy.audioList[1]);

        _enemyBase.StopImmediately(false);
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
