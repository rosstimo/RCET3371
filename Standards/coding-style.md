# RCET 3371 Coding Standard

[Standards index](README.md)

This standard applies unless a Programming Assignment explicitly overrides it.

## Primary goals

Code should be:

- correct;
- readable;
- testable;
- organized by responsibility;
- explainable by the student;
- consistent enough that another engineer can review it quickly.

## Naming

Use descriptive names that reflect domain meaning.

Prefer:

    sampleCount
    packetLength
    parse_status
    uart_read_byte

Avoid:

    x
    temp1
    stuff
    doThing

Short loop indices are acceptable when their meaning is obvious and scope is tiny.

## Functions

A function should have one coherent responsibility.

Make inputs, outputs, side effects, and persistent state obvious.

Avoid hidden global state when a parameter, return value, object/module field, or explicit state structure better represents ownership.

## Files/modules

Split code when responsibilities differ.

Do not create a file per tiny function merely to appear modular.

## Constants

Use named constants for protocol values, limits, thresholds, and configuration that carry meaning.

Avoid unexplained numeric literals in behavior logic.

## Comments

Comments should explain:

- why;
- contract;
- units;
- nonobvious constraint;
- hardware/protocol meaning.

Do not comment obvious syntax.

## Error handling

Do not silently ignore errors.

Use:

- validation;
- clear status results;
- exceptions where the language/library model expects them;
- logged context;
- defined recovery behavior.

## Formatting

Use the language's conventional formatter/style where practical.

Keep lines readable. Avoid horizontally dense expressions when splitting improves reasoning.

## Evidence

Code quality is part of verification. A student should be able to explain:

- state ownership;
- module boundaries;
- important invariants;
- failure behavior;
- tests.
