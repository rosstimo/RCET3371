# Testing and Verification Guide

[Guides index](../README.md)

## Start with a contract

Before writing a test, state:

- input/state;
- operation/event;
- expected output/state;
- tolerated error if numeric/timing;
- observable evidence.

## Test layers

### Pure logic

No files, ports, GUI, or hardware.

Examples:

- mask/shift;
- parser;
- state transition;
- conversion;
- statistics.

These should be the easiest and most deterministic tests.

### Boundary/component tests

Use fakes or fixed data.

Examples:

- fake transport;
- temporary file;
- captured byte stream;
- fake clock.

### Integration tests

Combine real components deliberately.

### Hardware verification

Physical verification is valuable but should not replace deterministic software tests.

## Boundary cases

Always consider:

- minimum;
- maximum;
- exactly at threshold;
- just below/above threshold;
- empty;
- one item;
- full buffer;
- malformed input;
- timeout;
- disconnect/reconnect.

## Regression

When a defect is fixed, preserve the smallest reproduction as a test when practical.

## Expected versus actual

Do not write "test passed" as the only evidence.

For important requirements preserve:

- test case;
- expected;
- actual;
- result.

## Time-dependent testing

Prefer a controllable clock for domain timing. Use real elapsed-time measurement only when scheduler/device timing itself is under test.

## Hardware-independent path

Every RCET 3371 host/device Programming Assignment must support a core verification path without physical hardware.

Hardware integration is an additional verified layer.

## References

- .NET testing: https://learn.microsoft.com/en-us/dotnet/core/testing/
- Python unittest: https://docs.python.org/3/library/unittest.html
