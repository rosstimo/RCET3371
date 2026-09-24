# RCET 3371 Course Toolchain Setup

This is the student setup checklist for the course. Follow it in order on the computer you plan to use for class work.

For every environment, reach a known working point:

```text
download -> install -> verify tool -> create project -> first program/build -> run or simulate -> debugger/build evidence
```

## Required environments

| Environment | What to install | First proof |
| --- | --- | --- |
| C# / Windows desktop | Visual Studio Community 2026 with .NET desktop development; .NET 10 SDK | console Hello World runs and breakpoint works |
| Python | Python 3.14; VS Code with Python extension | `hello.py` runs and breakpoint works |
| PIC embedded C | MPLAB X IDE 6.35; MPLAB XC8 4.00 | PIC16F883 XC8 project reports BUILD SUCCESSFUL |
| PIC assembly | same MPLAB X + XC8 installation; pic-as is included with XC8 | PIC16F883 assembly project reports BUILD SUCCESSFUL |
| Git | current supported Git | `git --version`, clone/init/status/commit work |

Use the course-adopted versions even if a newer major version exists. Supported patch updates within the adopted major version are normally fine unless class instructions say otherwise.

## Recommended order

### 1. Git

Download: https://git-scm.com/downloads

Verify:

```text
git --version
```

If Git is already working from RCET 2265, do not reinstall it simply to satisfy this checklist.

### 2. C# / .NET / Visual Studio

Follow [C# / .NET setup](csharp-dotnet.md) completely.

Stop only after you can:

- create a Console App;
- print `Hello, RCET 3371!`;
- build with no errors;
- run it;
- set a breakpoint and inspect a variable;
- create a basic Windows Forms project.

### 3. Python / VS Code

Follow [Python setup](python.md) completely.

Stop only after you can:

- verify Python 3.14;
- select the correct Python interpreter in VS Code;
- run `hello.py`;
- set a breakpoint;
- create and activate a virtual environment.

### 4. MPLAB X and XC8

MPLAB X and XC8 are separate installs.

Follow [MPLAB X / XC8 setup](xc8.md).

Stop only after:

- MPLAB X opens;
- XC8 4.x appears as a build tool;
- PIC16F883 is selectable;
- the minimal C project builds.

### 5. pic-as

Do **not** download a separate assembler package. The PIC assembler driver is part of the XC8 installation used by the course.

Follow [pic-as setup](pic-as.md).

Stop only after the minimal assembly project builds for PIC16F883.

## What counts as Hello World on an embedded target?

A console program can print text. A bare PIC16F883 does not have a console by default.

For embedded environments, "Hello World" means:

1. the correct device is selected;
2. the source file is recognized by the toolchain;
3. the project builds for that device;
4. the resulting program has a valid reset path and reaches a stable loop.

Later labs add visible hardware behavior.

## Save setup evidence

For Foundations Readiness, keep a short setup record containing:

- operating system;
- tool/version output or IDE version;
- project name;
- build/run result;
- any required deviation from this guide.

## If something fails

Do not reinstall everything immediately.

Record:

1. which step failed;
2. exact error text;
3. tool/version;
4. project/device/framework selection;
5. whether a brand-new minimal project fails too.

Then use the troubleshooting section in the detailed guide.
