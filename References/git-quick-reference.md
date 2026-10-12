# Git Quick Reference

[References index](README.md)

## Inspect

    git status
    git diff
    git diff --staged
    git log --oneline --decorate --graph -10
    git show <commit>
    git branch -vv
    git remote -v

## Stage and commit

    git add path
    git restore --staged path
    git commit -m "Describe the engineering change"

## Branch

    git switch main
    git pull
    git switch -c feature/clear-name
    git switch branch-name

## Synchronize

    git fetch
    git pull
    git push
    git push -u origin branch-name

## Restore an unstaged file

Inspect first:

    git diff -- path

Then, only if the edit should be discarded:

    git restore path

## Conflict checklist

1. status
2. inspect markers
3. reconstruct intended final behavior
4. stage resolved files
5. finish merge/rebase
6. build/test
7. inspect final diff/history

## Rule

When uncertain, **inspect first**. Do not force-push, hard-reset, or delete files merely because the repository feels confusing.

Official docs: https://git-scm.com/docs
