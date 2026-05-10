---
name: techdebt
description: Scan for technical debt — code duplication, dead code, outdated patterns, code smells. Run periodically or on a target directory.
user_invocable: true
---

# Technical Debt Finder

Identify and categorize technical debt in the codebase.

## When to Use

- End of a work session as a cleanup pass
- When a directory feels messy or hard to navigate
- Periodically (monthly) on high-churn directories
- When onboarding to an unfamiliar part of the codebase

## What to Scan For

### Code Duplication
- Functions/methods with similar logic that could be consolidated
- Copy-pasted code blocks (same pattern, slight variations)
- Repeated validation/setup logic that should be shared

### Dead Code
- Unused imports
- Unreachable branches (conditions that can never be true)
- Commented-out code blocks (>3 lines)
- Functions/methods with zero callers
- Unused assets, scenes, or resources

### Outdated Patterns
- Deprecated API usage that has modern replacements
- Verbose patterns where the language/framework now has shorthand
- Old library patterns when newer, better-supported alternatives exist

### Code Smells
- Functions longer than 50 lines
- Functions with more than 5 parameters
- Nesting deeper than 3 levels
- Magic numbers without named constants
- Overly complex conditionals (>3 boolean terms)
- God classes (>500 lines, >15 public methods)

### Missing Best Practices
- Public functions without type hints (where the language supports them)
- Missing error handling on external calls
- Unbounded loops or queries
- Hardcoded values that should be configurable

## Workflow

1. **Scan the target.** Accept a directory or file. Default to recently changed files.

2. **Report findings** by severity:

   ```
   TECH DEBT SCAN: src/systems/
   ================================

   HIGH SEVERITY (fix now)
   - [Dead Code] src/systems/OldMovement.cs -- 0 callers, entire class unused
   - [Missing Types] src/systems/Combat.cs:45 -- public method with no return type

   MEDIUM SEVERITY (fix this session)
   - [Outdated] src/utils/Math.cs:12 -- manual lerp, use built-in Mathf.Lerp
   - [Duplication] src/player/Jump.cs:28 -- same gravity calc as src/enemy/Fall.cs:15

   LOW SEVERITY (track for later)
   - [Commented Code] src/ui/Menu.cs:45-52 -- 8 lines of commented-out code

   Summary: 2 high, 2 medium, 1 low
   ```

3. **Fix high-severity items** first. Atomic commits for each fix.

4. **Verify.** Run tests after changes.

5. **Report remaining items** for future sessions.

## Arguments

- `/techdebt` — scan recently changed files
- `/techdebt src/systems/` — scan a specific directory
- `/techdebt src/player/Movement.cs` — scan a specific file
- `/techdebt --report-only` — scan and report without fixing

## Severity Guidelines

| Severity | Criteria | Action |
|----------|----------|--------|
| **High** | Security risk, potential runtime error, or completely dead code | Fix immediately |
| **Medium** | Outdated pattern, code smell, missing types | Fix in current session |
| **Low** | Style preference, minor readability issue | Track for later |
