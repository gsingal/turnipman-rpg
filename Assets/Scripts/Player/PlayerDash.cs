using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float _dashSpeed = 15f;
    [SerializeField] private float _dashDuration = 0.15f;
    [SerializeField] private float _dashCooldown = 0.8f;

    private PlayerController _controller;
    private Rigidbody2D _rb;
    private float _dashTimer;
    private float _cooldownTimer;
    private Vector2 _dashDirection;

    private void OnEnable()
    {
        _controller = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
        _controller.Controls.Player.Dash.performed += OnDash;
    }

    private void OnDisable()
    {
        _controller.Controls.Player.Dash.performed -= OnDash;
    }

    private void Update()
    {
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (_controller.State != PlayerState.Dashing) return;

        _dashTimer -= Time.fixedDeltaTime;
        if (_dashTimer <= 0f)
        {
            _rb.velocity = Vector2.zero;
            _controller.SetState(PlayerState.Idle);
            return;
        }

        _rb.velocity = _dashDirection * _dashSpeed;
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if (_controller.State == PlayerState.Attacking || _controller.State == PlayerState.Dashing)
        {
            return;
        }

        if (_cooldownTimer > 0f) return;

        // Dash in movement direction, or facing direction if standing still
        _dashDirection = _rb.velocity.normalized;
        if (_dashDirection.sqrMagnitude < 0.01f)
        {
            _dashDirection = _controller.FacingDirection;
        }

        _controller.SetState(PlayerState.Dashing);
        _dashTimer = _dashDuration;
        _cooldownTimer = _dashCooldown;
    }
}
