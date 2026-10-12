# RCET 3371 — Packaging and Continuous Integration

*Optional self-learning guide*

[Topics index](README.md)

Packaging and CI are useful when software must reproduce outside the author's development machine. The engineering target is a deterministic build/test path with explicit dependencies and independently produced evidence.

## Core model

A reproducible automation path should state:

- source revision;
- required runtime/toolchain;
- dependency restore/install step;
- deterministic build command;
- deterministic test command;
- artifact/output location;
- failure result visible to the team.

CI should execute an already-understood local process. It should not become the only place anyone knows how to build the project.

## Worked example

A repository works on one laptop but fails after a clean clone.

First define the local contract:

```text
restore dependencies
build
run deterministic tests
```

Then make CI execute the same contract on a clean runner.

Useful evidence is the independent result:

```text
clean checkout -> restore -> build -> test -> success/failure
```

That catches hidden local dependencies that "works on my machine" can conceal.

## Apply and verify

For this optional Topic, use the same engineering frame:

1. state the problem;
2. preserve the known-good baseline;
3. add one bounded mechanism;
4. identify new failure modes;
5. define evidence before claiming improvement.

## Practice

1. What does CI prove that a successful local build does not?
2. Why should CI reuse the same documented build/test commands developers can run locally?
3. What hidden dependency can a clean runner expose?
4. When is publishing an artifact useful?

## Answer reasoning

1. A clean automated environment can reproduce the documented process.
2. It prevents two different build/test truths and keeps failures reproducible locally.
3. Uncommitted files, global packages, implicit environment variables, generated files, undocumented tool versions, etc.
4. When another user/system needs a versioned deliverable rather than source-only reproduction.

## Ready to continue when

You can define a clean build/test contract, automate it on a fresh runner, interpret CI as reproducibility evidence, and identify dependencies that should be made explicit.

## References

- GitHub Docs, Actions — https://docs.github.com/en/actions
- [Verification and Evidence](verification-evidence.md)
- [Git and Project Organization](git-project-organization.md)
