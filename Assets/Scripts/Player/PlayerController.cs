using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rb;
    private PlayerControls _controls;
    private Vector2 _moveInput;
    private PlayerState _state = PlayerState.Idle;
    private Vector2 _facingDirection = Vector2.down;

    public PlayerState State => _state;
    public Vector2 FacingDirection => _facingDirection;
    public PlayerControls Controls => _controls;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controls = new PlayerControls();
    }

    private void Start()
    {
        _controls.Player.Enable();
        _controls.Player.Move.performed += OnMove;
        _controls.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        if (_controls == null) return;
        _controls.Player.Move.performed -= OnMove;
        _controls.Player.Move.canceled -= OnMove;
        _controls.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (_state == PlayerState.Attacking || _state == PlayerState.Dashing)
        {
            return;
        }

        if (_moveInput.sqrMagnitude > 0.01f)
        {
            _rb.linearVelocity = _moveInput.normalized * _moveSpeed;
            _facingDirection = _moveInput.normalized;
            _state = PlayerState.Moving;
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
            _state = PlayerState.Idle;
        }
    }

    public void SetState(PlayerState newState)
    {
        _state = newState;
    }

    private void OnDestroy()
    {
        _controls?.Dispose();
    }
}
