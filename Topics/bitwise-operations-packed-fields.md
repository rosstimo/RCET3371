<a id="top"></a>
# RCET 3371 — Bitwise Operations and Packed Fields

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

Registers, status bytes, protocol fields, and compact program state often place several independent meanings inside one byte. Bitwise operations let you inspect or change one field without destroying its neighbors.

[Back to top](#top) · [Topics index](README.md)

<a id="outcomes"></a>
## 2. What you should be able to do

You should be able to test one bit, extract a field, shift a field into position, build masks, and perform grab → modify → put-back in C#, Python, embedded C, and PIC16F883 pic-as.

[Back to top](#top) · [Topics index](README.md)

<a id="prerequisites"></a>
## 3. Prerequisites and related topics

Review [Numeric Representation, Width, and Range](numeric-representation-width-range.md) first. Arithmetic carry/borrow is treated separately in [Binary Addition, Adders, and Two's-Complement Subtraction](binary-addition-adders-twos-complement.md).

[Back to top](#top) · [Topics index](README.md)

<a id="core-model"></a>
## 4. Core model and vocabulary

## Bitwise operations begin with one bit

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

## Field example: extract a nibble

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

## Grab, modify, put back

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

## Try the same bounded operation in Python

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

[Back to top](#top) · [Topics index](README.md)

<a id="how-it-works"></a>
## 5. How it works

The reusable pattern is:

~~~text
grab field with mask/shift
        ↓
modify only the extracted value
        ↓
clear only the destination field
        ↓
shift replacement into position
        ↓
OR replacement with preserved neighbors
~~~

Write the masks in binary at least once. A hexadecimal mask is useful only after you know which bits it preserves and clears.

[Back to top](#top) · [Topics index](README.md)

<a id="worked-examples"></a>
## 6. Worked examples

Use the same input in every environment:

~~~text
before = 0xA5 = 1010 0101
upper nibble = 0xA
increment upper nibble
after = 0xB5 = 1011 0101
~~~

The lower nibble remains `0101`. That invariant is the verification target. The C#, Python, embedded-C, and pic-as implementations in the core examples all implement the same behavior contract.

[Back to top](#top) · [Topics index](README.md)

<a id="apply-verify-troubleshoot"></a>
## 7. Apply, verify, and troubleshoot

When a packed-field operation corrupts state:

1. write the original byte in binary;
2. mark the owned field;
3. mark bits that must remain unchanged;
4. calculate the extraction mask;
5. calculate the preservation mask;
6. predict the final byte;
7. compare the program/register result to the prediction.

Do not debug a packed byte only in hexadecimal when the failing relationship is bit-level.

[Back to top](#top) · [Topics index](README.md)

<a id="practice"></a>
## 8. Practice

1. Test bit 5 of `1010 0101`.
2. Extract bits 6:4 from `1101 1010`.
3. Increment the upper nibble of `1111 0011` modulo 16 while preserving the lower nibble.
4. Starting from `0xA5`, trace grab-modify-put-back and predict the result.
5. Why is `state = newValue << 4` usually wrong when only the upper nibble should change?
6. In the pic-as example, identify the operation that clears the old field and the operation that combines the replacement with preserved bits.

[Back to top](#top) · [Topics index](README.md)

<a id="answer-key"></a>
## 9. Answer key

1. Mask with `0010 0000`; the result is nonzero.
2. `101` = 5.
3. Upper nibble 15 increments to 0; result `0000 0011`.
4. `0xB5`.
5. It discards unrelated bits instead of preserving them.
6. AND with the preservation mask removes the old destination field; OR combines the replacement field with preserved bits.

[Back to top](#top) · [Topics index](README.md)

<a id="without-notes"></a>
## 10. What you should be able to explain without notes

Explain a one-bit mask, field extraction, shifting, field preservation, and grab-modify-put-back. Be able to trace the same packed-field operation across C#, Python, embedded C, and PIC assembly.

[Back to top](#top) · [Topics index](README.md)

<a id="references"></a>
## 11. References

- Microsoft, *Bitwise and shift operators (C# reference)* — https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators
- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet*, DS41291F, Section 15 — https://ww1.microchip.com/downloads/en/DeviceDoc/41291F.pdf
- Microchip Technology Inc., *MPLAB XC8 PIC Assembler User's Guide* — https://onlinedocs.microchip.com/oxy/GUID-4DC87671-9D8E-428A-ADFE-98D694F9F089/

[Back to top](#top) · [Topics index](README.md)
