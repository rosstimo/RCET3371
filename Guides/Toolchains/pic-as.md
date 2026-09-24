# PIC Assembly / pic-as Toolchain Guide

[Guides index](../README.md)

## Purpose

RCET 3371 uses PIC assembly to expose processor-near behavior and to compare implementations, not to turn the course into a second dedicated assembly course.

## Course baseline

- PIC16F883
- MPLAB X 6.35
- XC8 PIC Assembler / pic-as from the installed XC8 toolchain

The command-line driver is named pic-as.

## What students should recognize

- source file;
- selected processor/device;
- configuration;
- program sections (PSECTs);
- labels;
- instructions;
- symbols;
- assembly step;
- link step;
- map/listing/disassembly output.

## Build model

Conceptually:

    .S source
       ↓ pic-as
    object/intermediate
       ↓ linker
    device image / debug information

MPLAB X invokes the assembler/toolchain for normal course work. The command-line model exists so you understand what the IDE is orchestrating.

## C/assembly comparison

Use small functions with a stable contract.

Example contract:

- input: one 8-bit state value;
- output: upper nibble as 0..15;
- no persistent state;
- unrelated state bits unchanged by caller.

Compare:

- C mask/shift;
- generated assembly;
- hand-written bounded assembly.

Do not require line-for-line equivalence.

## Map/listing inspection

Use generated outputs to answer bounded questions:

- where did this symbol land?
- which instructions implement the mask?
- what call/return sequence appears?
- how large is the function?

## References

- XC8 PIC Assembler documentation: https://onlinedocs.microchip.com/oxy/GUID-4DC87671-9D8E-428A-ADFE-98D694F9F089/
- MPLAB XC8: https://www.microchip.com/en-us/tools-resources/develop/mplab-x-compilers/xc8
- PIC16F883 product/data sheet page: https://www.microchip.com/en-us/product/PIC16F883
