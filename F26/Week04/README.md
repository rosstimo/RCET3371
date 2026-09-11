# Week 4 - Program Structure Across Languages

## What this week is about

A larger program should not become one giant source file. The goal is not to create more files for its own sake. The goal is to make **responsibilities, data flow, and interfaces visible**.

By the end of this week you should be able to:

- identify separate responsibilities in a small program;
- explain what information a function/module needs and returns;
- keep reusable logic separate from console/GUI/file input and output;
- organize a small project into multiple source files for a reason;
- implement the same byte-decoding specification in C# and Python;
- use one shared set of test vectors to verify both implementations;
- explain why translating a design is different from rewriting code line by line in another language.

## Start here

Use this status-byte format throughout the week:

```text
bit 7      fault
bit 6      enabled
bits 5..3  mode (0-7)
bits 2..0  level (0-7)
```

Before writing code, be able to explain these responsibilities:

```text
input -> decode -> result/model -> output
              |
              -> tests
```

## Examples

- [C# status decoder](examples/StatusDecoder.cs)
- [Python status decoder](examples/status_decoder.py)

Use these test values and predict the result before running the code:

| input | fault | enabled | mode | level |
|---|---:|---:|---:|---:|
| `0x00` | 0 | 0 | 0 | 0 |
| `0xFF` | 1 | 1 | 7 | 7 |
| `0x48` | 0 | 1 | 1 | 0 |
| `0xB5` | 1 | 0 | 6 | 5 |

## Assignment

**[Multi-Language Status Decoder](MultiLanguageStatusDecoder.md)**

Do not build a GUI for this assignment. The focus is software structure, data flow, testability, and transfer between languages.

## Useful guide

Use the [Flowchart Guide](../../Guides/Flowcharts/RCET-Flowchart-Guide.md) when planning the responsibility/data-flow sketch and any algorithm that becomes difficult to trace.