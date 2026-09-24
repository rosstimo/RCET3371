# RCET 3371 — Bitwise Operations and Packed Fields

*Self-learning guide*

[Topics index](README.md)

Packed fields appear in status bytes, registers, protocols, and compact application state. The central skill is to change only the intended bits while preserving neighboring fields. Review [Numeric Representation, Width, and Range](numeric-representation-width-range.md) first.









## Practice

1. Test bit 5 of `1010 0101`.
2. Extract bits 6:4 from `1101 1010`.
3. Increment the upper nibble of `1111 0011` modulo 16 while preserving the lower nibble.
4. Starting from `0xA5`, trace the grab-modify-put-back operation and predict the result before running C#, Python, embedded C, or pic-as.
5. Why is `state = newValue << 4` usually wrong when only the upper nibble should change?
6. In the pic-as example, identify the operation that clears the old field and the operation that combines the replacement field with the preserved bits.

## Answer reasoning

1. Mask with `0010 0000`; the result is nonzero.
2. `101` = 5.
3. Upper nibble 15 increments to 0; result `0000 0011`.
4. `0xB5`.
5. It discards unrelated bits instead of preserving them.
6. AND with the preservation mask removes the old destination field; OR combines the prepared replacement field with the preserved bits.

## Ready to continue when

You can test a bit, extract a multi-bit field, perform grab-modify-put-back without damaging neighbors, explain why masks are necessary, and trace the same packed-field operation in C#, Python, embedded C, and PIC16F883 pic-as.

## References

- Microsoft, C# bitwise and shift operators — https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators
- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet*, Section 15 "Instruction Set Summary" — https://www.microchip.com/en-us/product/PIC16F883
- Microchip Technology Inc., *MPLAB XC8 PIC Assembler User's Guide* — https://onlinedocs.microchip.com/oxy/GUID-4DC87671-9D8E-428A-ADFE-98D694F9F089/
- [C# / Python / Embedded C / PIC Assembly comparison](../References/csharp-python-c-picas-comparison.md)
