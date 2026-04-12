using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float _attackDuration = 0.3f;
    [SerializeField] private float _attackOffset = 0.8f;
    [SerializeField] private float _attackRadius = 0.4f;
    [SerializeField] private float _attackDamage = 1f;
    [SerializeField] private LayerMask _enemyLayers;

    private PlayerController _controller;
    private Rigidbody2D _rb;
    private float _attackTimer;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _controller.Controls.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        if (_controller == null || _controller.Controls == null) return;
        _controller.Controls.Player.Attack.performed -= OnAttack;
    }

    private void Update()
    {
        if (_controller.State != PlayerState.Attacking) return;

        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0f)
        {
            _controller.SetState(PlayerState.Idle);
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (_controller.State == PlayerState.Attacking || _controller.State == PlayerState.Dashing)
        {
            return;
        }

        _controller.SetState(PlayerState.Attacking);
        _attackTimer = _attackDuration;

        // Stop movement during attack
        _rb.velocity = Vector2.zero;

        // Perform the attack hit detection
        PerformAttack();
    }

    private void PerformAttack()
    {
        Vector2 attackPos = (Vector2)transform.position + _controller.FacingDirection * _attackOffset;
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos, _attackRadius, _enemyLayers);

        foreach (var hit in hits)
        {
            // Damage dealing added in Phase 2 when enemies exist
            Debug.Log($"Hit: {hit.name}");
        }
    }

    // Visualize attack range in editor
    private void OnDrawGizmosSelected()
    {
        if (_controller == null) return;
        Vector2 attackPos = (Vector2)transform.position + _controller.FacingDirection * _attackOffset;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos, _attackRadius);
    }
}
