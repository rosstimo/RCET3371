# Git Workflow Guide

[Guides index](../README.md) · [Section 1](../../LearningPath/01-Git-and-Project-Organization.md)

Use this guide for the recurring Git workflow used throughout RCET 3371.

## Everyday cycle

1. Inspect before changing:
   
       git status
       git log --oneline --decorate -8

2. Make one coherent change.

3. Inspect exactly what changed:

       git diff

4. Stage intentionally:

       git add path/to/file

5. Inspect the staged snapshot:

       git diff --staged

6. Commit with a message that describes the engineering change:

       git commit -m "Add parser length validation"

7. Verify:

       git status
       dotnet test
       # or the appropriate course build/test command

8. Synchronize only after understanding local/remote state.

## Repository-per-assignment expectation

Each Programming Assignment should normally live in its own student repository unless the assignment explicitly says otherwise.

A good repository root makes the project obvious:

    README.md
    source/project files
    tests or verification
    supporting data only when needed
    .gitignore

Do not commit:

- build output;
- IDE caches;
- secrets/tokens;
- machine-specific temporary files;
- downloaded dependencies that the package manager can restore.

## Before recovery

Always capture:

    git status
    git diff
    git diff --staged
    git log --oneline --decorate --graph -10

Then decide which state is wrong.

See [Collaboration and Recovery](collaboration-and-recovery.md) for branches, remotes, pull requests, conflicts, and common recovery patterns.

## Commit-quality check

A commit should answer:

- what behavior/design changed?
- can another person review it independently?
- did unrelated formatting/generated files sneak in?
- does the project still build/test?
- is the message more informative than "update"?

## Official references

- https://git-scm.com/docs
- https://docs.github.com/en/pull-requests
