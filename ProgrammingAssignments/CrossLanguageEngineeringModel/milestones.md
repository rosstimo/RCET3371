# Cross-Language Engineering Model Milestones

Milestones are progress/checkoff points. They follow Sections 3–6 so the Programming Assignment grows with the concepts being taught.

The final grade is based on the completed Programming Assignment and rubric.

## Milestone 1 - Section 3: one C# status operation and the vectors

Start small.

- create the repository;
- read the status-byte and measurement-word parts of the specification;
- inspect the authoritative vector file without editing expected values;
- choose **one** status operation, such as `get mode` or `get sequence`;
- trace at least three applicable vectors by hand in binary/hex;
- implement that one operation in C#;
- verify the C# result against those vectors;
- add a second status operation only after the first is understood.

At this milestone you do **not** need:

- the complete API;
- Python;
- XC8 modules;
- pic-as;
- a large test framework.

The purpose is to connect the Section 3 representation work to a real, bounded problem.

## Milestone 2 - Sections 3–4: complete C# behavior, then direct Python translation

Grow the familiar-language solution first.

- complete the C# status-byte operations;
- add measurement-byte reconstruction and engineering conversion;
- add the encoded-sample behavior;
- organize the C# code into understandable methods/classes/files;
- load/use the authoritative vectors in repeatable C# checks;
- translate the already-understood behavior into Python;
- run the same authoritative vectors against Python;
- document one fixed-width difference you had to handle explicitly in Python.

Do not redesign the algorithm simply because the syntax changed.

## Milestone 3 - Sections 4–5: statistics, parsing/data handling, and equivalence evidence

After the collection/parsing material is in place:

- add summary statistics over decoded measurements;
- handle empty input according to the specification;
- preserve malformed/failing fixtures when a defect is found;
- complete repeatable C# and Python vector checks;
- produce an initial C# versus Python comparison table;
- document the method/module boundaries that keep parsing, calculation, and presentation separate.

The goal is now a complete and understandable high-level reference behavior before moving it to the embedded target.

## Milestone 4 - Section 6: XC8 translation in layers

Move only known behavior into C.

1. create a minimal PIC16F883 XC8 project and confirm it builds;
2. translate one already-tested bounded function into one C source file;
3. verify its behavior with fixed values;
4. add fixed-width types where width matters;
5. only then split the working code into `.h/.c` modules;
6. add the remaining required core encode/decode/conversion behavior;
7. verify authoritative vectors through the simulator/test harness or documented host-side equivalent;
8. inspect generated assembly for one small function.

A multi-file design is the result of a working one-file starting point, not a prerequisite for writing the first C function.

## Milestone 5 - Section 6: bounded pic-as routine and final evidence

- select `status_get_mode` or `status_set_sequence`;
- write the subroutine contract before the implementation;
- identify inputs, output, registers/memory used, and state clobbered;
- implement the routine in pic-as;
- verify all applicable vectors;
- finish the C#/Python/XC8 equivalence table;
- complete `evidence/comparison.md`;
- verify clean-clone/build/run instructions.

The assembly routine is deliberately bounded. The course is comparing abstraction levels, not requiring the complete project to be rewritten in assembly.
