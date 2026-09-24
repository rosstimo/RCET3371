# RCET 3371 — Embedded C and Software State

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 6](../LearningPath/06-Embedded-C-and-Software-State.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. Embedded C modules and state
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

Embedded software runs close to hardware and under tighter memory/timing constraints. C makes storage, linkage, and hardware-visible state more explicit than C#. Assembly makes processor behavior even more visible.

The goal is not to memorize compiler syntax. The goal is to preserve clear contracts while understanding what the toolchain and processor must actually do.

## 2. Outcomes

You should be able to:

- split embedded C into header/interface and source/implementation files;
- explain declaration, definition, translation unit, external symbol, and link step;
- use fixed-width integer types;
- explain why hardware- or asynchronously-modified state may require volatile semantics;
- separate application logic from device-driver access;
- inspect generated assembly for a small C function;
- build a software state machine;
- distinguish blocking delays from nonblocking state/time logic;
- compare a bounded C operation with pic-as implementation.

## 3. Prerequisites

Sections 3-5 and prior PIC exposure from the co-requisite embedded courses.

## 4. Core model

A practical module has:

**Header**
- public types;
- constants;
- function declarations;
- the contract callers need.

**Source**
- private helpers/state;
- function definitions;
- implementation details.

A translation unit is roughly one source file after preprocessing. Separate units become one program through the linker.

## 5. Embedded C modules and state

### Fixed-width types

Use stdint types when exact width is part of the contract:

    uint8_t
    int16_t
    uint32_t

Do not assume plain int has the same width across every target.

### Volatile

Volatile is about observation of changes that the normal flow of the current code cannot fully predict, such as hardware registers or state changed by an interrupt.

It is not:

- a mutex;
- an atomicity guarantee;
- a replacement for synchronization;
- a general "make it safe" keyword.

### Driver boundary

Keep hardware register details in a narrow component where practical.

For example:

    adc_read_raw()
    uart_try_read_byte()
    led_set(state)

Application code can then reason in terms of behavior rather than scattered register writes.

### State machines

Represent system behavior as:

- current state;
- event/condition;
- transition;
- action;
- next state.

Example:

    IDLE --start--> RUNNING
    RUNNING --timeout--> IDLE
    RUNNING --fault--> FAULT
    FAULT --reset && safe--> IDLE

### Blocking versus nonblocking

Blocking:

    delay 5 seconds
    do next thing

During the delay, the code cannot respond unless interrupts/other mechanisms handle the event.

Nonblocking:

    if state entered:
        record start time
    if elapsed >= target:
        transition

The loop remains free to process other work.

### Generated assembly

Compile a small C function and inspect assembly to ask:

- where are arguments/results stored?
- which instructions implement compare/branch?
- how is a function call represented?
- what compiler-generated setup appears?

Do not assume one C statement maps to one instruction.

## 6. Worked examples

### Example 1: header/source split

status.h:

    #include <stdint.h>
    uint8_t status_get_count(uint8_t state);

status.c:

    #include "status.h"
    uint8_t status_get_count(uint8_t state)
    {
        return (state >> 4) & 0x0F;
    }

main.c can call the public function without knowing how it is implemented.

### Example 2: explicit state

Instead of nested delays:

    GREEN
    wait
    YELLOW
    wait
    RED

store a state and transition when time/event conditions are met.

The state model is testable even if the real hardware timer is replaced by a fake clock.

## 7. Apply, verify, and troubleshoot

Linker error checklist:

- declaration matches definition;
- source file is actually part of the build;
- symbol spelling and type match;
- only intended external symbols are exposed;
- no duplicate definitions.

Hardware-state checklist:

- correct device/register;
- correct width;
- volatile where required;
- read/modify/write behavior understood;
- asynchronous concurrency risks considered separately.

State-machine checklist:

- every state has defined exits;
- unsafe transitions are impossible or rejected;
- timeout start/reset semantics are explicit;
- fault priority is explicit;
- tests cover each transition.

## 8. Practice

1. What belongs in a header that callers need but not private implementation details?
2. What problem does the linker solve?
3. Why is volatile not an atomicity guarantee?
4. Convert a blocking "wait 2 s then turn off" behavior into state/time logic conceptually.
5. Why inspect generated assembly?
6. A function is declared in device.h but the linker cannot find it. Name three likely causes.

## 9. Answer key

1. Public declarations, required types/constants, and the interface contract.
2. It resolves symbols/references among compiled units and produces the final linked program/image.
3. Volatile affects compiler assumptions about reads/writes; multiple-step operations may still be interrupted or interleaved.
4. Record an entry/start time, continue processing, test elapsed time on each pass, then transition/off when elapsed reaches the target.
5. To connect high-level constructs to target behavior, cost, calls, branches, memory, and compiler choices.
6. Missing source from project, name/signature mismatch, implementation omitted, conditional compilation excluded it, or wrong linkage.

## 10. Explain without notes

Explain:

- header versus source;
- translation unit versus link;
- fixed-width type;
- volatile;
- driver boundary;
- state machine;
- blocking versus nonblocking;
- why C and assembly comparison is bounded rather than line-by-line.

## 11. References

- Microchip, MPLAB XC8 Compiler — https://www.microchip.com/en-us/tools-resources/develop/mplab-xc-compilers/xc8
- Microchip, XC8 PIC Assembler documentation — https://onlinedocs.microchip.com/oxy/GUID-4DC87671-9D8E-428A-ADFE-98D694F9F089/
- C standard fixed-width integer header overview (compiler documentation should be used for target specifics) — https://en.cppreference.com/w/c/types/integer
