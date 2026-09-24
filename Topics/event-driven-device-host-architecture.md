# RCET 3371 — Event-Driven Device/Host Architecture

*Self-learning guide*

[Topics index](README.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. State, time, interlocks, and faults
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

As systems become interactive, timing and state often cause more failures than syntax. A device/host controller must know what state it is in, which events are legal, when timers begin, which failures override normal behavior, and what the user should see.

If those rules are implicit inside callbacks, the system becomes difficult to test and dangerous to modify.

## 2. Outcomes

You should be able to:

- define mutually exclusive application/device states;
- draw or tabulate allowed transitions;
- distinguish an event from a state;
- define timer start/reset/expiry semantics;
- implement periodic and delayed behavior without blocking the whole application;
- apply hysteresis and interlocks;
- define fault priority and recovery;
- test time-dependent behavior using controlled/fake time;
- separate transport, parser, domain model, control, persistence, and presentation.

## 3. Prerequisites

Sections 6-9, especially software state, host serial, and persistence.

## 4. Core model

A state machine is a behavior contract.

For each transition specify:

- current state;
- event/condition;
- guard;
- action;
- next state.

Example:

| Current | Event/condition | Action | Next |
| --- | --- | --- | --- |
| Idle | Start | start timer | Running |
| Running | target reached | turn output off | Idle |
| Running | fault | disable output, log fault | Fault |
| Fault | reset AND safe | clear fault | Idle |

Do not use a boolean pile when the system really has mutually exclusive modes.

## 5. State, time, interlocks, and faults

### Events versus state

**State** persists.  
**Event** happens.

"Connected" can be a state.  
"Connection lost" is an event.

### Timer contract

A timer requirement is incomplete unless it defines:

- what starts it;
- whether repeated events restart it;
- whether pause exists;
- what exactly happens at expiry;
- which clock/time source is used;
- tolerated timing error.

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

### Controlled time

Code tied directly to wall-clock sleeps is difficult to test.

Introduce a clock/time boundary so tests can advance time deliberately:

    fake time = 0
    start
    advance to 4.9 s -> still running
    advance to 5.0 s -> expiry transition

## 6. Worked examples

### Bridge example: from a button click to explicit state

A familiar Windows Forms handler might be:

```csharp
private int count = 0;

private void addButton_Click(object sender, EventArgs e)
{
    count++;
    countLabel.Text = count.ToString();
}
```

This already contains three useful ideas:

- an event occurs;
- persistent state changes;
- the presentation is updated.

The next step is not a large architecture. Add one second event, such as a timer tick, and write down which event is allowed to change which state.

For example:

```text
Button Click -> request RUNNING
Timer Tick   -> advance elapsed time
Stop Click   -> request IDLE
```

Trace those transitions on paper before implementing a formal state machine.

### The same transition model in C#

```csharp
enum ControllerState
{
    Idle,
    Running,
    Fault
}

enum ControllerEvent
{
    Start,
    TargetReached,
    FaultDetected,
    ResetSafe
}

static ControllerState NextState(
    ControllerState state,
    ControllerEvent input)
{
    return (state, input) switch
    {
        (ControllerState.Idle, ControllerEvent.Start)
            => ControllerState.Running,

        (ControllerState.Running, ControllerEvent.TargetReached)
            => ControllerState.Idle,

        (_, ControllerEvent.FaultDetected)
            => ControllerState.Fault,

        (ControllerState.Fault, ControllerEvent.ResetSafe)
            => ControllerState.Idle,

        _ => state
    };
}
```

This version is intentionally pure: given current state and event, it returns next state. That makes transition tests easy.

### The same transition model in embedded C

```c
typedef enum
{
    STATE_IDLE,
    STATE_RUNNING,
    STATE_FAULT
} controller_state_t;

typedef enum
{
    EVENT_START,
    EVENT_TARGET_REACHED,
    EVENT_FAULT_DETECTED,
    EVENT_RESET_SAFE
} controller_event_t;

controller_state_t next_state(
    controller_state_t state,
    controller_event_t input)
{
    if (input == EVENT_FAULT_DETECTED)
    {
        return STATE_FAULT;
    }

    if ((state == STATE_IDLE) && (input == EVENT_START))
    {
        return STATE_RUNNING;
    }

    if ((state == STATE_RUNNING) &&
        (input == EVENT_TARGET_REACHED))
    {
        return STATE_IDLE;
    }

    if ((state == STATE_FAULT) && (input == EVENT_RESET_SAFE))
    {
        return STATE_IDLE;
    }

    return state;
}
```

The syntax and type system changed. The transition table did not. That is why the table/model should exist before either implementation.


### Example 1: timer reset ambiguity

Requirement: "turn off five seconds after motion."

Questions:

- five seconds after first motion?
- after most recent motion?
- does motion during the interval restart timer?
- what if a fault occurs?
- what if the user manually turns it off?

Without answers, implementations can differ while each developer believes they are correct.

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

## 7. Apply, verify, and troubleshoot

Before coding:

1. list states;
2. list events;
3. build transition table;
4. define timing;
5. define priority/interlocks;
6. define invalid-event behavior;
7. create deterministic tests.

When debugging:

- log state before/after transition;
- log event;
- log relevant time;
- record guard result;
- reproduce with fake inputs/time.

Avoid logging only "something failed."

## 8. Practice

1. Is "button clicked" a state or event?
2. Why can two booleans be worse than one enum for mutually exclusive modes?
3. Design hysteresis for a fan: ON at 35 C or above, OFF at 32 C or below.
4. What must be specified about a retriggerable timer?
5. Why is fake time better than sleeping five real seconds in every automated test?
6. What should happen if a low-priority normal event and a high-priority safety fault occur together?

## 9. Answer key

1. Event.
2. Two booleans can represent impossible combinations such as both true or both false unless extra rules enforce exclusivity.
3. OFF below/equal 32; ON above/equal 35; preserve existing state between thresholds.
4. Trigger/start, whether later triggers reset, expiry action, and any pause/cancel/fault rules.
5. It makes boundary cases fast, deterministic, repeatable, and independent of scheduler jitter.
6. The explicitly defined fault-priority rule should win; this priority belongs in the transition contract.

## 10. Explain without notes

Explain:

- event versus state;
- transition table;
- guard/action;
- timer semantics;
- hysteresis;
- interlock;
- fault priority;
- fake/controlled time;
- why callback soup hides system behavior.

## 11. References

- Microsoft, C# enumeration types — https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
- Microchip Technology Inc., *MPLAB XC8 C Compiler User's Guide* — https://onlinedocs.microchip.com/
  - Used for: embedded C implementation context.


- Microsoft Learn, events overview — https://learn.microsoft.com/en-us/dotnet/standard/events/
- Microsoft Learn, timers — https://learn.microsoft.com/en-us/dotnet/standard/threading/timers
- Python Software Foundation, asyncio overview for later asynchronous work — https://docs.python.org/3/library/asyncio.html
