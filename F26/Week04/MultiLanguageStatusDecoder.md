# Assignment - Multi-Language Status Decoder

## Specification

Decode one 8-bit value using this format:

```text
bit 7      fault
bit 6      enabled
bits 5..3  mode (0-7)
bits 2..0  level (0-7)
```

## Required work

### 1. Design before code

Create a small responsibility/data-flow sketch showing:

- where the input comes from;
- where decoding occurs;
- what the decoder returns;
- where output/display occurs;
- where tests exercise the decoder.

### 2. C# implementation

Use more than one source file. Keep decoding behavior separate from the program entry point/output code.

### 3. Python implementation

Use at least one imported module file. Keep decoding behavior separate from the program entry point/output code.

### 4. Shared test vectors

Create one table with at least **eight** input values and expected results. Use the same table/specification to verify both implementations.

Include:

- `0x00`;
- `0xFF`;
- values that independently exercise `fault` and `enabled`;
- multiple `mode` and `level` values;
- at least two mixed values.

### 5. Comparison

Briefly explain:

- what stayed conceptually the same in C# and Python;
- at least three language-specific differences;
- why the same tests can be used for both implementations.

### 6. Git evidence

Your history should show meaningful stages rather than one final upload. At minimum preserve:

- design/start;
- first working language implementation;
- second language implementation;
- test/fix work.

## Mastery target

You should be able to explain the decoder's input/output interface without reading its implementation and change console formatting without changing the decoding logic.

## Do not add

- a GUI;
- serial communication;
- PIC hardware access.

Those would hide the software-structure goal of this assignment.