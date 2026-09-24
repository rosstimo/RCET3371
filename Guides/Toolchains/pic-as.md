# PIC Assembly / pic-as Toolchain Guide

[Toolchain setup index](README.md)

## Goal

Use the MPLAB X + XC8 installation from the previous guide to create and build the smallest useful PIC16F883 assembly project.

## Important: no separate assembler download

The course uses the PIC assembler driver supplied with the installed XC8 toolchain. If MPLAB X 6.35 and XC8 4.00 are already installed, do not search for a second unrelated assembler package.

## 1. Verify the prerequisite toolchain

Complete [MPLAB X / XC8 setup](xc8.md) first.

You should already have:

- MPLAB X working;
- XC8 4.x visible;
- PIC16F883 selectable;
- one successful XC8 C build.

## 2. Create a PIC16F883 project

Create another Standalone Project:

- Device: PIC16F883
- Tool: Simulator for the first build
- Toolchain: installed XC8 / PIC assembler support
- Project name: `HelloPicAs`

## 3. Add an assembly source file

Create `main.S`.

Use this minimal course-style skeleton:

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

The program deliberately mirrors the earlier `2 + 3` examples. The result is placed in the working register before execution loops.

If your project uses the course linker placement options/templates, follow the current course project template. The first goal here is toolchain recognition and a successful build, not peripheral setup.

## 4. Build

Use **Build Main Project**.

Record:

- device;
- MPLAB X version;
- XC8/pic-as version;
- build result.

## 5. Attach the new vocabulary after it builds

Identify:

- `PROCESSOR`: selected target family/device context;
- `#include <xc.inc>`: device/toolchain symbol support;
- `PSECT`: a program section handled by assembler/linker;
- label: a named code location;
- instruction: one processor operation;
- reset vector: where execution begins after reset;
- `goto Main`: explicit control flow.

Do not try to learn the entire instruction set from this first program.

## 6. Compare with C#

C#:

```csharp
int answer = 2 + 3;
```

Assembly:

```asm
movlw 2
addlw 3
```

The comparison is about the same computation appearing at different abstraction levels. It is not a claim that every C# statement maps directly to one or two assembly instructions.

## Troubleshooting

### Assembly source is ignored

Confirm the file is part of the project and uses the expected source extension/toolchain.

### Unknown symbol/directive

Confirm the selected device and current XC8 PIC Assembler syntax. Do not copy MPASM-era examples blindly into pic-as.

### Link/section error

Compare the project with the current course template and linker placement rules. Record the exact linker message.

## References

- MPLAB XC8: https://www.microchip.com/en-us/tools-resources/develop/mplab-x-compilers/xc8
- XC8 PIC Assembler documentation: https://onlinedocs.microchip.com/oxy/GUID-4DC87671-9D8E-428A-ADFE-98D694F9F089/
- PIC16F883: https://www.microchip.com/en-us/product/PIC16F883
