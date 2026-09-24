# Programming Assignment — Event-Driven Device/Host Controller

[Programming Assignments index](../README.md)

**Points: 150**

## Objective

Build a deterministic host/device-control model that combines explicit state, timing, hysteresis, interlocks, faults, persistence, protocol communication, and presentation.

The core system must be completely verifiable with a fake device and fake clock. Physical hardware integration is an additional layer, not the only proof of correctness.

## System scenario

Implement a small environmental controller.

Inputs:

- enable/run request;
- temperature sample in degrees C;
- connection/device identity;
- reset-fault request;
- clock/time.

Outputs:

- HEATER request;
- COOLER request;
- visible controller state;
- log/telemetry events.

## Controller states

At minimum:

- DISCONNECTED
- IDLE
- HEATING
- COOLING
- FAULT

You may introduce substates only when the design explains why.

## Control contract

Default configuration:

    target = 25.0 C
    deadband = ±1.0 C
    minimum_output_hold = 5.0 s
    sample_stale_timeout = 3.0 s
    valid_sensor_range = -40.0 .. 125.0 C

### Connection

- without an identified device, state is DISCONNECTED;
- after transport connection, identity must succeed before control becomes available;
- disconnect forces outputs off and transitions to DISCONNECTED.

### Enable/run

When connected and disabled:

- state is IDLE;
- HEATER = OFF;
- COOLER = OFF.

When enabled, normal control may transition among IDLE, HEATING, and COOLING.

### Hysteresis

- temperature <= target - deadband requests HEATING;
- temperature >= target + deadband requests COOLING;
- inside the deadband, do not switch directly from one output to the other;
- normal return toward IDLE should occur when the target is crossed in the appropriate direction.

Your transition table must state exact boundaries.

### Minimum output hold

After HEATER or COOLER turns on, do not switch that output off for 5.0 seconds solely because of normal temperature control.

Safety/fault/disable overrides the hold.

Use a controllable clock. Do not implement verification with real Sleep calls.

### Interlock

HEATER and COOLER must never be requested ON at the same time.

### Faults

Transition immediately to FAULT and force both outputs OFF when:

- a sample is outside valid sensor range;
- no valid sample has arrived for 3.0 seconds while enabled;
- the protocol/device reports a fault.

FAULT overrides normal hysteresis and minimum-hold timing.

### Fault recovery

A reset request may leave FAULT only when:

- connection/device identity is valid;
- latest sensor value is valid and not stale;
- device fault indication is clear;
- outputs are off.

Return to IDLE. Do not jump directly from FAULT to HEATING/COOLING in the same transition.

## Architecture

Keep responsibilities separable:

- transport;
- protocol/device API;
- device/domain model;
- controller/state machine;
- clock;
- configuration;
- persistence/logging;
- presentation.

Core controller logic must not depend directly on SerialPort or WinForms controls.

## Required deterministic tests

At minimum:

- disconnected startup;
- identity success;
- enable in deadband;
- heating threshold boundary;
- cooling threshold boundary;
- hysteresis behavior;
- 5 s hold just before and at expiry;
- disable during hold;
- fault during hold;
- stale timeout just before and at 3 s;
- invalid sensor;
- device fault;
- reset blocked when unsafe;
- successful reset;
- disconnect from every active state;
- proof that HEATER and COOLER are never simultaneously on.

## Configuration

Load target/deadband/timing values from an explicit configuration source.

Validate:

- deadband > 0;
- hold >= 0;
- stale timeout > 0;
- sensor min < max;
- target is within valid sensor range.

Malformed configuration must fail visibly.

## Presentation

Provide a usable presentation layer, initially console or simple GUI.

It must show at least:

- connection/device state;
- controller state;
- temperature;
- target/deadband;
- HEATER/COOLER outputs;
- fault reason;
- logging status.

Domain/control behavior must work without the UI.

## Logging

Record transitions and faults with enough context to reconstruct behavior:

- timestamp;
- prior state;
- event/reason;
- next state;
- temperature when relevant;
- output requests;
- fault reason.

## Live hardware

Integrate through the established device/protocol boundary when hardware is available.

Do not modify controller logic merely to make the live transport work.

## Complete when

- deterministic state/time tests all pass;
- transitions match a documented table;
- fault priority and reset rules are proven;
- outputs are interlocked;
- configuration is validated;
- presentation exposes meaningful state;
- logs explain transitions;
- core system runs against fake device/time;
- live hardware path is verified or explicitly marked as simulation-only under instructor-approved conditions.

## Evaluation

| Area | Points |
| --- | ---: |
| explicit state/transition design | 20 |
| hysteresis + normal control | 20 |
| timing/controlled-clock behavior | 20 |
| interlock + fault priority/recovery | 25 |
| architecture/testability | 20 |
| deterministic automated tests | 20 |
| configuration/logging/persistence | 10 |
| presentation/diagnostics | 5 |
| live/simulated integration evidence | 5 |
| documentation/repository quality | 5 |
| **Total** | **150** |

## Submission

Submit repository URL and final verification evidence requested by the LMS.
