# Section 2 - Toolchains and First Cross-Language Programs

This section is the course setup and on-ramp. Get each tool working **before** worrying about detailed toolchain vocabulary.

## Start from what you already know

From RCET 2265, you already know how a small C# console program and a simple Windows Forms event handler behave. We will use those familiar programs as the reference point for Python, XC8 C, and pic-as.

## Outcomes

You should be able to:

- install or verify the required course development tools;
- create, build, and run a C# Hello World program;
- create and run the equivalent first Python program;
- create a PIC16F883 XC8 project and obtain a successful first build;
- create a PIC16F883 pic-as project and obtain a successful first build;
- use a breakpoint in C# and Python;
- identify, at a practical level, the editor/IDE, compiler or interpreter, runtime/target, and debugger involved in each environment;
- create a simple WinForms event program using controls/events already introduced in RCET 2265.

Advanced repaint architecture and merge-conflict recovery are intentionally deferred until later sections.

## Learn

Start with the [Course Toolchain Setup Guide](../Guides/Toolchains/README.md). Follow it from top to bottom.

Then use the detailed guides as needed:

- [C# / .NET](../Guides/Toolchains/csharp-dotnet.md)
- [Python](../Guides/Toolchains/python.md)
- [MPLAB X / XC8](../Guides/Toolchains/xc8.md)
- [PIC assembler / pic-as](../Guides/Toolchains/pic-as.md)
- [Toolchains and first programs](../Topics/toolchains-and-first-cross-language-programs.md)

Runnable/minimal source examples are under [Examples/OnRamp](../Examples/OnRamp/).

## Example progression

Use the same tiny problem in more than one language:

> Store two integer measurements, compute their sum, and observe the result.

1. Review the C# version.
2. Run the Python version.
3. Compare the variable, arithmetic, and output syntax.
4. Build the embedded C version even though it has no console.
5. Build the assembly version and identify where execution loops.

The point is to recognize familiar programming ideas inside unfamiliar syntax.

## Practice

**Follow:** Complete every Hello World / first-build procedure in the toolchain guide.

**Predict:** Before running the Python translation, predict its output from the C# version.

**Modify:** Change the greeting or arithmetic in C# and Python and verify the same behavior.

**Debug:** Set one breakpoint in C# and Python and inspect a variable.

**Build:** Make the smallest successful XC8 C and pic-as builds for PIC16F883.

## Programming Assignment

Finish [Foundations Readiness](../ProgrammingAssignments/FoundationsReadiness/README.md).

## Assessment

Section 2 practice and graded assessment focus on setup verification, basic tool roles, build/run/debug distinctions, and reading simple cross-language code.

## Ready to continue when

You can start from a clean machine/project, reach a working first program in each course environment, and explain the basic role of each tool without needing to understand its internals.

Next: [Section 3 - Representation and Decisions](03-Representation-and-Decisions.md)
