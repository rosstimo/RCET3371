<a id="top"></a>
# RCET 3371 — Binary Addition, Adders, and Two's-Complement Subtraction

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

Digital logic does not need a completely different arithmetic machine for subtraction. A binary adder plus controlled inversion and an initial carry-in can perform both addition and subtraction.

This is the bridge from prior digital-logic work with adders, XOR gates, and registers to the arithmetic instructions and algorithms used later in PIC assembly.

[Back to top](#top) · [Topics index](README.md)

<a id="outcomes"></a>
## 2. What you should be able to do

You should be able to:

- add binary values by hand including carries;
- explain half-adder and full-adder behavior;
- trace carry propagation through a multi-bit adder;
- form the two's complement of a fixed-width binary value;
- subtract by adding the two's complement of the subtrahend;
- explain how XOR gates plus carry-in turn one adder into an adder/subtractor;
- distinguish unsigned carry/borrow from signed overflow;
- connect the digital model to PIC16F883 SUBWF/SUBLW behavior.

[Back to top](#top) · [Topics index](README.md)

<a id="prerequisites"></a>
## 3. Prerequisites and related topics

Review [Numeric Representation, Width, and Range](numeric-representation-width-range.md) and [Programming Arithmetic Operators](programming-arithmetic-operators.md).

This Topic reviews digital concepts students previously encountered with logic gates and adders. Continue to [Shift-and-Add Binary Multiplication](binary-multiplication-shift-add.md), where the adder becomes part of a multi-cycle arithmetic algorithm.

[Back to top](#top) · [Topics index](README.md)

<a id="core-model"></a>
## 4. Core model and vocabulary

<a id="one-bit-addition"></a>
### One-bit addition

~~~text
A B | Sum Carry
0 0 |  0    0
0 1 |  1    0
1 0 |  1    0
1 1 |  0    1
~~~

A half-adder implements:

~~~text
Sum   = A XOR B
Carry = A AND B
~~~

A full-adder adds A, B, and carry-in.

~~~text
Sum = A XOR B XOR Cin
~~~

Carry-out is 1 when at least two of the three inputs are 1.

<a id="multibit-adder"></a>
### Multi-bit addition

Connect the carry-out of one bit position to the carry-in of the next more-significant bit.

For a simple ripple-carry adder:

~~~text
bit 0 -> carry -> bit 1 -> carry -> bit 2 -> carry -> bit 3
~~~

The output width may need one more bit than either input.

<a id="twos-complement"></a>
### Two's complement

To negate an n-bit value:

1. invert every bit;
2. add 1;
3. keep only n bits.

Example using four bits:

~~~text
+3 = 0011

invert: 1100
+1:     1101

-3 = 1101
~~~

The four-bit signed range is -8 through +7.

<a id="subtraction-identity"></a>
### Subtraction as addition

The central identity is:

~~~text
A - B = A + (~B) + 1
~~~

That means a hardware adder can subtract if it can:

- invert every B input bit;
- force the least-significant carry-in to 1.

<a id="adder-subtractor"></a>
### One circuit for add and subtract

Use a control bit SUB.

For every B bit:

~~~text
B_to_adder = B XOR SUB
Cin0       = SUB
~~~

When SUB = 0:

~~~text
B XOR 0 = B
Cin0 = 0
result = A + B
~~~

When SUB = 1:

~~~text
B XOR 1 = NOT B
Cin0 = 1
result = A + NOT B + 1 = A - B
~~~

The XOR gates are acting as controlled inverters.

[Back to top](#top) · [Topics index](README.md)

<a id="how-it-works"></a>
## 5. How it works

<a id="binary-addition"></a>
### Binary addition by hand

Add from least significant bit to most significant bit exactly as with decimal arithmetic, but each column can hold only 0 or 1.

Example:

~~~text
   0110
 + 0011
 ------
   1001
~~~

Column trace:

- bit 0: 0 + 1 = 1, carry 0
- bit 1: 1 + 1 = 0, carry 1
- bit 2: 1 + 0 + carry 1 = 0, carry 1
- bit 3: 0 + 0 + carry 1 = 1

<a id="subtraction-with-adder"></a>
### Subtraction using the adder

Compute 6 - 3 using four bits.

~~~text
A = 0110
B = 0011
~~~

Take the two's complement of B:

~~~text
B        0011
invert   1100
+1       1101
~~~

Add:

~~~text
   0110
 + 1101
 ------
 1 0011
~~~

Discard the carry out of the fixed four-bit result:

~~~text
0011 = 3
~~~

This is exactly why subtraction can reuse an adder.

<a id="signed-overflow"></a>
### Carry is not signed overflow

Unsigned carry-out answers a width question. Signed overflow answers whether a two's-complement signed result fits the signed range.

For four-bit signed values:

~~~text
+7 = 0111
+3 = 0011

0111 + 0011 = 1010
~~~

Bit pattern 1010 represents -6 in four-bit two's complement. The mathematical result +10 is outside the legal +7 maximum, so signed overflow occurred.

For signed addition, overflow occurs when two operands have the same sign and the result has the opposite sign.

<a id="pic-subtraction"></a>
### PIC16F883 connection

PIC16F883 SUBWF is:

~~~text
f - W -> destination
~~~

The device documentation explicitly describes subtraction as a two's-complement operation.

Example:

~~~assembly
    movlw   3
    movwf   subtrahend

    movlw   6
    movwf   minuend

    movf    subtrahend,w
    subwf   minuend,w        ; W = 6 - 3 = 3
~~~

On this PIC family the Carry bit has reversed borrow polarity during subtraction:

- C = 1: no borrow;
- C = 0: borrow occurred.

Negating W with subtraction from zero also exposes the same two's-complement idea:

~~~assembly
    movlw   3
    sublw   0                ; W = 0 - 3 = 0xFD (-3 as 8-bit two's complement)
~~~

The processor instruction and the digital-adder model describe the same arithmetic at different abstraction levels.

[Back to top](#top) · [Topics index](README.md)

<a id="worked-examples"></a>
## 6. Worked examples

### Example 1: four-bit 5 + 6

~~~text
   0101
 + 0110
 ------
   1011
~~~

As unsigned values, 5 + 6 = 11 and fits in four bits.

As signed four-bit values, both inputs are positive but 1011 has sign bit 1. +11 cannot fit in the -8..+7 signed range, so signed overflow occurred.

### Example 2: four-bit 2 - 5

~~~text
A = 0010
B = 0101

two's complement of B:
0101 -> 1010 -> 1011

   0010
 + 1011
 ------
   1101
~~~

1101 is -3 in four-bit two's complement.

### Example 3: hardware control signal

For A = 0110 and B = 0011:

- ADD mode: SUB=0, B passes unchanged, Cin0=0 -> 1001
- SUB mode: SUB=1, B becomes 1100 and Cin0=1 -> 0011 after the fixed-width carry is discarded

Only the B path and initial carry changed. The same four full adders can do both jobs.

[Back to top](#top) · [Topics index](README.md)

<a id="apply-verify-troubleshoot"></a>
## 7. Apply, verify, and troubleshoot

When hand arithmetic and code disagree:

1. fix the width first;
2. write both operands with exactly that many bits;
3. state signed or unsigned interpretation;
4. for subtraction, explicitly form ~B + 1;
5. trace carry between every bit;
6. distinguish final carry-out from signed overflow;
7. on PIC, remember SUBWF means f - W;
8. interpret C after subtraction as no-borrow when set.

For simulator work, inspect W, the destination file register, and STATUS.C/Z at the instruction boundary.

[Back to top](#top) · [Topics index](README.md)

<a id="practice"></a>
## 8. Practice

1. Add 0101 + 0011 using four bits.
2. Form the four-bit two's complement of 0110.
3. Use two's-complement addition to calculate 7 - 5.
4. Use four-bit two's-complement addition to calculate 2 - 5.
5. Explain why XOR gates are useful in a combined adder/subtractor.
6. What must Cin0 be in subtract mode?
7. Is carry-out the same as signed overflow?
8. For PIC16F883 SUBWF, what does C=0 mean after subtraction?
9. Predict W after loading W=5 and executing SUBLW 0.
10. Explain how one adder circuit can implement both A+B and A-B.

[Back to top](#top) · [Topics index](README.md)

<a id="answer-key"></a>
## 9. Answer key

1. 1000.
2. 1010: invert 0110 -> 1001, add 1 -> 1010.
3. 0010.
4. 1101, which is -3.
5. B XOR SUB passes B when SUB=0 and inverts B when SUB=1.
6. 1, supplying the +1 required by two's complement.
7. No. Carry-out is useful for unsigned width/borrow reasoning; signed overflow depends on signed range/sign relationships.
8. A borrow occurred.
9. 0xFB, the 8-bit two's-complement representation of -5.
10. In ADD mode B is unchanged and Cin=0; in SUB mode B is inverted and Cin=1, producing A + ~B + 1.

[Back to top](#top) · [Topics index](README.md)

<a id="without-notes"></a>
## 10. What you should be able to explain without notes

Explain a half-adder, full-adder, ripple carry, two's-complement negation, A-B=A+(~B)+1, XOR-controlled adder/subtractor hardware, carry versus signed overflow, and how PIC16F883 SUBWF connects to the same model.

[Back to top](#top) · [Topics index](README.md)

<a id="references"></a>
## 11. References

- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet*, DS41291F, Section 2.2.2.1 and Section 15 — https://ww1.microchip.com/downloads/en/DeviceDoc/41291F.pdf
  - Used for: STATUS Carry/Borrow behavior and SUBWF/SUBLW two's-complement subtraction.
- Tocci, Widmer, Moss, *Digital Systems: Principles and Applications*, digital arithmetic/adders chapter.
  - Further reading for: binary arithmetic, two's-complement arithmetic, full adders, and adder/subtractor circuits.

[Back to top](#top) · [Topics index](README.md)
