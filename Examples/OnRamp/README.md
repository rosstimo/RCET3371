# On-Ramp Examples

These examples are deliberately small. They let you compare environments without also solving a new algorithm.

## Shared problem

The C#, Python, and embedded C examples all begin with the same familiar operation:

- values: 2 and 3;
- compute the sum;
- keep the program small enough to trace completely.

The pic-as example performs the same addition close to the processor.

## Order

1. [C#](CSharp/) - familiar baseline
2. [Python](Python/) - same behavior, new syntax/interpreter
3. [XC8 C](XC8/) - same arithmetic, embedded target, no console
4. [pic-as](PicAs/) - same arithmetic at instruction level

Do not judge a language by how many lines this tiny example requires. The purpose is to identify what stayed conceptually the same and what the toolchain/language changed.
