---
name: create-issue
description: Create well-structured GitHub issues with proper labeling, duplicate detection, and clear acceptance criteria. Use when creating any new issue.
user_invocable: true
---

# Create Issue

Structured workflow for creating GitHub issues that are actionable by both humans and AI agents.

**Announce at start:** "I'm using the create-issue skill to ensure this issue is properly structured and deduplicated."

## Why This Skill Exists

Poorly written issues waste more time than they save. This skill ensures every issue is:

1. **Not a duplicate** — searched thoroughly before creation
2. **Properly classified** — labeled by type and priority
3. **Actionable** — enough detail for someone with zero context to start working
4. **Connected** — linked to related issues and relevant code

## Phase 1: Duplicate Detection (REQUIRED)

**This is the most common failure mode.** Never skip.

```bash
# Primary keyword search
gh issue list --search "primary keywords" --limit 10

# Try alternate phrasings
gh issue list --search "alternate keywords" --limit 10

# Search closed issues too
gh issue list --search "keywords" --state closed --limit 5

# Check for related PRs
gh pr list --search "keywords" --limit 5
```

| Finding | Action |
|---------|--------|
| **Exact duplicate (open)** | STOP. Comment on the existing issue. |
| **Similar issue (open)** | Decide: same problem (comment) or distinct (proceed but reference)? |
| **Closed as fixed** | Check if fix worked. If broken again, reopen — don't create new. |
| **Nothing found** | Proceed. |

## Phase 2: Classify

### Priority

| Priority | Criteria |
|----------|----------|
| `P1-critical` | Crashes, data loss, security vulnerability, blocking progress |
| `P2-high` | Significant user-facing bug, blocks a major feature |
| `P3-medium` | Quality improvement, workaround exists |
| `P4-low` | Minor polish, edge case, cosmetic |

### Type Labels

`bug`, `enhancement`, `feature`, `documentation`, `infrastructure`, `investigation`

## Phase 3: Write the Issue

### Title
- **Under 70 characters.** Scannable at a glance.
- **Start with what, not how.** "Player falls through floor on level 3" not "Fix collision detection in PhysicsEngine"
- **Be specific.** "Controls broken" is useless. "Jump doesn't register when pressing spacebar during fall animation" is actionable.

### Body Template

```markdown
## Problem
[What's broken or missing — plain English, user-facing impact.]

## Context
[Why this matters now. What triggered this report.
Link to related issues or discussions.]

## Solution
[Proposed approach — which files/components are likely involved.
Start here: `path/to/likely/file`]

## Acceptance Criteria
- [ ] [Specific, testable condition 1]
- [ ] [Specific, testable condition 2]
- [ ] [Tests written and passing]

## Risks
[What could go wrong. What else might break.]

Refs #NNN (if related to existing issues)
```

#### For Bugs — add between Problem and Context:

```markdown
## Steps to Reproduce
1. [Exact steps]
2. [Include specific state/conditions]

**Expected:** [what should happen]
**Actual:** [what happens instead]
```

### Readability Rules

- Lead Problem with **user-facing impact**, not technical root cause
- Keep Problem to 2-3 sentences max
- Solution section starts with a **"Start here:"** line — the one file to look at first
- Acceptance criteria are **user-visible outcomes**, not implementation steps

### Agent-Readiness Checklist

Before creating:
- [ ] File paths mentioned in Solution
- [ ] Acceptance criteria are testable
- [ ] Scope is bounded
- [ ] No ambiguous requirements

## Phase 4: Create

```bash
gh issue create \
  --title "descriptive title under 70 chars" \
  --label "TYPE,PRIORITY" \
  --body "..."
```

## What NOT to Create Issues For

- Trivial one-line fixes that are part of a larger issue
- Questions that don't result in actionable work
- Problems already fixed (verify first)
- Vague ideas without a concrete problem statement

## Anti-Patterns

| Anti-Pattern | Why | Do Instead |
|---|---|---|
| Skip duplicate search | Fragmented tracking | Always search first |
| Vague title | Can't scan or prioritize | Specific, under 70 chars |
| No acceptance criteria | "Done" is undefined | 3-7 testable criteria |
| No file references | Can't start working | Identify at least one file |
| Priority inflation | Everything P1 = nothing P1 | Use the criteria honestly |
