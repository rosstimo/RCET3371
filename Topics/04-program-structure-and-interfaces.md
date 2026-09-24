# RCET 3371 — Program Structure and Interfaces

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 4](../LearningPath/04-Program-Structure-and-Interfaces.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. Structure across languages
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

Programs become difficult not because they contain many lines, but because responsibilities and contracts become unclear. Multi-language engineering makes this visible: syntax changes, but decomposition, state ownership, inputs, outputs, and invariants remain.

## 2. Outcomes

You should be able to:

- compare loops and control flow across C#, Python, C, and assembly;
- define function/method/subroutine inputs, outputs, side effects, and state;
- reason about scope and lifetime;
- choose local state versus retained object/module/static state deliberately;
- use classes/records/structures as data models;
- use interfaces or module boundaries to decouple a caller from an implementation;
- split a program into files based on responsibility;
- explain CALL/RETURN and stack use conceptually.

## 3. Prerequisites

Basic methods/functions, loops, classes, and conditionals from RCET 2265 plus Section 3 representation.

## 4. Core model

Before syntax, write the contract:

**Name:** DecodeStatus  
**Input:** one byte  
**Output:** structured flags/count  
**Side effects:** none  
**Errors:** none; every byte is valid  
**State retained between calls:** none

That contract can be implemented in multiple languages.

A good module boundary answers:

- what does this component own?
- what does it accept?
- what does it return or publish?
- what state persists?
- what is hidden?
- how can it be tested independently?

## 5. Structure across languages

### Loops

Different languages provide different syntax, but the algorithm still has:

- initialization;
- continuation condition;
- state update;
- loop body;
- termination/progress argument.

### Functions and methods

Prefer explicit inputs/outputs over hidden global coupling.

In C#:

    static int Clamp(int value, int min, int max) { ... }

In Python:

    def clamp(value, minimum, maximum):
        ...

In C:

    int16_t clamp_i16(int16_t value, int16_t min, int16_t max);

Assembly typically uses a documented register/memory convention rather than a language-enforced signature.

### Scope and lifetime

**Scope** asks where a name is visible.  
**Lifetime** asks how long the associated object/storage exists.

A local variable can disappear after a call. Application state that must survive events needs a longer lifetime.

### Interfaces and boundaries

An interface/abstract boundary is useful when the caller should depend on behavior rather than one device.

For example:

    IDeviceTransport
      Read()
      Write()
      IsConnected

A fake implementation can then be used for tests while a serial implementation talks to hardware.

### Multi-file design

Split because responsibilities differ, not merely because a file is long.

Example:

    Packet.cs          data model
    PacketParser.cs    parse/validate
    SerialTransport.cs transport
    Program.cs         composition/startup

## 6. Worked examples

### Example 1: hidden state defect

A method uses a static variable to count calls even though the specification says each call must be independent.

The syntax may be valid, but the contract is violated because state persists unexpectedly.

### Example 2: translation

Algorithm:

1. inspect lower four bits;
2. count set bits;
3. return count.

C#, Python, C, and assembly can implement different loop syntax while preserving the same input/output contract and test vectors.

### Example 3: interface benefit

A logger depends directly on SerialPort everywhere. Testing requires hardware.

Refactor so the logger depends on a byte-stream/device interface. Then:

- FakeTransport feeds fixed data in tests.
- SerialTransport wraps the real port.
- logger logic does not change.

## 7. Apply, verify, and troubleshoot

When a program grows:

1. write responsibilities;
2. write contracts;
3. identify state owners;
4. identify external dependencies;
5. separate pure logic from I/O;
6. create tests around boundaries;
7. split files only after the responsibilities are clear.

Debug hidden-state problems by asking:

- who can modify this value?
- how long does it live?
- is it reset when expected?
- can two callbacks touch it?
- can a test construct a known initial state?

## 8. Practice

1. What is the difference between scope and lifetime?
2. A parser reads directly from a serial port and updates labels in the same method. Name at least three responsibilities mixed together.
3. Why is a fake transport useful?
4. When translating a loop to assembly, which conceptual elements must remain even though syntax changes?
5. A helper function changes a global variable that is not mentioned in its name or documentation. What contract problem does this create?
6. Propose a four-file split for a program that reads a CSV, parses measurements, computes statistics, and prints a report.

## 9. Answer key

1. Scope controls name visibility; lifetime controls how long storage/object state exists.
2. Transport/I/O, parsing/domain logic, and presentation. It may also mix connection lifecycle.
3. It makes behavior deterministic and testable without the physical device.
4. Initialization, continuation/termination condition, state update, body, and branch/control flow.
5. A hidden side effect and hidden dependency make behavior harder to reason about and test.
6. One valid split: Record model, CsvParser, Statistics, Program/Report. Other designs are valid if responsibilities and contracts are explicit.

## 10. Explain without notes

Explain:

- contract before syntax;
- inputs/outputs/side effects/state;
- scope versus lifetime;
- retained state;
- interface as a dependency boundary;
- responsibility-based file splitting;
- why translation is not line-by-line copying.

## 11. References

- Microsoft Learn, C# methods — https://learn.microsoft.com/en-us/dotnet/csharp/methods
- Microsoft Learn, interfaces — https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces
- Python Software Foundation, defining functions — https://docs.python.org/3/tutorial/controlflow.html#defining-functions
- Python Software Foundation, modules — https://docs.python.org/3/tutorial/modules.html
- Python Software Foundation, classes — https://docs.python.org/3/tutorial/classes.html
- Microchip, MPLAB XC8 compiler documentation — https://www.microchip.com/en-us/tools-resources/develop/mplab-xc-compilers/xc8
