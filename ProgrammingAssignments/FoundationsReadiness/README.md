# Foundations Readiness

**Programming Assignments category value: 100 points out of 1000**

Sections: 1–2

## Objective

Demonstrate that your development environment, Git workflow, C#/.NET toolchain, Python toolchain, and minimal event-driven desktop model are ready for later RCET 3371 work.

This assignment is deliberately small in algorithmic difficulty. The evidence must show that the tools and workflow are real, reproducible, and understood.

## Deliverables

Submit one Git repository containing:

```text
README.md
.gitignore
csharp/
python/
desktop/
evidence/
```

### Part 1 — Git repository and history

Create the repository from the beginning of the assignment.

Your history must include meaningful separate commits for:

1. repository/project setup;
2. C# baseline;
3. Python baseline;
4. desktop retained-state example;
5. recovery/conflict exercise and documentation;
6. final verification cleanup.

Do not manufacture all commits after the work is finished.

### Part 2 — C# baseline

Under `csharp/`, create a .NET 10 console project.

The program must:

- accept one command-line argument;
- print the argument;
- print the running .NET version;
- return a nonzero exit code with a clear message when the argument is missing.

Document:

```bash
dotnet restore
dotnet build
dotnet run -- <value>
```

### Part 3 — Python baseline

Under `python/`, create a Python 3.14 script.

The program must:

- accept one command-line argument;
- print the argument;
- print `sys.version`;
- return a nonzero exit code with a clear message when the argument is missing.

Include instructions to create a local virtual environment even though this part needs only the standard library.

### Part 4 — Desktop retained-state example

Under `desktop/`, create a minimal .NET 10 WinForms application.

Required behavior:

- one control adds a point/marker to retained application state;
- the Paint event redraws **all** retained points/markers;
- resize, cover/uncover, and minimize/restore do not erase the logical drawing;
- presentation code is separate from the collection that owns the retained points.

The appearance is not graded beyond usability.

### Part 5 — Recovery/conflict exercise

Create two short-lived branches from the same base commit.

Both branches must modify the same line in `evidence/conflict.txt` differently.

Merge one branch, then merge the other and resolve the resulting conflict deliberately.

In `evidence/git-recovery.md`, include:

- the commands used;
- what the conflict represented;
- the intended integrated result;
- the final `git log --oneline --graph --decorate --all` excerpt;
- one paragraph explaining why blindly selecting "ours" or "theirs" would be weak engineering.

Delete merged branches after the evidence is recorded unless your hosting workflow preserves them automatically.

## README requirements

Your repository README must include:

- required tool versions;
- build/run commands for all three programs;
- what each program demonstrates;
- known OS limitation for WinForms;
- verification steps;
- repository layout.

## Complete when

A grader can clone the repository into a clean directory and:

1. build/run C# from documented commands;
2. run Python from documented commands;
3. build/run the WinForms project on Windows;
4. verify retained drawing survives repaint;
5. inspect meaningful Git history;
6. understand the conflict and its resolution.

## Evaluation

See [rubric.md](rubric.md).
