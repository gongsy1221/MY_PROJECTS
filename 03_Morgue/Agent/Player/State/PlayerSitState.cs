using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSitState : PlayerState
{
    public PlayerSitState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    private float originalCameraYPosition;
    private float sittingHeightOffset = 0.6f;
    private Vector3 moveDir;

    public override void Enter()
    {
        base.Enter();

        originalCameraYPosition = Camera.main.transform.localPosition.y;

        Camera.main.transform.localPosition = new Vector3(
            Camera.main.transform.localPosition.x,
            originalCameraYPosition - sittingHeightOffset,
            Camera.main.transform.localPosition.z);

        _player.PlayerInput.MovementEvent += HandleMovementEvent;
    }

    public override void Exit()
    {
        base.Exit();

        Camera.main.transform.localPosition = new Vector3(
            Camera.main.transform.localPosition.x,
            originalCameraYPosition,
            Camera.main.transform.localPosition.z);

        _player.CurrentSpeed = _player.WalkSpeed;
        _player.PlayerInput.MovementEvent -= HandleMovementEvent;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

        _player.CurrentSpeed = _player.SitSpeed;
        var right = Camera.main.transform.right;
        right.y = 0;
        var forward = Quaternion.AngleAxis(-90, Vector3.up) * right;
        Vector3 velocity = (moveDir.x * right) + ((moveDir.z * forward));
        velocity *= _player.CurrentSpeed;
        _player.MovementCompo.MovementInput = velocity;
        velocity.y = _player.MovementCompo.Verticalveocity;
        _player.MovementCompo.SetMovement(velocity);
    }

    private void HandleMovementEvent(Vector3 movement)
    {
        moveDir = movement;

        if (movement.sqrMagnitude > 0.05f)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Sit);
        }
    }
}
