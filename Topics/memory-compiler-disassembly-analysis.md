# RCET 3371 — Memory, Compiler, and Disassembly Analysis

*Optional self-learning guide*

[Topics index](README.md)

Low-level analysis is useful when it answers a bounded engineering question about code size, storage, generated instructions, optimization, or timing. The goal is explanation and measurement, not premature optimization.

## Core model

Choose one small function or hot path and compare:

- source contract;
- generated IL/assembly;
- memory/storage used;
- optimization differences;
- measured timing/code size when meaningful.

Keep the question bounded. Full-program disassembly usually adds noise before it adds understanding.

## Worked example

Use the already familiar low-nibble function:

```c
uint8_t low_nibble(uint8_t value)
{
    return value & 0x0Fu;
}
```

Ask only:

1. where does the input arrive?
2. what instruction(s) implement the mask?
3. where does the result leave?
4. does changing optimization alter the emitted sequence?
5. does observable behavior remain the same for the test vectors?

That is a useful compiler/disassembly experiment because the source contract is already known.

## Apply and verify

For this optional Topic, use the same engineering frame:

1. state the problem;
2. preserve the known-good baseline;
3. add one bounded mechanism;
4. identify new failure modes;
5. define evidence before claiming improvement.

## Practice

1. Why should generated code be inspected for a bounded question?
2. What must remain true across optimization levels?
3. When would code-size measurement be meaningful?
4. Why is "fewer instructions" not automatically "better engineering"?

## Answer reasoning

1. The output is easier to relate to a known source contract instead of becoming a large reverse-engineering exercise.
2. Required observable behavior for the tested/defined contract.
3. When flash/RAM limits or a defined deployment constraint make size relevant.
4. Readability, correctness, timing, maintainability, and compiler/device constraints can matter more than raw instruction count.

## Ready to continue when

You can frame one low-level question, inspect generated output against a known source contract, use measured evidence, and avoid treating disassembly as an end in itself.

## References

- Microchip Technology Inc., *MPLAB XC8 C Compiler User's Guide* — https://onlinedocs.microchip.com/
- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet*, Section 15 "Instruction Set Summary" — https://www.microchip.com/en-us/product/PIC16F883
- [C and Assembly Correspondence](c-assembly-correspondence.md)
