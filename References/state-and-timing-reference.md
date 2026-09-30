# State and Timing Quick Reference

[References index](README.md)

## State transition row

| Current | Event/condition | Guard | Action | Next |
| --- | --- | --- | --- | --- |
| Idle | Start | safe | start timer | Running |

## Timer contract

Always define:

- trigger/start;
- clock source;
- whether repeated trigger restarts;
- pause/cancel if any;
- expiry boundary;
- expiry action;
- tolerated timing error;
- fault/interlock behavior.

## Hysteresis template

For low/high thresholds:

- value <= low: force one state;
- value >= high: force opposite state;
- between: retain prior state.

## Interlock

An interlock prevents an otherwise requested action while an incompatible condition is active.

## Fault priority

Write priority explicitly. Example:

    safety fault
      > manual stop
      > normal automatic transition

Do not rely on callback execution order to imply priority.

## Controlled-time test

    start at t=0
    advance to target - epsilon -> not expired
    advance to target -> expires
    repeat trigger -> verify documented restart/no-restart behavior

## Evidence

For timing claims distinguish:

- requested/setpoint time;
- simulated logical behavior;
- measured real elapsed time;
- scheduler/hardware tolerance.
