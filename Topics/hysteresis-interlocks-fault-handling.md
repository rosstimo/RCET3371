# RCET 3371 — Hysteresis, Interlocks, and Fault Handling

*Self-learning guide*

[Topics index](README.md)

Threshold control and safety behavior need more than ordinary if/else tests. Hysteresis preserves prior state across a deadband, interlocks prevent incompatible actions, and fault rules define which conditions override normal control.

### Hysteresis

For a threshold-controlled output:

    turn on below 19 C
    turn off above 21 C

Between 19 and 21, preserve current state.

Hysteresis prevents chatter around one threshold.

### Interlock

An interlock prevents an otherwise valid action while another condition is active.

Example: heater and cooling outputs must never be energized together.

### Fault priority

Define which events override normal control.

A safety fault may transition immediately to Fault regardless of normal timing.

### Example 2: thermostat hysteresis

Initial output OFF, temperature 18.5 C -> output ON.

Temperature rises:
- 19.5 C -> remains ON;
- 20.5 C -> remains ON;
- 21.1 C -> OFF.

Temperature falls:
- 20.0 C -> remains OFF;
- 18.9 C -> ON.

### Example 3: responsibility separation

Transport receives packet. Parser validates it. Device model records telemetry. Controller decides transition. Logger records event. UI displays state.

Each layer can be tested with substitutes for adjacent layers.

## Practice

1. Trace the thermostat example through 18.5, 19.5, 20.5, 21.1, 20.0, and 18.9 C.
2. Why does hysteresis require knowledge of current output/state inside the deadband?
3. Write an interlock rule that prevents heater and cooling outputs from being active together.
4. What should happen if a high-priority safety fault occurs while the controller is Running?
5. Why should fault detection/control decisions remain separate from UI display code?

## Answer reasoning

1. ON, ON, ON, OFF, OFF, ON for the thresholds shown in the guide.
2. Between thresholds the correct output depends on which side was crossed previously.
3. Enabling either output must require the incompatible output to be off, with one owner enforcing the rule.
4. The defined fault transition should override normal timing/control and move to the safe Fault behavior.
5. The control contract must remain testable and valid even when no UI is present.

## Ready to continue when

You can trace hysteresis from state, define an interlock, specify fault priority/recovery, and separate safety/control behavior from transport, persistence, and presentation.

## References

- [State and timing reference](../References/state-and-timing-reference.md)
- [State Machines and Event Models](state-machines-event-models.md)
- [Timing and Nonblocking Behavior](timing-nonblocking-behavior.md)
