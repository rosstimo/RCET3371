# Protocol Parser Example

This example supports Section 7. It grows from a simple complete message into a streaming parser that can receive bytes in uneven pieces.

## Start with the simpler model

A beginner parser might receive one complete record:

```text
TEMP,21.5
```

and parse it in one operation.

A serial stream is different because one call may contain:

- bytes before a real frame;
- only part of a frame;
- one complete frame;
- several frames;
- the end of one frame plus the beginning of another.

The parser therefore has to remember partial progress.

## What the example demonstrates

- pre-frame noise handling;
- partial state across calls;
- length validation;
- check-value validation;
- multiple frames across uneven chunks.

## Run

```bash
dotnet run --project ProtocolParser.csproj
```

Expected output:

```text
type=0x01 len=0
type=0x10 len=0
```

## Trace before modifying

Use one valid frame and answer these questions byte by byte:

1. Which byte tells the parser that a frame may be starting?
2. At what point does the parser know the expected frame length?
3. Which fields must be stored while later bytes arrive?
4. At what exact point can the parser either publish a valid message or reject the frame?
5. What state should it return to after rejection?

Then split that **same valid frame** into two input chunks and verify the result is unchanged.

## Safe modification sequence

Do these one at a time:

1. add one noise byte before the valid frame;
2. split the frame at a different byte boundary;
3. feed two valid frames in one chunk;
4. corrupt the length;
5. corrupt the check value;
6. feed a bad frame followed by a valid one and verify resynchronization.

Keep each exact failing byte sequence when something goes wrong. A byte parser is much easier to debug from a reproducible fixture than from a live serial device.

## What not to add yet

Do not connect this example directly to:

- `SerialPort`;
- a GUI;
- logging;
- hardware control.

First prove the parser using fixed byte input. Later sections replace the source of the bytes without changing the parser contract.
