# Programming Assignment — Protocol Data Logger

[Programming Assignments index](../README.md)

**Points: 125**

## Objective

Build a robust host-side protocol/parser/logger system that works completely against deterministic simulated/captured data before any physical serial device is required.

Use the Telemetry Record v1 model from the Cross-Language Engineering Model assignment as the sample payload.

## RCET Telemetry Protocol v1

Frame:

| Offset | Field | Meaning |
| ---: | --- | --- |
| 0 | START | 0xA5 |
| 1 | TYPE | message type |
| 2 | LEN | payload length 0..16 |
| 3.. | PAYLOAD | LEN bytes |
| last | CHECK | XOR of TYPE, LEN, and every payload byte |

START is not included in CHECK.

### Message types

| Type | Name | Payload |
| ---: | --- | --- |
| 0x01 | ID_REQUEST | empty |
| 0x81 | ID_RESPONSE | ASCII RCET3371 |
| 0x10 | SAMPLE_REQUEST | empty |
| 0x90 | SAMPLE_RESPONSE | one Telemetry Record v1 (4 bytes) |
| 0xE0 | ERROR | one error code byte |

### Parser requirements

The streaming parser must:

- accept any read chunking;
- ignore bytes before START;
- reject LEN > 16;
- wait for partial frames without declaring success;
- validate CHECK;
- produce zero, one, or multiple messages from a supplied chunk;
- recover and continue after malformed input according to your documented resynchronization rule.

Do not assume one serial read equals one frame.

## Host architecture

Required boundaries:

    transport
        ↓
    stream parser
        ↓
    protocol/device API
        ↓
    domain model
        ↓
    logger/replay/presentation

The parser must not require SerialPort.

## Required host implementations

### C# primary host

Required:

- parser;
- fake transport;
- serial transport adapter;
- device identity handshake;
- sample request/response;
- visible connection/error state;
- logger;
- replay command/mode.

### Python diagnostic/replay tool

Required:

- parse captured protocol stream or log;
- report valid/invalid frames;
- decode Telemetry Record samples;
- summarize sample count/min/max/average voltage.

The Python tool may be command-line based.

## Logging contract

CSV header:

    timestamp_utc,sequence,fault,remote,enabled,data_ready,raw,volts,source

Requirements:

- UTC timestamp in explicit ISO 8601-compatible representation;
- one decoded valid sample per row;
- enough numeric precision for replay;
- source identifies fake/captured/live/replay as appropriate.

## Replay

Replay must load a produced log and run records through the same domain/statistics logic used for live samples.

It must not require a connected device.

## Supplied resources

Use:

- [protocol vectors](../../Resources/protocol-vectors/rcet-telemetry-protocol-v1.txt)
- [captured stream](../../Resources/captured-streams/telemetry-protocol-capture.txt)
- [sample telemetry log](../../Resources/datasets/sample-telemetry.csv)

## Required tests

At minimum:

- one frame in one chunk;
- one frame one byte at a time;
- two frames in one chunk;
- noise before START;
- invalid LEN;
- invalid CHECK;
- partial frame across chunks;
- valid frame after malformed frame;
- fake disconnect/reconnect;
- identity success/failure;
- log write/read round trip;
- malformed log row.

## Milestones

1. protocol specification + parser tests;
2. fake transport + device API;
3. logging/replay without hardware;
4. C# serial adapter + live integration when available;
5. Python diagnostic/replay tool;
6. final failure/recovery evidence.

## Complete when

- all deterministic tests pass without hardware;
- C# host can identify and request samples through the fake transport;
- parser behavior is independent of chunk size;
- logger and replay agree on decoded records;
- Python tool correctly processes supplied capture/log;
- real serial adapter exists and can be verified when hardware is available;
- failures are visible and documented.

## Evaluation

| Area | Points |
| --- | ---: |
| protocol/parser correctness | 25 |
| deterministic parser tests | 15 |
| architecture and fake transport | 15 |
| C# device/host behavior | 20 |
| logging + round-trip + replay | 15 |
| Python diagnostic/replay tool | 10 |
| disconnect/error/recovery behavior | 10 |
| live serial adapter/evidence or approved simulation evidence | 10 |
| documentation/repository quality | 5 |
| **Total** | **125** |

If physical hardware is unavailable, the instructor may award the 10 live-integration points from the supplied approved simulator/fake integration gate without changing the core assignment.

## Submission

Submit the repository URL and any LMS-requested demonstration evidence.
