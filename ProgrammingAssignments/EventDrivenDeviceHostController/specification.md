# Event-Driven Device/Host Controller Specification

## Scenario

Implement a small environmental controller.

Inputs: enable/run request, temperature sample in °C, connection/device identity, reset-fault request, device fault indication, and controllable clock/time.

Outputs: HEATER request, COOLER request, controller state, and log/telemetry events.

## Required states

DISCONNECTED, IDLE, HEATING, COOLING, FAULT. Additional states require a documented reason.

## Default configuration

```text
target = 25.0 °C
deadband = ±1.0 °C
minimum_output_hold = 5.0 s
sample_stale_timeout = 3.0 s
valid_sensor_range = -40.0 .. 125.0 °C
```

Validate: deadband > 0; hold >= 0; stale timeout > 0; sensor minimum < maximum; target inside valid sensor range.

## Connection and identity

Without an identified device state is DISCONNECTED. Transport connection alone is insufficient. Disconnect forces outputs OFF and state DISCONNECTED.

## Hysteresis

- temperature <= target - deadband may request HEATING;
- temperature >= target + deadband may request COOLING;
- within deadband, do not switch directly from one output to the other;
- return toward IDLE when target is crossed in the appropriate direction.

The transition table must define exact equality behavior.

## Minimum output hold

Once HEATER or COOLER turns ON, ordinary temperature control cannot turn it OFF for 5.0 s. Safety fault, disconnect, or disable overrides the hold.

Use a controllable clock. Real `Sleep` delays are not valid verification.

## Interlock and faults

HEATER and COOLER must never both be ON.

Immediately enter FAULT and force both outputs OFF when sample is outside valid range, no valid sample arrives for 3.0 s while enabled, or the device/protocol reports a fault.

FAULT overrides hysteresis and minimum hold.

## Fault recovery

Reset may leave FAULT only when connection/identity are valid, latest sample is valid/not stale, device fault is clear, and outputs are OFF. Recovery returns to IDLE.

## Logging and presentation

Log timestamp, prior state, event/reason, next state, temperature when relevant, output requests, and fault reason.

Show connection/identity, controller state, temperature, target/deadband, outputs, fault reason, and logging state. Controller behavior must work without the presentation layer.

## Required deterministic tests

Test disconnected startup; identity success/failure; enable in deadband; threshold below/at/above; hysteresis; hold just before/at/after expiry; disable/fault during hold; stale timeout before/at/after; invalid sensor; device fault; blocked/successful reset; disconnect from every active state; and proof both outputs are never ON together.
