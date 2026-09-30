# Programming Assignment — Protocol Data Logger

[Programming Assignments index](../README.md)

**Points: 200**  
**Sections: 7–9**

## Objective

Build a robust host-side protocol/parser/logger system that works completely against deterministic simulated/captured data before physical serial hardware is required.

This assignment integrates protocol design, streaming parsing, host serial architecture, persistence, replay, and failure recovery.

## Documents

- [Specification](specification.md)
- [Milestones](milestones.md)
- [Rubric](rubric.md)

## Required implementation

Your repository must include:

```text
README.md
src/
tests/
python/
evidence/
```

The C# implementation is the primary host. Python supplies a diagnostic/replay tool.

The parser and logging path must be fully testable without a serial port.

## Supplied resources

- [Protocol vectors](../../Resources/protocol-vectors/rcet-telemetry-protocol-v1.txt)
- [Captured stream](../../Resources/captured-streams/telemetry-protocol-capture.txt)
- [Sample telemetry log](../../Resources/datasets/sample-telemetry.csv)

## Complete when

A grader can clone the repository and verify, without hardware:

1. protocol parser correctness under arbitrary chunking;
2. fake transport identity/sample behavior;
3. error/disconnect recovery;
4. logging and replay round trip;
5. Python diagnostic/replay output.

The real serial adapter must also exist. Physical hardware verification is recorded separately when hardware is available.
