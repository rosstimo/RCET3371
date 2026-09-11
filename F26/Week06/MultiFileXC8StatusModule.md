# Assignment - Multi-File XC8 Status Module

Create an MPLAB X / XC8 project targeting the PIC16F883.

## Required source structure

```text
main.c
status.c
status.h
```

Use this byte format:

```text
bit 7      fault
bit 6      enabled
bits 5..3  mode
bits 2..0  level
```

## Requirements

- use fixed-width integer types where byte width is part of the interface;
- keep masks/field extraction out of `main.c`;
- put the public status type/prototype in `status.h`;
- put the implementation in `status.c`;
- use a header guard;
- build without unresolved or duplicate-symbol errors;
- inspect generated assembly/listing for `status_decode`;
- select at least three source operations and identify what the compiler generated;
- identify every object in your project that you believe requires `volatile` and explain why;
- preserve a one-file baseline, module extraction, and verification as meaningful Git commits.

## Assembly extension

Using current XC8 documentation, describe the steps required to replace one tiny helper with a separate assembly routine callable from C.

Do not guess parameter passing, symbol naming, or psect rules. Cite the compiler documentation used.

Implementation of the assembly replacement is optional unless assigned in class.