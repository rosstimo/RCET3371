# RCET 3371 — Collections, Memory, Parsing, and Debugging

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 5](../LearningPath/05-Collections-Memory-Parsing-and-Debugging.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. Collections, parsing, and debugging
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

Real systems rarely process one isolated value. They accumulate samples, messages, history, and state. The right collection model makes program behavior obvious; the wrong one hides invariants and creates edge-case failures.

Parsing and debugging belong here because collections are often where malformed data, stale values, off-by-one errors, and misunderstood state become visible.

## 2. Outcomes

You should be able to:

- choose among fixed arrays, dynamic lists, queues, and rolling/ring buffers;
- define collection invariants;
- parse structured text or byte data into useful program objects/state;
- use deterministic fixtures;
- compare data shifting with circular-buffer approaches;
- inspect variables, call stack, memory, registers, and logs;
- explain how addresses/references differ from the values stored there;
- collect evidence before changing code.

## 3. Prerequisites

Sections 3 and 4, including representation, functions, contracts, and responsibility boundaries.

## 4. Core model

A collection is not just "many values." It has a behavioral contract.

Examples:

**Queue invariant**
- first item removed is the earliest item still present.

**Fixed rolling window**
- contains no more than N samples;
- newest sample is always included;
- after N samples, adding one sample removes exactly one oldest sample.

**Unique-ID set**
- no duplicate identifiers.

Write invariants before implementation.

## 5. Collections, parsing, and debugging

### Arrays

Useful when:

- size is fixed or bounded;
- indexed access matters;
- memory layout matters;
- embedded constraints make dynamic allocation undesirable.

### Lists

Useful when:

- size changes;
- convenient insertion/removal/iteration is valuable;
- exact contiguous hardware layout is not the primary concern.

### Queues

Use when the order of arrival is part of the behavior.

### Ring buffers

A ring buffer stores a fixed maximum amount without shifting every element on each insertion. Track:

- storage;
- write/read index or head/tail;
- count/full state.

The implementation may be more complex than a queue, so use it when bounded storage or predictable memory behavior matters.

### Parsing

A parser should turn external representation into validated internal data.

A clean pipeline is:

    raw input -> framing/splitting -> conversion -> validation -> structured object

Do not combine "read from device" and "parse record" unless the design deliberately requires it.

### Deterministic fixtures

Before random/live input, create fixed samples with known results.

For a CSV parser:

    timestamp,temp_c,status
    2026-09-24T10:00:00,21.5,OK
    2026-09-24T10:00:01,21.7,OK

Then add deliberate bad cases:

    missing field
    nonnumeric value
    extra field
    blank line

### Debugging evidence

Useful evidence includes:

- exact input;
- exact expected output;
- current variables;
- call stack;
- collection contents;
- register/memory state for embedded code;
- logs showing time/order;
- repeatable test case.

## 6. Worked examples

### Example 1: rolling window

Window size: 3.

Input sequence:

    10, 20, 30, 40

States:

    [10]
    [10, 20]
    [10, 20, 30]
    [20, 30, 40]

Invariant: after the window fills, exactly the newest three remain.

### Example 2: queue versus shifting array

If you manually shift 999 elements each time a new sample arrives in a 1000-sample window, insertion cost grows with the window size.

A queue/ring abstraction expresses the desired behavior more directly and may avoid repeated full shifts.

### Example 3: parse first, then compute

Weak design:

- read CSV line;
- update GUI labels;
- compute average;
- print error;
- append to global list;
- all in one method.

Stronger design:

    TryParseRecord(line) -> Measurement or failure
    AddMeasurement(measurement)
    ComputeStatistics(collection)
    Present(model)

Each step can be tested separately.

## 7. Apply, verify, and troubleshoot

For collection defects:

1. state the invariant;
2. choose a small fixed input sequence;
3. write expected state after every operation;
4. compare actual state step by step;
5. check empty/one/full/overflow cases.

For parser defects:

1. preserve the exact failing input;
2. reduce it to the smallest failing sample;
3. identify framing/splitting/conversion/validation stage;
4. verify culture/units/base/encoding assumptions;
5. add the failing case to regression tests.

For debugger use:

- break before the suspected state transition;
- inspect state;
- step the smallest meaningful unit;
- compare expected versus actual;
- avoid changing multiple unrelated variables during diagnosis.

## 8. Practice

1. A 4-sample rolling window currently holds [2, 4, 6, 8]. Add 10. What should remain?
2. Why is a queue usually clearer than manually shifting an array for first-in/first-out behavior?
3. Name four malformed-input cases for a comma-separated measurement record.
4. What is an invariant?
5. Why is "it worked once with random input" weak verification?
6. A parser throws only on the 1001st record. What evidence would you capture before editing code?
7. When might a fixed array be preferable to a dynamic list?

## 9. Answer key

1. [4, 6, 8, 10].
2. The data structure directly represents FIFO behavior and avoids hand-maintained shifting logic.
3. Examples: missing field, extra field, invalid numeric text, empty required field, invalid timestamp, out-of-range value.
4. A condition that must remain true for a data structure/system state to be valid.
5. Random input may not repeat the same path and gives no guaranteed coverage or known expected result.
6. Exact record/input, collection size/state near failure, exception/stack, memory/log evidence, and a reduced deterministic reproduction.
7. Fixed size, predictable memory, hardware-oriented layout, embedded constraints, or simple indexed access.

## 10. Explain without notes

Explain:

- array/list/queue/ring-buffer tradeoffs;
- collection invariants;
- deterministic fixture;
- parsing stages;
- regression case;
- why debugging starts with evidence;
- value versus address/reference at a conceptual level.

## 11. References

- Microsoft Learn, collections — https://learn.microsoft.com/en-us/dotnet/standard/collections/
- Python Software Foundation, data structures — https://docs.python.org/3/tutorial/datastructures.html
- Python Software Foundation, input and output — https://docs.python.org/3/tutorial/inputoutput.html
- Microsoft Learn, Visual Studio debugger documentation — https://learn.microsoft.com/en-us/visualstudio/debugger/
