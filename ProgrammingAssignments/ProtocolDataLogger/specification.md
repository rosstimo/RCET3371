# Protocol Data Logger Specification

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
| 0x81 | ID_RESPONSE | ASCII `RCET3371` |
| 0x10 | SAMPLE_REQUEST | empty |
| 0x90 | SAMPLE_RESPONSE | Telemetry Record v1 |
| 0xE0 | ERROR | one error-code byte |

### Telemetry Record v1

Four bytes:

| Byte | Field | Meaning |
| ---: | --- | --- |
| 0 | STATUS | packed status byte from the Cross-Language Engineering Model |
| 1 | SAMPLE_SEQUENCE | independent 8-bit sample counter, 0..255 |
| 2 | RAW_HIGH | measurement high byte |
| 3 | RAW_LOW | measurement low byte |

The packed STATUS byte keeps the earlier course contract:

- bits 7:5 = MODE;
- bit 4 = FAULT;
- bits 3:0 = STATUS_SEQUENCE, 0..15.

`SAMPLE_SEQUENCE` is a separate transport/sample counter used to identify sample order and detect dropped/repeated samples. It is **not** the same field as the 4-bit `STATUS_SEQUENCE`.

Measurement is unsigned 16-bit big-endian:

```text
raw = (RAW_HIGH << 8) | RAW_LOW
volts = raw * 5.0 / 65535.0
```

## Streaming parser contract

The parser must:

- accept arbitrary read chunking;
- ignore bytes before START;
- reject LEN > 16;
- retain partial-frame state;
- validate CHECK;
- produce zero, one, or multiple messages from one supplied chunk;
- recover after malformed input using a documented resynchronization rule;
- never depend on `SerialPort`.

Do not assume one serial read equals one protocol frame.

## Architecture

```text
transport
    ↓
stream parser
    ↓
protocol/device API
    ↓
domain model
    ↓
logger/replay/presentation
```

### C# primary host

Required:

- streaming parser;
- fake transport;
- serial transport adapter;
- identity handshake;
- sample request/response;
- explicit connection/error state;
- CSV logger;
- replay path.

### Python diagnostic/replay tool

Required:

- parse supplied captured protocol bytes or a produced log;
- report valid/invalid frames;
- decode telemetry records;
- report count/min/max/average raw and voltage.

## Logging contract

CSV header:

```text
timestamp_utc,sample_sequence,status_hex,mode,fault,status_sequence,raw,volts,source
```

Rules:

- timestamp uses explicit UTC ISO 8601-compatible form;
- one decoded valid sample per row;
- `sample_sequence` preserves the independent 8-bit sample counter;
- `status_hex` preserves the complete packed status byte;
- `mode`, `fault`, and `status_sequence` are decoded convenience fields and must agree with `status_hex`;
- preserve enough numeric precision for replay;
- `source` identifies fake/captured/live/replay where applicable.

Replay must reconstruct the same domain record and feed it through the same domain/statistics logic used for live samples.

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
- log write/read round trip preserving both sequence fields;
- malformed log row;
- inconsistent decoded status columns;
- empty log;
- boundary raw values 0 and 65535.

## Failure behavior

Failures must be visible to the caller/user. Do not swallow parser, file, or transport failures and continue as if data were valid.
