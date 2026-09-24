# RCET 3371 - Toolchains and First Cross-Language Programs

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 2](../LearningPath/02-Toolchains-Recovery-and-Desktop-Events.md) · [Complete setup guide](../Guides/Toolchains/README.md)

## 1. Why this matters

You already know how a small C# program behaves. RCET 3371 adds several languages and development environments, but you should not have to learn a new algorithm, a new language, and a new toolchain at the same instant.

In this section, the program stays tiny while the environment changes.

The first goal is practical:

> Can you create the project, build or run it, and see the expected result?

Only after that works do we attach names such as compiler, interpreter, runtime, target, and debugger.

## 2. What carries over from RCET 2265

You already know these ideas:

- variables hold values;
- expressions produce results;
- methods can take inputs and return outputs;
- a program follows control flow;
- a Windows Forms Button can raise a Click event;
- the debugger can stop execution so you can inspect variables.

Those ideas do not disappear in Python, C, or assembly. Their syntax and implementation change.

## 3. Outcomes

You should be able to:

- install or verify the four course programming environments;
- create and run a small C# console program;
- create and run the equivalent Python program;
- create and build a minimal PIC16F883 XC8 project;
- create and build a minimal PIC16F883 pic-as project;
- use a breakpoint in C# and Python;
- distinguish build, run, and debug;
- identify the basic role of Visual Studio/.NET, Python/VS Code, MPLAB X, XC8, pic-as, the simulator, and the PIC target;
- create a simple Windows Forms Button event without needing custom-paint architecture.

## 4. Set up first

Use the [Course Toolchain Setup Guide](../Guides/Toolchains/README.md).

Do not continue past a toolchain until its first checkpoint works.

A useful setup record looks like:

| Environment | Check | Result |
| --- | --- | --- |
| C# | `dotnet --version` | 10.x |
| C# | Console App build/run | pass |
| Python | `python --version` or `py -3.14 --version` | 3.14.x |
| Python | `hello.py` | pass |
| XC8 | PIC16F883 Build Main Project | BUILD SUCCESSFUL |
| pic-as | PIC16F883 Build Main Project | BUILD SUCCESSFUL |

## 5. Worked example: one idea, four environments

We will use the same operation:

```text
2 + 3 = 5
```

### Example A: C# familiar baseline

```csharp
int first = 2;
int second = 3;
int answer = Add(first, second);

Console.WriteLine($"{first} + {second} = {answer}");

static int Add(int first, int second)
{
    return first + second;
}
```

Before running it, identify:

- two input variables;
- one returned value;
- one method call;
- one output statement.

Expected output:

```text
2 + 3 = 5
```

Try changing `second` to 8. Predict the output before running again.

### Example B: Python translation

```python
def add(first, second):
    return first + second

first = 2
second = 3
answer = add(first, second)

print(f"{first} + {second} = {answer}")
```

The syntax changed, but the algorithm did not.

| C# | Python |
| --- | --- |
| `static int Add(...)` | `def add(...):` |
| braces | indentation |
| `Console.WriteLine` | `print` |
| `int first = 2` | `first = 2` |

Do not infer that Python has no types. At this point, simply notice that Python does not require the same variable declaration syntax.

### Example C: XC8 C first build

```c
#include <xc.h>
#include <stdint.h>

static uint8_t add(uint8_t first, uint8_t second)
{
    return (uint8_t)(first + second);
}

void main(void)
{
    volatile uint8_t answer = add(2u, 3u);

    while (1)
    {
        (void)answer;
    }
}
```

There is no `Console.WriteLine`. A bare PIC16F883 does not automatically have a console.

For now, success means:

1. PIC16F883 is the selected target;
2. XC8 recognizes and compiles the source;
3. the linker produces the device image;
4. the build finishes successfully.

Later sections make the result observable with the simulator, registers, pins, peripherals, and test equipment.

### Example D: pic-as first build

```asm
RADIX dec
PROCESSOR 16F883

#include <xc.inc>

PSECT resetVect,class=CODE,delta=2
ResetVector:
    goto Main

PSECT code,class=CODE,delta=2
Main:
    movlw   2
    addlw   3
    goto    Main

END
```

For this first example, focus on only three instructions/ideas:

- `movlw 2`: put the literal value 2 in the working register;
- `addlw 3`: add literal 3 to the working register;
- `goto Main`: repeat.

Do not try to memorize the PIC instruction set yet.

## 6. What the tool names mean

Attach this vocabulary to the examples you just ran.

### IDE or editor

Where you edit and organize the project.

Examples:

- Visual Studio;
- VS Code;
- MPLAB X.

### Compiler

Translates source code toward executable machine/target code.

Examples:

- .NET C# compiler/build toolchain;
- XC8 for PIC C.

### Interpreter

Executes a language through an interpreter/runtime environment.

For this course, the practical Python question is:

> Which Python executable is running this file?

### Assembler

Translates assembly source into target machine-code/object information.

The course uses pic-as from the XC8 installation.

### Linker

Combines compiled/assembled pieces and resolves symbols into the final program/image.

You will see linker behavior more clearly when projects contain multiple C or assembly modules.

### Runtime

The environment executing a program.

A .NET console application uses the .NET runtime. Python uses the selected Python runtime/interpreter.

### Target

The system the program is built to run on.

For the embedded work, the target is the PIC16F883.

### Programmer/debugger

Hardware such as the PICkit communicates with a physical microcontroller for programming/debugging. A successful software build does not prove the physical target works.

## 7. Build versus run versus debug

These are different checkpoints.

**Build**

> Can the source/project be translated successfully?

**Run**

> What happens when the program actually executes?

**Debug**

> Can I deliberately stop and inspect execution?

Example:

A C# program may build perfectly and then fail at runtime because it tries to open a file that does not exist.

That is not a compiler error.

## 8. Familiar Windows Forms event

Start with ordinary RCET 2265 event handling.

Form field:

```csharp
private int count = 0;
```

Button handler:

```csharp
private void countButton_Click(object sender, EventArgs e)
{
    count++;
    countLabel.Text = count.ToString();
}
```

Trace three clicks:

| Event | count before | count after | label |
| --- | ---: | ---: | --- |
| click 1 | 0 | 1 | 1 |
| click 2 | 1 | 2 | 2 |
| click 3 | 2 | 3 | 3 |

That is enough event-driven architecture for this section.

Custom Paint events, redraw behavior, larger application state models, and concurrency arrive later.

## 9. Troubleshooting sequence

When a new environment fails:

1. preserve the exact error;
2. verify the installed tool/version;
3. create the smallest new project;
4. build or run the unmodified/minimal example;
5. verify the selected framework/interpreter/device/toolchain;
6. change one thing at a time.

Do not begin by reinstalling every tool.

## 10. Practice

1. In the C# and Python examples, which parts of the algorithm stayed the same?
2. Why does the XC8 example not print `5` to a terminal?
3. What does a successful XC8 build prove? What does it **not** prove?
4. Which tool should you check when VS Code is running the wrong Python installation?
5. What is the difference between build and run?
6. Why is the PIC16F883 device selection part of the build?
7. In the WinForms counter, why must `count` live outside the Click handler if it must remember prior clicks?

## 11. Answer reasoning

1. Inputs, addition, function/method call, returned result, and stored answer are conceptually the same.
2. A bare microcontroller does not provide the host console used by desktop applications.
3. It proves the selected software toolchain can translate/link that project for the selected target. It does not prove the physical PIC, wiring, programmer, or real peripheral behavior.
4. Verify/select the Python interpreter used by VS Code.
5. Build produces/validates executable program output from source; run executes the program.
6. The compiler/assembler/linker must generate code and memory placement appropriate to the actual processor.
7. A local variable created inside the handler would be recreated on each call. The form field survives between Click events.

## 12. Ready to continue when

Without notes, explain:

- one concept that stayed the same across C# and Python;
- compiler versus interpreter at a practical level;
- build versus run versus debug;
- IDE versus compiler/toolchain;
- target versus programmer;
- what successful first-build evidence means.

## 13. References

- [Complete course setup](../Guides/Toolchains/README.md)
- [C#/.NET setup](../Guides/Toolchains/csharp-dotnet.md)
- [Python setup](../Guides/Toolchains/python.md)
- [XC8 setup](../Guides/Toolchains/xc8.md)
- [pic-as setup](../Guides/Toolchains/pic-as.md)
- [On-Ramp examples](../Examples/OnRamp/)
