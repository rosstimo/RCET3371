# RCET 3371 — Git and Project Organization

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 1](../LearningPath/01-Git-and-Project-Organization.md)

## Contents

1. Why this matters
2. What you should be able to do
3. Prerequisites
4. Core model and vocabulary
5. How Git state works
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

Version control is part of the engineering evidence for a software system. A useful repository lets another person answer three questions: what is true now, what changed, and why did it change.

Git is not a substitute for understanding the filesystem. Most recovery mistakes happen because someone runs a command before identifying whether the problem is in the working tree, staging area, commit history, branch relationship, or remote relationship.

## 2. What you should be able to do

You should be able to:

- explain working tree, index/staging area, commit, branch, and remote;
- interpret Git status before modifying repository state;
- stage only intended changes;
- write commits that preserve useful engineering history;
- inspect changes with diff, log, and show;
- distinguish local history from remote synchronization;
- recover from a common mistaken edit or commit without guessing;
- organize a repository so another engineer can build and review it.

## 3. Prerequisites

You should be comfortable with files, directories, a terminal, and a small C# project.

## 4. Core model and vocabulary

Think of Git as several related states:

**Working tree**  
The files currently visible in your project directory.

**Index / staging area**  
The exact content selected for the next commit.

**HEAD**  
The commit currently checked out.

**Branch**  
A movable name that normally points at the latest commit in one line of work.

**Remote**  
A named relationship to another repository, commonly origin.

A useful mental model is:

    working files -> stage selected content -> commit snapshot -> synchronize commits

The arrows are deliberate actions. Editing a file does not stage it. Staging does not commit it. Committing does not push it.

## 5. How Git state works

### Status first

Run:

    git status

before recovery or cleanup. Status distinguishes:

- untracked files;
- tracked files modified in the working tree;
- staged changes;
- branch/upstream relationship.

Do not memorize recovery commands without knowing which state they affect.

### Inspect before changing

Useful inspection commands:

    git diff
    git diff --staged
    git log --oneline --decorate --graph
    git show <commit>

The first diff compares working content with the index. The staged diff compares the index with the current commit.

### Stage intentionally

Avoid treating "add everything" as the only workflow. A strong commit contains one coherent change.

Typical cycle:

    git status
    git diff
    git add path/to/file
    git diff --staged
    git commit -m "Describe the engineering change"
    git status

### Remote synchronization

Push publishes local commits to a configured remote branch. Pull normally fetches remote information and then integrates it.

A rejected push is evidence that local and remote histories differ. It is not a reason to force-push automatically.

## 6. Worked examples

### Example 1: modified but not staged

Suppose status reports Program.cs under "Changes not staged for commit."

That means:

- HEAD has the last committed Program.cs;
- the index still matches HEAD;
- the working tree contains a newer edit.

Before staging, use:

    git diff -- Program.cs

If the edit belongs in the next commit:

    git add Program.cs
    git diff --staged

### Example 2: accidental staging

You staged DebugNotes.txt but do not want it in the next commit.

The correct problem is not "delete the file." The problem is "remove this path from the index while preserving the working file."

Use the Git-supported restore/reset operation appropriate to your installed Git version, then confirm with status. The course quick reference gives the current command form.

### Example 3: useful history

Bad history:

    update
    stuff
    more changes
    fixed it

Useful history:

    Add packet status decoder
    Reject packets shorter than four bytes
    Separate display formatting from decoder
    Add fixed vectors for overflow cases

The second history communicates design evolution.

## 7. Apply, verify, and troubleshoot

When the repository seems wrong:

1. stop changing files;
2. run status;
3. inspect the relevant diff;
4. inspect recent history;
5. identify which state is wrong;
6. choose the smallest action that changes only that state;
7. run status again;
8. build/test the project if code changed.

For repository organization, a reviewer should be able to identify:

- source code;
- project/solution files;
- tests or verification scripts;
- documentation;
- generated files that should be ignored.

## 8. Practice

1. A file is modified in the working tree but not staged. Which comparison shows only that edit?
2. A file is staged and then edited again. How can the staged content and the newer unstaged edit coexist?
3. Why is a force push a poor first response to a rejected normal push?
4. Design four meaningful commits for a small program that reads a sensor log, validates records, computes an average, and reports malformed rows.
5. You accidentally staged a file. What state should you change if you want to keep the file but remove it from the next commit?
6. Explain why "Git is my backup" is an incomplete engineering model.

## 9. Answer key

1. Compare the working tree to the index with git diff for that path.
2. The index contains the version captured when add was last run; the working tree can then change independently.
3. A rejected push often means the remote contains commits you do not yet have. Forcing can discard shared history. Inspect/fetch/integrate first.
4. One valid sequence: add parser, add validation, add statistics, add malformed-row reporting/tests. The exact sequence may differ if each commit is coherent and buildable enough to review.
5. Change the index/staging state, not the working file.
6. Git preserves versions and history, but it does not automatically protect every uncommitted file, every unpushed commit, every ignored artifact, or the remote service itself. It is version control first.

## 10. Explain without notes

Before continuing, explain:

- working tree versus index versus commit;
- what status tells you;
- staged versus unstaged diff;
- local commit versus push;
- why recovery begins with inspection;
- what makes a commit useful engineering evidence.

## 11. References

- Git project, *git-status* — https://git-scm.com/docs/git-status
- Git project, *git-diff* — https://git-scm.com/docs/git-diff
- Git project, *git-log* — https://git-scm.com/docs/git-log
- Git project, *git-restore* — https://git-scm.com/docs/git-restore
- GitHub Docs, *About pull requests* — https://docs.github.com/en/pull-requests/get-started/about-pull-requests
