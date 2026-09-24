# RCET 3371 - Embedded C from Familiar Program Logic

*Self-learning guide*

[Topics index](README.md) · [XC8 setup](../Guides/Toolchains/xc8.md)

## 1. Start from an algorithm you already understand

Do not learn C syntax and invent a new algorithm at the same time.

C#:

```csharp
static int Clamp(int value, int min, int max)
{
    if (value < min)
    {
        return min;
    }

    if (value > max)
    {
        return max;
    }

    return value;
}
```

Trace these cases:

| value | min | max | result |
| ---: | ---: | ---: | ---: |
| 5 | 0 | 10 | 5 |
| -2 | 0 | 10 | 0 |
| 15 | 0 | 10 | 10 |

Once that behavior is clear, translate it.

## 2. The same function in ordinary C

```c
int clamp(int value, int min, int max)
{
    if (value < min)
    {
        return min;
    }

    if (value > max)
    {
        return max;
    }

    return value;
}
```

The syntax is very close.

New questions can now be introduced one at a time:

- what size is `int` on this target?
- where does the function declaration belong?
- how does another file call it?
- what machine instructions does the compiler generate?

## 3. Use fixed-width types when width is part of the requirement

If the value is specifically one 8-bit unsigned byte:

```c
#include <stdint.h>

uint8_t increment(uint8_t value)
{
    return (uint8_t)(value + 1u);
}
```

Useful types include:

```text
uint8_t
int8_t
uint16_t
int16_t
uint32_t
```

Use an exact-width type when the width itself matters to a register, protocol, packed field, or required range.

Do not replace every integer in every program with a fixed-width type automatically.

## 4. First one-file XC8 program

```c
#include <xc.h>
#include <stdint.h>

static uint8_t add(uint8_t first, uint8_t second)
{
    return (uint8_t)(first + second);
}

void main(void)
{
    volatile uint8_t answer = add(2u, 3u);

    while (1)
    {
        (void)answer;
    }
}
```

Build this as one file first.

Make sure the compiler/device/toolchain work before adding module structure.

## 5. Then split a function into header and source

After the one-file version works:

`math_helpers.h`:

```c
#ifndef MATH_HELPERS_H
#define MATH_HELPERS_H

#include <stdint.h>

uint8_t add_u8(uint8_t first, uint8_t second);

#endif
```

`math_helpers.c`:

```c
#include "math_helpers.h"

uint8_t add_u8(uint8_t first, uint8_t second)
{
    return (uint8_t)(first + second);
}
```

`main.c`:

```c
#include <xc.h>
#include <stdint.h>
#include "math_helpers.h"

void main(void)
{
    volatile uint8_t answer = add_u8(2u, 3u);

    while (1)
    {
        (void)answer;
    }
}
```

Now vocabulary has a concrete example:

- declaration: tells the compiler a function exists and its signature;
- definition: supplies the function body;
- header: commonly exposes declarations needed by callers;
- source file: contains definitions/implementation;
- linker: connects references among compiled pieces.

## 6. Linker errors from a real example

Suppose `main.c` calls `add_u8`, but `math_helpers.c` was never added to the project.

The compiler can understand the declaration from the header.

The final build can still fail because the linker cannot find the definition.

That is a useful way to learn the linker: observe the failure, then fix the missing source/module.

## 7. Hardware-facing code comes after ordinary C works

A hardware register is not an ordinary local variable.

Example idea:

```c
static void led_set(uint8_t on)
{
    if (on)
    {
        PORTC |= 0x01u;
    }
    else
    {
        PORTC &= (uint8_t)~0x01u;
    }
}
```

The application can call `led_set(1)` without scattering `PORTC` operations everywhere.

This small wrapper is the beginning of a hardware/software boundary.

Do not start with an elaborate driver framework.

## 8. Volatile from an observable problem

Suppose a value can change because hardware or an interrupt changes it while normal code is running.

The compiler must not assume that a previously read value remains unchanged merely because the current normal code did not assign it.

That is the situation where `volatile` matters.

It does **not** mean:

- atomic;
- thread-safe;
- interrupt-safe;
- protected from race conditions;
- automatically correct.

At this stage, remember:

> `volatile` affects how the compiler treats reads/writes. It is not a synchronization mechanism.

## 9. Compare generated assembly after the C works

Take a tiny C function:

```c
uint8_t low_nibble(uint8_t value)
{
    return value & 0x0Fu;
}
```

Build it and inspect generated/disassembled output.

Ask bounded questions:

- where does the value arrive?
- which operation performs the mask?
- where does the result go?
- how is return represented?

Do not try to predict the entire compiler output from the C source.

## 10. Hand-written pic-as comparison

Use exactly the same contract as the C function:

```text
input:  one 8-bit value
output: low nibble only
rule:   result = input AND 0x0F
```

A small PIC16F883 routine can make that operation explicit:

```assembly
input_value     EQU 0x70
result_value    EQU 0x71

;-----------------------------------------------------
; LowNibble
; Input:  input_value
; Output: result_value
; Uses:   WREG
;-----------------------------------------------------
LowNibble:
    movf    input_value,w
    andlw   0x0F
    movwf   result_value
    return
```

Test vectors:

| input | expected |
| ---: | ---: |
| `0xA5` | `0x05` |
| `0xFF` | `0x0F` |
| `0x30` | `0x00` |
| `0x07` | `0x07` |

Run the same vectors against the C function and the assembly routine.

The learning goal is:

```text
same contract
same test vectors
different abstraction level
```

not:

```text
one C line = one assembly line
```

The compiler is free to choose a different instruction sequence as long as the observable contract is equivalent.

## 11. State machines are previewed, not front-loaded

A simple embedded program often evolves from:

```text
do action
delay
do next action
delay
```

into explicit state and timing so the system can remain responsive.

That full design pattern is developed in [Event-Driven Device/Host Architecture](event-driven-device-host-architecture.md) after the supporting event/protocol/integration ideas are in place.

For this topic, recognize:

- blocking code prevents the main flow from doing other work during the wait;
- explicit state lets the program remember where it is between iterations.

You do not need to master a full nonblocking architecture here.

## 12. Troubleshooting sequence

For a new C build problem:

1. return to the smallest project that built;
2. read the first compiler/linker error;
3. identify which source/header it names;
4. confirm declarations and definitions match;
5. confirm every required source file is part of the project;
6. build again before changing another issue.

For a hardware-facing problem:

1. prove the pure calculation separately if possible;
2. verify the selected device;
3. verify register/pin configuration;
4. observe register/pin behavior independently;
5. distinguish software/simulator evidence from physical measurement.

## 13. Practice

1. Translate the C# `Clamp` method into C.
2. Why might `uint8_t` be preferable to `int` for an 8-bit packed field?
3. What is the difference between a declaration and a definition?
4. Why build a one-file C example before splitting it?
5. What happens conceptually if the header declares a function but the source defining it is missing from the build?
6. Why is `volatile` not a complete solution to interrupt/shared-state safety?
7. What should you ask when inspecting compiler-generated assembly?
8. Why postpone full state-machine design until later?

## 14. Answer reasoning

1. The condition/return structure is nearly identical; type/syntax details adapt to C.
2. It states the required width explicitly.
3. Declaration describes the callable symbol/signature; definition supplies implementation/storage.
4. It isolates toolchain/language problems from module/linking problems.
5. Compilation may succeed for the caller, but linking can fail because no matching definition exists.
6. It changes compiler assumptions about access, not atomicity or coordination.
7. Bounded questions about the implementation of one known operation, call, branch, or data movement.
8. Students first need comfort with ordinary C and simple persistent state; later sections provide a real system need for event/state architecture.

## 15. Ready to continue when

Explain and demonstrate:

- translate one familiar method from C# to C;
- fixed-width type;
- one-file build before multi-file split;
- header declaration versus source definition;
- linker purpose from a concrete example;
- a small register-facing wrapper;
- practical meaning and limits of `volatile`;
- why generated assembly is inspected after the C behavior is understood.

## 16. References

- MPLAB XC8 Compiler: https://www.microchip.com/en-us/tools-resources/develop/mplab-x-compilers/xc8
- XC8 PIC Assembler documentation: https://onlinedocs.microchip.com/oxy/GUID-4DC87671-9D8E-428A-ADFE-98D694F9F089/
- C fixed-width integer overview: https://en.cppreference.com/w/c/types/integer
