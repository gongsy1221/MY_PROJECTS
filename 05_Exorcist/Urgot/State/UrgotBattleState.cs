using UnityEngine;

public class UrgotBattleState : EnemyState<UrgotStateEnum>
{
    public UrgotBattleState(Enemy enemyBase, EnemyStateMachine<UrgotStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    protected Transform _player;
    private int _moveDirection;

    private float _skillCooldown = 3;
    private int _skillCnt = 3;

    private readonly int _xVelocityHash = Animator.StringToHash("x_velocity");

    public override void UpdateState()
    {
        base.UpdateState();
        float distance = Vector2.Distance(_player.transform.position, _enemyBase.transform.position);
        float xDistance = _player.transform.position.x - _enemyBase.transform.position.x;
        
        _enemyBase.AnimatorCompo.SetFloat(_xVelocityHash, Mathf.Abs(_rigidbody.velocity.x));

        if (_player.transform.position.x > _enemyBase.transform.position.x)
            _moveDirection = 1;
        else
            _moveDirection = -1;

        if (!CanAttack() && xDistance <= 1)
            _stateMachine.ChangeState(UrgotStateEnum.Idle);
        else
            _enemyBase.SetVelocity(_enemyBase.moveSpeed * _moveDirection, _rigidbody.velocity.y);

        RaycastHit2D hit = _enemyBase.IsPlayerDetected();


        if (hit)
        {
            Vector2 dir = _player.transform.position - _enemyBase.transform.position;

            if (CanSkill() && distance < 7f)
            {
                // ÇöÀç Ã¼·Â Ã¼Å©
                if (_enemyBase.HealthCompo.GetCurrentHealth() <= _enemyBase.HealthCompo.MaxHealth / 2)
                    _skillCnt += 1;

                int rand = Random.Range(1, _skillCnt);

                switch (rand)
                {
                    case 1:
                        _stateMachine.ChangeState(UrgotStateEnum.Up); // Á¡ÇÁ & ÂøÁö °ø°Ý
                        break;

                    case 2:
                        _stateMachine.ChangeState(UrgotStateEnum.Ultimate); // ¿ì¸£°÷ ±Ã±Ø±â
                        break;

                    case 3:
                        _stateMachine.ChangeState(UrgotStateEnum.Heal); // ÀÚÈú
                        break;
                }
                _skillCnt = 3;
                return;
            }

            if (CanAttack())
            {
                if (distance < _enemyBase.attackDistance)
                {
                    _stateMachine.ChangeState(UrgotStateEnum.Sting); // Âî¸£±â °ø°Ý
                    return;
                }
                else if (distance < 7f)
                {
                    _stateMachine.ChangeState(UrgotStateEnum.Shoot); // ÃÑ¾Ë ½î±â °ø°Ý
                    return;
                }
            }
        }
    }

    public override void Enter()
    {
        base.Enter();
        _player = PlayerManager.Instance.PlayerTrm;
        SetDirectionToEnemy();
    }

    private void SetDirectionToEnemy()
    {
        _enemyBase.FlipController(_player.transform.position.x - _enemyBase.transform.position.x);
    }

    private bool CanAttack()
    {
        return Time.time >= _enemyBase.lastTimeAttacked + _enemyBase.attackCooldown;
    }

    private bool CanSkill()
    {
        return Time.time >= _enemyBase.battleTime + _skillCooldown;
    }
}
