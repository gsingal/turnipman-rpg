# Phase 1: Core Movement & Combat Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Get the turnip character moving, attacking, and dashing in a test scene with correct feel — attacks have commitment, dash is near-instant with cooldown, both gamepad and keyboard work.

**Architecture:** Player uses a state machine (Idle, Moving, Attacking, Dashing) to prevent action canceling. Movement uses Rigidbody2D. Unity's new Input System handles dual input support via an Input Action Asset. A single `PlayerControls` instance is owned by `PlayerController` and shared with other scripts. All player logic lives in `Assets/Scripts/Player/`. A simple test arena scene is used for iteration.

**Tech Stack:** Unity 2022.3.30f1, C#, New Input System package, Rigidbody2D, Physics2D

**Note:** Unity 2022.3 uses `Rigidbody2D.velocity` (NOT `.linearVelocity` which is Unity 6+).

---

### Task 1: Install the New Input System Package

**Why:** The project currently uses the old Input Manager. The new Input System is required for equal gamepad + keyboard support through a single Input Action Asset.

**Files:**
- Modify: `Packages/manifest.json`
- Modify: `ProjectSettings/ProjectSettings.asset`

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
- Create: `Assets/Input/PlayerControls.cs` (auto-generated)

**Step 1: Create the Input Action Asset**

Use Coplay MCP's `create_input_action_asset` tool to create the asset at `Assets/Input/PlayerControls.inputactions`. If MCP is unavailable, create it via Unity's UI (Assets → Create → Input Actions).

Action Map: **"Player"** with these actions:

| Action | Type | Keyboard Bindings | Gamepad Bindings |
|--------|------|-------------------|-----------------|
| Move | Value (Vector2) | WASD composite, Arrow keys composite | Left Stick |
| Attack | Button | Space | West Button (X/Square) |
| Dash | Button | Left Shift | South Button (A/Cross) |

**Step 2: Enable "Generate C# Class"**

In the Input Action Asset inspector, check "Generate C# Class" with:
- File path: `Assets/Input/PlayerControls.cs`
- Class name: `PlayerControls`
- Namespace: (leave empty)

Click Apply. This generates a type-safe wrapper class.

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
- Create: `Assets/Scenes/TestArena.unity` (via Unity/MCP)

**Step 1: Create the scene**

Create a new scene called `TestArena` with:
- A Camera set to Orthographic (default for 2D)
- A flat ground area (just a background color, no physics needed for ground in top-down)
- 4 wall objects around the edges using BoxCollider2D (so the player can't walk off screen)

**Step 2: Set up the player GameObject**

Create a GameObject called "Player" with:
- `SpriteRenderer` — use Unity's default square sprite as placeholder
- `Rigidbody2D` — set Body Type to Dynamic, Gravity Scale to 0 (top-down, no gravity), Freeze Rotation Z
- `BoxCollider2D` — sized to the sprite

**Step 3: Verify in Unity**

Enter Play mode. The player square should appear. Nothing moves yet — that's expected.

**Step 4: Commit**

```bash
git add Assets/Scenes/TestArena.unity Assets/Scenes/TestArena.unity.meta
git commit -m "feat: add test arena scene with player placeholder"
```

---

### Task 4: Player Movement + State Machine + Facing Direction

**Why:** Top-down 8-directional movement using the new Input System and Rigidbody2D. The state machine and facing direction are included from the start because they're needed by attack and dash — avoids rewriting later.

**Files:**
- Create: `Assets/Scripts/Player/PlayerState.cs`
- Create: `Assets/Scripts/Player/PlayerController.cs`

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

**Step 2: Write the PlayerController script**

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
    private Vector2 _facingDirection = Vector2.down;

    public PlayerState State => _state;
    public Vector2 FacingDirection => _facingDirection;
    public PlayerControls Controls => _controls;

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
            _rb.velocity = _moveInput.normalized * _moveSpeed;
            _facingDirection = _moveInput.normalized;
            _state = PlayerState.Moving;
        }
        else
        {
            _rb.velocity = Vector2.zero;
            _state = PlayerState.Idle;
        }
    }

    public void SetState(PlayerState newState)
    {
        _state = newState;
    }
}
```

**Step 3: Attach to player**

Add the `PlayerController` component to the Player GameObject in the TestArena scene.

**Step 4: Playtest**

Enter Play mode. Verify:
- WASD moves the player in 8 directions
- Arrow keys also work
- Gamepad left stick works (if available)
- Diagonal movement is normalized (not faster than cardinal)
- Player stops when input is released
- Player collides with walls and cannot pass through them

**Step 5: Commit**

```bash
git add Assets/Scripts/Player/
git commit -m "feat: add player movement with state machine and facing direction"
```

---

### Task 5: Melee Attack

**Why:** The primary melee attack with commitment — once started, it must finish before the player can act again. Attack comes out quickly but locks the player.

**Files:**
- Create: `Assets/Scripts/Player/PlayerAttack.cs`

**Step 1: Write the PlayerAttack script**

```csharp
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

    private void OnEnable()
    {
        _controller.Controls.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
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
```

**Step 2: Set up layers**

Create a Layer called "Enemy" in Unity (Edit → Project Settings → Tags and Layers). Assign it to the `_enemyLayers` field on the PlayerAttack component.

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

### Task 6: Dash

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
    private Rigidbody2D _rb;
    private float _dashTimer;
    private float _cooldownTimer;
    private Vector2 _dashDirection;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
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

        if (_controller.State != PlayerState.Dashing) return;

        _dashTimer -= Time.deltaTime;
        if (_dashTimer <= 0f)
        {
            _rb.velocity = Vector2.zero;
            _controller.SetState(PlayerState.Idle);
        }
    }

    private void FixedUpdate()
    {
        if (_controller.State == PlayerState.Dashing)
        {
            _rb.velocity = _dashDirection * _dashSpeed;
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
```

**Step 2: Playtest**

Enter Play mode. Verify:
- Left Shift / gamepad A triggers a dash
- Dash is fast and short (near-instant feel)
- Cannot dash during an attack
- Cannot attack during a dash
- Dash has a cooldown — can't spam it
- Dash goes in movement direction (or facing direction if standing still)
- Player collides with walls during dash (cannot dash through)
- After dash ends, player can immediately move/attack

**Step 3: Commit**

```bash
git add Assets/Scripts/Player/PlayerDash.cs Assets/Scripts/Player/PlayerDash.cs.meta
git commit -m "feat: add dash with cooldown"
```

---

### Task 7: Polish & Tuning Pass

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
| Attack Offset | 0.8 | How far in front of player the hitbox appears |
| Attack Radius | 0.4 | Size of the hitbox — visible and fair |
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

| Task | What It Does | GitHub Issue |
|------|-------------|-------------|
| 1 | Install Input System package | #11 |
| 2 | Create Input Action Asset (keyboard + gamepad) | #12 |
| 3 | Create test arena scene with player placeholder | #13 |
| 4 | Player movement + state machine + facing direction | #14, #15 |
| 5 | Melee attack with commitment | #16 |
| 6 | Dash with cooldown | #17 |
| 7 | Polish and tune all values | #18 |

After Phase 1, the player can move, attack, and dash with correct feel. Ready for Phase 2: combat test with enemies.
