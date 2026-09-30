# RCET 3371 — Timing and Nonblocking Behavior

*Self-learning guide*

[Topics index](README.md)

Timing requirements are behavior contracts. Define what starts time, what resets it, what expiry means, what clock is used, and how accurately it must occur. Nonblocking designs preserve responsiveness while time is being measured.

### Timer contract

A timer requirement is incomplete unless it defines:

- what starts it;
- whether repeated events restart it;
- whether pause exists;
- what exactly happens at expiry;
- which clock/time source is used;
- tolerated timing error.

### Controlled time

Code tied directly to wall-clock sleeps is difficult to test.

Introduce a clock/time boundary so tests can advance time deliberately:

    fake time = 0
    start
    advance to 4.9 s -> still running
    advance to 5.0 s -> expiry transition

### Example 1: timer reset ambiguity

Requirement: "turn off five seconds after motion."

Questions:

- five seconds after first motion?
- after most recent motion?
- does motion during the interval restart timer?
- what if a fault occurs?
- what if the user manually turns it off?

Without answers, implementations can differ while each developer believes they are correct.

## Practice

### Host-side deterministic timing model

Instead of sleeping inside controller logic, pass current time into the decision:

```csharp
static bool HasExpired(
    TimeSpan now,
    TimeSpan started,
    TimeSpan duration)
{
    return now - started >= duration;
}
```

Test `4.9 s`, `5.0 s`, and `5.1 s` without waiting in real time.

### Embedded-C nonblocking pattern

A foreground loop can compare accumulated/tick-derived time without blocking all other work:

```c
if (timer_expired)
{
    timer_expired = 0u;
    elapsed_ticks++;

    if (elapsed_ticks >= required_ticks)
    {
        elapsed_ticks = 0u;
        handle_expiry();
    }
}

service_other_work();
```

The exact timer peripheral and tick interval belong to the implementation requirement. The concept is that the program remembers timing state across iterations instead of sitting inside a delay loop.

## Answer reasoning

1. A requirement says "turn off five seconds after motion." List at least three questions needed to make that timing contract unambiguous.
2. Why is controlled/fake time useful in tests?
3. What is the conceptual difference between a blocking delay and retained timing state checked across loop iterations?
4. Why should a Topic about nonblocking behavior not dictate one specific PIC timer unless the requirement does?
5. Which boundary cases should a five-second expiry test include?

## Ready to continue when

1. Define whether timing starts on first/latest motion, whether repeated motion restarts it, what faults/manual actions do, and what clock/tolerance applies.
2. Tests can reach boundaries instantly and reproducibly.
3. Blocking waits prevent normal flow from doing other work; retained timing state lets the loop continue servicing other responsibilities.
4. The behavior can be implemented with different timer resources/periods; the concept should survive that choice.
5. Just before expiry, exactly at expiry, and just after expiry.

## References

You can turn a vague timing statement into a precise contract, test timing with controlled time, and explain how retained timing state supports responsive host and embedded code.
