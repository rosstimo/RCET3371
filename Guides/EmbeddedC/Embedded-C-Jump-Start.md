# Embedded C Jump Start

**Target:** PIC16F883, MPLAB X IDE, MPLAB XC8 compiler, external 4 MHz crystal oscillator  
**Starting point:** You have programmed before, but may not have used C or written firmware for a microcontroller.

This guide introduces the C you need to begin controlling hardware. It is a starting point, not a complete C reference or a substitute for the microcontroller datasheet.

## Contents

1. [From desktop programming to firmware](#1-from-desktop-programming-to-firmware)
2. [C essentials](#2-c-essentials)
3. [Binary, hexadecimal, and bit operations](#3-binary-hexadecimal-and-bit-operations)
4. [Your first PIC program](#4-your-first-pic-program)
5. [Reading an input](#5-reading-an-input)
6. [Common problems](#6-common-problems)
7. [Practice and references](#7-practice-and-references)

## 1. From desktop programming to firmware

A desktop program typically runs under an operating system and interacts with other software. Bare-metal firmware runs directly on a microcontroller and interacts with hardware registers.

| Desktop programming | Bare-metal embedded C |
| --- | --- |
| An operating system launches the program | Reset starts the processor; startup code runs, then `main()` |
| Output might go to a terminal or GUI | Output might drive a pin or peripheral |
| Input might come from a keyboard | Input might come from a switch, sensor, or communication peripheral |
| Memory is often plentiful | RAM and program memory are limited |
| A program might finish and exit | Firmware commonly runs indefinitely |

The basic program structure is familiar:

```c
void main(void)
{
    // Initialize hardware once.

    while (1) {
        // Run the application repeatedly.
    }
}
```

`while (1)` is an intentional infinite loop. On a PIC16F883, the compiler's startup code prepares the C runtime before calling `main()`.

[Back to top](#embedded-c-jump-start)

## 2. C essentials

C will look familiar if you know C#, Java, or similar languages. The biggest difference is that C has fewer built-in protections and abstractions. You work closer to memory and hardware.

### Types and variables

```c
#include <stdint.h>
#include <stdbool.h>

uint8_t count = 0;       // Unsigned 8-bit integer: 0 to 255
int16_t offset = -10;    // Signed 16-bit integer
bool ready = false;      // true or false
uint8_t values[4] = {0}; // Array of four bytes
```

Prefer fixed-width integer types such as `uint8_t`, `int16_t`, and `uint32_t` when the size matters. This is especially helpful when a value represents a hardware register or protocol byte. The types are supplied by `<stdint.h>`.

### Conditions and loops

```c
if (count >= 10) {
    count = 0;
} else {
    count++;
}

for (uint8_t i = 0; i < 4; i++) {
    values[i] = i;
}
```

Use `==` to compare values and `=` to assign a value. A condition such as `if (count = 1)` is legal C in many contexts, but assigns instead of comparing.

### Functions

```c
uint8_t limitValue(uint8_t value, uint8_t maximum)
{
    if (value > maximum) {
        value = maximum;
    }

    return value;
}
```

Each parameter has an explicit type. `void` means no value is returned, or, in a parameter list, no parameters are accepted. A function should have a clear responsibility and a predictable result.

### A few C-specific details

- Statements usually end with `;`. Braces `{ }` enclose a block.
- `#include` makes declarations available; `#define` creates a preprocessor macro.
- `const` means code must not modify a value through that declaration.
- `volatile` tells the compiler a value may change outside the normal flow of C code, as with hardware registers or data changed by an interrupt. It does **not** make multi-step access atomic.
- C arrays do **not** perform automatic bounds checking.
- Pointers contain addresses. You will use them later, but you do not need to master pointers to blink an LED.

[Back to top](#embedded-c-jump-start)

## 3. Binary, hexadecimal, and bit operations

Hardware registers are groups of bits. Binary and hexadecimal make the intended pattern easier to see.

```c
uint8_t a = 0b00001111; // Binary, supported by XC8
uint8_t b = 0x0F;       // Hexadecimal: same value
uint8_t c = 15;         // Decimal: same value
```

| Operation | C operator | Example | Result |
| --- | --- | --- | --- |
| AND | `&` | `0b1100 & 0b1010` | `0b1000` |
| OR | `\|` | `0b1100 \| 0b1010` | `0b1110` |
| XOR | `^` | `0b1100 ^ 0b1010` | `0b0110` |
| NOT | `~` | `~value` | Inverts bits within the promoted integer type |
| Shift left | `<<` | `1u << 3` | Bit 3 set |
| Shift right | `>>` | `0b1000 >> 2` | `0b0010` |

You will often see these patterns:

```c
uint8_t flags = 0;
flags |=  (uint8_t)(1u << 2);  // Set bit 2
flags &= (uint8_t)~(1u << 2); // Clear bit 2
bool set = (flags & (1u << 2)) != 0; // Test bit 2
```

**Important:** `&` and `|` operate on bits. `&&` and `||` operate on logical conditions. Do not interchange them.

[Back to top](#embedded-c-jump-start)

## 4. Your first PIC program

**Goal:** Blink an LED attached to RC0. Use a series resistor and connect the LED so a high output turns it on. Provide proper power, ground, and an MCLR pull-up for the circuit. For a 28-pin DIP PIC16F883, RC0 is pin 11.

Use a **4 MHz external crystal** connected between OSC1/CLKIN and OSC2/CLKOUT with load capacitors selected for the crystal. Follow the [PIC16F883 4 MHz Crystal Oscillator guide](https://github.com/rosstimo/RCET3375/blob/main/HowTo/PIC16F883-4MHz-Crystal-Oscillator.md) for the oscillator circuit and verification. The example below selects XT crystal mode.

In MPLAB X, create a standalone project for **PIC16F883**, select your programmer/debugger (or the simulator), and select **XC8**. Add a C source file named `main.c`.

```c
#include <xc.h>

#define _XTAL_FREQ 4000000UL

#pragma config FOSC = XT
#pragma config WDTE = OFF
#pragma config PWRTE = ON
#pragma config MCLRE = ON
#pragma config LVP = OFF

void main(void)
{
    ANSEL = 0x00;            // Disable analog input functions
    ANSELH = 0x00;

    TRISCbits.TRISC0 = 0;    // RC0 is an output
    PORTCbits.RC0 = 0;      // Begin with LED off

    while (1) {
        PORTCbits.RC0 = 1;  // LED on
        __delay_ms(500);

        PORTCbits.RC0 = 0;  // LED off
        __delay_ms(500);
    }
}
```

### What makes this embedded C?

- `#include <xc.h>` provides device-specific register definitions.
- `#pragma config` sets nonvolatile device configuration options, such as oscillator mode and watchdog behavior.
- `FOSC = XT` selects the external crystal oscillator mode. The crystal and its supporting components must be connected to the PIC; this setting does not generate a clock on its own.
- `ANSEL` and `ANSELH` select analog or digital input behavior for applicable pins.
- `TRISC` controls direction: **1 = input**, **0 = output**.
- `PORTC` accesses the I/O port.
- `__delay_ms(500)` is a compiler-provided blocking delay. `_XTAL_FREQ = 4000000UL` tells XC8 the expected 4 MHz oscillator frequency; defining it does not set the hardware clock.

**Check:** The LED should alternate on and off every 500 ms (one complete cycle per second). Change the delays and verify the new period with an oscilloscope if available.

**PIC16F883 caution:** PORT writes use read-modify-write behavior. Changing one output bit can unexpectedly affect other bits if pin levels do not match the output latch. Keep this in mind when you begin controlling multiple outputs together.

[Back to top](#embedded-c-jump-start)

## 5. Reading an input

**Goal:** Use a pushbutton on RB0 to control the RC0 LED. Connect RB0 through a pull-up resistor to VDD and the button from RB0 to ground. The signal is **active low**: pressed = `0`.

Replace the infinite loop in the previous example and add the input setup **before** the loop:

```c
TRISBbits.TRISB0 = 1; // RB0 is an input

while (1) {
    if (PORTBbits.RB0 == 0) {
        PORTCbits.RC0 = 1; // Pressed: LED on
    } else {
        PORTCbits.RC0 = 0; // Released: LED off
    }
}
```

Keep `ANSELH = 0x00` from the first example: RB0 shares an analog-input function and must be configured for digital input. This is a simple polling example. Mechanical switches can bounce, so a later exercise can introduce debouncing.

[Back to top](#embedded-c-jump-start)

## 6. Common problems

| Symptom | First things to check |
| --- | --- |
| Code builds, but nothing happens | Power and ground, MCLR pull-up, correct device, correct project programmed, LED polarity |
| Pin never changes | `TRIS` direction, analog/digital configuration, whether another peripheral owns the pin |
| Delay timing is wrong | External crystal operation, oscillator mode, and actual frequency versus `_XTAL_FREQ` |
| Input changes unpredictably | Missing pull-up/pull-down, floating input, button bounce |
| Unexpected PORT behavior | Read-modify-write effects or shared pins |
| Build error about a register or bit | Selected device, exact name in `<xc.h>`, and device datasheet |

Measure physical signals when you can. A successful build proves the compiler accepted the program, not that the circuit is behaving correctly.

[Back to top](#embedded-c-jump-start)

## 7. Practice and references

Try these in sequence:

1. Blink RC0 and verify the period.
2. Change the blink period to 200 ms on and 800 ms off.
3. Read RB0 and light the LED only while the button is pressed.
4. Add a `uint8_t` counter and increment it once per button press. Investigate why a raw button may count more than once.
5. Write a function `setLed(bool on)` that performs the output operation. Keep `main()` focused on application behavior.

**Next topics:** timer-driven scheduling, interrupts, ADC, UART, SPI, I2C, and separating hardware-access functions from application logic.

### Official references

- [Microchip PIC16F883 product page and datasheet](https://www.microchip.com/en-us/product/PIC16F883)
- [MPLAB XC8 C Compiler User's Guide](https://ww1.microchip.com/downloads/aemDocuments/documents/DEV/ProductDocuments/ReferenceManuals/MPLAB-XC8-C-Compiler-Users-Guide-for-PIC-DS50002737.pdf)
- [XC8 `__delay_ms()` reference](https://onlinedocs.microchip.com/oxy/GUID-BB433107-FD4E-4D28-BB58-9D4A58955B1A-en-US-9/GUID-4756AF01-B8F7-4CBC-9430-BBDF48641A83.html)

[Back to top](#embedded-c-jump-start)
