# Phase 1: Core Movement & Combat Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Get the turnip character moving, attacking, and dashing in a test scene with correct feel — attacks have commitment, dash is near-instant with cooldown, both gamepad and keyboard work.

**Architecture:** Player uses a state machine (Idle, Moving, Attacking, Dashing) to prevent action canceling. Movement uses Rigidbody2D. Unity's new Input System handles dual input support via an Input Action Asset. All player logic lives in `Assets/Scripts/Player/`. A simple test arena scene is used for iteration.

**Tech Stack:** Unity 2022.3.30f1, C#, New Input System package, Rigidbody2D, Physics2D

---

### Task 1: Install the New Input System Package

**Why:** The project currently uses the old Input Manager. The new Input System is required for equal gamepad + keyboard support through a single Input Action Asset.

**Step 1: Install the package**

Add `"com.unity.inputsystem": "1.7.0"` to `Packages/manifest.json` in the dependencies block.

```json
"com.unity.inputsystem": "1.7.0",
```

**Step 2: Configure the project to use both input systems during transition**

Modify `ProjectSettings/ProjectSettings.asset` — find the `activeInputHandler` setting and set it to `2` (Both). This allows the new Input System to work alongside the old one so nothing breaks.

Search for `activeInputHandler` in the file and change its value to `2`.

**Step 3: Verify in Unity**

Open Unity. It may prompt to restart for Input System backend changes — accept it. Verify no console errors after restart.

**Step 4: Commit**

```bash
git add Packages/manifest.json ProjectSettings/ProjectSettings.asset
git commit -m "feat: install Unity Input System package"
```

---

### Task 2: Create the Input Action Asset

**Why:** The Input Action Asset defines all player controls in one place, mapping both keyboard and gamepad to the same actions.

**Files:**
- Create: `Assets/Input/PlayerControls.inputactions`

**Step 1: Create the Input Action Asset via script**

Since `.inputactions` files are JSON, we can create it directly. Create `Assets/Input/PlayerControls.inputactions` with the following action maps:

- **Action Map: "Player"**
  - **Move** (Value, Vector2)
    - Keyboard: WASD composite (W=up, S=down, A=left, D=right)
    - Keyboard: Arrow keys composite
    - Gamepad: Left Stick
  - **Attack** (Button)
    - Keyboard: Space
    - Gamepad: West Button (X on Xbox, Square on PS)
  - **Dash** (Button)
    - Keyboard: Left Shift
    - Gamepad: South Button (A on Xbox, Cross on PS)

**Step 2: Generate C# wrapper class**

Enable "Generate C# Class" on the asset. This creates `Assets/Input/PlayerControls.cs` — a type-safe wrapper we can use in code. The generated class file path should be `Assets/Input/PlayerControls.cs`.

**Step 3: Verify in Unity**

Open the Input Action Asset in Unity's Input Actions editor. Confirm all bindings show up for both keyboard and gamepad.

**Step 4: Commit**

```bash
git add Assets/Input/
git commit -m "feat: add PlayerControls input action asset with keyboard and gamepad bindings"
```

---

### Task 3: Create the Test Arena Scene

**Why:** We need a simple scene to test movement and combat before building real levels. A flat area with some walls to test collision.

**Files:**
- Create: `Assets/Scenes/TestArena.unity` (via Unity)
- Create: `Assets/Sprites/` directory for placeholder sprites

**Step 1: Create the scene**

Create a new scene called `TestArena` with:
- A Camera set to Orthographic (default for 2D)
- A flat ground area (just a background color or sprite, no physics needed for ground in top-down)
- 4 wall objects around the edges using BoxCollider2D (so the player can't walk off screen)
- A simple colored square sprite as the player placeholder (can use Unity's default square sprite)

**Step 2: Set up the player GameObject**

Create a GameObject called "Player" with:
- `SpriteRenderer` — use a colored square as placeholder (we'll replace with pixel art later)
- `Rigidbody2D` — set Body Type to Dynamic, Gravity Scale to 0 (top-down, no gravity), Freeze Rotation Z
- `BoxCollider2D` — sized to the sprite
- Set the sorting layer appropriately

**Step 3: Verify in Unity**

Enter Play mode. The player square should appear. Nothing moves yet — that's expected.

**Step 4: Commit**

```bash
git add Assets/Scenes/TestArena.unity Assets/Scenes/TestArena.unity.meta Assets/Sprites/
git commit -m "feat: add test arena scene with player placeholder"
```

---

### Task 4: Player Movement Script

**Why:** Top-down 8-directional movement using the new Input System and Rigidbody2D.

**Files:**
- Create: `Assets/Scripts/Player/PlayerController.cs`

**Step 1: Write the PlayerController script**

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rb;
    private PlayerControls _controls;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
        _controls.Player.Move.performed += OnMove;
        _controls.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
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
        _rb.linearVelocity = _moveInput.normalized * _moveSpeed;
    }
}
```

**Step 2: Attach to player**

Add the `PlayerController` component to the Player GameObject in the TestArena scene.

**Step 3: Playtest**

Enter Play mode. Verify:
- WASD moves the player in 8 directions
- Arrow keys also work
- Gamepad left stick works (if available)
- Diagonal movement is normalized (not faster than cardinal)
- Player stops when input is released
- Player collides with walls and cannot pass through them

**Step 4: Commit**

```bash
git add Assets/Scripts/Player/PlayerController.cs Assets/Scripts/Player/PlayerController.cs.meta
git commit -m "feat: add top-down player movement with Input System"
```

---

### Task 5: Player State Machine

**Why:** The player needs states (Idle, Moving, Attacking, Dashing) to enforce commitment — you can't cancel an attack or dash spam. A simple enum-based state machine keeps this clean.

**Files:**
- Create: `Assets/Scripts/Player/PlayerState.cs`
- Modify: `Assets/Scripts/Player/PlayerController.cs`

**Step 1: Create the PlayerState enum**

```csharp
public enum PlayerState
{
    Idle,
    Moving,
    Attacking,
    Dashing
}
```

**Step 2: Integrate state into PlayerController**

Update `PlayerController` to track state and only allow movement when in Idle or Moving states:

```csharp
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

    public PlayerState State => _state;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
        _controls.Player.Move.performed += OnMove;
        _controls.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
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
}
```

**Step 3: Playtest**

Enter Play mode. Movement should work exactly as before — state machine doesn't change behavior yet, just tracks it.

**Step 4: Commit**

```bash
git add Assets/Scripts/Player/PlayerState.cs Assets/Scripts/Player/PlayerState.cs.meta Assets/Scripts/Player/PlayerController.cs
git commit -m "feat: add player state machine (Idle, Moving, Attacking, Dashing)"
```

---

### Task 6: Melee Attack

**Why:** The primary melee attack with commitment — once started, it must finish before the player can act again. Attack comes out quickly but locks the player.

**Files:**
- Create: `Assets/Scripts/Player/PlayerAttack.cs`
- Modify: `Assets/Scripts/Player/PlayerController.cs` (add attack input binding)

**Step 1: Write the PlayerAttack script**

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float _attackDuration = 0.3f;
    [SerializeField] private float _attackRange = 0.8f;
    [SerializeField] private float _attackDamage = 1f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayers;

    private PlayerController _controller;
    private PlayerControls _controls;
    private float _attackTimer;
    private Vector2 _facingDirection = Vector2.down;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
        _controls.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        _controls.Player.Attack.performed -= OnAttack;
        _controls.Player.Disable();
    }

    private void Update()
    {
        UpdateFacingDirection();
        UpdateAttackState();
    }

    private void UpdateFacingDirection()
    {
        if (_controller.State == PlayerState.Moving)
        {
            Vector2 move = GetComponent<Rigidbody2D>().linearVelocity;
            if (move.sqrMagnitude > 0.01f)
            {
                _facingDirection = move.normalized;
            }
        }
    }

    private void UpdateAttackState()
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
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        // Perform the attack hit detection
        PerformAttack();
    }

    private void PerformAttack()
    {
        Vector2 attackPos = (Vector2)transform.position + _facingDirection * _attackRange;
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos, _attackRange * 0.5f, _enemyLayers);

        foreach (var hit in hits)
        {
            // We'll add damage dealing when enemies exist
            Debug.Log($"Hit: {hit.name}");
        }
    }

    // Visualize attack range in editor
    private void OnDrawGizmosSelected()
    {
        Vector2 attackPos = (Vector2)transform.position + _facingDirection * _attackRange;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos, _attackRange * 0.5f);
    }
}
```

**Step 2: Set up attack point**

Create an empty child GameObject on the Player called "AttackPoint" and assign it to the `_attackPoint` field. Create a Layer called "Enemy" for future use and assign it to `_enemyLayers`.

**Step 3: Playtest**

Enter Play mode. Verify:
- Pressing Space / gamepad X triggers the attack
- Player stops moving during the attack
- Player cannot move or attack again until the attack duration finishes
- The attack direction follows the last movement direction
- Attack gizmo is visible in Scene view showing the hitbox

**Step 4: Commit**

```bash
git add Assets/Scripts/Player/PlayerAttack.cs Assets/Scripts/Player/PlayerAttack.cs.meta
git commit -m "feat: add melee attack with commitment (no cancel, locks movement)"
```

---

### Task 7: Dash

**Why:** Near-instant startup dash with cooldown. Used for dodging in combat and faster overworld traversal. Cannot dash through obstacles.

**Files:**
- Create: `Assets/Scripts/Player/PlayerDash.cs`

**Step 1: Write the PlayerDash script**

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float _dashSpeed = 15f;
    [SerializeField] private float _dashDuration = 0.15f;
    [SerializeField] private float _dashCooldown = 0.8f;

    private PlayerController _controller;
    private PlayerControls _controls;
    private Rigidbody2D _rb;
    private float _dashTimer;
    private float _cooldownTimer;
    private Vector2 _dashDirection;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _controls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
        _controls.Player.Dash.performed += OnDash;
    }

    private void OnDisable()
    {
        _controls.Player.Dash.performed -= OnDash;
        _controls.Player.Disable();
    }

    private void Update()
    {
        UpdateCooldown();
        UpdateDashState();
    }

    private void FixedUpdate()
    {
        if (_controller.State == PlayerState.Dashing)
        {
            _rb.linearVelocity = _dashDirection * _dashSpeed;
        }
    }

    private void UpdateCooldown()
    {
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    private void UpdateDashState()
    {
        if (_controller.State != PlayerState.Dashing) return;

        _dashTimer -= Time.deltaTime;
        if (_dashTimer <= 0f)
        {
            _rb.linearVelocity = Vector2.zero;
            _controller.SetState(PlayerState.Idle);
        }
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if (_controller.State == PlayerState.Attacking || _controller.State == PlayerState.Dashing)
        {
            return;
        }

        if (_cooldownTimer > 0f) return;

        // Dash in movement direction, or facing direction if standing still
        _dashDirection = _rb.linearVelocity.normalized;
        if (_dashDirection.sqrMagnitude < 0.01f)
        {
            // Use the facing direction from PlayerAttack or default to down
            var attack = GetComponent<PlayerAttack>();
            // Fallback to down if no clear direction
            _dashDirection = Vector2.down;
        }

        _controller.SetState(PlayerState.Dashing);
        _dashTimer = _dashDuration;
        _cooldownTimer = _dashCooldown;
    }
}
```

**Step 2: Refactor facing direction to PlayerController**

The facing direction is needed by both PlayerAttack and PlayerDash. Move it to PlayerController so both can access it:

Add to `PlayerController`:

```csharp
private Vector2 _facingDirection = Vector2.down;
public Vector2 FacingDirection => _facingDirection;
```

Update the `HandleMovement` method to track facing:

```csharp
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
```

Update `PlayerAttack` to use `_controller.FacingDirection` instead of its own `_facingDirection`.

Update `PlayerDash.OnDash` to use `_controller.FacingDirection` as fallback:

```csharp
_dashDirection = _rb.linearVelocity.normalized;
if (_dashDirection.sqrMagnitude < 0.01f)
{
    _dashDirection = _controller.FacingDirection;
}
```

**Step 3: Playtest**

Enter Play mode. Verify:
- Left Shift / gamepad A triggers a dash
- Dash is fast and short (near-instant feel)
- Cannot dash during an attack
- Cannot attack during a dash
- Dash has a cooldown — can't spam it
- Dash goes in movement direction (or facing direction if standing still)
- Player collides with walls during dash (cannot dash through)
- After dash ends, player can immediately move/attack

**Step 4: Commit**

```bash
git add Assets/Scripts/Player/
git commit -m "feat: add dash with cooldown, refactor facing direction to PlayerController"
```

---

### Task 8: Polish & Tuning Pass

**Why:** Get the numbers feeling right. This is a feel-driven task — iterate in Play mode.

**Files:**
- Modify: `Assets/Scripts/Player/PlayerController.cs`
- Modify: `Assets/Scripts/Player/PlayerAttack.cs`
- Modify: `Assets/Scripts/Player/PlayerDash.cs`

**Step 1: Tune values in the Inspector**

All key values are exposed as `[SerializeField]` fields. Playtest and adjust:

| Parameter | Starting Value | Adjust For |
|-----------|---------------|------------|
| Move Speed | 5 | Should feel brisk but controllable |
| Attack Duration | 0.3s | Short enough to feel snappy, long enough to commit |
| Attack Range | 0.8 | Visible and fair hitbox |
| Dash Speed | 15 | Noticeably faster than walking |
| Dash Duration | 0.15s | Brief burst, not a teleport |
| Dash Cooldown | 0.8s | Prevents spam, but available often enough |

**Step 2: Playtest the full loop**

Run around the test arena and verify the full action set feels cohesive:
- Move around, attack, dash away, re-engage
- Try to break it: attack then immediately dash, dash then attack, mash everything
- Confirm state machine prevents all illegal transitions

**Step 3: Commit final tuned values**

```bash
git add Assets/Scenes/TestArena.unity Assets/Scripts/Player/
git commit -m "feat: tune movement, attack, and dash values"
```

---

## Summary

| Task | What It Does |
|------|-------------|
| 1 | Install Input System package |
| 2 | Create Input Action Asset (keyboard + gamepad) |
| 3 | Create test arena scene with player placeholder |
| 4 | Player movement script (8-directional, Rigidbody2D) |
| 5 | State machine (Idle, Moving, Attacking, Dashing) |
| 6 | Melee attack with commitment |
| 7 | Dash with cooldown + facing direction refactor |
| 8 | Polish and tune all values |

After Phase 1, the player can move, attack, and dash with correct feel. Ready for Phase 2: tutorial area and enemies.
