using Hellmade.Sound;
using UnityEngine;

public class UrgotUpState : EnemyState<UrgotStateEnum>
{
    private float _upTime;
    private float _upCoolTime = 3f;
    private float _moveDirection;

    private Transform _player;
    private SpriteRenderer _attackZone;
    private EnemyUrgot _enemy;

    public UrgotUpState(Enemy enemyBase, EnemyStateMachine<UrgotStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = _enemyBase as EnemyUrgot;
    }

    public override void Enter()
    {
        base.Enter();
        EazySoundManager.PlaySound(_enemy.audioList[4]);

        _player = PlayerManager.Instance.PlayerTrm;
        _attackZone = GameObject.Find("AttackZone").GetComponent<SpriteRenderer>();
        _upTime = Time.time;

        _enemyBase.RigidbodyCompo.gravityScale = 0;
        _enemyBase.ColliderCompo.enabled = false;
        _enemyBase.moveSpeed = 7;

        _enemyBase.StopImmediately(false);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            float xDistance = _player.transform.position.x - _enemyBase.transform.position.x;

            if (_player.transform.position.x > _enemyBase.transform.position.x)
                _moveDirection = 1;
            else
                _moveDirection = -1;

            if(xDistance <= 0f)
                _enemyBase.SetVelocity(_enemyBase.moveSpeed * _moveDirection, _rigidbody.velocity.y, true);
            else
                _enemyBase.SetVelocity(_enemyBase.moveSpeed * _moveDirection, _rigidbody.velocity.y);

            _attackZone.enabled = true;
            if (CanDown())
            {
                _attackZone.enabled = false;
                _stateMachine.ChangeState(UrgotStateEnum.Down);
            }
        }
    }

    private bool CanDown()
    {
        return Time.time >= _upTime + _upCoolTime;
    }
}
