# MPLAB X / XC8 Toolchain Guide

[Toolchain setup index](README.md)

## Goal

Install MPLAB X and XC8, create a PIC16F883 C project, and reach a successful first build before adding hardware behavior.

## Course baseline

- PIC16F883
- MPLAB X IDE 6.35
- MPLAB XC8 compiler 4.00
- simulator for the first software-only check
- PICkit 3 or course-supported equivalent when physical programming/debugging is required later

MPLAB X and XC8 are separate installers.

## 1. Download and install MPLAB X

Official page:

https://www.microchip.com/en-us/tools-resources/develop/mplab-x-ide

Download the installer for your operating system.

Run it and use the normal/default components unless class instructions specify otherwise.

Launch MPLAB X after installation.

## 2. Download and install XC8

Official page:

https://www.microchip.com/en-us/tools-resources/develop/mplab-x-compilers/xc8

Download **MPLAB XC8** for your operating system and run the installer.

For the course baseline, XC8 4.00 is the adopted major release.

After installation, restart MPLAB X so it can discover the compiler.

## 3. Verify the compiler in MPLAB X

In MPLAB X, open the build-tools/compiler settings and confirm an XC8 4.x installation is listed.

If multiple compiler versions are installed, select the course-supported XC8 version when creating the project.

## 4. Create the first PIC16F883 C project

1. **File > New Project**.
2. Choose a **Standalone Project** for a Microchip embedded target.
3. Device: **PIC16F883**.
4. Hardware tool: choose the **Simulator** for this first build.
5. Compiler/toolchain: choose **XC8**.
6. Project name: `HelloXC8`.
7. Finish.

The exact wizard grouping can vary slightly by MPLAB X release. The required facts are the device, tool, compiler, and project name.

## 5. Add the first source file

Create `main.c`:

```c
#include <xc.h>
#include <stdint.h>

void main(void)
{
    volatile uint8_t answer = 2u + 3u;

    while (1)
    {
        (void)answer;
    }
}
```

This intentionally resembles the arithmetic from the C# and Python first programs. There is no console output yet.

## 6. Build

Use **Build Main Project**.

The first checkpoint is a successful build for PIC16F883.

Record the final build result and the selected XC8 version.

If the project does not build, do not add hardware code until this minimal project builds.

## 7. Attach the new vocabulary after it builds

- MPLAB X: IDE/project/debug orchestration
- XC8: C compiler toolchain for the PIC target
- PIC16F883 selection: tells the toolchain which processor/device rules apply
- Simulator: software target used for early debugging without physical hardware
- PICkit/target: later physical program/debug path

## 8. Add physical hardware later

When an assignment actually requires hardware:

1. connect the PICkit and target;
2. select the physical hardware tool;
3. verify target/device identification;
4. program/debug;
5. measure visible behavior independently.

Do not describe simulator success as physical verification.

## Troubleshooting

### XC8 is not listed

Restart MPLAB X after installing XC8. Confirm XC8 itself was installed, not only MPLAB X.

### PIC16F883 cannot be selected

Check installed device-family support and the MPLAB X installation. Record the exact dialog/error.

### Build fails immediately

Return to a brand-new minimal project and confirm the selected device, toolchain, and source file before adding more code.

## References

- MPLAB X IDE: https://www.microchip.com/en-us/tools-resources/develop/mplab-x-ide
- MPLAB X installation walkthrough: https://developerhelp.microchip.com/xwiki/bin/view/software-tools/ides/x/install-guide/
- MPLAB XC8: https://www.microchip.com/en-us/tools-resources/develop/mplab-x-compilers/xc8
- XC8 installation: https://developerhelp.microchip.com/xwiki/bin/view/software-tools/compilers/xc8/install/
- PIC16F883: https://www.microchip.com/en-us/product/PIC16F883
