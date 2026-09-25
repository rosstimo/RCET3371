# Status Decoder Example

This example belongs with Section 3. It starts with one byte and shows how to extract or replace packed fields without changing unrelated bits.

## Before running it

Given:

```text
status = 0110 1100
```

The format is:

```text
bits 7:5  MODE
bit  4    FAULT
bits 3:0  SEQUENCE
```

Trace the fields by hand first.

### MODE

Mask/shift:

```text
0110 1100
1110 0000   mask
---------
0110 0000
>> 5
---------
0000 0011 = 3
```

### FAULT

```text
0110 1100
0001 0000
---------
0000 0000 = no fault
```

### SEQUENCE

```text
0110 1100
0000 1111
---------
0000 1100 = 12
```

Expected first line:

```text
mode=3 fault=0 sequence=12
```

## Run the C# version

```bash
dotnet run --project StatusDecoder.csproj
```

Relevant operations:

```csharp
static int GetMode(byte status) => (status >> 5) & 0x07;
static bool GetFault(byte status) => (status & 0x10) != 0;
static int GetSequence(byte status) => status & 0x0F;
```

Read each mask against the bit layout above. Do not memorize the hex constants without knowing which bit positions they select.

## Run the Python version

```bash
python status_decoder.py
```

The Python operations are intentionally almost identical.

That is the point of the comparison: the packed-byte **behavior** stays the same even though the language/runtime differs.

## Replacing one field

The example also changes SEQUENCE while preserving MODE and FAULT.

Starting status:

```text
0110 1100
```

New sequence:

```text
0010
```

Preserve the upper nibble and replace only the lower nibble:

```text
status & 1111 0000  -> 0110 0000
new sequence        -> 0000 0010
OR                   -> 0110 0010
```

Expected updated value:

```text
0x62
```

## Try these modifications

1. Change the starting byte to `1011 0101`. Predict MODE, FAULT, and SEQUENCE before running.
2. Change sequence to 15. Verify upper bits remain unchanged.
3. Try sequence 16. Explain why the high-level function rejects it instead of silently allowing neighboring bits to change.
4. Write `SetMode` using the same grab/clear/put-back reasoning before looking for another implementation.

## What this example demonstrates

- one value can contain several packed fields;
- masks select only the intended bits;
- shifts move a field to a convenient position;
- replacing a field requires preserving unrelated bits;
- the same contract can be implemented in C# and Python.
