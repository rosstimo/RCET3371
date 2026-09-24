# Event-Driven Device/Host Controller Specification

## Required states

```text
Disconnected
Idle
Heating
Cooling
Fault
```

## Inputs

- `connected`
- `sensorValid`
- `temperatureC`
- `setpointC`
- `resetRequested`
- controlled current time

## Outputs and interlock

Outputs are `heaterOn` and `coolerOn`.

**Heater and cooler must never both be ON.**

## Setpoint

Legal setpoint: **15.0 °C through 30.0 °C inclusive**. Reject values outside this range.

## Normal thresholds

Let `S = setpointC`.

From **Idle**:

- enter Heating when `temperatureC <= S - 1.0 °C`;
- enter Cooling when `temperatureC >= S + 1.0 °C`;
- otherwise remain Idle.

From **Heating**:

- normal exit target is Idle when `temperatureC >= S`;
- Heating must run at least **5.0 s** before normal exit.

From **Cooling**:

- normal exit target is Idle when `temperatureC <= S`;
- Cooling must run at least **5.0 s** before normal exit.

## Idle deadtime

After leaving Heating or Cooling normally, no new Heating/Cooling mode may begin until at least **2.0 s** have elapsed in Idle.

Fault/disconnect behavior overrides this rule.

## Timing semantics

When entering Heating or Cooling:

- record mode-start time once on state entry;
- repeated evaluation while remaining in that state must not reset the timer.

When entering Idle from active mode:

- record idle-entry time;
- deadtime is measured from that entry.

Use `elapsed >= duration`, not equality with one exact timestamp.

## Connection behavior

When `connected == false`:

- command both outputs OFF;
- state must be Disconnected.

When connection becomes valid:

- transition Disconnected → Idle;
- normal deadtime begins at that Idle entry.

## Sensor fault and reset

If `sensorValid == false` while connected:

- command both outputs OFF immediately;
- enter Fault from Idle, Heating, or Cooling;
- remain Fault until reset is requested **and** sensor is valid **and** connection is valid.

A reset request while unsafe must not leave Fault.

## Priority

1. disconnected;
2. invalid sensor/fault;
3. valid reset from Fault;
4. normal state/timing/temperature transitions.

Normal minimum-run/deadtime rules never delay safe shutdown.

## Output truth table

| State | Heater | Cooler |
| --- | --- | --- |
| Disconnected | OFF | OFF |
| Idle | OFF | OFF |
| Heating | ON | OFF |
| Cooling | OFF | ON |
| Fault | OFF | OFF |

## Required boundary tests

At minimum test:

- exactly `S - 1.0`;
- just above `S - 1.0`;
- exactly `S + 1.0`;
- just below `S + 1.0`;
- active-mode elapsed 4.999 s and 5.000 s;
- idle elapsed 1.999 s and 2.000 s;
- fault during Heating before minimum run time;
- disconnect during Cooling before minimum run time;
- invalid reset;
- valid reset.

## Architecture rule

The controller must not call wall-clock sleep, UI controls, or concrete serial APIs directly. Those belong behind boundaries.
