<a id="top"></a>
# RCET 3371 — Register-Based Binary Division

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

Writing a `/` operator hides how division can be built from registers, complementing, addition, shifting, and carry tests when the processor has no divide instruction.

The RCET machine-division method uses three conceptual registers:

- `X`: one's complement of the divisor;
- `A`: upper dividend / trial remainder / final remainder;
- `Q`: lower dividend / developing quotient / final quotient.

The algorithm performs one fixed iteration per quotient bit. It does **not** repeatedly subtract until the dividend becomes too small.

[Back to top](#top) · [Topics index](README.md)

<a id="outcomes"></a>
## 2. What you should be able to do

You should be able to:

- state the quotient/remainder identity;
- explain why an n-bit quotient is produced in exactly n iterations;
- initialize X, A, and Q for unsigned machine division;
- explain why X contains the one's complement of the divisor;
- trace the combined A:Q left shift;
- distinguish the shift carry C1, trial-add carry C2, and final success condition C3;
- explain why an all-ones trial result represents the exact-equality case;
- explain why a successful trial adds 1 to both A and Q;
- identify Q as the quotient and A as the remainder after the final iteration;
- step an 8-bit PIC16F883 implementation in MPLAB X;
- detect divide-by-zero and quotient-overflow conditions before corrupting the result.

[Back to top](#top) · [Topics index](README.md)

<a id="prerequisites"></a>
## 3. Prerequisites and related topics

Review:

- [Numeric Representation, Width, and Range](numeric-representation-width-range.md)
- [Programming Arithmetic Operators](programming-arithmetic-operators.md)
- [Binary Addition, Adders, and Two's-Complement Subtraction](binary-addition-adders-twos-complement.md)
- [Shift-and-Add Binary Multiplication](binary-multiplication-shift-add.md)

This Topic uses **unsigned** division. Signed division requires deliberate sign handling and is not silently implied here.

[Back to top](#top) · [Topics index](README.md)

<a id="core-model"></a>
## 4. Core model and vocabulary

For n-bit X, A, and Q registers:

~~~text
D = divisor
X = one's complement of D
A:Q = dividend
A = final remainder
Q = final quotient
count = n iterations
~~~

For an ordinary n-bit dividend, initialize:

~~~text
A = 0
Q = dividend
X = NOT divisor
~~~

The more general form allows a 2n-bit dividend already present in A:Q, provided the quotient will still fit in n bits.

### Quotient/remainder contract

~~~text
dividend = quotient × divisor + remainder

0 <= remainder < divisor
~~~

### Initial register-size test

If A already contains a value greater than or equal to the divisor, the quotient would require more than n bits.

The teaching method tests A against the complemented divisor before the loop. In the common case where the dividend begins entirely in Q, A starts at zero.

The simulator-ready implementation also checks the equality edge explicitly so a full A:Q dividend cannot silently overflow the n-bit quotient.

[Back to top](#top) · [Topics index](README.md)

<a id="how-it-works"></a>
## 5. How it works

Repeat exactly once for each bit in Q.

### Step 1: shift A:Q left

Shift the combined A:Q register left and always shift a zero into Q0.

~~~text
old A7 -> C1
old Q7 -> A0
0      -> Q0
~~~

For four-bit hand work, use the same pattern with A3/Q3.

Save the shifted A value because a failed trial must restore it.

### Step 2: trial subtraction using X

X is the one's complement of the divisor:

~~~text
X = NOT D
~~~

Add X to A:

~~~text
trial = A + X
~~~

This deliberately stops one step short of ordinary two's-complement subtraction:

~~~text
A - D = A + NOT(D) + 1
~~~

The final +1 is applied only if the trial proves that subtraction is legal.

### Step 3: determine C3, the success condition

The trial succeeds if **any one** of these conditions is true:

1. `C1 = 1`: the A:Q left shift shifted a 1 out of A;
2. `C2 = 1`: A + X produced a carry;
3. trial A is all ones: `1111` for four bits or `0xFF` for eight bits.

The all-ones case is important. If the shifted A equals the divisor:

~~~text
A + NOT(D) = 111...111
~~~

Adding 1 then produces exactly zero remainder.

In the normal operating range these three success cases are mutually exclusive, which is why the original teaching diagram can combine them into one C3 decision.

### Step 4A: failed trial

If C3 = 0:

- restore the shifted A value;
- leave Q0 = 0.

That quotient bit therefore records that the divisor did not fit into the current partial dividend.

### Step 4B: successful trial

If C3 = 1:

~~~text
A = A + 1
Q = Q + 1
~~~

The two +1 operations do different jobs:

- incrementing A completes `A + NOT(D) + 1`, the actual subtraction;
- incrementing Q sets Q0 to 1, recording a successful quotient bit.

Because Q0 was forced to zero by the left shift, the Q increment sets only that new quotient bit.

After n iterations:

~~~text
Q = quotient
A = remainder
~~~

[Back to top](#top) · [Topics index](README.md)

<a id="worked-examples"></a>
## 6. Worked examples

<a id="four-bit-example"></a>
### Four-bit worked example: 15 ÷ 5

Use the same X/A/Q arrangement as the RCET Binary Math hand method.

~~~text
dividend = 15 = 1111
divisor  =  5 = 0101

X = NOT(0101) = 1010
A = 0000
Q = 1111
~~~

Initial size test:

~~~text
A + X = 0000 + 1010 = 1010
carry = 0
proceed
~~~

| Cycle | Shift result A:Q | C1 | A + X trial | C2 | C3 reason | Result A:Q |
| ---: | --- | :---: | --- | :---: | --- | --- |
| 1 | `0001 1110` | 0 | `1011 1110` | 0 | none | restore -> `0001 1110` |
| 2 | `0011 1100` | 0 | `1101 1100` | 0 | none | restore -> `0011 1100` |
| 3 | `0111 1000` | 0 | `0001 1000` | 1 | C2 | +1 to A and Q -> `0010 1001` |
| 4 | `0101 0010` | 0 | `1111 0010` | 0 | A all ones | +1 to A and Q -> `0000 0011` |

Final result:

~~~text
Q = 0011 = quotient 3
A = 0000 = remainder 0

15 = 3 × 5 + 0
~~~

<a id="eight-bit-pic"></a>
### Eight-bit PIC16F883 implementation

The full simulator-ready example is in [Examples/BinaryArithmetic/Divide8](../Examples/BinaryArithmetic/Divide8/).

The example keeps the conceptual registers visible:

~~~text
X = x_register
A = a_register
Q = q_register
~~~

Default simulator input:

~~~text
A:Q = 0x000F
divisor = 0x05
~~~

Expected result:

~~~text
divide_status = 0
Q = 0x03
A = 0x00
~~~

The routine additionally reports:

~~~text
divide_status = 1  divide by zero
divide_status = 2  quotient does not fit in eight bits
~~~

<a id="test-vectors"></a>
### Test vectors

| Dividend A:Q | Divisor | Quotient Q | Remainder A |
| ---: | ---: | ---: | ---: |
| `0x000F` | 5 | 3 | 0 |
| `0x001D` | 8 | 3 | 5 |
| `0x00FF` | 13 | 19 | 8 |
| `0x0080` | 3 | 42 | 2 |
| `0x0100` | 3 | 85 | 1 |

The last case demonstrates why the algorithm names the dividend as A:Q rather than assuming A is always zero.

[Back to top](#top) · [Topics index](README.md)

<a id="apply-verify-troubleshoot"></a>
## 7. Apply, verify, and troubleshoot

For hand work:

1. write D, X, A, and Q at a fixed width;
2. confirm X is the **one's complement** of D;
3. perform the initial size check;
4. shift A:Q left with zero entering Q0;
5. record C1 before the trial add changes Carry;
6. save the shifted A value;
7. add X to A and record C2;
8. check the all-ones equality case;
9. restore A when C3=0;
10. add 1 to both A and Q when C3=1;
11. perform exactly n iterations;
12. read Q as quotient and A as remainder.

For MPLAB X simulator work, watch:

- `divisor`
- `x_register`
- `a_register`
- `q_register`
- `shifted_a`
- `shift_carry`
- `loop_count`
- `divide_status`
- `STATUS`

Common defects:

- using the two's complement of the divisor in X instead of the one's complement;
- forgetting that the successful-trial +1 to A completes the subtraction;
- forgetting that the successful-trial +1 to Q sets the quotient bit;
- losing C1 when the trial ADDWF overwrites STATUS.C;
- failing to restore A after an unsuccessful trial;
- shifting a 1 into Q0 instead of always shifting in zero;
- stopping based on a register value instead of performing exactly n iterations;
- dividing by zero without a defined error path.

[Back to top](#top) · [Topics index](README.md)

<a id="practice"></a>
## 8. Practice

1. For 4-bit division by 5, what value is loaded into X?
2. Why is X the one's complement rather than the two's complement?
3. After shifting A:Q left, what bit always enters Q0?
4. What are the three ways C3 can indicate a successful trial?
5. Why must the shifted A value be saved before adding X?
6. Why does a successful trial add 1 to A?
7. Why does that same successful trial add 1 to Q?
8. Trace 13 ÷ 3 with four-bit A/Q registers.
9. Predict quotient and remainder for 29 ÷ 8.
10. For an 8-bit quotient, why must an initial A >= divisor be rejected?
11. What should happen when divisor = 0?
12. In the final state, which register contains quotient and which contains remainder?

[Back to top](#top) · [Topics index](README.md)

<a id="answer-key"></a>
## 9. Answer key

1. `1010`, the one's complement of `0101`.
2. The trial intentionally computes A + NOT(D); the final +1 is delayed until the trial succeeds.
3. Zero.
4. C1 from the left shift, C2 from A+X, or an all-ones trial A indicating exact equality.
5. A failed trial must restore the partial remainder that existed immediately after the shift.
6. It completes A + NOT(D) + 1, which is the two's-complement subtraction.
7. Q0 was shifted to zero; incrementing Q sets that quotient bit to one.
8. Final quotient 4, remainder 1.
9. Quotient 3, remainder 5.
10. The mathematical quotient would require nine or more bits.
11. Reject the operation with a defined divide-by-zero status; do not enter the iteration loop.
12. Q is quotient; A is remainder.

[Back to top](#top) · [Topics index](README.md)

<a id="without-notes"></a>
## 10. What you should be able to explain without notes

Explain X/A/Q initialization, one's-complement X, the A:Q left shift, C1/C2/C3, restore versus successful trial, why +1 is added to both A and Q, fixed iteration count, and the final Q/A quotient/remainder interpretation.

[Back to top](#top) · [Topics index](README.md)

<a id="references"></a>
## 11. References

- RCET Binary Math teaching method, machine-division sequence and four-bit 15 / 5 worked example.
  - Used for: X/A/Q register roles, one's-complement divisor, initial size test, C1/C2/all-ones trial decision, restore rule, and final quotient/remainder interpretation.
- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet*, DS41291F, Section 15 — https://ww1.microchip.com/downloads/en/DeviceDoc/41291F.pdf
  - Used for: RLF, ADDWF, COMF, INCF, BTFSC/BTFSS, DECFSZ, and STATUS.C/Z behavior.
- Microchip Technology Inc., *MPLAB XC8 PIC Assembler User's Guide* — https://onlinedocs.microchip.com/oxy/GUID-4DC87671-9D8E-428A-ADFE-98D694F9F089/
  - Used for: pic-as source/tool syntax.
- [RCET PIC-AS Style Guide](https://github.com/rosstimo/pic_projects/blob/main/RCET_PIC-AS_Style_Guide.md)
  - Used for: RCET assembly naming, source structure, PSECT, and formatting conventions.

[Back to top](#top) · [Topics index](README.md)
