# RCET 3371 Protocol Specification

This protocol is the authoritative contract for the Protocol Data Logger assignment.

## Frame format

```text
+--------+------+--------+-------------------+----------+
| START  | TYPE | LENGTH | PAYLOAD (N bytes) | CHECKSUM |
+--------+------+--------+-------------------+----------+
   1        1       1          N bytes            1
```

- START = `0xA5`
- TYPE = one byte
- LENGTH = unsigned payload length, 0–32
- PAYLOAD = exactly LENGTH bytes
- CHECKSUM = XOR of TYPE, LENGTH, and every payload byte
- START is **not** included in the checksum
- all multi-byte numeric fields are **big-endian** unless stated otherwise

## Parser behavior

The parser consumes a byte stream and must not assume read-call boundaries match frame boundaries.

Required conceptual states:

```text
WAIT_START
READ_TYPE
READ_LENGTH
READ_PAYLOAD
READ_CHECKSUM
```

### Invalid length

If LENGTH > 32:

- reject the candidate frame;
- report/increment a framing error;
- return to searching for START.

### Bad checksum

Reject the candidate frame, report checksum error, and resume searching for START.

### Noise

Bytes before `0xA5` are ignored while waiting for a start byte.

### Timeout

Transport/device layers own timeout policy. A partially accumulated frame may be reset after the configured frame timeout. Document when timing starts, what resets it, and what happens to partial data.

## Message types

### 0x01 — Identity Request

Direction: host → device

Payload length: 0

```text
A5 01 00 01
```

### 0x81 — Identity Response

Direction: device → host

Payload is ASCII:

```text
RCET3371
```

Required frame:

```text
A5 81 08 52 43 45 54 33 33 37 31 8F
```

The host marks a device compatible only after this response validates.

### 0x10 — Telemetry

Direction: device → host

Payload length: 4

```text
byte 0: sequence number 0–255, wraps
byte 1: signed temperature high byte
byte 2: signed temperature low byte
byte 3: status byte from Cross-Language Engineering Model
```

Temperature is a signed 16-bit two's-complement integer in **hundredths of a degree Celsius**.

Examples:

```text
09 29 = 2345 = 23.45 °C
FF 9C = -100 = -1.00 °C
```

## Logging contract

Each accepted telemetry frame produces one CSV row.

Header:

```text
timestamp_utc,sequence,temperature_c,status_hex,raw_frame_hex
```

Requirements:

- timestamp in ISO 8601 UTC with `Z`;
- sequence decimal;
- temperature with at least two decimal places;
- status as two uppercase hexadecimal digits;
- raw frame as uppercase hexadecimal bytes separated by spaces.

Example:

```text
2026-09-24T14:15:00.000Z,1,23.45,00,A5 10 04 01 09 29 00 35
```

## Rotation contract

Rotate after **100 telemetry records per file**.

Use deterministic names:

```text
telemetry-0001.csv
telemetry-0002.csv
...
```

The first file contains records 1–100; the next begins at record 101.

## Replay contract

Replay must:

- validate the header;
- parse all valid rows;
- report malformed rows rather than silently accepting them;
- calculate required summary statistics;
- preserve sequence/status/temperature meaning.

## Supplied captures

- [valid-stream.hex](../../Resources/captured-streams/valid-stream.hex)
- [chunking-stream.hex](../../Resources/captured-streams/chunking-stream.hex)
- [malformed-stream.hex](../../Resources/captured-streams/malformed-stream.hex)

Capture files contain whitespace-separated hexadecimal bytes and comments beginning with `#`.
