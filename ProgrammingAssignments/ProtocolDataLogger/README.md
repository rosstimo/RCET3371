# Protocol Data Logger

**Programming Assignments category value: 250 points out of 1000**

Sections: 7–9

## Objective

Build a robust protocol/parser/logger system that works completely against deterministic simulated and captured data before physical serial hardware is introduced.

The authoritative wire/logging contract is [protocol-specification.md](protocol-specification.md). Do not create a second incompatible protocol inside your implementation.

## Required repository structure

Organize responsibilities clearly. One acceptable shape is:

```text
README.md
csharp/
python/
tests/
evidence/
```

Your exact source layout may differ when the same boundaries remain easy to identify.

## Required architecture

The C# host must separate, at minimum:

```text
transport
  ↓
stream parser
  ↓
protocol/device API
  ↓
domain model
  ↓
logging/replay/presentation
```

The parser must not depend directly on `SerialPort`.

## C# host requirements

Implement:

- streaming protocol parser;
- deterministic parser tests over supplied captures;
- fake/captured transport;
- real `SerialPort` adapter;
- identity request/response handling;
- decoded telemetry model;
- visible connection/error state;
- CSV logger using the specification;
- 100-record file rotation;
- replay of produced logs;
- malformed-row reporting;
- summary statistics over replayed telemetry.

## Python diagnostic/replay tool

Implement a command-line Python tool that can:

- identify valid/invalid frames from the supplied captures;
- decode telemetry;
- replay the supplied/produced CSV log format;
- report record count, minimum/maximum temperature, arithmetic mean temperature, and status/fault observations;
- exit nonzero with a clear diagnostic for unusable input.

It must not simply invoke the C# executable.

## Supplied resources

Use the authoritative captures:

- [valid stream](../../Resources/captured-streams/valid-stream.hex)
- [chunking stream](../../Resources/captured-streams/chunking-stream.hex)
- [malformed stream](../../Resources/captured-streams/malformed-stream.hex)

The same logical frames must parse identically regardless of how input bytes are chunked.

## Required verification

At minimum verify:

- identity request/response;
- one complete frame in one chunk;
- one frame one byte at a time;
- multiple frames in one chunk;
- noise before START;
- invalid length;
- bad checksum;
- partial frame across chunks;
- successful recovery after malformed input;
- fake disconnect/reconnect behavior;
- 100/101-record log rotation boundary;
- log write/read round trip;
- malformed log row handling.

## Hardware rule

All core functional credit must be achievable without physical hardware.

The real serial adapter is still required, but hardware verification is recorded separately. If compatible hardware is not available in the scheduled course environment, the approved fake/captured integration path is the verification authority for core behavior.

## Milestones

Use [milestones.md](milestones.md) as progress/checkoff guidance. Milestones do not create separate grade categories.

## Evaluation

Use [rubric.md](rubric.md).

## Complete when

A clean clone can:

1. run all deterministic protocol tests;
2. process supplied captures;
3. exercise the C# device API through the fake transport;
4. write and rotate logs;
5. replay those logs;
6. run the Python diagnostic/replay tool;
7. build the real serial adapter;
8. reproduce the documented evidence without private instructor material.

## Submission

Submit the repository URL and any LMS-requested demonstration evidence.
