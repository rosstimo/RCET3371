# RCET 3371 — Serial Communication and Protocol Design

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 7](../LearningPath/07-Protocol-Design.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. Protocol design
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

A serial port moves bytes. A protocol gives those bytes meaning.

Many software/hardware integration failures blamed on "serial" are actually protocol failures: ambiguous framing, undocumented byte order, partial reads, missing validation, or assumptions about message boundaries.

## 2. Outcomes

You should be able to:

- distinguish physical/transport behavior from message semantics;
- define message framing;
- define command IDs, arguments, payloads, responses, and errors;
- choose delimiter, length, fixed-size, or other framing intentionally;
- define byte order;
- write parser state explicitly;
- detect malformed, short, long, and timed-out messages;
- decide whether checksum/integrity detection is needed;
- document a protocol so two independent implementations can interoperate.

## 3. Prerequisites

Sections 3-6: bit/byte representation, contracts, parsing, modules, and software state.

## 4. Core model

Use layers:

    transport bytes
        ↓
    framing
        ↓
    message fields
        ↓
    validation
        ↓
    command/domain meaning

Keep these questions separate.

**Transport:** how do bytes arrive?  
**Framing:** where does one message begin/end?  
**Encoding:** what do fields mean?  
**Behavior:** what response/state change follows?

## 5. Protocol design

### Framing choices

**Fixed length**
- simple;
- efficient;
- poor fit if messages vary widely.

**Delimiter terminated**
- readable for text;
- delimiter must be escaped/disallowed inside payload.

**Length prefixed**
- supports arbitrary payload;
- corrupted length can desynchronize parser.

**Start marker + length + integrity**
- robust but more complexity.

The best protocol is the simplest one that satisfies the system's actual reliability and extensibility needs.

### Explicit command contract

Example:

    Request:
      0: 0xA5 start
      1: command
      2: payload length N
      3..: payload
      last: checksum

Document:

- allowed commands;
- field width;
- byte order;
- legal lengths;
- expected response;
- errors;
- timeout expectation.

### Parser state

A streaming parser may have states:

    WAIT_START
    READ_COMMAND
    READ_LENGTH
    READ_PAYLOAD
    READ_CHECK
    COMPLETE / ERROR

Do not assume one read call returns exactly one message.

### Timeouts

A timeout is part of behavior, not merely a library setting.

Define:

- when timing starts;
- what activity resets it;
- what state follows expiry;
- whether partial data is discarded or preserved.

### Integrity

Checksums/CRCs can detect some corruption; they do not prove authenticity or semantic correctness. Use only what the system needs.

## 6. Worked examples

### Bridge example: from one complete string to a framed message

In RCET 2265 you might have processed a complete string such as:

```text
TEMP,21.5
```

A serial link may deliver data in pieces and may contain unrelated bytes. Start by wrapping a familiar payload:

```text
START | LENGTH | TYPE | PAYLOAD | CHECK
 A5       4       10    01 02 03 04   ??
```

First parse one complete fixed frame. Then split that exact frame across two reads. Only after both cases work should you add malformed lengths, bad check values, resynchronization, and a streaming state machine.

This keeps the new problem focused: the payload idea is familiar; framing is the new layer.


### Example 1: partial read

Expected message:

    A5 10 02 34 12 C3

Read calls return:

    [A5 10]
    [02]
    [34 12 C3]

A correct streaming parser accumulates state across reads. Code that assumes each read equals one full packet fails.

### Example 2: byte order

Payload bytes:

    34 12

If protocol says little-endian 16-bit:

    value = 0x1234

If documentation never states byte order, two correct programs can disagree.

### Example 3: malformed length

A length field says 20 but the protocol maximum is 8.

Reject immediately. Do not allocate blindly or wait forever.

## 7. Apply, verify, and troubleshoot

Protocol design checklist:

- unique message boundary;
- legal command values;
- field widths;
- byte order;
- length limits;
- validation;
- timeout;
- error response;
- examples;
- test vectors.

Troubleshooting checklist:

1. capture raw bytes;
2. verify transport settings separately;
3. mark frame boundaries;
4. compare each field to spec;
5. reproduce with captured data before live hardware;
6. add malformed variants.

## 8. Practice

1. Why can one serial read contain half a message?
2. What ambiguity does a delimiter protocol need to address?
3. Why is byte order part of the protocol?
4. A parser reads a length of 255 when max payload is 32. What should it do?
5. Define three timeout semantics that must be documented.
6. Why are captured byte streams valuable tests?

## 9. Answer key

1. The OS/driver/transport exposes available bytes; application message boundaries are a separate layer.
2. What happens if the delimiter occurs inside data: escaping, encoding, or prohibition.
3. Multi-byte values have more than one byte sequence representation; both endpoints must agree.
4. Reject/resynchronize according to the protocol rather than trusting an impossible length.
5. Start condition, reset condition, expiry action/state are the most important.
6. They make a real interaction repeatable without hardware and preserve exact problematic data.

## 10. Explain without notes

Explain:

- transport versus protocol;
- framing;
- command contract;
- streaming parser state;
- partial reads;
- byte order;
- timeout semantics;
- checksum limits.

## 11. References

- Microsoft Learn, System.IO.Ports — https://learn.microsoft.com/en-us/dotnet/api/system.io.ports
- pySerial documentation — https://pyserial.readthedocs.io/en/latest/
- Microchip documentation should be used for the exact UART peripheral behavior of the target PIC; RCET 3373 owns the hardware-level UART theory.
