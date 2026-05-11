---
name: reviewing-plans
description: Reviews implementation plans for strategic correctness before execution — catches missing alternatives, unstated assumptions, risk blind spots, scope issues, and measurement gaps.
---

# Reviewing Plans

## Overview

Reviews implementation plans for **strategic correctness** before execution begins. Uses a separate subagent to avoid self-confirmation bias. Iterates up to 3 rounds until convergence (zero GAPs).

**Core insight:** Plan review asks "should we build this? this way?" Code review asks "is this built correctly?" These are complementary but non-overlapping.

## Review Dimensions

### Full 8 dimensions (Strategic Plans)

| # | Dimension | Core Question |
|---|-----------|---------------|
| 1 | **Problem Validity** | Is the right problem being solved? Could a simpler intervention work? |
| 2 | **Alternative Exploration** | Were meaningful alternatives considered, including "do less"? |
| 3 | **Assumption Exposure** | Are assumptions explicit? What breaks if each is wrong? |
| 4 | **Decision Rationale** | For each choice, is the "why" convincing to a skeptical senior engineer? |
| 5 | **Risk Identification** | Top 3 risks named with mitigations? Rollback plan? |
| 6 | **Scope Discipline** | Non-goals stated? Boundary clear? Tasks all serve the goal? |
| 7 | **Success Criteria** | How do we verify this worked? Measurable, time-bound checks? |
| 8 | **Internal Consistency** | Do goals, approach, tasks, and success criteria align? |

### Reduced 5 dimensions (Standard Plans)

| # | Dimension | Core Question |
|---|-----------|---------------|
| 1 | **Problem Validity** | Is the right problem being solved? Could a simpler intervention work? |
| 3 | **Assumption Exposure** | Are assumptions explicit? What breaks if each is wrong? |
| 4 | **Decision Rationale** | For each choice, is the "why" convincing? |
| 6 | **Scope Discipline** | Do all tasks serve the goal? Any nice-to-haves that don't belong? |
| 8 | **Internal Consistency** | Do goal, approach, and tasks align? |

### How to detect plan tier

Check the plan file for a `## Why Standard` section. If present -> Standard (5 dimensions). If absent -> Strategic (8 dimensions).

## Convergence Criteria

The review loop stops when ANY of these:

1. **Zero GAPs** in the latest round
2. **Max 3 rounds reached**
3. **RETHINK verdict** — immediate exit, escalate to user
4. **User intervention**

## Process

### Step 1: Read the plan fully.

### Step 2: Initialize tracking
> "Starting iterative plan review. Up to 3 rounds, fixing GAPs between rounds, until zero GAPs remain."

### Step 3: Dispatch the plan review subagent

Use the Agent tool with a separate agent context. The reviewer **MUST** be a different context — self-review has confirmation bias.

**Strategic Plan review prompt:**

```
Review this implementation plan for STRATEGIC correctness. You are a senior
technical architect performing a PLAN review — NOT a code review.

Focus on whether we should build this and whether the approach is sound.

## Review across these 8 dimensions:

### 1. Problem Validity
- Is the stated problem real, correctly scoped, and worth solving now?
- Could a simpler intervention achieve the same goal?

### 2. Alternative Exploration
- Were meaningful alternatives considered and honestly evaluated?
- Is a "do less" or "do nothing" option missing?

### 3. Assumption Exposure
- List every assumption the plan makes (stated AND unstated)
- For each: what breaks if it's wrong?

### 4. Decision Rationale
- For each significant design choice, is the reasoning convincing?
- Would a skeptical senior engineer be persuaded?

### 5. Risk Identification
- Top 3 risks with mitigations? Rollback plan?

### 6. Scope Discipline
- Non-goals stated? Every task serves the stated goal?

### 7. Success Criteria
- Measurable, time-bound checks?

### 8. Internal Consistency
- Do goals, approach, tasks, and success criteria align?

## Cross-reference against the actual codebase
Read referenced files. Verify paths and compatibility.

## Output format
For each dimension:
- **Score:** PASS / CONCERN / GAP
- **Finding:** Specific observation with evidence
- **Recommendation:** Concrete action if CONCERN or GAP

End with:
- **Quality rating:** 0-10. What would get it to a 10?
- **Overall verdict:** APPROVE / REVISE / RETHINK
- **Top 3 actions**
```

**Rounds 2+:** Focused re-review on only GAP/CONCERN dimensions, plus a full-plan coherence check for regressions.

### Step 4: Triage and iterate

- **RETHINK:** STOP. Present to user.
- **APPROVE:** Proceed to final report.
- **REVISE:** Fix GAPs, re-run review.

### Step 5: Final report

```
ITERATIVE PLAN REVIEW COMPLETE
===============================
Plan: [name]
Rounds: N
Total findings: X (Y fixed, Z accepted as minor CONCERN)
Convergence trend: [GAPs per round]

Verdict: PASS / WARN / FAIL

Dimension Scores (final):
[list each dimension with PASS/CONCERN/GAP]

Round-by-Round Summary:
[what was found and fixed each round]

Remaining CONCERNs (non-blocking):
[list any]
```

### Step 6: Executive summary (for the user)

```
## Plan Review Summary

**What we're building:** [1-2 sentences]

**Key decisions made during review:**
1. [Decision]: [What] -- [Why]

**Tradeoffs accepted:**
- [Tradeoff]: We chose [X over Y] because [reason]. Cost: [what we give up].

**Risks we're accepting:**
- [Risk] (probability/impact) -- Mitigation: [plan]

**What changed from the original plan:**
- [Change]: [Original] -> [Revised] -- [Why]
```

## Common Plan Review Failures

| Failure | What it looks like |
|---------|--------------------|
| Wrong problem | Solves a symptom, not root cause |
| Missing "do less" | Complex solution when a simpler one would suffice |
| Unstated assumptions | "Users will..." without evidence |
| No rollback plan | Changes are one-way with no revert path |
| No success criteria | "Deploy and see" instead of measurable outcomes |
| Scope creep | Tasks that don't serve the stated goal |
| Circular rationale | "We chose X because X is best" |
