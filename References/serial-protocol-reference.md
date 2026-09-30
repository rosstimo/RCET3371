# Serial Protocol Quick Reference

[References index](README.md)

## Layers

    serial transport bytes
        ↓
    framing
        ↓
    message fields
        ↓
    validation
        ↓
    domain command/telemetry

## Protocol specification checklist

Document:

- baud/data/parity/stop settings when serial transport is part of the contract;
- frame start/end rule;
- command/message IDs;
- field widths;
- byte order;
- length limits;
- encoding;
- checksum/integrity rule if used;
- request/response behavior;
- timeout start/reset/expiry;
- malformed-message behavior;
- error messages/status;
- example valid frames;
- example invalid frames.

## Parser reminders

Never assume:

- one read = one frame;
- one frame = one read;
- bytes arrive immediately;
- the port name identifies the device;
- text line APIs fit a binary protocol.

## Test chunking

For the same frame, test:

    [entire frame]
    [1 byte][rest]
    [2 bytes][2 bytes][rest]
    [one byte at a time]
    [two frames in one read]

Parsed messages should be equivalent.

## Host state

A useful lifecycle:

    Disconnected
    Connecting
    Identifying
    Ready
    Faulted

## References

- System.IO.Ports: https://learn.microsoft.com/en-us/dotnet/api/system.io.ports
- pySerial: https://pyserial.readthedocs.io/en/latest/
