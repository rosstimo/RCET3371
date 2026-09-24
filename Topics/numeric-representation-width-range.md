# RCET 3371 — Numeric Representation, Width, and Range

*Self-learning guide*

[Topics index](README.md)

Numbers used by software and hardware have an engineering meaning, a representation, and a storage width. This Topic separates those ideas before bit-field manipulation is introduced. For masks and packed fields, continue with [Bitwise Operations and Packed Fields](bitwise-operations-packed-fields.md).













## Practice

1. Write decimal 42 as 8-bit binary and hexadecimal.
2. Give the unsigned ranges for 4, 8, and 12 bits.
3. Reconstruct high byte `0xBE` and low byte `0xEF` as a big-endian 16-bit value.
4. Predict the result of adding 1 to an explicitly 8-bit value containing 255 in embedded C and in an 8-bit PIC file register.
5. Explain why Python can hide a fixed-width overflow defect.
6. Explain why display formatting should usually happen after calculation.

## Answer reasoning

1. `0010 1010`, `0x2A`.
2. 0–15, 0–255, and 0–4095.
3. `0xBEEF`.
4. The stored 8-bit result wraps to `0x00`; surrounding flag behavior depends on the operation/environment and must be checked when relevant.
5. Ordinary Python integers can grow beyond the hardware width unless width is deliberately modeled.
6. Early rounding discards information that later calculations may still need.

## Ready to continue when

You can distinguish value, representation, type, and width; calculate fixed-width ranges; reconstruct a multi-byte value from a stated byte order; predict a width-boundary case; and separate internal precision from display formatting.

## References

- Microsoft, C# integral numeric types — https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
- Python numeric types — https://docs.python.org/3/library/stdtypes.html#numeric-types-int-float-complex
- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet*, Section 15 "Instruction Set Summary" — https://www.microchip.com/en-us/product/PIC16F883
- [C# / Python / Embedded C / PIC Assembly comparison](../References/csharp-python-c-picas-comparison.md)
