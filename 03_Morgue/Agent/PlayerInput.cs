using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> MovementEvent;
    public event Action JumpEvent;

    public Vector3 MousePosition { get; private set; }

    private bool _playerInputEnabled = true;

    public void SetPlayerInput(bool enable)
    {
        _playerInputEnabled = enable;
    }

    private void Update()
    {
        if (_playerInputEnabled == false) return;
        CheckJumpInput();
        CheckMoveInput();
    }

    private void CheckJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpEvent?.Invoke();
        }
    }

    private void CheckMoveInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveVector = new Vector3(horizontal, 0, vertical);

        MovementEvent?.Invoke(moveVector.normalized);
    }
}