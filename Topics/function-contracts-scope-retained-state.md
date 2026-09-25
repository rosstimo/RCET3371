# RCET 3371 — Function Contracts, Scope, and Retained State

*Self-learning guide*

[Topics index](README.md)

Methods, functions, and assembly subroutines all become easier to reason about when their contract is explicit: inputs, outputs, side effects, retained state, and control flow. This Topic begins with familiar C# and carries that reasoning down to a PIC subroutine.











## Practice

1. For `CalculatePower`, identify inputs, output, side effects, and retained state.
2. Give one example of a local value and one value that must survive between events.
3. For the PIC `AddOne` subroutine, identify input, output, side effects, retained state, and return behavior.
4. Explain the difference between scope and lifetime.
5. Why is "what does this routine promise?" a better starting question than "what syntax does this language use?"

## Answer reasoning

1. Inputs: voltage/current; output: power; side effects: none; retained state: none.
2. A calculation temporary may be local; a form/controller field that records state across events must outlive one method call.
3. Input: `input_value`; output: `result_value`; side effects include WREG/status behavior and the RAM write; RAM values persist; `call` transfers control and `return` resumes the caller.
4. Scope is where a name can be used; lifetime is how long the underlying data/storage remains relevant or alive.
5. The contract survives translation between languages and abstraction levels; syntax does not.

## Ready to continue when

You can describe a routine contract, distinguish local from retained state, reason about scope versus lifetime, and explain the same callable behavior in C# and PIC assembly terms.

## References

- Microsoft, C# methods — https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/methods
- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet*, Section 15 "Instruction Set Summary" — https://www.microchip.com/en-us/product/PIC16F883
- [C# / Python / Embedded C / PIC Assembly comparison](../References/csharp-python-c-picas-comparison.md)
