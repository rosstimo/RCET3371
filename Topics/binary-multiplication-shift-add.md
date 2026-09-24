<a id="top"></a>
# RCET 3371 — Shift-and-Add Binary Multiplication

*Self-learning guide*

[Topics index](README.md)

## Contents

- [1. Why this matters](#why-this-matters)
- [2. What you should be able to do](#outcomes)
- [3. Prerequisites and related topics](#prerequisites)
- [4. Core model and vocabulary](#core-model)
- [5. How it works](#how-it-works)
- [6. Worked examples](#worked-examples)
- [7. Apply, verify, and troubleshoot](#apply-verify-troubleshoot)
- [8. Practice](#practice)
- [9. Answer key](#answer-key)
- [10. What you should be able to explain without notes](#without-notes)
- [11. References](#references)

<a id="why-this-matters"></a>
## 1. Why this matters

Writing a * operator hides how a small processor can multiply when the instruction set has no multiply instruction.

The register algorithm used here turns multiplication into a fixed sequence of:

- test one multiplier bit;
- conditionally add the multiplicand;
- shift a combined register;
- repeat once for each multiplier bit.

This is more useful than "add the multiplicand multiplier-times" because execution count depends on operand width, not the numerical value of the multiplier.

[Back to top](#top) · [Topics index](README.md)

<a id="outcomes"></a>
## 2. What you should be able to do

You should be able to:

- explain why an n-bit by n-bit unsigned product needs up to 2n bits;
- identify M/X as multiplicand, A as accumulator, Q as multiplier/product-low, and C as the carry extension;
- trace the four-bit C:A:Q algorithm one cycle at a time;
- predict the product before running code;
- explain why Q0 controls the conditional add;
- explain why C:A:Q is shifted as one combined register;
- step an 8-bit PIC16F883 implementation in MPLAB X;
- verify the 16-bit product using known test vectors.

[Back to top](#top) · [Topics index](README.md)

<a id="prerequisites"></a>
## 3. Prerequisites and related topics

Review:

- [Numeric Representation, Width, and Range](numeric-representation-width-range.md)
- [Programming Arithmetic Operators](programming-arithmetic-operators.md)
- [Binary Addition, Adders, and Two's-Complement Subtraction](binary-addition-adders-twos-complement.md)

The current algorithm is **unsigned**. Signed multiplication needs additional sign handling and is not silently implied by this guide.

[Back to top](#top) · [Topics index](README.md)

<a id="core-model"></a>
## 4. Core model and vocabulary

For an n-bit unsigned multiply:

~~~text
M (or X) = multiplicand, n bits, unchanged
A        = accumulator / high half of product, n bits
Q        = multiplier initially, low half of product finally, n bits
C        = one-bit carry extension during A + M
count    = n cycles
~~~

Initial state:

~~~text
C = 0
A = 0
Q = multiplier
M = multiplicand
~~~

Repeat exactly n times:

~~~text
1. Test Q0.
2. If Q0 = 1, add M to A and keep the carry in C.
   If Q0 = 0, set C = 0.
3. Shift the combined C:A:Q register right by one bit.
4. Repeat.
~~~

After n cycles:

~~~text
A:Q = 2n-bit product
~~~

The multiplier bits are consumed from least significant to most significant as Q shifts right.

[Back to top](#top) · [Topics index](README.md)

<a id="how-it-works"></a>
## 5. How it works

<a id="why-q0"></a>
### Why Q0 controls the add

The least-significant multiplier bit represents either:

~~~text
0 × current multiplicand contribution
~~~

or:

~~~text
1 × current multiplicand contribution
~~~

If Q0 is 0, there is nothing to add for that bit position.

If Q0 is 1, add M into A.

The following combined shift advances the algorithm to the next multiplier bit while also aligning the partial product.

<a id="combined-shift"></a>
### Why the shift is C:A:Q, not three unrelated shifts

Suppose A+M produces a carry. That carry is one bit more significant than A.

During the right shift:

~~~text
C -> A most-significant bit
A least-significant bit -> Q most-significant bit
Q least-significant bit -> discarded after it has been tested
~~~

The three pieces therefore behave as one register during the shift.

<a id="pic-rotate"></a>
### PIC16F883 rotate-through-carry maps directly to C:A:Q

PIC16F883 RRF rotates a file register right through STATUS.C.

That gives a direct implementation:

~~~text
ADDWF A,f        produces carry C when Q0 = 1
RRF A,f          C enters A7, A0 moves into C
RRF Q,f          previous A0 enters Q7
~~~

When Q0=0 and no addition occurs, C must be cleared before the combined shift so an old carry does not contaminate the product.

[Back to top](#top) · [Topics index](README.md)

<a id="worked-examples"></a>
## 6. Worked examples

<a id="four-bit-example"></a>
### Four-bit worked example: 3 × 5

Use:

~~~text
M = 0011  (3)
Q = 0101  (5)
A = 0000
C = 0
~~~

The table shows the state **after the optional add** and **after the combined shift**.

| Cycle | Q0 | Operation | C | A | Q |
| ---: | :---: | --- | :---: | :---: | :---: |
| start |  | initialize | 0 | 0000 | 0101 |
| 1 | 1 | A=A+M | 0 | 0011 | 0101 |
| 1 |  | shift C:A:Q right | 0 | 0001 | 1010 |
| 2 | 0 | no add | 0 | 0001 | 1010 |
| 2 |  | shift C:A:Q right | 0 | 0000 | 1101 |
| 3 | 1 | A=A+M | 0 | 0011 | 1101 |
| 3 |  | shift C:A:Q right | 0 | 0001 | 1110 |
| 4 | 0 | no add | 0 | 0001 | 1110 |
| 4 |  | shift C:A:Q right | 0 | 0000 | 1111 |

Final product:

~~~text
A:Q = 0000 1111 = 15
~~~

Check:

~~~text
3 × 5 = 15
~~~

<a id="eight-bit-pic"></a>
### Eight-bit PIC16F883 implementation

The full simulator-ready example is in [Examples/BinaryArithmetic/Multiply8](../Examples/BinaryArithmetic/Multiply8/).

The core routine is:

~~~assembly
Multiply8:
    clrf    product_high
    movlw   8
    movwf   loop_count

multiplyLoop:
    btfss   product_low,0
    goto    noAdd

    movf    multiplicand,w
    addwf   product_high,f
    goto    shiftProduct

noAdd:
    bcf     C

shiftProduct:
    rrf     product_high,f
    rrf     product_low,f

    decfsz  loop_count,f
    goto    multiplyLoop

    return
~~~

Inputs:

~~~text
multiplicand = M
product_low  = Q = multiplier
~~~

Outputs:

~~~text
product_high:product_low = 16-bit product
~~~

The example initializes:

~~~text
13 × 11
~~~

Expected simulator result:

~~~text
product_high = 0x00
product_low  = 0x8F

0x008F = 143
~~~

<a id="test-vectors"></a>
### Test vectors

| Multiplicand | Multiplier | Expected 16-bit product |
| ---: | ---: | ---: |
| 3 | 5 | 0x000F |
| 13 | 11 | 0x008F |
| 128 | 2 | 0x0100 |
| 255 | 255 | 0xFE01 |

The last vector forces carry propagation and is especially useful for verifying the C:A:Q shift.

[Back to top](#top) · [Topics index](README.md)

<a id="apply-verify-troubleshoot"></a>
## 7. Apply, verify, and troubleshoot

For hand work:

1. write M, A, Q, and C at fixed width;
2. record Q0 before any change;
3. when Q0=1, show A+M and carry separately;
4. shift C:A:Q as one register;
5. perform exactly n cycles;
6. concatenate A:Q only after the final cycle.

For MPLAB X simulator work:

1. set a breakpoint at Multiply8;
2. add multiplicand, product_high, product_low, loop_count, and STATUS to Watch;
3. step the conditional add;
4. watch C after ADDWF;
5. step RRF product_high;
6. watch A0 move through C;
7. step RRF product_low;
8. verify one complete cycle against the hand table;
9. run to return and compare A:Q with the expected product.

Common defects:

- forgetting to clear C on a no-add cycle;
- shifting A and Q independently instead of through Carry;
- testing Q0 after shifting instead of before;
- repeating until Q becomes zero instead of performing exactly n cycles;
- storing only eight bits of a sixteen-bit result.

[Back to top](#top) · [Topics index](README.md)

<a id="practice"></a>
## 8. Practice

1. Why can an 8-bit by 8-bit product require 16 bits?
2. In the algorithm, which register is unchanged?
3. What decides whether M is added to A?
4. Why is C part of the combined shift?
5. Trace 2 × 7 using four-bit M/A/Q registers.
6. Trace the first two cycles of 7 × 3.
7. Predict the 16-bit result of 128 × 2.
8. What fault occurs if C is not cleared during a no-add cycle?
9. Why does the algorithm always perform eight cycles for an 8-bit multiplier?
10. In the PIC routine, what do the two consecutive RRF instructions accomplish together?

[Back to top](#top) · [Topics index](README.md)

<a id="answer-key"></a>
## 9. Answer key

1. Maximum 255 × 255 = 65025, which exceeds eight bits but fits in 16 bits.
2. M, the multiplicand.
3. The current least-significant bit Q0.
4. A+M can produce an n+1-bit intermediate value; C preserves that extra bit through the shift.
5. Final A:Q is 0000 1110 = 14.
6. Start M=0111, Q=0011. Cycle 1 adds M then shifts; cycle 2 again sees Q0=1, adds M, then shifts.
7. 0x0100.
8. A stale carry can be shifted into the high product bit and corrupt the result.
9. One cycle processes one multiplier bit; an 8-bit multiplier has eight bits regardless of its numeric value.
10. The first moves C into A7 and A0 into C; the second moves that A0 bit from C into Q7, completing the C:A:Q right shift.

[Back to top](#top) · [Topics index](README.md)

<a id="without-notes"></a>
## 10. What you should be able to explain without notes

Explain M/A/Q/C, Q0 testing, conditional addition, the combined C:A:Q shift, fixed cycle count, 2n-bit product width, and how PIC16F883 ADDWF plus RRF implements the paper algorithm.

[Back to top](#top) · [Topics index](README.md)

<a id="references"></a>
## 11. References

- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet*, DS41291F, Section 15 — https://ww1.microchip.com/downloads/en/DeviceDoc/41291F.pdf
  - Used for: ADDWF, RRF, BTFSS, DECFSZ, and STATUS.C behavior.
- Microchip Technology Inc., *MPLAB XC8 PIC Assembler User's Guide* — https://onlinedocs.microchip.com/oxy/GUID-4DC87671-9D8E-428A-ADFE-98D694F9F089/
  - Used for: pic-as source/tool syntax.
- [RCET PIC-AS Style Guide](https://github.com/rosstimo/pic_projects/blob/main/RCET_PIC-AS_Style_Guide.md)
  - Used for: RCET assembly naming, source structure, PSECT, and formatting conventions.

[Back to top](#top) · [Topics index](README.md)
