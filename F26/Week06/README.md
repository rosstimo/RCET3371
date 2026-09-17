# Week 6 - Embedded C Project Structure

## What this week is about

Embedded C is still software engineering, but hardware-visible state, fixed-width data, compiler/linker behavior, and timing make some boundaries more explicit.

You should be able to:

- explain compile/assemble/link at a practical level;
- use fixed-width data types when width is part of an interface;
- explain what `volatile` tells the compiler and what it does not guarantee;
- distinguish declarations in a header from implementation in a source file;
- organize a small project into `main.c`, a module `.c`, and a module `.h`;
- keep direct SFR access near a hardware boundary;
- inspect compiler-generated assembly/listing rather than assuming one C line equals one instruction;
- explain why hand-written assembly called from C must follow the compiler's documented interface.

## Reference example

- [main.c](examples/main.c)
- [status.c](examples/status.c)
- [status.h](examples/status.h)

The example demonstrates source organization. It is **not a complete board project**. Use the configuration bits, clock setup, and hardware connections specified in class/lab before programming a PIC.

## Assignment

**[Multi-File XC8 Status Module](MultiFileXC8StatusModule.md)**