# RCET 3371 - Representation and Decisions

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 3](../LearningPath/03-Representation-and-Decisions.md)

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

C#, C, Python, and assembly do not all expose overflow in exactly the same way. The course will compare those differences after the intended width is clear.

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
7. Why does Python sometimes need an explicit mask when imitating an 8-bit target?
8. Why should formatting generally happen after calculation?

## 12. Answer reasoning

1. `0010 1010`, `0x2A`.
2. 0-15 and 0-255.
3. Mask with `0010 0000`; result is nonzero, so bit 5 is set.
4. `101` = 5.
5. Upper nibble 15 increments to 0; result `0000 0011`.
6. `0xBEEF`.
7. Ordinary Python integers can grow beyond the intended hardware width.
8. Early rounding discards information that later calculations may need.

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
