---
name: iterative-review
description: Run iterative code review rounds until convergence (zero Critical issues). Tracks findings across rounds and produces a final verdict.
---

# Iterative Review

Runs multiple rounds of structured code review, fixing issues between rounds, until
convergence is reached.

## When to Use

- After completing a major implementation task
- Before marking a PR as ready for review
- When you want thorough review beyond a single pass

## Convergence Criteria

The loop stops when ANY of these:

1. **Zero Critical issues** in the latest round
2. **Max 5 rounds reached**
3. **Diminishing returns** — only Minor issues, no new Important+
4. **User intervention**

## Severity Classification

| Severity | Definition | Action |
|----------|-----------|--------|
| **Critical** | Would cause crashes, data loss, or security vulnerability | Must fix before next round |
| **Important** | Would cause problems, confusion, or maintenance burden | Must fix OR create tracking issue |
| **Minor** | Cosmetic, documentation, style | Fix if trivial, otherwise note |

## Steps

### Phase 0: Simplify (Pre-Review Cleanup)

Before deep review, run a simplification pass:

Launch three review agents in parallel:
- **Agent 1: Code Reuse** — Search for existing utilities that could replace new code
- **Agent 2: Code Quality** — Redundant state, copy-paste, parameter sprawl
- **Agent 3: Efficiency** — Unnecessary work, duplicate calls, N+1 patterns, memory leaks

Fix valid findings. Skip false positives.

### Phase 1: Setup

1. **Identify the review target.** Use `git diff` for changed files.
2. **Initialize tracking.** Round number, cumulative issues, convergence trend.
3. **Announce:**
   > "Starting iterative review. Up to 5 rounds, fixing Critical and Important issues between rounds."

### Phase 2: Review Loop

For each round:

4. **Dispatch reviewer subagent.** Must be a separate agent context (not inline) to avoid confirmation bias.

   Provide:
   - The full diff
   - What was fixed in the previous round
   - Instruction to classify each finding as Critical / Important / Minor

5. **Triage findings.** Count by severity. Check convergence.

6. **Fix issues** in priority order:
   - **Critical**: Fix immediately. Mandatory.
   - **Important**: Fix or create tracking issue.
   - **Minor**: Fix if trivial (< 1 minute).

   **Test assertion rule:** When a code change breaks a test, update the assertion to verify the NEW expected value — do not broaden the assertion until it passes anything.

   ### Triage Rules for Important Findings

   | Disposition | When | Action |
   |-------------|------|--------|
   | **Fix now** | Straightforward fix (default) | Apply the fix |
   | **Defer with issue** | Would bloat the PR | Create a GitHub issue |
   | **Dispute** | Finding is technically incorrect | Reply with reasoning |

7. **Log the round:**
   > "Round N: X Critical, Y Important, Z Minor. All Critical/Important fixed. Quality: [N]/10."

8. **Loop** to next round.

### Phase 3: Final Report

```
ITERATIVE REVIEW COMPLETE
========================
Rounds: N
Total issues found: X (Y fixed, Z deferred)
Convergence trend: [issues per round]

Quality: [N]/10

Verdict: PASS / WARN / FAIL
- PASS: Zero Critical, zero Important remaining
- WARN: Zero Critical, Important deferred with tracking issues
- FAIL: Critical or Important issues remain (hit iteration cap)

Round-by-Round Summary:
Round 1: X Critical, Y Important, Z Minor
  Fixed: [summary]
Round 2: ...

Remaining unfixed items:
- [list with severity and description]
```

Present unfixed findings to user for decision before proceeding.

## Review Focus Areas

1. **Correctness** — Logic errors, wrong assumptions, missing edge cases
2. **Cross-references** — File paths, function signatures match actual code
3. **Security** — Exposed secrets, missing auth, injection vectors
4. **Consistency** — Internal consistency between related files
5. **Completeness** — Missing steps, undocumented dependencies

## Best Practices

- **Separate reviewer context**: Self-review has confirmation bias
- **Diff-only re-review**: On rounds 2+, tell reviewer what changed
- **Evidence requirements**: Every finding needs file path, line number, explanation
- **Convergence over perfection**: Approve when work "definitely improves overall code health"
- **Iteration cap**: Always set a maximum. Escalate to human if issues persist.
