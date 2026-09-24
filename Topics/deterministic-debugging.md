# RCET 3371 — Deterministic Debugging

*Self-learning guide*

[Topics index](README.md)

Debugging starts by making failure repeatable. Preserve the exact failing input, predict the state you should see, and use the debugger to find the first point where actual state diverges.





## Practice

Use one fixture across environments:

```text
22.4,FAULT,EXTRA
```

Expected parser contract: exactly two fields, so the record must be rejected before numeric/status processing continues.

- **Visual Studio / C#:** break immediately after `Split`; inspect `fields.Length`.
- **VS Code / Python:** break immediately after `split(",")`; inspect `len(fields)`.
- **MPLAB X / XC8 C:** feed an equivalent fixed byte buffer and explicit length; stop after delimiter counting/field extraction and inspect the counters/buffer indexes.
- **MPLAB X / pic-as:** use a fixed RAM/input pattern and step the delimiter/count logic while watching WREG, file registers, and STATUS only where documented flag behavior matters.

The fixture, expected result, and comparison point stay constant. The observation tool changes.

## Answer reasoning

1. What should you preserve before changing code when a parser fails on one row?
2. Why is a random new input a poor first debugging step?
3. What stays identical when reproducing one bug in Visual Studio, VS Code, and MPLAB X?
4. What does a debugger add that a deterministic fixture does not?
5. Why should you compare expected versus actual state at the earliest useful boundary?

## Ready to continue when

1. Exact failing input, expected behavior, actual behavior/error, and useful state at the failure boundary.
2. It may not reproduce the same defect, so cause/effect becomes ambiguous.
3. The fixture, expected behavior, and observation point.
4. It lets you inspect execution/state at the point where behavior diverges.
5. The first divergence usually narrows the cause more effectively than inspecting downstream symptoms.

## References

You can turn a one-off failure into a reproducible case, state the expected intermediate result, choose a useful breakpoint/watch point, and use the appropriate debugger without changing the underlying test.
