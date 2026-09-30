# RCET 3371 — Hardware-Facing C

*Self-learning guide*

[Topics index](README.md)

Once ordinary C works, hardware registers introduce a new boundary: values may represent physical device state, can change outside ordinary control flow, and often require exact-width/volatile access. Keep hardware-facing code narrow so the rest of the program can reason in ordinary software terms.





## Practice

1. Why is wrapping `PORTC` access in a small function useful?
2. What problem is `volatile` intended to address?
3. List three things `volatile` does **not** guarantee.
4. Why should an application avoid scattering direct SFR writes throughout unrelated logic?
5. What evidence would distinguish "the C logic is correct" from "the physical output works"?

## Answer reasoning

1. It creates a small hardware/software boundary and gives callers a behavior-oriented operation.
2. It prevents the compiler from assuming a value cannot change merely because the current normal code did not assign it.
3. It does not guarantee atomicity, thread/interrupt safety, race freedom, or correct synchronization.
4. Centralizing hardware access reduces coupling and makes substitution/testing/troubleshooting clearer.
5. Logic can be shown with simulator/debugger/register state; physical behavior requires the actual target, wiring, programmer, and measurement/observation.

## Ready to continue when

You can identify when exact-width and volatile access matter, keep direct register access behind a small boundary, and distinguish software proof from simulator and physical-hardware proof.

## References

- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet* — https://www.microchip.com/en-us/product/PIC16F883
- Microchip Technology Inc., *MPLAB XC8 C Compiler User's Guide* — https://onlinedocs.microchip.com/
- [XC8 setup](../Guides/Toolchains/xc8.md)
