using UnityEngine;

public class AgentMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float _gravity = -9.8f;

    protected CharacterController _characterController;

    #region 속도관련 로직
    private Vector3 _velocity;
    public Vector3 Velocity => _velocity;
    private float _verticalVelocity;
    #endregion

    public bool IsGround => _characterController.isGrounded;

    public float Verticalveocity => _verticalVelocity;

    public CharacterController CC => _characterController;

    public Vector3 MovementInput { get; set; }

    private Quaternion _targetRotation;

    private Agent _agent;

    public float JumpPower;

    public void Initalize(Agent agent)
    {
        _characterController = GetComponent<CharacterController>();
        _agent = agent;
    }

    private void Update()
    {
        ApplyRotation();
        Move();
        ApplyGraivity();
    }

    private void ApplyRotation()
    {
        float rotationSpeed = 8f;
        transform.rotation = Quaternion.Lerp(
                            transform.rotation,
                            _targetRotation,
                            Time.fixedDeltaTime * rotationSpeed);
    }

    private void ApplyGraivity()
    {
        if (IsGround && _verticalVelocity <= 0)
        {
            _verticalVelocity = -4f;
        }
        else
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }
        _velocity.y = _verticalVelocity * Time.deltaTime;
    }

    public void SetMovement(Vector3 movement, bool isRotation = true)
    {
        _velocity = movement * Time.deltaTime;
    }
    public void Jump()
    {
        _verticalVelocity = JumpPower;
    }
    private void Move()
    {
        _characterController.Move(_velocity);
    }

    public void StopImmediately()
    {
        _velocity = Vector3.zero;
    }
}
