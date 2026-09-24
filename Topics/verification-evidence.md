# RCET 3371 — Verification and Evidence

*Self-learning guide*

[Topics index](README.md)

Engineering verification connects a requirement to an observable criterion, a repeatable procedure, captured evidence, and a conclusion. A successful demo is useful evidence only when the conditions and expected result are defined.









### Reproducible setup

Document:

- repository/commit/tag;
- required software versions or compatible range;
- build command;
- run command;
- configuration;
- required data/assets;
- optional hardware;
- expected first-run behavior.

### Verification matrix

Map outcomes/requirements to evidence.

| Requirement | Test | Expected | Evidence |
| --- | --- | --- | --- |
| reconnect | unplug/replug fake/live transport | returns to Ready after identity | log/test output |
| parser max length | feed N=33 | reject | unit test |
| replay | log then replay same file | same parsed records | comparison output |

### Failure handling

Verify:

- missing file;
- malformed config;
- device absent;
- disconnect;
- malformed packet;
- timeout;
- invalid state transition;
- empty dataset.

A robust system has defined failure behavior, not merely error-free happy-path output.

### Bridge example: turn "it works" into reproducible evidence

Start with a vague claim:

```text
The parser handles a valid telemetry frame.
```

Replace it with evidence:

```text
Input:       known valid frame A
Expected:    type=0x10, payload=01 02
Procedure:   run the named parser test
Result:      pass
Environment: .NET 10.x
```

The same pattern works for other claims:

- build: command + expected successful artifact;
- malformed input: exact input + expected rejection;
- timing: expected interval + independent measurement;
- hardware: connection/setup + measured result;
- UI: action + expected visible/state result.

Verification is the habit of making a claim repeatable by someone other than the original programmer.

### Example 1: clean-clone test

Another person:

1. clones repository;
2. follows README;
3. restores/builds;
4. runs tests;
5. runs fake-device demo;
6. produces expected output.

If they need an undocumented local file from your desktop, handoff failed.

### Example 2: measured timing

Requirement: action occurs 5.0 s ± 0.2 s after event.

Do not report only code constant 5000 ms.

Measure observed behavior over multiple runs and record results or automated fake-time boundary tests plus real scheduling measurement when appropriate.

## Practice

1. Turn "the parser works" into a requirement -> test -> evidence -> conclusion chain.
2. What belongs in reproducible build/run instructions?
3. Why should failure cases appear in the verification matrix?
4. What does a clean-clone test prove that running from the author's existing workspace does not?
5. When is a screenshot strong evidence, and when is it weak evidence?

## Answer reasoning

1. State an observable parser requirement, feed named vectors/procedure, record expected/actual output, then conclude only for the tested cases.
2. Revision, dependencies/versions, build/run commands, configuration/assets/hardware, and expected first-run behavior.
3. Robust requirements include defined behavior when inputs/resources fail, not only the happy path.
4. It exposes hidden local files, build output, environment assumptions, or undocumented setup.
5. Strong for genuinely visual state; weak for nonvisual behavior better proven by logs/tests/measurements.

## Ready to continue when

You can construct a verification chain, design a verification matrix including boundaries/failures, perform a reproducibility check, and choose evidence appropriate to the requirement.

## References

- [Verification evidence standard](../Standards/verification-evidence.md)
- [Testing guide](../Guides/Testing/README.md)
