# RCET 3371 — Program Decomposition, Modules, and Interfaces

*Self-learning guide*

[Topics index](README.md)

Once a small routine has a clear contract, the next question is where responsibility belongs. This Topic grows a working one-file program into modules/files deliberately, then introduces interfaces only when multiple implementations need the same behavior contract. Review [Function Contracts, Scope, and Retained State](function-contracts-scope-retained-state.md) first.











## Practice

1. Split a small C# program by responsibility. What behavior should change merely because a class moved to another file?
2. Translate one already-tested C# calculation to Python and C.
3. Explain why a one-file C version should usually work before introducing a header/source pair.
4. What is the difference between a declaration and a definition?
5. Give one case where an interface is useful and one where it adds no useful boundary.

## Answer reasoning

1. None, assuming project/namespace relationships remain correct.
2. The syntax changes, but the input/output contract and expected test vectors should remain the same.
3. It separates language/algorithm correctness from module/linking problems.
4. A declaration describes a symbol/signature; a definition supplies the implementation/storage.
5. An interface helps when callers should depend on behavior shared by multiple implementations, such as real and simulated devices; it adds little when no meaningful substitution boundary exists.

## Ready to continue when

You can decompose a small program by responsibility, translate a known contract across languages, explain module/header/source relationships, diagnose a basic linker-boundary problem, and justify an interface only when it solves a real substitution/testing problem.

## References

- Microsoft, C# interfaces — https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces
- Python documentation, Modules — https://docs.python.org/3/tutorial/modules.html
- Microchip Technology Inc., *MPLAB XC8 C Compiler User's Guide* — https://onlinedocs.microchip.com/
- [C# / Python / Embedded C / PIC Assembly comparison](../References/csharp-python-c-picas-comparison.md)
