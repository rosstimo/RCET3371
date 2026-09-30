# RCET 3371 — Embedded C Fundamentals

*Self-learning guide*

[Topics index](README.md)

Embedded C should begin as ordinary C applied to an algorithm the student already understands. First prove the language/toolchain and function behavior, then add exact-width types and multi-file build structure.













## Practice

1. Translate the C# `Clamp` method into C.
2. When is `uint8_t` preferable to ordinary `int`?
3. Why should the one-file XC8 version build before splitting into `.h/.c` files?
4. What does a linker error teach that a compiler syntax error does not?
5. What is the difference between declaration and definition?

## Answer reasoning

1. The control-flow/return contract stays nearly identical; syntax/types adapt to C.
2. When the required width is part of the register/protocol/storage contract.
3. It isolates language/toolchain behavior from module/linking behavior.
4. The compiler may accept a declaration while the final build cannot locate the corresponding definition.
5. A declaration describes a symbol/signature; a definition supplies implementation/storage.

## Ready to continue when

You can translate a known algorithm into C, choose fixed-width types deliberately, build a minimal XC8 program, split a working function into header/source files, and explain a basic linker failure.

## References

- Microchip Technology Inc., MPLAB XC8 Compiler — https://www.microchip.com/en-us/tools-resources/develop/mplab-x-compilers/xc8
- C fixed-width integer types — https://en.cppreference.com/w/c/types/integer
- [XC8 setup](../Guides/Toolchains/xc8.md)
