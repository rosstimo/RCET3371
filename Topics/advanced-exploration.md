# RCET 3371 — Advanced Exploration

*Self-learning guide*

[Topics index](README.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. Supported advanced paths
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

Advanced programming is not a list of fashionable technologies. It is the ability to extend a working system deliberately, evaluate tradeoffs, and verify that the new complexity solves a real problem.

This section gives several supported extension paths. A course offering may require one or allow a choice.

## 2. Outcomes

For the chosen path, you should be able to:

- state the engineering problem being solved;
- explain the new mechanism;
- identify new failure modes;
- implement a bounded extension;
- prove the extension with evidence;
- explain why the added complexity is justified.

## 3. Prerequisites

Sections 1-14 and a functioning project/system to extend.

## 4. Core model

Use this decision frame:

1. What problem exists now?
2. What measurable requirement is not met?
3. What candidate technique addresses it?
4. What new complexity/failure mode does it add?
5. How will you verify improvement?
6. How will you know when the technique is unnecessary?

## 5. Supported advanced paths

### Path A: automated testing and regression

Extend manual/fixed tests into automated tests that run consistently.

Good targets:

- parser vectors;
- state transitions;
- file round trips;
- protocol serialization;
- pure calculations.

Goal: make previous failures difficult to reintroduce silently.

### Path B: asynchronous/concurrent event handling

Use asynchronous operations where blocking would harm responsiveness or throughput.

Questions:

- what work overlaps?
- who owns mutable state?
- how is cancellation handled?
- how are errors surfaced?
- what ordering is guaranteed?

Do not use concurrency merely to "make it faster."

### Path C: networking / HTTP / JSON

Move a bounded interface from local/serial/file into networked request/response or structured interchange.

Define:

- endpoint;
- request/response schema;
- timeout;
- status/error behavior;
- retry policy;
- serialization contract.

### Path D: packaging and continuous integration

Make the software reproducible outside the development machine.

Possible evidence:

- deterministic build command;
- automated tests in CI;
- published artifact;
- versioned release;
- environment/dependency documentation.

### Path E: deeper memory/compiler/disassembly analysis

Choose one function or hot path and compare:

- source;
- generated IL/assembly;
- memory/storage behavior;
- optimization effects;
- measured performance/code size when meaningful.

The goal is explanation, not premature optimization.

## 6. Worked examples

### Bridge example: advanced means one justified extension

Choose a program that already works. Add **one** advanced capability while preserving its existing behavior.

Examples:

- add one automated regression test to a parser;
- make one blocking I/O operation asynchronous;
- serialize one known object to JSON;
- add CI that runs an existing build/test command.

For the first experiment, do not combine async, networking, packaging, and CI.

A strong exploration answers:

1. What problem am I trying to solve?
2. What existing behavior must remain unchanged?
3. What is the smallest new feature that demonstrates the idea?
4. How will I prove it worked?
5. What complexity did the new technique add?


### Example 1: regression test

A parser once accepted payload length 255 even though maximum is 32.

Add a test that feeds length 255 and requires an explicit error. Future changes now prove they preserve the fix.

### Example 2: async justified

Host UI freezes while waiting on a network request.

Async I/O can allow UI/event processing to continue, but state updates still need clear ownership and cancellation/error handling.

### Example 3: CI value

A repository builds on one laptop but fails from a clean clone.

A CI workflow that restores dependencies, builds, and runs tests becomes an independent reproducibility check.

## 7. Apply, verify, and troubleshoot

For any extension, submit:

- problem statement;
- baseline behavior;
- design choice;
- implementation;
- test/measurement plan;
- before/after evidence where applicable;
- failure cases;
- conclusion including whether complexity was worth it.

Do not claim improvement without evidence.

## 8. Practice

1. When is concurrency unnecessary?
2. What makes a regression test valuable?
3. Why must an HTTP/JSON interface still have a contract?
4. What does CI prove that "it builds on my machine" does not?
5. Why inspect generated code only for a bounded question?
6. Give one example where the best advanced decision is to keep the simpler design.

## 9. Answer key

1. When work is already responsive/sequential and overlap adds no requirement value, or state complexity outweighs benefit.
2. It permanently reproduces a previously important behavior/failure boundary.
3. Network transport does not define message semantics, validation, timeout, or error behavior.
4. It proves a clean automated environment can reproduce the build/tests using documented dependencies.
5. Full-program disassembly is noisy; bounded inspection ties low-level evidence to a specific engineering question.
6. Example: a 10 Hz sensor logger with simple file output may not need async pipelines if synchronous work finishes far below the sample period.

## 10. Explain without notes

Explain:

- requirement-driven advanced work;
- regression test;
- async tradeoff;
- network contract;
- reproducible CI;
- bounded low-level inspection;
- evidence-based conclusion.

## 11. References

- Microsoft Learn, .NET testing — https://learn.microsoft.com/en-us/dotnet/core/testing/
- Microsoft Learn, Task asynchronous programming — https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/task-asynchronous-programming-model
- Microsoft Learn, HttpClient — https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient
- Microsoft Learn, System.Text.Json — https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview
- Python Software Foundation, asyncio — https://docs.python.org/3/library/asyncio.html
- Python Software Foundation, json — https://docs.python.org/3/library/json.html
- GitHub Docs, Actions — https://docs.github.com/en/actions
