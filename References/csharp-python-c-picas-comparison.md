# C# / Python / Embedded C / PIC Assembly Comparison

[References index](README.md)

This reference highlights recurring course concepts. It is not a complete syntax manual.

| Concept | C# | Python | Embedded C / XC8 | PIC assembly / pic-as |
| --- | --- | --- | --- | --- |
| typing | static | dynamic | static | register/memory convention |
| ordinary integer width | fixed by type | arbitrary precision for int | target/type dependent; use stdint widths | register/file width explicit |
| function | method/local function | def | function | label + CALL/RETURN convention |
| module boundary | namespace/class/project | module/package | .h + .c / translation unit | PSECT/symbol/file convention |
| object model | classes/records/interfaces | classes/protocol by convention | structs + functions/interfaces by convention | explicit memory/register state |
| collections | arrays/List/Queue/etc. | list/tuple/deque/etc. | arrays/buffers | RAM tables/manual indexing |
| exceptions | yes | yes | normally explicit status/error paths | explicit flags/branches |
| hardware registers | uncommon in host code | uncommon in host code | volatile device headers/registers | direct SFR/register instructions |
| build | compiler + .NET toolchain | interpreter | preprocess/compile/assemble/link | assemble/link |
| runtime | .NET | Python interpreter | bare-metal/device runtime model | processor directly |

## Condition

C#:

    if (value > 10)
    {
        ...
    }

Python:

    if value > 10:
        ...

C:

    if (value > 10)
    {
        ...
    }

Assembly concept:

    compare / test flags
    conditional branch

## Fixed-width mask

C#:

    byte field = (byte)((state >> 4) & 0x0F);

Python hardware-like emulation:

    field = (state >> 4) & 0x0F

C:

    uint8_t field = (state >> 4) & 0x0Fu;

Assembly:

    copy value
    mask/shift using target instructions
    preserve caller-defined state according to contract

## Translation rule

Translate **behavior and contract**, not lines of syntax.

Before translating, state:

- input;
- output;
- persistent state;
- side effects;
- range/width;
- error behavior;
- test vectors.
