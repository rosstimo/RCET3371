# RCET 3371 — Toolchains, Recovery, and Desktop Events

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 2](../LearningPath/02-Toolchains-Recovery-and-Desktop-Events.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. How the toolchains and event model work
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

A programmer who only knows which button to press in one IDE cannot diagnose where a failure occurs. RCET 3371 treats the toolchain as part of the system.

The same principle applies to event-driven programs. An event handler is a reaction to an event, not a safe place to hide the entire application model.

## 2. Outcomes

You should be able to:

- distinguish editor, IDE, compiler, interpreter, linker, runtime, debugger, programmer, and target;
- build/run a C# console application from the command line;
- run/debug Python with an explicitly selected interpreter;
- explain what MPLAB X, XC8, pic-as, PICkit, and the PIC target each do;
- use branches and pull requests without confusing repository state;
- explain retained application state versus transient event data;
- explain why drawing must be reproducible during repaint.

## 3. Prerequisites

Complete Section 1 and be able to inspect Git state confidently.

## 4. Core model

A useful software toolchain pipeline is:

    source -> translator -> intermediate/object form -> linker/build -> executable/image -> runtime/target

Not every language uses every stage in the same visible way.

### C#/.NET

Source is compiled by the .NET build toolchain into managed assemblies. The .NET runtime loads and executes them.

### Python

Source is executed by the selected Python interpreter. Python may create internal bytecode/cache artifacts, but the engineering question remains: which interpreter/environment is executing the program?

### XC8

C source is compiled for a specific 8-bit PIC target, assembled/linked into target code, then programmed or simulated.

### pic-as

Assembly source is processed by Microchip's PIC assembler driver and linked for the selected target device.

## 5. How the toolchains and event model work

### Build versus run versus debug

**Build** answers: can the toolchain translate/link the project?

**Run** answers: what happens when the built/interpreted program executes?

**Debug** adds controlled observation: breakpoints, stepping, call stack, variables, registers, or memory.

Do not treat a successful build as proof of correct runtime behavior.

### Environment identity

When diagnosing toolchain problems, record:

- operating system;
- runtime/compiler/interpreter version;
- project target/framework/device;
- working directory;
- selected debugger/programmer;
- exact command or IDE action.

### Event-driven baseline

A UI or event-driven system has at least:

- persistent application/domain state;
- events;
- handlers that translate events into state changes;
- rendering/presentation that derives visible output from current state.

Avoid making the pixels on the screen the only copy of state.

### Repaint behavior

Windows Forms raises Paint when a control needs to redraw. A robust display uses retained data and renders from that data whenever Paint occurs.

If a click handler draws directly once and does not store the underlying object/state, resizing or covering/uncovering the window can expose the design error.

## 6. Worked examples

### Example 1: C# command-line baseline

In an empty directory:

    dotnet new console
    dotnet build
    dotnet run

If build succeeds but run fails because a file is missing, that is a runtime/resource/path problem, not a compiler problem.

### Example 2: Python interpreter identity

These two commands may not resolve to the same interpreter on every system:

    python script.py
    python3 script.py

Verify the interpreter deliberately, especially when using packages or virtual environments.

### Example 3: retained state

Weak design:

- mouse click occurs;
- handler gets a graphics surface;
- handler draws one point;
- point data is discarded.

Stronger design:

- mouse click occurs;
- handler converts click into domain coordinates;
- point is appended to retained state;
- control is invalidated;
- Paint renders all retained points.

## 7. Apply, verify, and troubleshoot

Toolchain checklist:

1. identify the source language;
2. identify the expected translator/runtime;
3. verify the version/target;
4. build or syntax-check;
5. run the smallest known-good program;
6. debug with observation;
7. only then integrate larger dependencies.

Git recovery checklist:

1. status;
2. diff;
3. history;
4. branch/upstream;
5. smallest corrective action;
6. status/build/test again.

Event-driven checklist:

1. identify persistent state;
2. identify events;
3. identify state transitions;
4. identify rendering;
5. prove redraw without losing information.

## 8. Practice

1. A program compiles but fails to find a CSV file. Which stage should you investigate first?
2. A Python package imports in one terminal but not another. What identity should you verify?
3. What is the difference between a compiler and a linker?
4. Why is an MPLAB X project not the same thing as XC8?
5. A form draws correctly immediately after a button click but loses the drawing after resize. What design information is probably missing?
6. Why should merge-conflict resolution finish with a build/test rather than only a clean Git status?

## 9. Answer key

1. Runtime working-directory/path/resource behavior.
2. The Python interpreter/environment actually running the command.
3. A compiler translates source units; a linker resolves/combines compiled units and symbols into a final program/image.
4. MPLAB X is the development environment; XC8 is the compiler toolchain. The project configures how those tools target a device.
5. The underlying domain/display state was not retained and repaint cannot reconstruct it.
6. Git can be syntactically clean while the chosen conflict resolution is behaviorally wrong.

## 10. Explain without notes

Explain:

- compile, link, run, debug;
- interpreter versus runtime;
- IDE versus toolchain;
- target versus programmer;
- persistent state versus event data;
- repaint from state;
- why repository recovery and software verification are separate checks.

## 11. References

- Microsoft Learn, .NET console application tutorials — https://learn.microsoft.com/en-us/dotnet/core/tutorials/
- Microsoft Learn, Windows Forms events — https://learn.microsoft.com/en-us/dotnet/desktop/winforms/forms/events
- Microsoft Learn, Windows Forms custom painting — https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/custom-painting-drawing
- Python Software Foundation, Python tutorial — https://docs.python.org/3/tutorial/
- Microchip, MPLAB XC8 Compiler — https://www.microchip.com/en-us/tools-resources/develop/mplab-xc-compilers/xc8
- GitHub Docs, merge conflicts — https://docs.github.com/en/pull-requests/reference/merge-conflicts
