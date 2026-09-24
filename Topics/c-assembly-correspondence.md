# RCET 3371 — C and Assembly Correspondence

*Self-learning guide*

[Topics index](README.md)

Generated assembly and hand-written pic-as make implementation details visible, but source lines do not map one-for-one to instructions. Compare behavior contracts and test vectors first, then inspect how the toolchain or your routine realizes them.





## Practice

1. For `low_nibble(0xA5)`, what result should both C and assembly produce?
2. Run the supplied vectors `0xA5`, `0xFF`, `0x30`, and `0x07` through both implementations.
3. Why should you not require one C statement to equal one assembly instruction?
4. When inspecting generated code, which bounded questions are more useful than trying to predict the whole listing?
5. What should remain identical when comparing the compiler output with a hand-written routine?

## Answer reasoning

1. `0x05`.
2. `0x05`, `0x0F`, `0x00`, `0x07`.
3. The compiler may choose any equivalent legal instruction sequence and may optimize across source structure.
4. Ask where input arrives, what instruction(s) realize the key operation, where the result goes, and how call/return is represented.
5. The routine contract and test vectors.

## Ready to continue when

You can compare one bounded C function with generated/disassembled code, implement the same contract in pic-as, run identical test vectors, and explain why behavioral equivalence matters more than line-by-line correspondence.

## References

- Microchip Technology Inc., *MPLAB XC8 PIC Assembler User's Guide* — https://onlinedocs.microchip.com/oxy/GUID-4DC87671-9D8E-428A-ADFE-98D694F9F089/
- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet*, Section 15 "Instruction Set Summary" — https://www.microchip.com/en-us/product/PIC16F883
- [PIC-as setup](../Guides/Toolchains/pic-as.md)
- [C# / Python / Embedded C / PIC Assembly comparison](../References/csharp-python-c-picas-comparison.md)
