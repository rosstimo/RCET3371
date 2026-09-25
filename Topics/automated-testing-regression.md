# RCET 3371 — Automated Testing and Regression

*Optional self-learning guide*

[Topics index](README.md)

Automated tests are valuable when they repeatedly protect behavior that matters. The goal is not maximum test count. The goal is to make important failures difficult to reintroduce silently.

## Core model

Good targets include pure calculations, parser vectors, state transitions, file round trips, and protocol serialization.

A regression test begins with a specific behavior that must remain true:

```text
known input
    -> operation
    -> expected result
```

Keep the test deterministic. A failure should tell you which contract changed.

## Worked example

A parser once accepted payload length 255 although the protocol maximum is 32.

Create two fixed cases:

```text
length 32  -> accepted
length 255 -> LengthOutOfRange
```

Run them automatically on every relevant change. The value is not that the test is "advanced"; the value is that a previously important boundary is now checked consistently.

## Apply and verify

For this optional Topic, use the same engineering frame:

1. state the problem;
2. preserve the known-good baseline;
3. add one bounded mechanism;
4. identify new failure modes;
5. define evidence before claiming improvement.

## Practice

1. What makes a regression test different from an ad-hoc manual check?
2. Why are deterministic vectors strong test inputs?
3. Name two RCET3371 behaviors well suited to automated tests.
4. When is adding another test not useful?

## Answer reasoning

1. The regression test is repeatable and remains part of the verification process after the original defect is fixed.
2. Expected results are known and failures can be reproduced.
3. Parser boundaries, state transitions, file round trips, protocol serialization, pure calculations, etc.
4. When it protects no meaningful requirement/edge case or duplicates existing evidence without adding confidence.

## Ready to continue when

You can turn one important requirement or past defect into a deterministic automated test and explain what future regression it protects against.

## References

- Microsoft Learn, .NET testing — https://learn.microsoft.com/en-us/dotnet/core/testing/
- [Verification and Evidence](verification-evidence.md)
- [Deterministic Debugging](deterministic-debugging.md)
