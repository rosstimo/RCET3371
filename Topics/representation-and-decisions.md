# RCET 3371 - Representation and Decisions

*Self-learning guide*

[Topics index](README.md)

## 1. Start with a familiar C# integer

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

## 2. Value, representation, and type

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

## 3. Width and range

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

## 4. Bitwise operations begin with one bit

Suppose:

```text
value = 0010 1010
mask  = 0000 0010
```

AND the values:

```text
0010 1010
0000 0010
---------
0000 0010
```

Bit 1 is set.

In C#:

```csharp
byte value = 0b0010_1010;
bool bit1IsSet = (value & 0b0000_0010) != 0;
```

First understand that one-bit case.

Then expand to fields.

## 5. Field example: extract a nibble

Suppose:

```text
state = 1010 0101
```

The upper four bits contain a count.

### Step 1: mask

```text
state       1010 0101
mask        1111 0000
AND         1010 0000
```

### Step 2: shift right

```text
1010 0000 >> 4 = 0000 1010
```

The count is decimal 10.

C#:

```csharp
byte state = 0b1010_0101;
int count = (state & 0xF0) >> 4;

Console.WriteLine(count); // 10
```

## 6. Grab, modify, put back

Now increment the upper nibble while preserving the lower flags.

```csharp
byte state = 0b1010_0101;

int count = (state & 0xF0) >> 4;  // grab
count = (count + 1) & 0x0F;      // modify within 4 bits
state = (byte)((state & 0x0F) | (count << 4)); // put back
```

Trace it:

```text
before: 1010 0101
count:  1010
+1:     1011
after:  1011 0101
```

Notice the lower nibble stayed `0101`.

That preservation is the reason for the masks.

### The same grab-modify-put-back operation in embedded C

On the PIC side, the intended width should be explicit:

```c
#include <stdint.h>

uint8_t state = 0xA5u;

uint8_t count = (uint8_t)((state & 0xF0u) >> 4);  // grab
count = (uint8_t)((count + 1u) & 0x0Fu);          // modify
state = (uint8_t)((state & 0x0Fu) | (count << 4)); // put back
```

Expected result:

```text
before: 0xA5 = 1010 0101
count:  0x0A
+1:     0x0B
after:  0xB5 = 1011 0101
```

The algorithm is the same as C#. The important C difference is that `uint8_t` states the intended 8-bit storage width directly.

### The same operation in PIC16F883 pic-as

Use the same input and expected result. The code below uses common RAM so the example can stay focused on representation rather than banking.

```assembly
state_value     EQU 0x70
count_value     EQU 0x71
new_upper       EQU 0x72

    movlw   0xA5
    movwf   state_value

    ; grab upper nibble: 0xA5 -> 0x0A
    swapf   state_value,w
    andlw   0x0F
    movwf   count_value

    ; modify within four bits: 0x0A -> 0x0B
    incf    count_value,f
    movlw   0x0F
    andwf   count_value,f

    ; prepare 0xB0
    swapf   count_value,w
    andlw   0xF0
    movwf   new_upper

    ; preserve lower nibble and put the new upper nibble back
    movlw   0x0F
    andwf   state_value,f
    movf    new_upper,w
    iorwf   state_value,f
```

Final `state_value`:

```text
0xB5 = 1011 0101
```

In assembly, the masks are not hidden inside a typed expression. You can see the byte being transformed one instruction at a time. Some instructions in this sequence also affect CPU `STATUS` flags; that is separate from preserving the lower-nibble application flags stored in `state_value`.

## 7. Try the same bounded operation in Python

```python
state = 0b1010_0101

count = (state & 0xF0) >> 4
count = (count + 1) & 0x0F
state = (state & 0x0F) | (count << 4)

print(f"{state:08b}")
```

Expected:

```text
10110101
```

The algorithm is nearly identical.

Python integers are not normally limited to 8 bits, which is why explicit masks matter when you want hardware-like width behavior.

## 8. Multi-byte value

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

## 9. Overflow

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

## 10. Engineering display versus stored value

Suppose the internal value is:

```text
0.000004732 V
```

A display may show:

```text
4.73 uV
```

Formatting the display should not destroy the extra internal precision unless the specification explicitly requires quantization.

## 11. Practice

1. Write decimal 42 as 8-bit binary and hex.
2. What is the unsigned range of 4 bits? Of 8 bits?
3. Test bit 5 of `1010 0101`.
4. Extract bits 6:4 from `1101 1010`.
5. Increment the upper nibble of `1111 0011` modulo 16 while preserving the lower nibble.
6. Reconstruct `0xBE` high and `0xEF` low into a 16-bit value.
7. Translate the upper-nibble grab-modify-put-back operation into embedded C and predict the final byte before running it.
8. In the pic-as example, which instructions preserve the lower nibble by masking and recombining the byte?
9. Why does Python sometimes need an explicit mask when imitating an 8-bit target?
10. Why should formatting generally happen after calculation?

## 12. Answer reasoning

1. `0010 1010`, `0x2A`.
2. 0-15 and 0-255.
3. Mask with `0010 0000`; result is nonzero, so bit 5 is set.
4. `101` = 5.
5. Upper nibble 15 increments to 0; result `0000 0011`.
6. `0xBEEF`.
7. The embedded C version should produce `0xB5`; the same masks preserve the lower nibble while the upper field changes from `0xA` to `0xB`.
8. `andwf state_value,f` with `0x0F` clears the old upper nibble while retaining the lower nibble; `iorwf state_value,f` combines the prepared upper nibble with those preserved lower bits.
9. Ordinary Python integers can grow beyond the intended hardware width.
10. Early rounding discards information that later calculations may need.

## 13. Ready to continue when

Explain and demonstrate:

- value versus representation;
- width and range;
- one-bit mask;
- multi-bit field extraction;
- grab -> modify -> put back;
- multi-byte reconstruction;
- why unrelated bits must be preserved;
- storage precision versus display formatting.

## 14. References

- C# integral types: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
- C# bitwise operators: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators
- Python numeric types: https://docs.python.org/3/library/stdtypes.html#numeric-types-int-float-complex
- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet*, DS40001291, Section 15 "Instruction Set Summary" — https://www.microchip.com/en-us/product/PIC16F883
  - Used for: PIC16F883 byte-oriented, bit-oriented, literal, and status-flag behavior.
- Microchip Technology Inc., *MPLAB XC8 PIC Assembler User's Guide* — https://onlinedocs.microchip.com/oxy/GUID-4DC87671-9D8E-428A-ADFE-98D694F9F089/
  - Used for: current `pic-as` assembler/tool syntax and build model.
- [RCET C# / Python / Embedded C / PIC Assembly comparison](../References/csharp-python-c-picas-comparison.md)
