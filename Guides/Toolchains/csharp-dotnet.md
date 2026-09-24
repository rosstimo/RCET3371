# C# / .NET Toolchain Guide

[Guides index](../README.md)

## Course baseline

Reference environment for the S27 candidate:

- .NET 10 LTS SDK
- Visual Studio 2026 stable on Windows for full desktop/WinForms work
- command-line .NET SDK for reproducible build/run/test
- VS Code may be used for console/library work when appropriate

A newer supported patch release is acceptable if course examples/tests pass unchanged.

## Verify installation

Run:

    dotnet --info
    dotnet --version

The course candidate is written for .NET 10.

## Create and run a console project

    mkdir Demo
    cd Demo
    dotnet new console
    dotnet build
    dotnet run

## Add a test project

From a solution/workspace root, one common pattern is:

    dotnet new xunit -n Demo.Tests
    dotnet add Demo.Tests reference Demo
    dotnet test

The exact directory layout may differ by assignment.

## Build versus run

    dotnet build

proves the project translates/restores/links far enough to produce build output.

    dotnet run

builds as needed and executes the selected project.

A successful build does not prove runtime behavior.

## WinForms

Use Visual Studio 2026 with the .NET desktop development workload for the course's Windows Forms examples.

The course uses WinForms as a familiar event-driven desktop reference, not as the central learning goal.

Key rule:

- retain domain/display state in program data;
- handle events as state transitions/actions;
- repaint from retained state.

## Debugging

Use breakpoints to inspect:

- current line;
- local variables;
- object fields/properties;
- call stack;
- exceptions;
- collection contents.

When a bug depends on input, preserve the exact input as a regression fixture.

## Package/dependency discipline

Prefer project/package declarations over copying binaries manually.

Commit:

- source;
- project files;
- lock/config files when the project policy requires them.

Do not commit normal bin/obj build trees.

## References

- .NET download/support: https://dotnet.microsoft.com/en-us/download
- .NET CLI: https://learn.microsoft.com/en-us/dotnet/core/tools/
- .NET testing: https://learn.microsoft.com/en-us/dotnet/core/testing/
- Visual Studio 2026 release history: https://learn.microsoft.com/en-us/visualstudio/releases/2026/release-history
