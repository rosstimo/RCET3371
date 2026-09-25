# RCET 3371 - Git and Project Organization

*Self-learning guide*

[Topics index](README.md)

## 1. Start with the workflow you already used

RCET 2265 already used Git as part of normal programming work.

A familiar cycle is:

```text
edit -> build/test -> git status -> git add -> git commit -> git push
```

RCET 3371 begins by making that cycle easier to see and explain. Team branches, pull requests, and deliberate merge conflicts are taught later in [Collaborative Development](collaborative-development.md).

## 2. Outcomes

You should be able to:

- identify your repository and project files;
- distinguish working files, staged content, and committed content;
- use `git status` before changing repository state;
- inspect an edit with `git diff`;
- inspect the next proposed commit with `git diff --staged`;
- create several meaningful commits while a program develops;
- inspect recent history with `git log`;
- explain commit versus push;
- keep generated project output out of Git.

## 3. A picture of the basic Git states

For this section, use this model:

```text
edit file
   ↓
WORKING TREE
   ↓ git add
STAGING AREA
   ↓ git commit
LOCAL HISTORY
   ↓ git push
REMOTE / GITHUB
```

Each arrow is a separate action.

Editing does not stage.

Staging does not commit.

Committing does not push.

## 4. Worked example: watch one file move through the states

Start with a committed C# program:

```csharp
Console.WriteLine("Hello");
```

Change it to:

```csharp
Console.WriteLine("Hello, RCET 3371");
```

### Step A: inspect status

```text
git status
```

You should see `Program.cs` as modified but not staged.

### Step B: inspect the edit

```text
git diff -- Program.cs
```

Read the diff. Confirm it contains only the change you intended.

### Step C: stage it

```text
git add Program.cs
```

Run:

```text
git status
git diff --staged
```

Now the proposed commit contains that change.

### Step D: commit it

```text
git commit -m "Update greeting for RCET3371"
```

Then:

```text
git status
git log --oneline -5
```

The working tree should be clean, and the new commit should appear in local history.

### Step E: push

```text
git push
```

Push synchronizes committed history with the configured remote. It is not part of the commit itself.

## 5. Worked example: staged and unstaged changes at the same time

This situation often looks strange the first time.

1. Edit `Program.cs`.
2. Run `git add Program.cs`.
3. Edit `Program.cs` again.
4. Run `git status`.

The same file can now appear as:

- staged changes ready for the next commit;
- newer unstaged changes still only in the working tree.

Use:

```text
git diff
git diff --staged
```

to inspect the two versions separately.

This is a useful reason to inspect what you are committing rather than using Git commands mechanically.

## 6. Meaningful history

A useful history might look like:

```text
Add measurement input
Add average calculation
Reject invalid measurement count
Document build and run procedure
```

A weak history might look like:

```text
stuff
update
fix
final
final2
```

The useful version helps another person reconstruct what changed.

## 7. Repository organization

A small programming repository should make the important material obvious.

Typical contents:

```text
README.md
.gitignore
project/solution files
source files
required data/resources
```

Generated output such as ordinary `bin/` and `obj/` directories should normally be ignored because the toolchain can recreate them.

A README should at least tell another person:

- what the program does;
- what tools/version it expects;
- how to build it;
- how to run it.

## 8. What is deliberately postponed

You may see these Git ideas in existing repositories, but they are not the target of this topic:

- feature branches;
- pull requests;
- merge conflicts;
- rebasing;
- collaborative review;
- semantic conflicts between parallel changes.

Those are easier to understand after ordinary single-developer repository state is comfortable.

## 9. Troubleshooting

If Git looks wrong:

1. stop making unrelated changes;
2. run `git status`;
3. inspect `git diff`;
4. inspect `git diff --staged`;
5. inspect recent `git log`;
6. describe what state you expected and what state you actually have.

Do not delete `.git`, reclone, reset, or force-push just because you are unsure. Preserve the evidence first.

## 10. Practice

1. You edit `Program.cs` but have not run `git add`. Where is the new content?
2. What changes when you run `git add Program.cs`?
3. Does `git commit` upload the commit to GitHub?
4. Why run `git diff --staged` before committing?
5. A file was staged and then edited again. Which two commands show the two sets of changes?
6. Write three useful commit messages for adding input, calculation, and validation to a small program.
7. Why should `bin/` and `obj/` usually be ignored?

## 11. Answer reasoning

1. In the working tree.
2. The current file content is copied into the staging area as the proposed next commit content.
3. No. Push is separate.
4. It lets you review exactly what the next commit will contain.
5. `git diff --staged` shows staged content; `git diff` shows newer unstaged edits.
6. Examples: `Add measurement input`, `Calculate average measurement`, `Reject empty measurement list`.
7. They are reproducible build output and add noise/machine-specific artifacts to source history.

## 12. Ready to continue when

Explain without notes:

- working tree;
- staging area;
- commit;
- push;
- `git status`;
- `git diff` versus `git diff --staged`;
- what makes a useful commit.

## 13. References

- Git status: https://git-scm.com/docs/git-status
- Git diff: https://git-scm.com/docs/git-diff
- Git add: https://git-scm.com/docs/git-add
- Git commit: https://git-scm.com/docs/git-commit
- Git log: https://git-scm.com/docs/git-log
