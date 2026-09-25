# Section 3 - Representation, Operators, and Binary Arithmetic

This is the first section where the languages and the underlying digital arithmetic are compared in earnest. The comparison begins with familiar C# from RCET 2265, then moves downward toward the adder/register operations visible on the PIC.

## Start from what you already know

RCET 2265 already used integer types, arithmetic operators, integer division/remainder, comparisons, Boolean logic, binary/hexadecimal values, and conversions.

Earlier digital-logic coursework already introduced binary arithmetic, adders, XOR, and registers.

RCET 3371 reconnects those two prior views:

~~~text
C# expression
    ↓
Python / C expression
    ↓
fixed-width binary arithmetic
    ↓
adder / two's-complement behavior
    ↓
PIC16F883 instructions and register algorithms
~~~

## Outcomes

You should be able to:

- predict ordinary arithmetic expressions in C#;
- mirror those expressions in Python and embedded C and identify important semantic differences;
- explain integer division and remainder;
- relate a numeric value to its binary/hex representation and required width;
- trace binary addition with carry;
- form a two's-complement negative value;
- explain how an adder performs subtraction using B inversion plus carry-in;
- distinguish carry/borrow from signed overflow;
- trace the four-bit X:A:Q shift-and-add multiplication algorithm;
- step the same multiplication algorithm using 8-bit registers in PIC16F883 pic-as;
- trace the four-bit X:A:Q machine-division algorithm including C1/C2/C3 and restore/success behavior;
- step the same division method using 8-bit X/A/Q registers in PIC16F883 pic-as;
- use masks to inspect or change selected bits;
- perform grab -> modify -> put-back without changing unrelated bits;
- reconstruct a multi-byte value from bytes when byte order is given;
- recognize overflow/range problems;
- keep calculation precision separate from display formatting.

## Learn

- [Numeric representation, width, and range](../Topics/numeric-representation-width-range.md)
- [Programming arithmetic operators](../Topics/programming-arithmetic-operators.md)
- [Binary addition, adders, and two's-complement subtraction](../Topics/binary-addition-adders-twos-complement.md)
- [Shift-and-add binary multiplication](../Topics/binary-multiplication-shift-add.md)
- [Register-based binary division](../Topics/binary-division-register-algorithm.md)
- [Bitwise operations and packed fields](../Topics/bitwise-operations-packed-fields.md)
- [Engineering notation quick reference](../References/engineering-notation.md)
- [Cross-language comparison](../References/csharp-python-c-picas-comparison.md)
- [8-bit PIC multiplication example](../Examples/BinaryArithmetic/Multiply8/)
- [8-bit PIC division example](../Examples/BinaryArithmetic/Divide8/)
- [Status Decoder example](../Examples/StatusDecoder/)

## Example progression

1. Re-run familiar C# +, -, *, /, and % examples.
2. Mirror them in Python and identify / versus //.
3. Mirror them in embedded C and make width explicit.
4. Trace a one-bit half-adder/full-adder case.
5. Work four-bit subtraction as A + two's-complement(B).
6. Explain the XOR-controlled adder/subtractor circuit.
7. Trace 5 × 3 using the four-bit X:A:Q multiplication table.
8. Trace 15 ÷ 5 using the four-bit X:A:Q division table.
9. Step the 8-bit PIC16F883 Multiply8 routine in the simulator.
10. Step the 8-bit PIC16F883 Divide8 routine in the simulator.
11. Mask one bit.
12. Mask a small field.
13. Change that field while preserving the rest of the byte.

Do not memorize an arithmetic or mask recipe before tracing at least one complete fixed-width example by hand.

## Practice

**Predict:** Work C# arithmetic results on paper, including quotient/remainder and precedence.

**Mirror:** Translate the same bounded expression to Python and embedded C. Identify any semantic difference.

**Digital:** Trace one addition and one two's-complement subtraction through a four-bit adder.

**Algorithms:** Trace one four-bit multiplication and one four-bit division with X:A:Q before stepping the 8-bit PIC versions.

**Follow:** Trace the complete packed-byte example in the bitwise Topic.

**Modify:** Change the field width/position and recalculate the mask.

## Programming Assignment

Start [Cross-Language Engineering Model](../ProgrammingAssignments/CrossLanguageEngineeringModel/README.md).

## Assessment

Section 3 practice and graded assessment should cover C# operator recall, cross-language arithmetic differences, fixed-width representation, binary addition/two's-complement subtraction, the multiplication and division register algorithms, and bit-field reasoning.

Next: [Section 4 - From Methods and Classes to Program Structure](04-Program-Structure-and-Interfaces.md)
