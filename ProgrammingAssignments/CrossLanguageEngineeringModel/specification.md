# Cross-Language Engineering Model Specification

## 1. Status byte

A device status byte is defined as:

| Bits | Name | Meaning |
| --- | --- | --- |
| 7:5 | MODE | unsigned 0–7 |
| 4 | FAULT | 0=no fault, 1=fault |
| 3:0 | SEQUENCE | unsigned 0–15 |

Required operations:

- get mode;
- set mode while preserving all other fields;
- get fault;
- set/clear fault while preserving all other fields;
- get sequence;
- set sequence while preserving all other fields.

Inputs outside the legal field range must be rejected by high-level APIs rather than silently corrupt neighboring fields.

## 2. Measurement word

A 16-bit unsigned raw measurement is transmitted **big-endian** as:

```text
HIGH BYTE, LOW BYTE
```

Reconstruct:

```text
raw = (high << 8) | low
```

The engineering voltage is:

```text
volts = raw * 5.0 / 65535.0
```

Keep full calculation precision internally.

Display voltage in engineering output with three digits after the decimal point.

## 3. Encoded sample

One sample contains:

```text
status byte
measurement high byte
measurement low byte
```

Required API behavior:

```text
decode_sample(3 bytes) -> structured sample
encode_sample(sample) -> exactly 3 bytes
```

Decode must reject input whose length is not exactly three bytes.

## 4. Summary statistics

Given a nonempty sequence of decoded raw measurements, calculate:

- count;
- minimum raw value;
- maximum raw value;
- arithmetic mean raw value as floating-point;
- arithmetic mean voltage.

Empty input must produce an explicit failure/status according to the language's normal conventions rather than returning a misleading zero.

## 5. Authoritative vectors

Use the course file:

[status-measurement-vectors.csv](../../Resources/protocol-vectors/status-measurement-vectors.csv)

Do not edit expected values to match your program.

## 6. Formatting

Example human-readable record:

```text
mode=3 fault=0 seq=12 raw=32768 voltage=2.500 V
```

Whitespace may vary unless a test explicitly states exact output.

## 7. Required separation

At minimum separate:

- status packing/unpacking;
- sample encode/decode;
- engineering conversion/statistics;
- presentation/CLI.

The embedded C version may group files differently when target constraints justify it, but the public function contract must remain clear.
