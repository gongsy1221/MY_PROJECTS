using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrgotHealState : EnemyState<UrgotStateEnum>
{
    private EnemyUrgot _enemy;

    private ParticleSystem _healParticle;

    public UrgotHealState(Enemy enemyBase, EnemyStateMachine<UrgotStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = _enemyBase as EnemyUrgot;
    }

    public override void Enter()
    {
        base.Enter();

        _enemyBase.StopImmediately(false);
        _healParticle = GameObject.Find("HealParticle").GetComponent<ParticleSystem>();
        _healParticle.Play();
        EazySoundManager.PlaySound(_enemy.audioList[6]);
        _enemyBase.HealthCompo.ApplyDamage(-15, Vector2.zero, Vector2.zero);
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
