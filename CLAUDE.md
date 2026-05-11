# TurnipmanRPG

Unity 6 (6000.4.6f1) with Universal Render Pipeline (URP), multi-platform target.

- Uses `Rigidbody2D.linearVelocity` (NOT `.velocity` which was Unity 2022)
- Uses New Input System only (`activeInputHandler: 1`), old Input Manager is deprecated

## Code Conventions

- C# with Unity conventions
- PascalCase for public fields and methods
- _camelCase for private fields
- Use `[SerializeField]` for private fields exposed in Inspector
- Prefer composition over inheritance
- New scripts go in `Assets/Scripts/` organized by feature folder (e.g., `Assets/Scripts/Player/`, `Assets/Scripts/Enemies/`)

## Critical Rules

- NEVER modify `.meta` files directly
- NEVER rename or remove `[SerializeField]` fields without warning (breaks Inspector bindings)
- NEVER use force push on main branch
- Always use feature branches for new work (`feature/descriptive-name`)
- Scene files are Force Text YAML — you can read and modify them

## Git Workflow

- Claude manages ALL Git operations
- Always pull before starting new work
- Always commit with descriptive messages
- Feature branches merge to main when tested

## Unity MCP

Unity MCP (Coplay) is installed for direct Editor interaction (creating GameObjects, reading console, etc.)

## Explaining Code

This is a father-son learning project. When asked to explain code, give clear explanations suitable for someone who knows JavaScript but is learning C# and Unity.
