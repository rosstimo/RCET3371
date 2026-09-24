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

Four bytes: status byte, sequence byte, measurement high byte, measurement low byte.

Measurement is unsigned 16-bit big-endian:

```text
volts = raw * 5.0 / 65535.0
```

Status-bit interpretation uses the course status model from the Cross-Language Engineering Model.

## Streaming parser contract

The parser must accept arbitrary read chunking, ignore bytes before START, reject LEN > 16, retain partial-frame state, validate CHECK, produce zero/one/multiple messages from one supplied chunk, recover after malformed input using a documented resynchronization rule, and never depend on `SerialPort`.

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

Required: streaming parser, fake transport, serial transport adapter, identity handshake, sample request/response, explicit connection/error state, CSV logger, and replay path.

### Python diagnostic/replay tool

Required: parse supplied captured protocol bytes or produced log, report valid/invalid frames, decode telemetry records, and report count/min/max/average raw and voltage.

## Logging contract

```text
timestamp_utc,sequence,fault,mode,raw,volts,source
```

Use explicit UTC timestamps, one valid decoded sample per row, enough precision for replay, and a source label. Replay must feed records through the same domain/statistics logic used for live samples.

## Required tests

Test one frame/one chunk, one byte at a time, two frames/one chunk, noise before START, invalid LEN, invalid CHECK, partial frame, valid after malformed, fake disconnect/reconnect, identity success/failure, log round trip, malformed row, empty log, and raw boundaries 0/65535.

Failures must be visible. Do not swallow parser, file, or transport failures and continue as if data were valid.
