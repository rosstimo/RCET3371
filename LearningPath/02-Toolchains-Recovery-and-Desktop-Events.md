# Section 2 — Toolchains, Recovery, and Desktop Events

## Outcomes

You should be able to:

- distinguish editor/IDE, compiler, interpreter, linker, runtime, debugger, programmer, and target;
- build/run/debug C# and Python in the documented course environments;
- explain branches, remotes, pull requests, merge conflicts, and recovery;
- preserve application state outside event handlers;
- explain why a GUI redraw must reconstruct the display from retained state rather than depend on one transient drawing call.

## Learn

- [Toolchains and recovery](../Topics/02-toolchains-recovery-and-desktop-events.md)
- [C#/.NET toolchain guide](../Guides/Toolchains/csharp-dotnet.md)
- [Python toolchain guide](../Guides/Toolchains/python.md)
- [Git collaboration and recovery](../Guides/Git/collaboration-and-recovery.md)

## Practice

**Predict:** Determine which component produces each error: source editor, compiler, linker, runtime, debugger, or target.

**Repair:** Resolve a small merge conflict and prove the repository is clean afterward.

**Build:** Create a minimal event-driven program that stores state outside the event handler and redraws from that state.

## Programming Assignment

Finish [Foundations Readiness](../ProgrammingAssignments/FoundationsReadiness/README.md).

## Assessment

Section 2 practice and graded quiz/assessment.

## Ready to continue when

You can set up the baseline toolchains, build/debug a small program, recover a repository intentionally, and explain the retained-state event model.

Next: [Section 3 — Representation and Decisions](03-Representation-and-Decisions.md)
