---
name: writing-plans
description: Two-tier implementation plans with mandatory research phase and mandatory review. Standard Plans (small features) have a lighter structure. Strategic Plans (complex features) include full strategic context. Both tiers run /reviewing-plans before presenting to the user.
---

# Writing Plans

## Overview

Write implementation plans that are right-sized for the task: **Standard Plans** for small, clear-cut features, and **Strategic Plans** for complex work that needs full strategic context.

Both tiers include a research phase, implementation tasks with TDD steps, and **mandatory `/reviewing-plans` review before presenting to the user**.

**Announce at start:** "I'm using the writing-plans skill to create the implementation plan."

**Save plans to:** `docs/plans/YYYY-MM-DD-<feature-name>.md`

## Tier Classification

Before writing anything, classify the plan as Standard or Strategic.

**Standard Plan** — lighter structure. ALL of these must be true:
- <=5 implementation tasks
- Touches <=3 files
- No new architectural patterns introduced
- No external service integrations
- No data model changes
- Clear single approach — no meaningful alternatives to consider

**Strategic Plan** — full strategic header + tasks:
- Everything else: >5 tasks, new patterns, new systems, multi-file changes
- Any case where you think "I'm not sure which approach is best"
- Any case where the user asked for a thorough plan

**When in doubt, classify as Strategic.** The cost of an unnecessary header (~15 min) is lower than the cost of missing a strategic gap.

**User override — skip review:** If the user explicitly says to skip review, still write the full plan but skip the `/reviewing-plans` auto-review and present the plan directly.

---

## Phase 0: Research (Both Tiers)

Before writing the plan, search for how others have solved the same problem.

### For Standard Plans (quick check, 1-2 searches):
1. Search official docs for the tools/APIs being used
2. One search for how others approach the same problem
3. Note findings in the Research Check section

### For Strategic Plans (thorough research):
1. Search for current best practices
2. Check official docs for tools/APIs being used
3. Look for expert implementations
4. Search for community discussions and blog posts
5. Synthesize into 2-4 bullet points with sources in the Prior Art section

**When to skip research:** Pure refactors or bug fixes where the approach is established. Note "N/A — established pattern" in the Research Check section.

---

## Standard Plan Template

```markdown
# [Feature Name] Implementation Plan

## Goal
[One sentence describing what this builds]

## Why Standard
[One sentence: why this qualifies for the lighter Standard tier.]

## Approach
[One sentence: key technology/pattern being used. Optional — omit if obvious.]

## Research Check
- [What was searched and whether it changed the approach.]

---

## Tasks
[Implementation tasks — see Task Structure below]
```

---

## Strategic Plan Template

Each section maps to a review dimension in `/reviewing-plans`.

```markdown
# [Feature Name] Implementation Plan

## Problem & Why Now
[What's broken or missing and why it matters now. 2-5 sentences.
Could a simpler intervention work? If so, why isn't it enough?]

## Prior Art & Research
[2-4 bullet points with sources. What did you find when researching
how others solve this? What influenced the approach?]

## Alternatives Considered
[2-3 alternatives, each 3-5 lines. Always include "do less" or "do nothing."
Each MUST end with a Verdict line: Chosen / Rejected — [reason].]

## Assumptions
[3-5 bullet points. For each: what breaks if it's wrong?]

## Approach & Rationale
[ADR-style Y-statement: "In the context of [X], facing [Y], we chose [Z]
to achieve [W], accepting [V]."
Then 1-2 paragraphs of detail.]

## Risks & Rollback
[Top 3 risks with probability, impact, mitigation, and rollback plan.]

## Non-Goals
[3-5 bullet points. What this plan deliberately does NOT do.]

## Success Criteria
[3-5 testable, time-bound checks. How do we verify this worked?]

---

## Tasks
[Implementation tasks — see Task Structure below]
```

---

## Task Structure (Both Tiers)

Bite-sized tasks. Each step is one action (2-5 minutes).

````markdown
### Task N: [Component Name]

**Files:**
- Create: `exact/path/to/file`
- Modify: `exact/path/to/existing`
- Test: `tests/exact/path/to/test`

**Step 1: Write the failing test**

```
[test code]
```

**Step 2: Run test to verify it fails**

Run: `[test command]`
Expected: FAIL with "[reason]"

**Step 3: Write minimal implementation**

```
[implementation code]
```

**Step 4: Run test to verify it passes**

Run: `[test command]`
Expected: PASS

**Step 5: Commit**

```bash
git add [files]
git commit -m "feat: [description]"
```
````

**Task rules:**
- Exact file paths always
- Complete code in plan (not "add validation")
- Exact commands with expected output
- TDD: test first, then implement
- Frequent commits

---

## Review Gate (Both Tiers)

After saving the plan, **always run `/reviewing-plans`** — do NOT present to the user first.

Flow: write plan -> auto-run review -> fix issues -> THEN present to user.

1. Save the plan to `docs/plans/<filename>.md`
2. **Self-rate before review.** Rate the plan 0-10. Fix anything you can before submitting to review.
3. Announce: **"Plan saved (self-rated [N]/10). Running plan review before presenting for your approval."**
4. **Invoke `/reviewing-plans`**
5. Fix any GAPs and CONCERNs found during review
6. After review converges:
   - **PASS/WARN:** Present the reviewed plan with the review summary and final rating
   - **FAIL:** Present remaining GAPs to the user for input
7. Wait for user approval before proceeding to execution
