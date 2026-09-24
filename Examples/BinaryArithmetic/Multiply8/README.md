# 8-Bit Shift-and-Add Multiplication

This example implements the same C:A:Q multiplication algorithm used in the four-bit hand example in [Shift-and-Add Binary Multiplication](../../../Topics/binary-multiplication-shift-add.md).

## Contract

Unsigned 8-bit inputs:

- `multiplicand`: multiplicand M
- `product_low`: multiplier Q

Output after `Multiply8` returns:

- `product_high:product_low`: unsigned 16-bit product

`multiplicand` remains unchanged.

## Default test

The supplied program loads:

~~~text
13 × 11
~~~

Expected result:

~~~text
product_high = 0x00
product_low  = 0x8F
~~~

## Simulator verification

Watch:

- `multiplicand`
- `product_high`
- `product_low`
- `loop_count`
- `STATUS`

Step one cycle at a time and compare the conditional add plus the two `rrf` instructions with the hand algorithm.

Additional useful vectors:

| M | Q | Expected |
| ---: | ---: | ---: |
| 3 | 5 | 0x000F |
| 13 | 11 | 0x008F |
| 128 | 2 | 0x0100 |
| 255 | 255 | 0xFE01 |

## MPLAB X / pic-as

Target: PIC16F883.

Linker options:

~~~text
-Wl,-presetVect=0000h,-pcode=0008h
~~~

See [PIC-as Toolchain Setup](../../../Guides/Toolchains/pic-as.md).
