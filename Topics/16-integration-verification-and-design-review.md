# RCET 3371 — Integration, Verification, and Design Review

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 16](../LearningPath/16-Integration-and-Verification.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. Verification and handoff
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

A system is not finished when its author can make it work. It is finished enough for engineering use when another person can reproduce, inspect, operate, test, and maintain it from the supplied evidence and documentation.

This section turns the course's recurring habits into a final design/verification discipline.

## 2. Outcomes

You should be able to:

- create reproducible build/run/deploy instructions;
- distinguish requirement, test, evidence, and conclusion;
- verify normal, boundary, and failure behavior;
- use Git history to explain design evolution;
- document interfaces and assumptions;
- identify remaining risk honestly;
- conduct a design review that connects problem, architecture, implementation, and evidence;
- hand work to another engineer.

## 3. Prerequisites

All prior sections.

## 4. Core model

Use a verification chain:

    requirement
       ↓
    observable criterion
       ↓
    test/procedure
       ↓
    evidence
       ↓
    conclusion

Example:

**Requirement**  
Parser rejects payload lengths greater than 32.

**Test**  
Feed length fields 32 and 33.

**Evidence**  
32 accepted; 33 returns LengthOutOfRange.

**Conclusion**  
Boundary behavior matches requirement for tested cases.

Do not replace the chain with "tested, works."

## 5. Verification and handoff

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

### Git history as design evidence

Useful history can show:

- when architecture changed;
- why validation was added;
- which defect introduced a regression;
- how interfaces evolved.

A final repository should not erase that evidence with one giant "final project" commit.

### Design review structure

Explain:

1. engineering problem;
2. success criteria;
3. architecture/responsibilities;
4. language/tool choices;
5. protocol/data contracts;
6. state/timing behavior;
7. persistence/presentation;
8. tests/evidence;
9. failure/recovery;
10. remaining risks and future work.

## 6. Worked examples

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

### Example 3: remaining risk

Honest:

    Live reconnect verified on Windows with one USB adapter.
    Linux fake-device path verified.
    Physical Linux reconnect remains unverified.

This is better engineering documentation than claiming "cross-platform" from one successful build.

## 7. Apply, verify, and troubleshoot

Final audit:

- can a clean environment build?
- can core behavior run without hardware?
- are hardware claims separately verified?
- are all requirements mapped to evidence?
- are tests repeatable?
- do docs match code?
- are known limitations explicit?
- can another person locate configuration/data?
- does repository state identify the version tested?

When a handoff attempt fails, treat the missing instruction/asset/assumption as a defect and repair the documentation or package.

## 8. Practice

1. What is the difference between a requirement and evidence?
2. Why is a clean-clone test useful?
3. What makes "works on my machine" insufficient?
4. Give three failure cases a serial data logger should verify.
5. What information belongs in a remaining-risk statement?
6. Why should design review include Git/design evolution?

## 9. Answer key

1. A requirement states expected behavior; evidence records what happened under a defined test/procedure.
2. It exposes hidden local dependencies and incomplete setup documentation.
3. It proves only one environment and may depend on undocumented state.
4. Examples: missing device, disconnect, malformed packet, timeout, file permission error, malformed log/config.
5. What remains uncertain, what has been verified, environment/scope, consequence, and next verification step.
6. Engineering designs evolve; history helps explain rationale and verifies that changes responded to evidence rather than appearing magically in the final snapshot.

## 10. Explain without notes

Explain:

- requirement/test/evidence/conclusion;
- clean-clone verification;
- normal/boundary/failure tests;
- reproducible build;
- known risk;
- design review;
- why handoff quality is part of software quality.

## 11. References

- Git documentation — https://git-scm.com/docs
- Microsoft Learn, .NET testing — https://learn.microsoft.com/en-us/dotnet/core/testing/
- Python Software Foundation, unittest — https://docs.python.org/3/library/unittest.html
- GitHub Docs, Actions — https://docs.github.com/en/actions
