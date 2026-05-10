---
name: bug-fix
description: End-to-end bug investigation and fix workflow — from error report through root cause analysis, generalization, TDD fix, and systemic cleanup.
---

# Bug Fix Workflow

Structured process for investigating, fixing, and generalizing bug fixes. Ensures bugs are
fixed at the root cause, similar patterns are identified across the codebase, and fixes are
verified with tests.

**Announce at start:** "I'm using the bug-fix skill to investigate and fix this bug."

## When to Use

- Error reports (crash logs, error monitoring)
- User-reported bugs
- Bugs discovered during development or code review
- Test failures that indicate real bugs (not flaky tests)
- Any unexpected behavior

## The Two Iron Laws

```
1. NO FIXES WITHOUT ROOT CAUSE INVESTIGATION FIRST
2. NO FIX IS COMPLETE UNTIL YOU'VE CHECKED FOR SIBLINGS
```

Law 1 prevents symptom-chasing. Law 2 prevents fixing one instance while leaving 10 others.

## Phase 0: Intake

### 0a. Gather Bug Details

```
BUG INTAKE:
- Source: [error monitor / user report / code review / test failure]
- Error: [exact error message]
- File(s): [stack trace files]
- Frequency: [how often, how many users affected]
- Severity: [P1-critical / P2-high / P3-medium / P4-low]
- Reproducible: [yes / no / unknown]
```

### 0b. Create GitHub Issue

Every non-trivial bug gets an issue BEFORE investigation begins. Use `/create-issue`.

## Phase 1: Root Cause Analysis

### 1a. Read the Error Carefully

- Read the FULL stack trace — don't skim
- Note every file and line number
- Read the actual code at those locations
- Understand what the code is trying to do

### 1b. Reproduce

Can you trigger the bug reliably?
- If yes, document exact reproduction steps
- If no, trace the code path manually and identify the trigger condition

### 1c. Check Recent Changes

```bash
# What changed in the affected files?
git log --oneline -20 -- path/to/affected/file

# What changed recently that could cause this?
git log --oneline -20
```

### 1d. Trace the Data Flow

Start at the error and work BACKWARDS:
1. What value caused the error?
2. Where did that value come from?
3. What called this code with that value?
4. Keep tracing until you find the SOURCE of the bad state

```
ROOT CAUSE TRACE:
1. Error at [file:line] -- [what failed]
2. Bad value came from [file:line] -- [what passed it]
3. That value originated at [file:line] -- [why it's wrong]
-> Root cause: [concise statement]
```

### 1e. Classify the Root Cause

| Category | Description | Example |
|----------|-------------|---------|
| **Type mismatch** | Wrong type passed or stored | float where int expected |
| **Missing null guard** | Accessing property on null | null reference exception |
| **Missing input validation** | No validation on user input | invalid ID reaching logic |
| **Config gap** | Missing or incomplete configuration | unregistered component |
| **Race condition** | Concurrent operations conflicting | shared state corruption |
| **State mismatch** | Code assumes wrong state | stale cache, wrong game phase |
| **External service** | Third-party API/SDK change or failure | SDK version incompatibility |

## Phase 2: Generalize — Find Siblings

**Invoke the `/sibling-search` skill.** Pass it your root cause finding. It will:
1. Generalize from the specific finding to the abstract vulnerability class
2. Write and execute codebase-wide searches
3. Classify each result (fix in PR / create issue / already guarded / false positive)

## Phase 3: TDD Fix

### 3a. Write Failing Test First

Write a test that reproduces the EXACT bug. The test must:
- Fail with the same error as the original
- Test the behavior, not the implementation
- Be minimal — smallest possible reproduction

### 3b. Verify Test Fails

Run the test. Confirm it fails for the RIGHT reason (the bug), not a setup error.

If the test passes immediately, your test doesn't reproduce the bug. Fix the test.

### 3c. Implement the Fix

Write the MINIMUM code to make the test pass. Fix at the root cause, not the symptom.

**Good fix:** Add null check where the null originates
**Bad fix:** Wrap the crash in a try/catch

### 3d. Verify All Tests Pass

Run the specific test, then the full suite to check for regressions.

### 3e. Fix Siblings

For each sibling in this PR:
1. Write a failing test
2. Apply the same fix pattern
3. Verify it passes

### 3f. Commit

```bash
git add [specific files]
git commit -m "fix: [description]

Refs #NNNN"
```

## Phase 4: Review and Verify

### 4a. Self-Review Checklist

- [ ] Root cause identified and documented
- [ ] All siblings found and either fixed or tracked in issues
- [ ] Every fix has a corresponding test
- [ ] All tests pass
- [ ] No unrelated changes snuck in
- [ ] Commit messages reference the issue

### 4b. Run Code Review

Invoke `/iterative-review` to catch anything you missed.

### 4c. Create Issues for Systemic Findings

If Phase 2 found siblings that need separate work, create tracking issues.

## Anti-Patterns

| Anti-Pattern | Why It's Wrong | Do This Instead |
|---|---|---|
| Fix without RCA | Treating symptoms | Complete Phase 1 first |
| Fix one instance, ignore siblings | You'll be back fixing the same pattern | Complete Phase 2 |
| Fix first, test later | Tests written after are biased | Write failing test first |
| Wrap errors in try/catch | Hides the bug | Fix at the source |
| "Quick fix" without generalization | Leaves landmines | Always search for siblings |
| Broad catch blocks | Masks future bugs | Catch specific exceptions |
