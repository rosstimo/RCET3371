# Foundations Readiness

**Programming Assignments category value: 100 points out of 1000**

Sections: 1-2

## Objective

Prove that your course tools work and that you can carry familiar RCET 2265 programming ideas into the new RCET 3371 environments.

This is an **on-ramp assignment**. It is not intended to test advanced architecture, merge-conflict recovery, protocol design, or custom GUI drawing.

## Deliverables

Submit one Git repository containing:

```text
README.md
.gitignore
csharp/
python/
winforms/
embedded-c/
assembly/
evidence/
```

Your README must tell a classmate how to reproduce each first program/build.

## Part 1 - Repository and ordinary Git workflow

Create the repository at the beginning of the assignment.

Use the normal development cycle from RCET 2265:

```text
edit -> build/run -> git status -> inspect -> add -> commit -> push
```

Your history must contain multiple meaningful commits created while the work develops.

Required evidence:

- one screenshot or text capture of `git status` with a modified file;
- one `git diff` example;
- one `git diff --staged` example;
- final `git log --oneline`.

Branches and merge conflicts are taught later in the course and are not required here.

## Part 2 - C# familiar baseline

Under `csharp/`, create a .NET 10 console program that:

1. stores at least three integer measurements;
2. computes their sum and average;
3. uses at least one helper method;
4. prints the individual values and result;
5. can be built and run from the command line.

Document:

```text
dotnet restore
dotnet build
dotnet run
```

This part should feel like RCET 2265.

## Part 3 - Python translation

Under `python/`, recreate the same small measurement program in Python 3.14.

Keep the behavior intentionally similar to the C# version.

Your README must identify:

- which C# ideas transferred directly;
- which syntax changed;
- how you ran the script;
- which Python interpreter was used.

Create a local virtual environment and document how to activate it, even if the program uses only the standard library.

## Part 4 - Simple Windows Forms event review

Under `winforms/`, create a .NET 10 Windows Forms application with:

- one Button;
- one Label;
- one integer counter stored as form/class state;
- each button click increments the counter and updates the Label.

This reviews ordinary event-driven programming. Custom Paint/repaint architecture is not required in Foundations Readiness.

## Part 5 - PIC16F883 first builds

### Embedded C

Under `embedded-c/`, include the source from a minimal PIC16F883 XC8 project that successfully builds.

The program must contain:

- `#include <xc.h>`;
- at least one fixed-width integer value;
- a stable `while (1)` loop.

### Assembly

Under `assembly/`, include the source from a minimal PIC16F883 pic-as project that successfully builds.

It must have:

- processor/device selection;
- reset path;
- at least one simple arithmetic or move operation;
- a stable loop.

Physical hardware behavior is not required for this assignment.

## Part 6 - Setup evidence

Under `evidence/`, create `toolchains.md`.

For each environment record:

- OS;
- tool/version;
- project or command used;
- expected result;
- actual result;
- any setup problem and its fix.

The record should be detailed enough that another student can determine whether their setup reached the same checkpoint.

## Complete when

A grader can clone the repository and determine that:

1. Git history reflects real development;
2. the C# program builds/runs;
3. the Python program runs with documented interpreter/venv;
4. the WinForms counter works on Windows;
5. the XC8 PIC16F883 source reached a successful build;
6. the pic-as PIC16F883 source reached a successful build;
7. setup evidence is reproducible and understandable.

## Evaluation

See [rubric.md](rubric.md).
