# 8-Bit Shift-and-Add Multiplication

This example implements the same X:A:Q multiplication method used in the four-bit hand example in [Shift-and-Add Binary Multiplication](../../../Topics/binary-multiplication-shift-add.md).

## Contract

Unsigned 8-bit inputs:

- `x_register`: multiplicand X
- `q_register`: multiplier Q

Output after `Multiply8` returns:

- `a_register:q_register`: unsigned 16-bit product

`x_register` remains unchanged.

## Default test

The supplied program loads the same values as the hand example:

~~~text
5 × 3
~~~

Expected result:

~~~text
a_register = 0x00
q_register = 0x0F
~~~

## Simulator verification

Watch:

- `x_register`
- `a_register`
- `q_register`
- `loop_count`
- `STATUS`

Step one cycle at a time and compare the conditional add plus the two `rrf` instructions with the hand algorithm.

Additional useful vectors:

| X | Q | Expected A:Q |
| ---: | ---: | ---: |
| 5 | 3 | 0x000F |
| 13 | 11 | 0x008F |
| 128 | 2 | 0x0100 |
| 255 | 255 | 0xFE01 |

[verify_algorithms.py](../verify_algorithms.py) exhaustively checks every 8-bit X/Q pair against ordinary multiplication.

## MPLAB X / pic-as

Target: PIC16F883.

Linker options:

~~~text
-Wl,-presetVect=0000h,-pcode=0008h
~~~

A current classroom pic-as build/simulator pass remains the final toolchain-specific verification.

See [PIC-as Toolchain Setup](../../../Guides/Toolchains/pic-as.md).
