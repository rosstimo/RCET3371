# Git Collaboration and Recovery

[Git guide](README.md)

## Principle

Do not choose a recovery command from memory until you know which state is wrong.

## Common cases

### Discard an unstaged edit

First inspect:

    git status
    git diff -- path

If you truly want the working file to match the index/HEAD version, use the current Git restore workflow:

    git restore path

This is destructive to the unstaged edit. Verify before running it.

### Remove a file from the next commit but keep the edit

If the file is staged but should remain modified in the working tree:

    git restore --staged path

Then verify with status and diff.

### Inspect an old version without rewriting current history

Use:

    git show <commit>:path/to/file

or inspect the commit:

    git show <commit>

### Branch workflow

Typical team flow:

    git switch main
    git pull
    git switch -c feature/clear-name

Work, commit, push, then open a pull request.

### Update a branch from main

Before integration:

- commit or deliberately preserve current work;
- fetch/pull current main;
- merge or rebase according to the repository workflow;
- resolve conflicts by reconstructing intended final behavior;
- build/test after resolution.

### Rejected push

Do not force automatically.

Investigate:

    git status
    git branch -vv
    git fetch
    git log --oneline --graph --decorate --all

Understand why histories differ.

### Merge conflict

Conflict markers are evidence that Git cannot choose the final text.

Resolving them is not complete until:

- markers are gone;
- diff is reviewed;
- build succeeds;
- tests/verification pass;
- intended behavior from both lines of work is preserved.

## Pull-request review

A useful PR description states:

- problem;
- behavior changed;
- tests/evidence;
- known limitations;
- public interfaces/paths affected.

A useful review checks requirements and behavior, not only formatting.

## Never use destructive history rewriting casually

Commands that reset, force-push, or rewrite history can be appropriate in controlled situations, but they are not default recovery tools. Preserve shared work and inspect state first.

## Official references

- Git restore: https://git-scm.com/docs/git-restore
- Git reset: https://git-scm.com/docs/git-reset
- Git branch: https://git-scm.com/docs/git-branch
- GitHub merge conflicts: https://docs.github.com/en/pull-requests/reference/merge-conflicts
