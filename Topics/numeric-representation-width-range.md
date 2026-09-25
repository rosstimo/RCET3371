<a id="top"></a>
# RCET 3371 — Numeric Representation, Width, and Range

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

Programming and digital hardware both operate on finite representations. Before arithmetic, bit fields, protocols, or registers make sense, you need to separate the **quantity** you mean from the **bit pattern**, **type**, and **storage width** used to represent it.

[Back to top](#top) · [Topics index](README.md)

<a id="outcomes"></a>
## 2. What you should be able to do

After this Topic you should be able to:

- represent the same value in decimal, binary, and hexadecimal;
- calculate unsigned and two's-complement signed ranges from a bit width;
- explain value versus representation versus type;
- reconstruct a multi-byte value when byte order is specified;
- predict fixed-width overflow behavior;
- compare width behavior in C#, Python, embedded C, and PIC16F883 registers;
- keep engineering calculation precision separate from display formatting.

[Back to top](#top) · [Topics index](README.md)

<a id="prerequisites"></a>
## 3. Prerequisites and related topics

RCET 2265 experience with C# integers, arithmetic, conversions, binary/hexadecimal display, and basic data types is sufficient.

Continue from here to [Programming Arithmetic Operators](programming-arithmetic-operators.md), [Binary Addition, Adders, and Two's-Complement Subtraction](binary-addition-adders-twos-complement.md), and [Bitwise Operations and Packed Fields](bitwise-operations-packed-fields.md).

[Back to top](#top) · [Topics index](README.md)

<a id="core-model"></a>
## 4. Core model and vocabulary

## Start with a familiar C# integer

RCET 2265 already used integer variables, arithmetic, comparisons, binary/hexadecimal values, and conversions.

Start here:

```csharp
int value = 42;

Console.WriteLine(value);
Console.WriteLine(Convert.ToString(value, 2));
Console.WriteLine(value.ToString("X"));
```

The same value can be displayed as decimal, binary, or hexadecimal.

That is the bridge into representation.

## Value, representation, and type

Keep these questions separate:

1. **Value:** what quantity/state do I mean?
2. **Representation:** which bits/bytes encode it?
3. **Type:** how does this language interpret/store it?
4. **Operation:** what am I doing to it?

For example:

```text
value:          42
8-bit binary:   0010 1010
hex:            2A
```

The quantity did not change when the display format changed.

## Width and range

An unsigned 8-bit value has 256 possible patterns:

```text
0000 0000 through 1111 1111
0 through 255
```

For unsigned `n` bits:

```text
minimum = 0
maximum = 2^n - 1
```

A common two's-complement signed `n`-bit range is:

```text
-2^(n-1) through 2^(n-1)-1
```

Do several 4-bit and 8-bit examples by hand before treating the formulas as shortcuts.

## Multi-byte value

Suppose a protocol gives:

```text
high = 0x12
low  = 0x34
```

and states that the high byte comes first.

```csharp
int value = (0x12 << 8) | 0x34;
```

Result:

```text
0x1234 = 4660
```

Do not memorize "shift the first byte." The protocol tells you which byte is high and which is low.

## Overflow

Fixed-width types have finite ranges.

If an operation is supposed to behave like an 8-bit register, test boundary values such as:

- 0;
- 1;
- 254;
- 255.

C#, C, Python, and assembly do not all expose overflow in exactly the same way.

Use the same boundary case in several environments:

```text
start = 255
add 1
```

Python's ordinary integer grows to `256`.

For an explicitly 8-bit unsigned C value:

```c
uint8_t value = 255u;
value = (uint8_t)(value + 1u);
```

the stored result becomes `0`.

On the PIC16F883:

```assembly
value           EQU 0x70

    movlw   255
    movwf   value
    incf    value,f
```

the 8-bit file register also returns to `0x00`. The instruction's documented status-flag effects are part of the processor behavior and should be checked whenever those flags matter to surrounding code.

The lesson is not "all languages overflow the same way." The lesson is to state the required width first, then verify how the chosen language or processor realizes that width.

## Engineering display versus stored value

Suppose the internal value is:

```text
0.000004732 V
```

A display may show:

```text
4.73 uV
```

Formatting the display should not destroy the extra internal precision unless the specification explicitly requires quantization.

[Back to top](#top) · [Topics index](README.md)

<a id="how-it-works"></a>
## 5. How it works

For any numerical operation, ask in this order:

1. What quantity or state is intended?
2. What width is required?
3. Is the interpretation signed or unsigned?
4. What representation or byte order is used?
5. What happens at the range boundary?
6. Is the displayed format different from the stored value?

The same questions apply to a C# variable, a Python model of hardware, an XC8 `uint8_t`, and an 8-bit PIC file register.

[Back to top](#top) · [Topics index](README.md)

<a id="worked-examples"></a>
## 6. Worked examples

Trace these boundary cases before relying on a debugger:

~~~text
4-bit unsigned: 0000 through 1111 = 0 through 15
8-bit unsigned: 0000 0000 through 1111 1111 = 0 through 255
8-bit signed two's complement: -128 through +127
~~~

For an explicitly 8-bit result, `255 + 1` returns to bit pattern `0000 0000`. Python's ordinary integer instead grows to 256 unless you deliberately model the width.

[Back to top](#top) · [Topics index](README.md)

<a id="apply-verify-troubleshoot"></a>
## 7. Apply, verify, and troubleshoot

When a numerical result is wrong, check width and interpretation before changing the algorithm.

Useful questions:

- Did a value exceed its type/register range?
- Did signed and unsigned interpretations get mixed?
- Did Python hide a fixed-width wraparound that the target hardware cannot?
- Is byte order reversed?
- Was a value rounded for display and then reused for calculation?

Use boundary vectors such as 0, 1, maximum-1, and maximum.

[Back to top](#top) · [Topics index](README.md)

<a id="practice"></a>
## 8. Practice

1. Write decimal 42 as 8-bit binary and hexadecimal.
2. Give the unsigned ranges for 4, 8, and 12 bits.
3. Give the signed two's-complement range for 8 bits.
4. Reconstruct high byte `0xBE` and low byte `0xEF` as a big-endian 16-bit value.
5. Predict the stored 8-bit result of 255 + 1.
6. Explain why Python can hide a fixed-width overflow defect.
7. Explain why display formatting should usually happen after calculation.

[Back to top](#top) · [Topics index](README.md)

<a id="answer-key"></a>
## 9. Answer key

1. `0010 1010`, `0x2A`.
2. 0–15, 0–255, and 0–4095.
3. -128 through +127.
4. `0xBEEF`.
5. `0x00`; surrounding flags depend on the target operation.
6. Ordinary Python integers grow beyond 8 bits unless width is deliberately modeled.
7. Early rounding discards information that later calculations may need.

[Back to top](#top) · [Topics index](README.md)

<a id="without-notes"></a>
## 10. What you should be able to explain without notes

Explain value versus representation, signed versus unsigned width, fixed-width range, multi-byte reconstruction, overflow/wraparound, and why hardware width must sometimes be modeled explicitly in a high-level language.

[Back to top](#top) · [Topics index](README.md)

<a id="references"></a>
## 11. References

- Microsoft, *Integral numeric types (C# reference)* — https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
  - Used for: C# integer widths and ranges.
- Python Software Foundation, *Numeric Types* — https://docs.python.org/3/library/stdtypes.html#numeric-types-int-float-complex
  - Used for: Python integer behavior.
- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet*, DS41291F, Section 2.2.2.1 and Section 15 — https://ww1.microchip.com/downloads/en/DeviceDoc/41291F.pdf
  - Used for: PIC16F883 STATUS arithmetic flags and instruction behavior.

[Back to top](#top) · [Topics index](README.md)
