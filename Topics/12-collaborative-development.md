# RCET 3371 — Collaborative Development with Git

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 12](../LearningPath/12-Collaborative-Development.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. Shared-repository workflow
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

Individual Git skill is not enough for team engineering. Collaboration adds coordination: concurrent changes, review, merge decisions, ownership, integration, and a shared definition of trustworthy main.

The goal is not to maximize branches or pull requests. The goal is to make change visible, reviewable, recoverable, and attributable.

## 2. Outcomes

You should be able to:

- create focused branches from current main;
- update a branch when main changes;
- open a clear pull request;
- review behavior, tests, documentation, and design rather than only style;
- diagnose and resolve conflicts;
- distinguish merge conflict from semantic/integration conflict;
- use issues/milestones to coordinate work;
- preserve a trustworthy default branch;
- provide evidence of contribution and review.

## 3. Prerequisites

Section 1 Git state/recovery plus experience with the course projects.

## 4. Core model

A healthy shared workflow has:

**trusted main**  
Known integrated state.

**temporary branch**  
A bounded change.

**pull request**  
A proposed integration point with discussion and automated checks.

**review**  
Human verification of correctness, clarity, and impact.

**merge**  
Deliberate integration after requirements pass.

The branch is temporary. The commit history and merged result are durable.

## 5. Shared-repository workflow

Typical sequence:

    update local main
    create focused branch
    make coherent commits
    push branch
    open draft/PR
    run checks
    review
    respond to feedback
    update from main if needed
    merge
    delete branch

### Review questions

A useful reviewer asks:

- does it satisfy the requirement?
- are edge cases handled?
- do tests prove the behavior?
- did the change alter a public interface?
- are names/contracts understandable?
- does documentation agree?
- did this introduce duplicated responsibility?

### Merge conflicts

A merge conflict means Git cannot automatically combine text/history.

It does **not** tell you which behavior is correct.

After resolving markers:

- inspect diff;
- build;
- run tests;
- verify both intended changes survived.

### Semantic conflict

Two branches can merge without a textual conflict and still be behaviorally incompatible.

Example:

- branch A changes protocol field from Celsius to millidegrees;
- branch B updates UI assuming Celsius;
- files do not overlap;
- automatic merge succeeds;
- system is wrong.

This is why review and integration tests matter.

## 6. Worked examples

### Example 1: focused PR

Weak PR: "finish project" with 40 unrelated files.

Stronger PR: "separate protocol parser from SerialPort transport" with parser tests and no UI redesign.

### Example 2: conflict resolution

Both branches edit one function.

Do not choose "ours" or "theirs" by habit. Reconstruct the intended final contract, combine as needed, then test.

### Example 3: issue as coordination

Issue:

    Add replay of captured serial log
    Acceptance:
      - reads current log format
      - produces same parsed messages as live parser
      - rejects malformed record with line number

A teammate can implement without guessing the goal.

## 7. Apply, verify, and troubleshoot

Before PR:

- branch is based reasonably close to current main;
- diff contains intended work;
- build/tests pass;
- generated/noise files excluded;
- description explains behavior and verification.

During review:

- reproduce important behavior;
- ask for evidence;
- distinguish required change from preference.

After merge:

- update local main;
- delete stale branch;
- verify integrated main.

## 8. Practice

1. Why can an automatic merge still be wrong?
2. What makes a PR easier to review?
3. What should you do after resolving textual conflict markers?
4. Why should branches normally be temporary?
5. Write acceptance criteria for "add reconnect support."
6. What evidence demonstrates meaningful review contribution?

## 9. Answer key

1. Separate files/lines can encode incompatible assumptions.
2. Focused scope, clear contract, coherent commits, tests, and concise description.
3. Inspect diff, build, test, and verify behavior from both changes.
4. Durable state belongs in main/history/tags; old writable branches create ambiguity and synchronization risk.
5. Example: detect disconnect, transition visible state, no crash, bounded retry policy, re-identify device, restore ready state, log transitions.
6. Review comments tied to correctness/design, reproduced tests, identified defect/risk, or approval after checking stated criteria.

## 10. Explain without notes

Explain:

- trusted main;
- branch and PR lifecycle;
- review versus style preference;
- textual conflict versus semantic conflict;
- why tests run after conflict resolution;
- issue acceptance criteria;
- contribution evidence.

## 11. References

- GitHub Docs, pull requests — https://docs.github.com/en/pull-requests
- GitHub Docs, merge conflicts — https://docs.github.com/en/pull-requests/reference/merge-conflicts
- Git documentation — https://git-scm.com/docs
