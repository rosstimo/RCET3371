# MPLAB X / XC8 Toolchain Guide

[Guides index](../README.md)

## Course baseline

Reference environment for the S27 candidate:

- PIC16F883
- MPLAB X IDE 6.35
- MPLAB XC8 compiler 4.00 or compatible supported patch
- PICkit 3 where physical programming/debugging is required
- simulator/fake path for software learning when hardware is unavailable

Microchip may recommend newer tooling by the time the course runs. Do not silently change the toolchain mid-course: verify the course examples and record the adopted version.

## Install

Use Microchip's official MPLAB X and XC8 downloads.

After installation verify:

- MPLAB X launches;
- XC8 appears as an installed toolchain;
- PIC16F883 can be selected;
- a minimal project builds;
- simulator is available;
- physical PICkit/target detection is verified separately.

## Minimal C project expectations

A course project should make these visible:

- selected device;
- configuration bits;
- oscillator assumptions;
- headers;
- source files;
- build configuration;
- warnings/errors.

Treat warnings as defects unless a specific warning is documented and justified.

## Header/source module

Header:

    #ifndef STATUS_H
    #define STATUS_H

    #include <stdint.h>

    uint8_t status_get_count(uint8_t state);

    #endif

Source:

    #include "status.h"

    uint8_t status_get_count(uint8_t state)
    {
        return (state >> 4) & 0x0Fu;
    }

## Generated assembly

For bounded comparisons, generate/list assembly output and inspect how:

- function calls;
- branches;
- masks/shifts;
- local values

map onto target operations.

Do not infer execution cost from C source alone.

## Hardware verification

Separate:

**software verification**
- compile;
- simulator;
- pure-function tests;
- known vectors.

**physical verification**
- programmer connection;
- target voltage/device;
- pins/peripheral;
- measured behavior.

Document which has actually been performed.

## References

- MPLAB X IDE: https://www.microchip.com/en-us/tools-resources/develop/mplab-x-ide
- MPLAB XC8: https://www.microchip.com/en-us/tools-resources/develop/mplab-x-compilers/xc8
- PIC16F883: https://www.microchip.com/en-us/product/PIC16F883
