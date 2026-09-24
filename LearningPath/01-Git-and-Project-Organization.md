# Section 1 - Git and Project Organization

This section starts with a workflow you already used in RCET 2265. The goal is not to turn Git into an advanced topic immediately. The goal is to make the familiar edit/build/commit/push cycle easier to understand and troubleshoot.

## Start from what you already know

You should already have used a basic cycle similar to:

```text
edit -> build/test -> git status -> git add -> git commit -> git push
```

We will keep that cycle and add only a few new inspection skills at first.

## Outcomes

By the end of this section you should be able to:

- create or clone a repository and find the project inside it;
- use `git status` to identify untracked, modified, and staged files;
- explain the difference between your working files, staged content, and a commit;
- inspect changes with `git diff` and `git diff --staged`;
- make several meaningful commits while developing a small program;
- use `git log` to inspect history;
- push and pull without confusing local work with GitHub;
- organize a repository so another student can find and build the program.

Branch collaboration, pull requests, deliberate merge conflicts, and team integration are taught later in [Section 12](12-Collaborative-Development.md).

## Learn

1. Work through [Git and project organization](../Topics/git-project-organization.md).
2. Use the [Git workflow guide](../Guides/Git/README.md) while you work.
3. Keep the [Git quick reference](../References/git-quick-reference.md) open when needed.
4. Review the [repository and submission standard](../Standards/repository-and-submission.md).

## Example progression

1. Make one edit and use `git status`.
2. Inspect it with `git diff`.
3. Stage only that file.
4. Compare `git diff` with `git diff --staged`.
5. Commit it.
6. Make a second small edit and repeat.
7. Use `git log --oneline` to see the story you just created.

## Practice

**Predict:** Given a short `git status` output, identify what is untracked, modified, or staged.

**Follow:** Reproduce the worked edit -> inspect -> stage -> inspect -> commit example.

**Modify:** Add a second program feature and commit it separately.

**Build:** Create a clean repository for a small RCET 2265-style C# program with a README and useful commit history.

## Programming Assignment

Begin [Foundations Readiness](../ProgrammingAssignments/FoundationsReadiness/README.md), Part 1.

## Assessment

Complete the Section 1 practice quiz before the graded Section 1 assessment.

## Ready to continue when

You can make a small C# change, show exactly where that change exists in Git, commit it deliberately, and explain the difference between the local repository and GitHub.

Next: [Section 2 - Toolchains and First Cross-Language Programs](02-Toolchains-Recovery-and-Desktop-Events.md)
