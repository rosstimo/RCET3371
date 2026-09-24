# Section 3 - Representation and Decisions

This is the first section where the languages are compared in earnest. The comparison always begins in C#.

## Start from what you already know

RCET 2265 already used integer types, arithmetic, comparisons, Boolean logic, binary/hex values, and conversions. Begin there. New material is added in layers: fixed width, bit masks, packed fields, byte order, and embedded constraints.

## Outcomes

You should be able to:

- predict ordinary integer and Boolean expressions in C#;
- relate a numeric value to its binary/hex representation;
- choose an appropriate signed or unsigned fixed-width type for a stated range;
- use a mask to inspect or change selected bits;
- perform a simple grab -> modify -> put-back operation without changing unrelated bits;
- reconstruct a multi-byte value from bytes when the byte order is given;
- recognize overflow/range problems;
- compare the same operation in C#, Python, XC8 C, and pic-as after understanding it in C#;
- keep calculation precision separate from display formatting.

## Learn

- [Representation and decisions](../Topics/numeric-representation-width-range.md)
- [Bitwise operations and packed fields](../Topics/bitwise-operations-packed-fields.md)
- [Engineering notation quick reference](../References/engineering-notation.md)
- [Cross-language comparison](../References/csharp-python-c-picas-comparison.md)
- [Status Decoder example](../Examples/StatusDecoder/)

## Example progression

1. Write and print an ordinary C# integer value.
2. Print the same value in decimal, binary, and hex.
3. Mask one bit.
4. Mask a small field.
5. Change that field while preserving the rest of the byte.
6. Only then compare the operation in Python, C, and assembly.

Do not memorize a mask formula before tracing at least one byte by hand.

## Practice

**Predict:** Work the C# result on paper, including binary/hex, before running it.

**Follow:** Trace the complete packed-byte example in the topic guide.

**Modify:** Change the field width or position and recalculate the mask.

**Translate:** Recreate a bounded, already-understood operation in Python and C.

## Programming Assignment

Start [Cross-Language Engineering Model](../ProgrammingAssignments/CrossLanguageEngineeringModel/README.md).

## Assessment

Section 3 practice and graded assessment include code reading and fixed-width reasoning, but the first questions stay close to C# before moving cross-language.

Next: [Section 4 - From Methods and Classes to Program Structure](04-Program-Structure-and-Interfaces.md)
