---
name: lessons
description: Use after any mistake, correction, or unexpected outcome to capture the most generalizable version of the learning in the right place.
---

# Capturing Lessons

Turn specific mistakes into general principles. Put them where they'll actually prevent the next mistake.

## When to Use

- After any user correction ("no, don't do that", "that's wrong")
- After discovering a wrong assumption
- After a code review catches something you should have caught
- After a fix that required rework
- Periodically, to audit `docs/lessons.md` for entries that should be promoted or pruned

## The Process

### 1. Identify the Root Pattern

Don't record what happened. Record **why** it happened.

| Level | Example | Quality |
|-------|---------|---------|
| **Symptom** | "Don't store strings in the health field" | Bad — too specific |
| **Mechanism** | "Typed fields should always contain valid typed values" | Better — covers a class |
| **Principle** | "Extend the model, don't bolt on workarounds" | Best — applies everywhere |

**Ask yourself:** If I encountered a completely different codebase with a completely different bug, would this lesson still help me? If not, generalize further.

### 2. Decide Where It Belongs

```
CLAUDE.md          <- Rules that govern ALL behavior (rare — only after 2+ occurrences)
  ^ promote
Skill files        <- Patterns specific to a workflow (e.g., bug-fix, test-writing)
  ^ promote
docs/lessons.md    <- General principles not yet promoted (staging area)
  ^ capture
```

| The learning is about... | Put it in... |
|--------------------------|-------------|
| A specific workflow step that failed | The skill file for that workflow |
| A general engineering principle | `docs/lessons.md` — promote to CLAUDE.md if it recurs |
| A tool/API behavior | The skill that uses that tool |

### 3. Write the Lesson

**Format:** One sentence stating the rule, then a **Test** or **Why** that makes it actionable.

Good:
> **Extend the model, don't bolt on workarounds.** Test: if your fix requires `if/else` in every consumer, you're patching around the model instead of fixing it.

Bad:
> Don't store 'AI' as a string in the health column of the players table because it breaks joins.

**Rules:**
- Lead with the principle, not the story
- No proper nouns unless the lesson is specifically about that thing
- Include a **test** (how to detect you're about to make this mistake) or a **why** (the cost of ignoring this)
- One lesson per entry

### 4. Check for Duplicates and Promotions

Before adding:
1. Read `docs/lessons.md` — does a similar lesson already exist? Update instead of adding a duplicate.
2. Check CLAUDE.md — is this principle already there?
3. Same mistake happened twice -> promote from `docs/lessons.md` to a skill file or CLAUDE.md

### 5. Prune

After adding or promoting:
- **Obsolete:** Can't happen anymore -> delete
- **Promoted:** Now lives in CLAUDE.md or a skill -> delete from lessons.md
- **Too specific:** Won't recur -> delete or generalize
- **Redundant:** Same as another entry -> merge

Keep `docs/lessons.md` under 20 entries.

## Anti-Patterns

| Anti-Pattern | Why It's Wrong | Do Instead |
|---|---|---|
| Recording every specific mistake | Unreadable list nobody checks | Generalize to principles |
| "Don't do X" without why/test | Future you won't know when it applies | Add a test or reason |
| Putting everything in lessons.md | Important rules get buried | Promote to the right level |
| Never pruning | Stale entries erode trust | Audit periodically |
| Recording the story, not the principle | "Last Tuesday..." | State the rule, not the narrative |
