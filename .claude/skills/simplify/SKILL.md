---
name: simplify
description: Post-implementation cleanup — simplify code using modern patterns. Run after completing a feature or fix.
user_invocable: true
---

# Simplify

Clean up and simplify code after making changes, using modern patterns for the project's stack.

## When to Use

- After completing a feature or fix, before committing
- When reviewing code that uses verbose/outdated patterns
- As a standalone cleanup pass on a specific file or directory

## Workflow

1. **Identify changed files.** Focus on files modified in the current session, or accept a specific target.

2. **Check each file for simplification opportunities:**
   - Verbose patterns that have modern shorthand
   - Repeated logic that could use a shared helper
   - Complex conditionals that could be simplified
   - Unnecessary intermediate variables
   - Overly defensive code (checking for things that can't happen)

3. **Apply simplifications.** For each:
   - Verify the transformation preserves behavior
   - Apply the change
   - Run tests after each change if available

4. **Verify.** Run the test suite to confirm no regressions.

## Rules

- **Preserve behavior.** Never change what code does, only how it's written.
- **One pattern at a time.** Don't combine multiple transformations in a single edit.
- **Skip if unclear.** If a simplification might change semantics, leave it alone.
- **Respect existing style.** Only simplify recently changed or adjacent sections.

## Arguments

- `/simplify` — simplify recently changed files
- `/simplify path/to/file` — simplify a specific file
- `/simplify path/to/dir/` — simplify all files in a directory
