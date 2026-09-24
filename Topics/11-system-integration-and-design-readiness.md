# RCET 3371 — System Integration and Design Readiness

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 11](../LearningPath/11-System-Integration.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. Integration strategy
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

Integration should not be the first time individual components are tested.

A reliable engineering system is built by proving contracts in increasingly realistic environments: fixed data, fake components, captured data, loopback, and only then live hardware.

## 2. Outcomes

You should be able to:

- decompose a system into responsibilities and interfaces;
- justify language and host/device boundaries;
- define what is verified at each integration stage;
- use fixed/fake/captured data before live hardware;
- document protocol and interface assumptions;
- identify risks and unknowns explicitly;
- distinguish system failure from component failure;
- present verification evidence rather than only a successful demo.

## 3. Prerequisites

Sections 1-10.

## 4. Core model

Use an integration ladder:

1. **pure logic with fixed vectors**
2. **component with fake dependency**
3. **captured real-world data**
4. **loopback/simulator**
5. **live device**
6. **failure/recovery cases**

Each step adds uncertainty. Do not add several unknowns at once when you can avoid it.

## 5. Integration strategy

### Responsibility map

A typical RCET 3371 device/host system may have:

- firmware/device behavior;
- physical serial transport;
- host transport wrapper;
- protocol parser;
- device model;
- controller/state logic;
- persistence/logging;
- presentation;
- deployment/configuration.

Draw arrows that show dependencies. Prefer dependencies toward interfaces/contracts rather than tangled two-way knowledge.

### Contract-first integration

For each boundary record:

- data/function/event crossing boundary;
- valid input;
- output;
- error behavior;
- timing assumption;
- owner of state.

### Verification evidence

Strong evidence includes:

- automated test output;
- captured protocol bytes;
- version/commit identifier;
- expected/actual table;
- screenshots only when visual state matters;
- logs tied to a known scenario;
- measured timing when timing is a requirement.

A screenshot of "it works" is not enough for nonvisual behavior.

### Risks and unknowns

An unknown is not a failure if it is explicit.

Example:

    Unknown: USB serial device renames after reconnect on Linux.
    Plan: identify by VID/PID/serial and verify on target machine.

## 6. Worked examples

### Bridge example: integrate three pieces that already work

Do not begin integration with the complete device/host system.

Use three known pieces:

1. fixed captured bytes;
2. a parser already proven against those bytes;
3. a logger already proven with constructed records.

Connect fixed bytes -> parser first and verify the parsed object. Then connect parser -> logger and verify the saved record. Only after the software chain works should a live transport replace the fixed capture.

If the full system fails later, this staged build gives you known-good boundaries to test instead of one large unknown.


### Example 1: parser before hardware

Protocol parser tests use fixed byte streams.

Then a fake transport emits those chunks in different sizes.

Only after both pass does the serial transport feed the same parser.

When live serial fails, you can distinguish transport/device from parser logic.

### Example 2: fault isolation

UI shows stale temperature.

Potential layers:

- device stopped sending;
- transport disconnected;
- parser rejected packet;
- model did not update;
- UI did not refresh.

Layered logging/test points let you isolate the failure instead of editing all layers.

### Example 3: language choice

Python may be ideal for quick analysis/replay tooling; C# may be preferred for the primary Windows desktop host; embedded C targets the PIC firmware.

A sound choice is justified by deployment, libraries, team familiarity, performance, hardware, and maintenance, not by language loyalty.

## 7. Apply, verify, and troubleshoot

Before integration:

- each component has tests;
- protocol/interface documented;
- simulated path works;
- version/commit recorded.

During integration:

1. verify lower layer first;
2. capture evidence at boundary;
3. compare to contract;
4. move upward only when boundary is correct.

After integration:

- test disconnect;
- malformed data;
- missing configuration;
- timeout;
- restart/reconnect;
- boundary values.

## 8. Practice

1. Why is live hardware a poor first parser test?
2. Name five system responsibilities in a data-logging host/device system.
3. What makes an interface contract testable?
4. A system demo succeeds once. What additional evidence would make it engineering verification?
5. Why record risks/unknowns before implementation?
6. Give a reason Python, C#, and embedded C might all belong in one system without that being unnecessary complexity.

## 9. Answer key

1. It adds transport/device/timing uncertainty to parser uncertainty, making failures harder to isolate.
2. Examples: firmware, transport, parser, model, control, persistence, UI, config.
3. Explicit valid inputs, outputs, error/timing behavior, and a substitutable boundary.
4. Repeatable tests, known commit, expected/actual results, failure cases, captured/logged evidence, measured requirements.
5. It prevents assumptions from masquerading as facts and gives verification work a target.
6. Different components may have different deployment/runtime/hardware needs; justified separation is acceptable if interfaces are clear.

## 10. Explain without notes

Explain:

- integration ladder;
- responsibility map;
- interface contract;
- verification evidence;
- risk versus defect;
- fixed/fake/captured/live progression;
- how layered architecture helps fault isolation.

## 11. References

- Microsoft Learn, .NET testing guidance — https://learn.microsoft.com/en-us/dotnet/core/testing/
- Python Software Foundation, unittest — https://docs.python.org/3/library/unittest.html
- Git documentation for preserving engineering history — https://git-scm.com/docs
