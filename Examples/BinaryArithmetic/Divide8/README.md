# 8-Bit Register Division

This example implements the same X:A:Q division method used in the four-bit hand example in [Register-Based Binary Division](../../../Topics/binary-division-register-algorithm.md).

## Conceptual registers

- `X`: one's complement of the divisor
- `A`: upper dividend / partial remainder / final remainder
- `Q`: lower dividend / developing quotient / final quotient

Additional RAM variables are implementation scratch, not additional conceptual arithmetic registers.

## Contract

Inputs:

- `a_register:q_register`: unsigned 16-bit dividend
- `divisor`: unsigned 8-bit divisor

Successful outputs:

- `q_register`: 8-bit quotient
- `a_register`: 8-bit remainder
- `divide_status = 0`

Errors:

- `divide_status = 1`: divisor is zero
- `divide_status = 2`: quotient would not fit in eight bits

The quotient-fit condition is equivalent to requiring the initial `A < divisor`.

## Default test

The supplied program loads:

~~~text
A:Q = 0x000F = 15
divisor = 5
~~~

Expected result:

~~~text
q_register = 0x03
a_register = 0x00
divide_status = 0
~~~

This is the eight-bit-register version of the four-bit 15 / 5 teaching example.

## Simulator verification

Watch:

- `divisor`
- `x_register`
- `a_register`
- `q_register`
- `shifted_a`
- `shift_carry`
- `loop_count`
- `divide_status`
- `STATUS`

For each iteration verify:

1. `A:Q` shifts left and zero enters `Q0`;
2. `shift_carry` captures the bit shifted out of `A7`;
3. `A + X` produces the trial subtraction;
4. the trial succeeds on shift carry, add carry, or `A == 0xFF`;
5. failed trials restore shifted A;
6. successful trials increment both A and Q.

Useful vectors:

| Dividend A:Q | Divisor | Expected Q | Expected A |
| ---: | ---: | ---: | ---: |
| `0x000F` | 5 | 3 | 0 |
| `0x001D` | 8 | 3 | 5 |
| `0x00FF` | 13 | 19 | 8 |
| `0x0080` | 3 | 42 | 2 |
| `0x0100` | 3 | 85 | 1 |

Error vectors:

| A:Q | Divisor | Expected status |
| ---: | ---: | ---: |
| `0x0063` | 0 | 1 |
| `0x0500` | 5 | 2 |

## Mechanical algorithm verification

[verify_algorithms.py](../verify_algorithms.py) exhaustively checks:

- every 8-bit by 8-bit multiplication input pair;
- every 8-bit dividend with every nonzero 8-bit divisor;
- representative 16-bit A:Q dividends across every legal high-byte/divisor relationship.

This verifies the register algorithm independently of the classroom pic-as toolchain.

## MPLAB X / pic-as

Target: PIC16F883.

Linker options:

~~~text
-Wl,-presetVect=0000h,-pcode=0008h
~~~

A current MPLAB X/pic-as build and simulator pass remains the final toolchain-specific verification.

See [PIC-as Toolchain Setup](../../../Guides/Toolchains/pic-as.md).
