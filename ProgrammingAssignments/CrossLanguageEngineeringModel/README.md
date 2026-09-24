# Cross-Language Engineering Model

**Programming Assignments category value: 250 points out of 1000**

Sections: 3–6

## Objective

Implement one small engineering data contract across C#, Python, embedded C/XC8, and a bounded PIC assembly routine.

The goal is not to make four unrelated programs. The goal is to preserve the **same behavior and test vectors** while observing how representation, typing, modules, toolchains, and processor-level implementation differ.

Read [specification.md](specification.md) before coding.

## Required implementations

Your repository must contain:

```text
README.md
spec/
vectors/
csharp/
python/
xc8/
pic-as/
evidence/
```

### C#

Implement the complete specification as a .NET 10 console/library project with automated tests over all supplied vectors.

### Python

Implement the same complete specification using Python 3.14.

Because Python integers do not naturally overflow at 8/16 bits, explicitly model fixed-width behavior where the specification requires it.

### Embedded C / XC8

Implement the core encode/decode/conversion API as `.h/.c` modules targeting the PIC16F883.

The module must not require live hardware for logical verification.

### PIC assembly

Implement **one bounded routine** from the specification in `pic-as`:

- either `status_get_mode`;
- or `status_set_sequence`.

Document the subroutine contract, registers/state clobbered, and verification method.

You are not required to rewrite the entire project in assembly.

## Cross-language evidence

For the authoritative vector set, produce a comparison table showing that C#, Python, and embedded C produce equivalent required results.

For the selected assembly routine, show equivalent results for all applicable vectors.

## Engineering explanation

In `evidence/comparison.md`, explain:

- where fixed width is explicit in each language;
- how overflow/range errors are prevented or modeled;
- how module/function boundaries differ;
- what generated assembly reveals about at least one small XC8 C function;
- one case where line-by-line translation would be misleading.

## Milestones

See [milestones.md](milestones.md).

## Evaluation

See [rubric.md](rubric.md).
