---
name: sibling-search
description: Systematic codebase-wide search for sibling instances of a root cause pattern. Generalizes from the specific finding to the abstract vulnerability, then searches exhaustively.
---

# Sibling Search

Find every instance of a root cause pattern across the codebase. The key insight:
**generalize from the specific finding to the abstract vulnerability, then search for that.**

**Announce at start:** "I'm using the sibling-search skill to find related instances of this root cause."

## When to Use

- After identifying a bug's root cause, before creating a PR
- When refactoring a pattern and need to find all instances
- After fixing a bug, as belt-and-suspenders for related patterns

## The Generalization Ladder

The most common failure mode is searching too narrowly. Every specific finding sits on
a generalization ladder. Climb it before searching.

**Example — player health float-to-integer bug:**

| Level | Pattern | Search |
|-------|---------|--------|
| Too narrow | `player_health` receives float | `grep player_health` |
| Narrow | `Math.round()` writes to integer field | `grep Math.round` |
| **Right level** | **Any computed value writing to any typed field without casting** | `grep` for assignment patterns cross-ref with type definitions |
| Broader | **Any type mismatch at a boundary** | All typed field assignments |
| Too broad | Type mismatches | (not searchable) |

**The right level is the highest level that is still concretely searchable.**

Rules:
- The search pattern should describe the **vulnerability class**, not the specific instance
- If you're grepping for the exact variable name from the bug, you're too narrow
- If you can't write a grep command, you're too broad

## Process

### Step 1: Define the Root Cause Category

| Category | Generalized Pattern | Search Strategy |
|----------|-------------------|-----------------|
| **Type mismatch** | Computed values written to typed fields without casting | Find typed fields, check if callers cast before write |
| **Null access** | Accessing properties on nullable objects without guards | Search for property access patterns, check which guard for null |
| **Unvalidated input** | User-supplied values used without validation | Search for input usage patterns, check validation |
| **Missing error handling** | Operations that can fail without retry/fallback | Search for external calls, check error handling |
| **Stale state** | Code reading state that could be outdated | Search for cached reads, check invalidation |
| **Race condition** | Concurrent access to shared mutable state | Search for shared state mutations |

### Step 2: Write Search Commands

Plan before executing:

```
SIBLING SEARCH PLAN:
Root cause: [generalized description]
Category: [from table above]
Searches:
  1. [grep command] -- finds [what]
  2. [grep command] -- cross-references with [what]
  3. [check] -- verifies [what]
```

### Step 3: Execute and Classify

For each result:

| Classification | Criteria | Action |
|---------------|----------|--------|
| **Same fix applies** | Exact same pattern, same fix works | Fix in this PR |
| **Same root cause, different fix** | Same vulnerability class, different fix | Create issue |
| **Already guarded** | Pattern exists but has proper handling | Note as safe |
| **False positive** | Looks similar but different cause | Note why |

### Step 4: Report

```
SIBLING SEARCH COMPLETE:
Root cause: [generalized pattern]
Category: [category name]
Searched: [N] files/locations
Found: [N] siblings
  - [file:line] -- [status: fixing in PR / issue #NNNN / already guarded / false positive]
Issues created: [list or "none"]
```

## Anti-Patterns

| Anti-Pattern | Example | Fix |
|---|---|---|
| Searching for the specific variable | `grep player_health` | Search for the pattern class |
| "No siblings found" without evidence | Agent says "searched and found nothing" | Must show actual search output |
| Searching only the same file | Checked only PlayerController | Search ALL files of that type |
| Grouping by symptom, not cause | "All NullRef errors are the same" | Different objects = different root causes |
| Declaring safe without verification | "The engine validates this" | Verify the engine actually does |
