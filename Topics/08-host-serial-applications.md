# RCET 3371 — Host Serial Applications in C# and Python

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 8](../LearningPath/08-Host-Serial-Applications.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. Host serial architecture
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

A host application must survive more than the happy path. Ports disappear, devices reconnect under different names, reads are partial, callbacks arrive at awkward times, and presentation code can accidentally consume data before the parser is ready.

A reusable host design isolates those concerns.

## 2. Outcomes

You should be able to:

- enumerate available serial ports;
- identify a device using a deliberate handshake or known metadata;
- manage open/close/reconnect lifecycle;
- use C# System.IO.Ports and Python pySerial at an appropriate level;
- separate transport from parser, model, control, and UI;
- reason about blocking versus callback/asynchronous receive;
- buffer bytes until the parser can consume them;
- expose connection/error state;
- test without hardware.

## 3. Prerequisites

Section 7 protocol design plus Sections 4-6 architecture/state concepts.

## 4. Core model

Use a boundary:

    application
       |
    device API
       |
    parser/protocol
       |
    transport
       |
    serial library / OS

The application should ask for domain operations such as:

    get_temperature()
    set_output(value)

rather than scatter low-level port reads/writes everywhere.

## 5. Host serial architecture

### Discovery

Port names differ by operating system.

Windows commonly exposes COM-style names. Linux commonly exposes devices under /dev.

Do not assume "first port in the list" is your device.

Use one or more of:

- VID/PID/serial metadata when reliable;
- configured port;
- protocol identity command;
- user selection.

### Lifecycle

Represent states explicitly, for example:

    DISCONNECTED
    CONNECTING
    IDENTIFYING
    READY
    FAULTED

Opening a port is not the same as proving the correct device is connected.

### Buffering

The transport receives bytes. The parser decides message boundaries.

Do not call ReadLine unless your protocol is actually line-oriented and line termination is part of the contract.

### Thread/callback boundaries

A serial receive callback may not execute on the UI thread. Do not let transport callbacks mutate UI controls and domain state arbitrarily.

Instead:

- collect data;
- parse;
- update model through one controlled path;
- notify/present appropriately.

### Fake transport

Define a transport contract and a fake implementation.

The fake can:

- return fixed byte chunks;
- simulate partial reads;
- inject malformed packets;
- simulate disconnect;
- record writes for assertions.

## 6. Worked examples

### Bridge example: replace a familiar file source with a byte source

RCET 2265 file code often had this shape:

```text
read line -> parse -> update program
```

A host serial application can be introduced with the same separation:

```text
read bytes -> buffer/frame -> parse -> update program
```

Keep the parser callable with fixed test bytes before connecting a real serial port.

A useful progression is:

1. feed the parser one hard-coded valid frame;
2. feed the same frame from a captured binary/text fixture;
3. prove malformed input is rejected;
4. only then replace the fixture with `SerialPort` or pySerial.

Changing the input source should not require rewriting the parser.


### Example 1: Python port listing

pySerial supplies serial.tools.list_ports for discovery. Returned metadata varies by platform, so code should tolerate missing fields.

### Example 2: device identity

Candidate port opens successfully. Host sends ID request. Expected device responds with:

    ID,QYAT,1

Only then does the application mark the device READY.

### Example 3: separation

Weak:

    DataReceived -> read -> parse -> set label text -> write CSV

Stronger:

    SerialTransport receives bytes
    PacketParser yields messages
    DeviceModel updates state
    Logger records model event
    UI observes model

## 7. Apply, verify, and troubleshoot

Before hardware:

- parser tests with fixed chunks;
- fake transport;
- expected writes;
- disconnect/reconnect tests.

With hardware:

1. verify OS sees device;
2. verify correct port/settings;
3. capture raw bytes;
4. verify protocol;
5. verify parser/model;
6. only then troubleshoot UI/logging.

Expose connection state to the user. Silent automatic retries can hide failures.

## 8. Practice

1. Why is port enumeration not device identification?
2. Why can a serial callback be dangerous to UI code?
3. What test proves the parser is independent of read chunk size?
4. When is ReadLine appropriate?
5. Design a fake transport behavior that tests reconnect handling.
6. Why should logging not live directly inside every receive callback?

## 9. Answer key

1. A listed port is only an OS resource; multiple devices may exist and names can change.
2. Callback threading/timing may differ from the UI thread and can introduce unsafe coupling/races.
3. Feed the same packet bytes in different chunk boundaries and require identical parsed messages.
4. When the protocol is explicitly text line-delimited with known termination and encoding.
5. First read succeeds, next operation throws/disconnects, reopening/identity later succeeds. The exact fake API depends on the transport contract.
6. It mixes transport, persistence, and domain behavior, making each harder to test and recover independently.

## 10. Explain without notes

Explain:

- discovery versus identification;
- connection lifecycle;
- transport versus parser;
- partial reads;
- callback/thread boundary;
- fake transport;
- why hardware comes after deterministic tests.

## 11. References

- Microsoft Learn, SerialPort — https://learn.microsoft.com/en-us/dotnet/api/system.io.ports.serialport
- pySerial API — https://pyserial.readthedocs.io/en/latest/pyserial_api.html
- pySerial port tools — https://pyserial.readthedocs.io/en/stable/tools.html
