# Section 6 - Embedded C from Familiar Program Logic

This section introduces embedded C by translating programs you already understand. Hardware-specific rules are added only after ordinary C syntax is no longer the main obstacle.

## Start from what you already know

A C# method such as:

```csharp
static int Clamp(int value, int min, int max)
{
    if (value < min) return min;
    if (value > max) return max;
    return value;
}
```

already contains the algorithm. Embedded C changes the syntax, types, build target, and hardware context. It does not require inventing a new algorithm at the same time.

## Outcomes

You should be able to:

- create and build a PIC16F883 XC8 project;
- translate a small C# method into a C function;
- use fixed-width types such as `uint8_t` when width matters;
- split a small C program into `.h` and `.c` files after first building it as one file;
- explain declaration versus definition at a practical level;
- isolate direct register access in a small hardware-facing function;
- explain why hardware/interrupt-modified data may require `volatile`;
- inspect generated assembly for a small function without expecting one C line to equal one instruction;
- compare a bounded C function with a pic-as routine.

Nonblocking state machines are developed more fully in Section 10.

## Learn

- [Embedded C and software state](../Topics/embedded-c-and-software-state.md)
- [MPLAB X / XC8 setup](../Guides/Toolchains/xc8.md)
- [pic-as setup](../Guides/Toolchains/pic-as.md)
- [On-ramp embedded examples](../Examples/OnRamp/)

## Example progression

1. Build the minimal XC8 project from Section 2 again.
2. Add one ordinary variable and arithmetic expression.
3. Translate a known C# method into C.
4. Test/trace the function with fixed values.
5. Move the function to a second `.c` file with a header.
6. Add one small register-facing wrapper.
7. Inspect generated assembly.
8. Compare one bounded operation with pic-as.

## Programming Assignment

Complete [Cross-Language Engineering Model](../ProgrammingAssignments/CrossLanguageEngineeringModel/README.md).

## Assessment

Section 6 practice and graded assessment move from ordinary C into embedded-specific ideas in that order.

Next: [Section 7 - Protocol Design](07-Protocol-Design.md)
