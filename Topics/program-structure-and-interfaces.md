# RCET 3371 - From Methods and Classes to Program Structure

*Self-learning guide*

[Topics index](README.md)

## 1. Start with what you already know

RCET 2265 introduced methods, parameters, return values, scope, classes, properties, constructors, and multiple source files.

RCET 3371 does not replace those ideas. It asks you to use them more deliberately, then recognize their equivalents in other languages.

The progression is:

```text
one familiar C# program
        ↓
small methods with clear jobs
        ↓
classes/files with clear jobs
        ↓
same small logic in Python and C
        ↓
modules/interfaces when they solve a real problem
```

## 2. Outcomes

You should be able to:

- describe a method by its inputs, output, and side effects;
- distinguish local state from data that must survive after a method returns;
- split a small C# program into files by responsibility;
- translate a small, already-tested method into Python and C;
- explain a Python module and a C header/source pair at a practical level;
- trace call and return behavior;
- recognize why an interface can be useful without being expected to design a large interface architecture immediately.

## 3. Worked example: begin in one C# file

Suppose the entire program starts like this:

```csharp
double voltage = 2.4;
double current = 0.015;

double power = voltage * current;

Console.WriteLine($"Power = {power:F3} W");
```

It works, but there is no reusable calculation.

### Step 1: extract a familiar method

```csharp
double voltage = 2.4;
double current = 0.015;

double power = CalculatePower(voltage, current);

Console.WriteLine($"Power = {power:F3} W");

static double CalculatePower(double voltage, double current)
{
    return voltage * current;
}
```

Write the contract in plain language:

- inputs: voltage and current;
- output: calculated power;
- side effects: none;
- retained state: none.

That is not advanced architecture. It is a precise description of a method you already know how to write.

## 4. Add a simple class only when it helps

Now suppose each measurement belongs together.

```csharp
Measurement sample = new(2.4, 0.015);

Console.WriteLine($"Power = {sample.Power:F3} W");

class Measurement
{
    public double Voltage { get; }
    public double Current { get; }

    public double Power => Voltage * Current;

    public Measurement(double voltage, double current)
    {
        Voltage = voltage;
        Current = current;
    }
}
```

The class groups related data and behavior.

A reasonable next step is to move `Measurement` to `Measurement.cs`. The behavior does not change just because the source is in a second file.

## 5. Split by responsibility, not file length

Imagine a program that reads measurements from text and prints a report.

A beginner version might put everything in `Program.cs`.

A clearer next version could be:

```text
Program.cs       startup and overall flow
Measurement.cs   data model
Parser.cs        text -> Measurement
Statistics.cs    calculations across measurements
```

Each file has a reason to exist.

Do not create ten files just because "advanced programs use many files."

## 6. Translate a known method to Python

C#:

```csharp
static double CalculatePower(double voltage, double current)
{
    return voltage * current;
}
```

Python:

```python
def calculate_power(voltage, current):
    return voltage * current
```

Before discussing Python modules, prove the function behaves the same:

```python
print(calculate_power(2.4, 0.015))
```

Expected result is approximately `0.036`.

## 7. Translate the same method to C

```c
double calculate_power(double voltage, double current)
{
    return voltage * current;
}
```

Again, the algorithm and contract stayed nearly identical.

Now new C concepts can be introduced one at a time.

## 8. Header/source pair

After a one-file C program works, split it.

`power.h`:

```c
#ifndef POWER_H
#define POWER_H

double calculate_power(double voltage, double current);

#endif
```

`power.c`:

```c
#include "power.h"

double calculate_power(double voltage, double current)
{
    return voltage * current;
}
```

The header tells callers what is available. The source contains the implementation.

Do not start with the words "translation unit" and "external symbol." First build the two-file example. Then those terms have something concrete to describe.

## 9. Scope and lifetime from a familiar example

This variable is local:

```csharp
static int AddOne(int value)
{
    int result = value + 1;
    return result;
}
```

`result` is available only inside the method and is no longer needed after the method returns.

This form field must survive between events:

```csharp
private int count = 0;
```

**Scope** asks where the name can be used.

**Lifetime** asks how long the data/storage remains relevant/alive.

## 10. Interface as a later tool

RCET 2265 intentionally did not require interfaces as an application-design tool. This course introduces them after ordinary classes and method boundaries make sense.

Suppose code initially depends directly on one class:

```csharp
SerialDevice device = new();
```

Later, you may want the same program logic to work with:

- the real serial device;
- a simulated device used for tests.

An interface can describe the small behavior both provide:

```csharp
interface IDevice
{
    string Read();
}
```

At this stage, the important idea is simply:

> The caller can depend on a small behavior contract instead of one concrete device.

You are not expected to build a dependency-injection framework.

## 11. Call and return near the processor

A C#, Python, or C function call looks high-level. A processor still has to:

1. remember where execution should return;
2. transfer control to the called routine;
3. operate on data;
4. return to the caller.

PIC assembly makes those steps more visible with `call` and `return`.

Use tiny subroutines first. Stack-depth analysis comes when nested calls make it relevant.

## 12. Practice

1. For `CalculatePower`, identify input, output, side effect, and retained state.
2. Move `Measurement` to its own C# file. What behavior should change?
3. Translate `CalculatePower` to Python and C.
4. Why should a header/source split usually follow a working one-file example for a beginner?
5. Is a four-line file automatically evidence of good design? Why or why not?
6. Give one reason an interface might become useful later.
7. Explain scope versus lifetime using the Click-counter example.

## 13. Answer reasoning

1. Inputs: voltage/current. Output: power. Side effects: none. Retained state: none.
2. None, assuming the class remains part of the same project/namespace setup.
3. Syntax changes, but multiply two inputs and return the result remains the contract.
4. It separates learning the algorithm from learning the build/module structure.
5. No. Files should separate meaningful responsibilities, not satisfy a line-count rule.
6. It can let the same caller work with a real and simulated implementation.
7. A local handler variable is scoped to the method and recreated per call; a form field can remain available across events.

## 14. Ready to continue when

Explain without notes:

- method contract;
- local versus retained state;
- why multiple files exist;
- Python module/C header-source purpose at a practical level;
- why interfaces are introduced only after ordinary class boundaries make sense;
- why translation is about preserving behavior, not copying punctuation.
