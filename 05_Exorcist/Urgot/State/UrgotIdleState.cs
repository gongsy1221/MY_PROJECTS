using UnityEngine;

public class UrgotIdleState : EnemyState<UrgotStateEnum>
{
    public UrgotIdleState(Enemy enemyBase, EnemyStateMachine<UrgotStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        RaycastHit2D hit = _enemyBase.IsPlayerDetected();

        if (hit.distance <= 2 || Time.time >= _enemyBase.lastTimeAttacked + _enemyBase.attackCooldown)
        {
            _stateMachine.ChangeState(UrgotStateEnum.Battle);
        }
    }
}
