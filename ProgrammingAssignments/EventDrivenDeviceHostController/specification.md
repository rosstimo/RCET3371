# Event-Driven Device/Host Controller Specification

This document is the authoritative behavior contract for the assignment.

## 1. System purpose

Control a simulated or live environmental device from a host application while preserving explicit state, timing, safety interlocks, failure behavior, logging, and testability.

## 2. Inputs

The controller consumes:

- connection/device-identity state;
- enable/run request;
- temperature sample in degrees Celsius;
- protocol/device fault indication;
- reset-fault request;
- monotonic time from an injected clock.

## 3. Outputs

The controller produces:

- HEATER request;
- COOLER request;
- controller state;
- fault reason when applicable;
- transition events suitable for logging/presentation.

## 4. States

Required states:

- `DISCONNECTED`
- `IDLE`
- `HEATING`
- `COOLING`
- `FAULT`

Additional substates require explicit justification and tests.

## 5. Default configuration

```text
target_c = 25.0
deadband_c = 1.0
minimum_output_hold_s = 5.0
sample_stale_timeout_s = 3.0
sensor_min_c = -40.0
sensor_max_c = 125.0
```

Configuration must reject:

- deadband <= 0;
- hold < 0;
- stale timeout <= 0;
- sensor_min >= sensor_max;
- target outside sensor range.

## 6. Connection behavior

Without a successfully identified device:

- state = DISCONNECTED;
- HEATER = OFF;
- COOLER = OFF.

After transport connection, protocol identity must succeed before normal control is available.

Disconnect from any state:

- immediately turns both outputs OFF;
- transitions to DISCONNECTED;
- records the transition reason.

## 7. Enable behavior

Connected + disabled:

- state = IDLE;
- both outputs OFF.

Enabling permits normal temperature control.

Disabling from IDLE/HEATING/COOLING:

- immediately turns both outputs OFF;
- transitions to IDLE;
- overrides minimum-output hold.

## 8. Hysteresis and normal transitions

Define:

```text
heat_on_threshold = target - deadband = 24.0 C
cool_on_threshold = target + deadband = 26.0 C
```

When enabled and not faulted:

### From IDLE

- temperature <= 24.0 C -> HEATING;
- temperature >= 26.0 C -> COOLING;
- otherwise remain IDLE.

### From HEATING

After minimum hold is satisfied:

- temperature >= target (25.0 C) -> IDLE;
- otherwise remain HEATING.

Do not transition directly HEATING -> COOLING from one sample. Return through IDLE.

### From COOLING

After minimum hold is satisfied:

- temperature <= target (25.0 C) -> IDLE;
- otherwise remain COOLING.

Do not transition directly COOLING -> HEATING from one sample. Return through IDLE.

## 9. Minimum-output hold

When entering HEATING or COOLING:

- record output start time;
- normal temperature control may not turn that output off until elapsed time >= 5.0 s.

Boundary:

- elapsed < 5.0 s: hold active;
- elapsed >= 5.0 s: normal exit rule may apply.

These override hold immediately:

- disable;
- disconnect;
- any FAULT transition.

## 10. Interlock invariant

At all observable times:

```text
NOT (HEATER == ON AND COOLER == ON)
```

Violation is a controller defect.

## 11. Sample freshness

While enabled in IDLE/HEATING/COOLING:

- latest valid sample age < 3.0 s is fresh;
- age >= 3.0 s triggers FAULT with reason `STALE_SAMPLE`.

Timing begins from the timestamp of the latest accepted valid sample.

## 12. Sensor validity

A sample outside inclusive range:

```text
-40.0 C <= temperature <= 125.0 C
```

triggers FAULT with reason `SENSOR_RANGE`.

Do not clamp an invalid sample into range.

## 13. Device fault

A validated device/protocol fault indication triggers FAULT with reason `DEVICE_FAULT`.

FAULT priority overrides normal temperature/timing behavior.

## 14. FAULT behavior

On entry:

- HEATER = OFF;
- COOLER = OFF;
- fault reason retained;
- transition logged.

Normal enable/temperature events do not leave FAULT.

## 15. Fault reset

Reset succeeds only when all are true:

- device remains connected and identified;
- latest sample is valid;
- latest sample is fresh;
- device fault indication is clear;
- both outputs are OFF.

Successful reset:

- clears fault reason;
- transitions to IDLE.

It does not transition directly to HEATING or COOLING in the same event. Normal control is evaluated on a later controller update/event.

## 16. Clock contract

Controller domain logic receives time through an abstraction such as:

```text
IClock.Now / elapsed API
```

Tests must control time without real `Thread.Sleep`/wall-clock waits.

Use a monotonic elapsed-time concept for control intervals. Presentation may separately show wall-clock timestamps.

## 17. Transition logging

Each transition record includes:

- timestamp;
- prior state;
- event/reason;
- next state;
- latest temperature when relevant;
- HEATER request;
- COOLER request;
- fault reason when applicable.

## 18. Presentation contract

Presentation must expose:

- connection/identity;
- controller state;
- current temperature;
- target/deadband;
- output requests;
- fault reason;
- logging state.

Presentation must not own controller truth.

## 19. Required test boundaries

Test exact values:

- temperature 24.0, just above 24.0;
- temperature 26.0, just below 26.0;
- return target 25.0;
- elapsed 4.999 s and 5.000 s;
- sample age 2.999 s and 3.000 s;
- sensor -40.0, 125.0, and just outside each;
- reset safe/unsafe combinations.

The course reference solution and grader use this document as the authority.
