# Integration and Verification Portfolio

**Programming Assignments category value: 200 points out of 1000**

Sections: 12–16

## Objective

Demonstrate that you can extend, integrate, verify, and hand off software engineering work beyond the initial implementation.

This is **not** the program capstone. It is a bounded evidence portfolio built around one earlier RCET 3371 system.

Choose either:

- Protocol Data Logger; or
- Event-Driven Device/Host Controller

as the primary system.

## Portfolio structure

```text
README.md
collaboration/
linux/
visualization/
advanced/
verification/
```

Evidence should link to real commits/tests/files rather than screenshots alone.

## Part 1 — Collaborative development

Complete one meaningful contribution through a shared Git workflow.

Required evidence:

- issue/task statement;
- focused branch;
- coherent commits;
- pull request;
- peer/instructor review evidence;
- integration result;
- any merge conflict and how it was reasoned about;
- branch cleanup or documented hosting behavior.

The contribution must change real behavior, tests, documentation, or integration quality.

## Part 2 — Linux portability

From Linux:

- clean clone the selected system;
- build/run applicable C# or Python components;
- run deterministic tests/input;
- document environment/tool versions;
- identify any portability defect;
- repair it if within scope;
- verify serial-device discovery behavior when the system contains a serial adapter.

Hardware is optional; fake/captured input must work.

## Part 3 — UI / visualization

Add or improve a presentation layer for real model data.

Required:

- presentation separated from transport/parser/control;
- retained model survives repaint;
- resize behavior is sensible;
- visible connection/mode/fault state when applicable;
- one rolling or historical visualization if the selected system produces time-series data.

Do not replace raw engineering data with pixel coordinates.

## Part 4 — Advanced extension

Complete **one** option from [advanced-options.md](advanced-options.md).

## Part 5 — Final verification and design review

Produce:

- clean-clone setup/run record;
- requirements-to-evidence matrix;
- normal/boundary/failure verification;
- known limitations/remaining risks;
- architecture diagram;
- selected Git history showing design evolution;
- concise design-review document/presentation following the course verification guide.

## Evaluation

See [rubric.md](rubric.md).
