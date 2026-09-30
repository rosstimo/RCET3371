# Controlled-Time State Machine Example

This example supports Section 10. It shows how to remember control state and react to supplied elapsed time instead of stopping the program with `Sleep`.

## Start from a familiar blocking sequence

A first version of a timed program might be imagined as:

```text
turn heater on
wait 5 seconds
turn heater off
```

The problem is the word **wait**. While ordinary flow is blocked, the program cannot conveniently process another event such as disable, disconnect, or fault.

A state-based version remembers where it is and checks time on repeated updates.

## Run

```bash
dotnet run --project StateMachine.csproj
```

Expected output:

```text
Heating
Heating
Idle
```

## Trace it as a table

Before editing code, make a table like this for the supplied example:

| Update | Current state | Event/condition | Elapsed time | Next state |
| --- | --- | --- | ---: | --- |
| 1 | Idle | heat requested | 0 | Heating |
| 2 | Heating | hold not expired | below limit | Heating |
| 3 | Heating | hold expired / target condition satisfied | at or above limit | Idle |

The exact names depend on the source, but the method is the same.

## Why supplied time matters

Tests should not actually sleep for five seconds to prove a five-second rule.

Instead, the test/example supplies the time value. That allows cases such as:

```text
4.999 s -> still Heating
5.000 s -> boundary behavior
5.001 s -> after boundary
```

to run immediately and repeatably.

## Safe modifications

1. Change the required hold interval and predict which update first changes state.
2. Add a disable event that must override the ordinary hold.
3. Add a fault event and verify the output becomes safe immediately.
4. Add one boundary test immediately before, at, and after a transition time.
5. Write the expected transition table before changing the code.

## What this example demonstrates

- current state must persist between updates;
- time is input to the control logic rather than a reason to block the whole program;
- transitions should be explainable from current state + event/condition;
- boundary timing can be tested deterministically.
