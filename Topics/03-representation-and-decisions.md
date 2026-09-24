# RCET 3371 — Representation and Decisions

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 3](../LearningPath/03-Representation-and-Decisions.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. Representation and operations
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

A value and its representation are not the same thing. Hardware, files, protocols, and programming languages all impose representation rules. Many real failures come from using correct arithmetic on the wrong representation, or correct bits with the wrong meaning.

## 2. Outcomes

You should be able to:

- determine range from bit width and signedness;
- predict fixed-width overflow behavior when the language/context defines it;
- use bitwise AND, OR, XOR, NOT, shifts, masks, and comparisons;
- extract, modify, and reinsert a packed field without damaging unrelated bits;
- reconstruct multi-byte values with an explicit byte order;
- distinguish logical from bitwise operations;
- separate full-resolution internal values from rounded display formatting;
- use engineering prefixes and significant figures appropriately.

## 3. Prerequisites

Binary, hexadecimal, arithmetic, variables, and conditionals from RCET 2265/digital coursework.

## 4. Core model

Always separate four questions:

1. **Value:** what quantity or state does this mean?
2. **Representation:** which bits/bytes encode it?
3. **Type:** how does the language interpret those bits?
4. **Operation:** what transformation is being performed?

For an unsigned n-bit integer:

    minimum = 0
    maximum = 2^n - 1

For a common two's-complement signed n-bit integer:

    minimum = -2^(n-1)
    maximum = 2^(n-1) - 1

## 5. Representation and operations

### Masks

A mask selects or modifies bit positions.

Read selected bits:

    selected = value & mask

Set selected bits:

    value = value | mask

Clear selected bits:

    value = value & ~mask

Toggle selected bits:

    value = value ^ mask

### Grab, modify, put back

For a packed field:

1. mask the field;
2. shift it to a convenient position;
3. modify it;
4. constrain it to the legal width;
5. shift it back;
6. clear the destination field;
7. OR the new field into the original word.

This preserves unrelated bits.

### Multi-byte values

If a 16-bit unsigned value is transmitted high byte first:

    value = (high << 8) | low

If low byte is first, the reconstruction order changes. Byte order is part of the protocol contract.

### Overflow

Fixed-width types have finite range. C# integral operations can use checked or unchecked contexts. Embedded C behavior depends on type/conversion rules and should be verified against the compiler/language rules. Python integers are not fixed-width in the same ordinary way, so translating fixed-width algorithms into Python often requires explicit masking when you want hardware-like behavior.

### Display versus storage

Keep a measured/calculated value at useful internal precision. Apply rounding and engineering formatting at the presentation boundary unless the specification explicitly requires quantization earlier.

## 6. Worked examples

### Example 1: packed state byte

Suppose:

    state = 0b1010_0101

Upper nibble is a count. Lower nibble contains flags.

Extract count:

    count = (state & 0xF0) >> 4

Count is 10.

Increment modulo 16:

    count = (count + 1) & 0x0F

Repack while preserving flags:

    state = (state & 0x0F) | (count << 4)

### Example 2: 16-bit value

Bytes:

    high = 0x12
    low  = 0x34

Big-endian reconstruction:

    0x1234 = 4660 decimal

### Example 3: engineering display

Internal value:

    0.000004732 V

A display might show:

    4.73 uV

The program should not replace the internal value with 4.73e-6 merely because that is what was displayed.

## 7. Apply, verify, and troubleshoot

When bitwise code is wrong:

1. write the value in binary/hex;
2. mark the field positions;
3. write the mask;
4. trace each operation;
5. verify unaffected bits;
6. test boundary values: all zero, all one, minimum field, maximum field, rollover.

When numeric formatting is wrong:

- verify the raw value first;
- verify units;
- verify prefix threshold;
- verify significant-figure rule;
- only then inspect string formatting.

## 8. Practice

1. What is the unsigned range of 8 bits?
2. Extract bits 6:4 from 0b1101_1010.
3. Set bit 2 of 0b1000_0001 without changing other bits.
4. Reconstruct big-endian bytes 0xBE and 0xEF.
5. A 4-bit field currently contains 15. What should modulo-16 increment produce?
6. Why may Python require an explicit mask when emulating an 8-bit hardware operation?
7. A program rounds a sensor value before storing it. What information is lost?

## 9. Answer key

1. 0 through 255.
2. Mask 0x70 and shift right four: 0b101 = 5.
3. OR with 0x04: result 0b1000_0101.
4. 0xBEEF.
5. 0.
6. Ordinary Python integers grow beyond fixed hardware widths; masking constrains the representation to the intended width.
7. Any precision below the chosen rounding point is permanently discarded, which can distort later calculations or comparisons.

## 10. Explain without notes

Explain:

- value versus representation versus type;
- signed/unsigned range;
- mask/shift;
- packed-field preservation;
- byte order;
- overflow;
- internal precision versus display precision.

## 11. References

- Microsoft Learn, integral numeric types — https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
- Microsoft Learn, checked and unchecked — https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/checked-and-unchecked
- Python Software Foundation, numeric types — https://docs.python.org/3/library/stdtypes.html#numeric-types-int-float-complex
- Microchip, MPLAB XC8 compiler documentation — https://www.microchip.com/en-us/tools-resources/develop/mplab-xc-compilers/xc8
