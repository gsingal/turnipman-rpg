---
name: quality-pipeline
description: Run the full quality pipeline (audit -> critique -> staged improvements -> verify). Enforces strict stage ordering with gate conditions.
arguments: file_path
---

# Quality Pipeline

Run a structured quality improvement pipeline on a file or component. Each stage has a gate condition that must be met before advancing.

## Pipeline Stages

```
audit -> critique -> determine improvements -> execute -> verify
```

**CRITICAL**: Never skip, reorder, or partially execute stages.

## Stage 1: Audit

Perform a thorough audit of the target file/component.

**Gate**: Audit report must include:
- Issue counts by severity (Critical / Important / Minor)
- Detailed findings with file:line references
- Recommendations by priority

If any section is missing, re-run the audit.

## Stage 2: Critique

Perform a design-level critique — not just correctness, but quality.

**Gate**: Critique must include:
- Overall impression
- Priority issues (each with What / Why / Fix)
- At least one "Question to Consider"
- **Quality rating:** 0-10, with specific items that would raise the score

**NEVER skip the critique.** It catches design-level issues the audit misses.

## Stage 3: Determine Improvements

Review both audit and critique findings. Create a prioritized improvement plan.

**Gate**: Present the plan:
```
QUALITY PIPELINE -- IMPROVEMENT PLAN:
Based on audit + critique:
1. [improvement] -- addresses [specific issues]
2. [improvement] -- addresses [specific issues]
3. [improvement] -- addresses [specific issues]
-> Proceeding unless you redirect.
```

Wait for user override before proceeding.

## Stage 4: Execute Improvements

Apply each improvement **in sequence**. After each:

1. Apply the changes
2. Verify no breakage (run tests if available)
3. Note what was changed and why

**Gate per improvement**: Verify:
- No broken structure
- No removed content that shouldn't have been removed
- Changes align with the recommendations

If an improvement breaks something, revert it and note the issue.

## Stage 5: Verify

Run all tests. Do a final read-through of the changed files.

**Gate**: All tests pass. Changes are coherent.

## Stage 6: Report

Provide:
1. Summary of all changes organized by category
2. Any issues encountered and how they were resolved
3. **Quality improvement:** Re-rate 0-10. Show before/after.

## Error Recovery

- If a stage fails, retry once with adjusted approach
- If it fails again, stop the pipeline, report what completed, ask the user
- Never silently skip a failed stage
- Never proceed past a gate that wasn't met
