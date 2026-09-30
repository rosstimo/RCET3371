# Programming Assignment — Event-Driven Device/Host Controller

[Programming Assignments index](../README.md)

**Points: 250**  
**Sections: 10–11**

## Objective

Build a deterministic host/device-control model that combines explicit state, timing, hysteresis, interlocks, faults, configuration, persistence, protocol communication, and presentation.

The core system must be completely verifiable with a fake device and controllable clock. Physical hardware is an integration layer, not the only proof of correctness.

## Documents

- [Specification](specification.md)
- [Milestones](milestones.md)
- [Rubric](rubric.md)

## Required architecture

Keep transport, protocol/device API, domain/device model, controller/state machine, clock/time provider, configuration, persistence/logging, and presentation separable.

Core control logic must not depend directly on `SerialPort` or GUI controls.

## Complete when

A grader can clone the repository and deterministically prove all normal, boundary, timing, fault, and recovery transitions without sleeping or requiring hardware.
