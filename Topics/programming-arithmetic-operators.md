<a id="top"></a>
# RCET 3371 — Programming Arithmetic Operators

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

RCET 2265 used C# arithmetic constantly: addition, subtraction, multiplication, integer division, remainder, increment/decrement, compound assignment, and expression precedence. RCET 3371 keeps that familiar C# behavior as the baseline, then asks what stays the same and what changes in Python, embedded C, and PIC assembly.

The operator symbol is only the surface. The important questions are:

- what operation is requested?
- what types and widths are involved?
- how is division defined?
- where does a remainder go?
- does the target processor have one instruction for the operation?
- what flags or overflow behavior matter?

[Back to top](#top) · [Topics index](README.md)

<a id="outcomes"></a>
## 2. What you should be able to do

You should be able to:

- predict arithmetic expressions using +, -, *, /, and remainder/modulo operations;
- explain integer division versus floating-point division;
- use increment/decrement and compound assignment without hiding important state changes;
- compare C#, Python, and C arithmetic syntax and semantics;
- identify the important negative-integer division difference in Python;
- explain why exponentiation is not represented by the same syntax in the three languages;
- map basic arithmetic operations to PIC16F883 instructions or to a multi-instruction algorithm when no single instruction exists;
- recognize when width, overflow, carry, or borrow changes the meaning of a result.

[Back to top](#top) · [Topics index](README.md)

<a id="prerequisites"></a>
## 3. Prerequisites and related topics

Review [Numeric Representation, Width, and Range](numeric-representation-width-range.md) first.

This Topic introduces the programming-language surface. The digital hardware underneath addition/subtraction is developed in [Binary Addition, Adders, and Two's-Complement Subtraction](binary-addition-adders-twos-complement.md). Multiplication without using the language's multiply operator is developed in [Shift-and-Add Binary Multiplication](binary-multiplication-shift-add.md). Division without using the language's divide operator is developed in [Register-Based Binary Division](binary-division-register-algorithm.md).

[Back to top](#top) · [Topics index](README.md)

<a id="core-model"></a>
## 4. Core model and vocabulary

For arithmetic expressions, keep five ideas separate:

1. **operator** — the requested operation;
2. **operands** — the values supplied to the operation;
3. **type/width** — how those values and the result are represented;
4. **evaluation order** — which operation is performed first;
5. **target implementation** — compiler/library instruction sequence or explicit assembly algorithm.

For integer division:

~~~text
dividend = quotient × divisor + remainder
~~~

For positive integers:

~~~text
9 / 4 = 2 remainder 1
9 = 2 × 4 + 1
~~~

That quotient/remainder relationship is more durable than any one language's syntax.

[Back to top](#top) · [Topics index](README.md)

<a id="how-it-works"></a>
## 5. How it works

<a id="csharp-recap"></a>
### C# recap from RCET 2265

~~~csharp
int a = 9;
int b = 4;

int sum = a + b;        // 13
int difference = a - b; // 5
int product = a * b;    // 36
int quotient = a / b;   // 2
int remainder = a % b;  // 1

double ratio = 9.0 / 4; // 2.25
~~~

When both C# operands are integral types, division truncates toward zero.

Increment/decrement:

~~~csharp
int count = 3;

count++;
count--;

count += 4;
count -= 2;
count *= 3;
count /= 2;
count %= 5;
~~~

Keep prefix/postfix updates out of larger expressions unless the timing of the update is actually part of what you intend to communicate.

<a id="python-mirror"></a>
### Python mirror

~~~python
a = 9
b = 4

sum_value = a + b        # 13
difference = a - b       # 5
product = a * b          # 36
ratio = a / b            # 2.25
quotient = a // b        # 2
remainder = a % b        # 1

count = 3
count += 1
count -= 1
~~~

Python has no C#/C-style ++ or -- operator. Use += 1 or -= 1.

Python also has an exponentiation operator:

~~~python
power = 2 ** 3  # 8
~~~

<a id="c-mirror"></a>
### Embedded C mirror

~~~c
#include <stdint.h>

int a = 9;
int b = 4;

int sum = a + b;        // 13
int difference = a - b; // 5
int product = a * b;    // 36
int quotient = a / b;   // 2
int remainder = a % b;  // 1

double ratio = 9.0 / 4.0;
~~~

For integer operands in modern C, division truncates toward zero.

Fixed-width embedded code should make width deliberate when width matters:

~~~c
uint8_t first = 250u;
uint8_t second = 10u;
uint8_t result = (uint8_t)(first + second);
~~~

The stored 8-bit result wraps modulo 256.

<a id="negative-division"></a>
### Important cross-language difference: negative integer division

For positive integers, C#, C, and Python's // operator look similar. Negative values expose a semantic difference.

~~~text
C#:     -7 / 3  = -2      -7 % 3 = -1
C:      -7 / 3  = -2      -7 % 3 = -1
Python: -7 // 3 = -3      -7 % 3 =  2
~~~

C# and C truncate the quotient toward zero. Python floor division rounds the quotient downward toward negative infinity, then chooses the remainder so the quotient/remainder identity still holds.

Python's ordinary / operator is floating-point true division:

~~~python
-7 / 3  # approximately -2.3333333333
~~~

<a id="precedence"></a>
### Precedence and grouping

~~~csharp
int first = 3 + 4 * 2;       // 11
int second = (3 + 4) * 2;    // 14
~~~

The same basic precedence relationship exists in Python and C. Parentheses are still useful when they make intent easier to read.

Do not rely on a slogan alone. Be able to trace the actual expression.

<a id="exponentiation"></a>
### Exponentiation is not the same operator everywhere

- C#: Math.Pow(2, 3)
- Python: 2 ** 3 or pow(2, 3)
- C: a library function such as pow() when floating-point exponentiation is needed
- PIC16F883 pic-as: no general exponentiation instruction; choose an algorithm appropriate to the required integer/fixed-point problem.

<a id="division-by-zero"></a>
### Division by zero is not portable behavior

A divisor of zero must be treated as a deliberate error case.

- C# integer division by zero throws `DivideByZeroException`.
- Python division, floor division, and remainder by zero raise `ZeroDivisionError`.
- C integer division or remainder by zero has undefined behavior.
- PIC16F883 has no built-in divide instruction, so a software division routine must reject a zero divisor explicitly.

Do not let a cross-language example imply that divide-by-zero behavior is interchangeable.

<a id="assembly-mirror"></a>
### PIC16F883 pic-as mirror

The PIC16F883 instruction set makes the underlying operations more explicit.

| Programming operation | PIC16F883 idea |
| --- | --- |
| add | ADDWF / ADDLW |
| subtract | SUBWF / SUBLW |
| increment | INCF |
| decrement | DECF |
| multiply | no general multiply instruction; use an algorithm |
| divide | no general divide instruction; use an algorithm |
| remainder | produced as part of a division algorithm |
| unary negate | form two's complement, for example complement + increment or 0 - value |
| compound assignment | execute the operation directly on the destination register |

Example addition:

~~~assembly
    movlw   4
    movwf   second_value

    movlw   9
    addwf   second_value,w   ; W = 13
~~~

Example subtraction:

~~~assembly
    movlw   4
    movwf   subtrahend

    movlw   9
    movwf   minuend

    movf    subtrahend,w
    subwf   minuend,w        ; W = 9 - 4 = 5
~~~

For SUBWF on this PIC family, the Carry bit uses reversed borrow polarity: C = 1 means no borrow was required; C = 0 means a borrow occurred.

The subtraction itself is implemented using the two's-complement method. That connects the assembly instruction directly to the digital-adder model in the next Topic.

[Back to top](#top) · [Topics index](README.md)

<a id="worked-examples"></a>
## 6. Worked examples

### Example 1: one expression in three languages

Evaluate:

~~~text
9 + 4 * 2
~~~

Multiplication occurs first:

~~~text
4 * 2 = 8
9 + 8 = 17
~~~

C#, Python, and C all produce 17 for ordinary integer operands.

### Example 2: quotient and remainder

For 29 divided by 8:

~~~text
quotient = 3
remainder = 5

29 = 3 × 8 + 5
~~~

The two outputs are one mathematical relationship. Later, the binary division algorithm will produce both explicitly.

### Example 3: width changes the stored result

An 8-bit unsigned target computes:

~~~text
250 + 10 = 260 mathematical result
260 modulo 256 = 4 stored 8-bit result
~~~

C# can detect or control overflow depending on context, Python will naturally retain 260, and an 8-bit PIC register can only retain the low eight bits.

[Back to top](#top) · [Topics index](README.md)

<a id="apply-verify-troubleshoot"></a>
## 7. Apply, verify, and troubleshoot

When arithmetic output is surprising:

1. write operand values and types;
2. mark integer versus floating-point division;
3. add parentheses and trace precedence;
4. calculate the expected mathematical result;
5. calculate the target-width stored result;
6. check quotient/remainder together;
7. on PIC, inspect C/DC/Z when the instruction is documented to affect them;
8. for SUBWF/SUBLW, verify operand order before assuming the arithmetic is wrong.

A common assembly error is reading SUBWF as W - f. On PIC16F883 it is f - W.

[Back to top](#top) · [Topics index](README.md)

<a id="practice"></a>
## 8. Practice

1. With a = 17 and b = 5, predict a+b, a-b, a*b, a/b, and a%b in C#.
2. What does Python produce for 17/5 and 17//5?
3. Explain why -7//3 in Python differs from integer -7/3 in C#.
4. Rewrite count++ in Python.
5. What is the result of 3 + 4 * 2? What does (3 + 4) * 2 produce?
6. Which PIC instruction family performs f - W?
7. If an 8-bit unsigned value stores 255 and is incremented, what bit pattern remains?
8. Why should multiplication/division on PIC16F883 be treated as algorithms rather than assumed single instructions?
9. What is 29 / 8 as quotient and remainder?
10. Why must a portable division algorithm reject divisor zero explicitly?
11. Why is C# Math.Pow not equivalent to a built-in exponentiation operator?

[Back to top](#top) · [Topics index](README.md)

<a id="answer-key"></a>
## 9. Answer key

1. 22, 12, 85, 3, 2.
2. 3.4 and 3.
3. Python // is floor division; C# integer / truncates toward zero.
4. count += 1.
5. 11 and 14.
6. SUBWF; verify its operand order as f - W.
7. 0000 0000.
8. The PIC16F883 instruction set has add/subtract/rotate/increment/decrement primitives but no general multiply or divide instruction, so software must compose the operation.
9. quotient 3, remainder 5.
10. C#, Python, C, and a PIC software routine do not share one automatic divide-by-zero behavior; define and handle the error at the interface.
11. C# uses a method call such as Math.Pow; there is no C# ** exponentiation operator.

[Back to top](#top) · [Topics index](README.md)

<a id="without-notes"></a>
## 10. What you should be able to explain without notes

Explain +, -, *, /, remainder, integer versus floating division, increment/decrement, compound assignment, precedence, the Python negative-division difference, fixed-width wraparound, and how those ideas map to PIC16F883 instructions or algorithms.

[Back to top](#top) · [Topics index](README.md)

<a id="references"></a>
## 11. References

- RCET 2265, [Basic Operators in C#](https://github.com/rosstimo/RCET2265/blob/main/Topics/basic_operators.md)
  - Used for: the incoming C# arithmetic baseline.
- RCET 2265, [Math & the Math Class](https://github.com/rosstimo/RCET2265/blob/main/Topics/math_the_math_class.md)
  - Used for: integer division, Math methods, and precedence baseline.
- Microsoft, *Arithmetic operators (C# reference)* — https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators
- Python Software Foundation, *Expressions* — https://docs.python.org/3/reference/expressions.html
- Microchip Technology Inc., *PIC16F882/883/884/886/887 Data Sheet*, DS41291F, Section 15 — https://ww1.microchip.com/downloads/en/DeviceDoc/41291F.pdf
  - Used for: ADDWF, SUBWF, SUBLW, INCF, DECF, STATUS carry/borrow, and rotate behavior.

[Back to top](#top) · [Topics index](README.md)
