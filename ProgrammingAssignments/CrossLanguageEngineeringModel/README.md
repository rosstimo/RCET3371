# Cross-Language Engineering Model

**Programming Assignments category value: 250 points out of 1000**

Sections: 3–6

## Objective

Build one small engineering data model gradually across C#, Python, embedded C/XC8, and one bounded PIC assembly routine.

The final project preserves the **same behavior and authoritative test vectors** across languages, but you are **not** expected to design the complete cross-language solution at the beginning of Section 3.

Work through the [section-aligned milestones](milestones.md) in order. Each milestone adds only the ideas introduced by that point in the Learning Path.

Read [specification.md](specification.md) for the final required behavior. Use it as the destination, not as a signal that every part must be implemented immediately.

## Final repository shape

By the end of Section 6, your repository must contain:

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

It is normal for some of these directories to be added later as the course reaches the corresponding language/toolchain work.

## Final required implementations

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

These explanations should grow as the corresponding concepts are taught. Do not write them all from guesses at the start of the assignment.

## Milestones

See [milestones.md](milestones.md).

## Evaluation

See [rubric.md](rubric.md).
