# Event-Driven Device/Host Controller

**Programming Assignments category value: 200 points out of 1000**

Sections: 10–11

## Objective

Build a deterministic host/device environmental controller that combines explicit state, timing, hysteresis, interlocks, faults, configuration, persistence, protocol communication, and presentation.

Read [specification.md](specification.md) before implementing. The core system must be fully verifiable with a fake device and fake clock. Physical hardware integration is a separately documented layer.

## Required architecture

Keep these responsibilities separable:

- transport;
- protocol/device API;
- device/domain model;
- controller/state machine;
- clock/time source;
- configuration;
- logging/persistence;
- presentation.

The controller must not depend directly on `SerialPort`, WinForms controls, or real wall-clock sleeps.

## Required deliverables

Your repository must include:

- state/transition table or diagram;
- controller implementation;
- fake clock;
- fake device/transport path;
- deterministic automated tests;
- validated configuration;
- transition/fault log;
- usable presentation layer;
- documented live-hardware adapter/integration path;
- verification evidence.

## Required behavior

The authoritative control contract, thresholds, timing, fault priority, and recovery rules are in [specification.md](specification.md).

Do not change thresholds or state semantics merely to make tests pass.

## Verification

Your automated verification must prove:

- disconnected startup;
- identity/connection transition;
- enabled/disabled behavior;
- both hysteresis boundaries;
- minimum-output-hold boundary just before and exactly at expiry;
- disable override during hold;
- fault override during hold;
- stale-sample timeout boundary;
- invalid sensor range;
- reported device fault;
- blocked unsafe reset;
- valid reset;
- disconnect from every active state;
- HEATER/COOLER interlock invariant.

## Hardware rule

Core grading is hardware-independent. The live adapter must preserve the same device/controller contract.

If compatible hardware is available, verify and record the live path. If it is unavailable, do not alter controller logic to fake hardware-specific success.

## Milestones

See [milestones.md](milestones.md).

## Evaluation

See [rubric.md](rubric.md).

## Complete when

A clean clone can run the full deterministic suite, exercise the controller through fake time/device inputs, reproduce logs and presentation behavior, and build the documented live integration layer.

## Submission

Submit the repository URL and final verification evidence requested by the LMS.
