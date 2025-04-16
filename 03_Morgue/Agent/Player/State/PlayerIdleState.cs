using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.MovementEvent += HandleMovementEvent;
        //_player.MovementCompo.StopImmediately();
        _player.isMove = false;
    }

    public override void Exit()
    {
        _player.PlayerInput.MovementEvent -= HandleMovementEvent;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    private void HandleMovementEvent(Vector3 movement)
    {
        float inputThreshold = 0.05f;
        if (movement.sqrMagnitude > inputThreshold)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _stateMachine.ChangeState(PlayerStateEnum.Run);
                return;
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                _stateMachine.ChangeState(PlayerStateEnum.Sit);
                return;
            }

            _stateMachine.ChangeState(PlayerStateEnum.Walk);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            _stateMachine.ChangeState(PlayerStateEnum.Sit);
        }
    }
}
