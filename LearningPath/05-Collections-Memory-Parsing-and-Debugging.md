# Section 5 - Collections, Parsing, and Debugging

This section begins with arrays, lists, strings, files, and debugging techniques already seen in RCET 2265. New collection types are introduced only when a familiar collection becomes awkward.

## Outcomes

You should be able to:

- trace and modify arrays and lists;
- read a small text/CSV file and split a record into fields;
- validate a field before converting it;
- preserve a failing input so a bug can be reproduced;
- choose a queue when first-in/first-out behavior is required;
- explain the idea of a fixed rolling window;
- recognize a ring buffer as a bounded implementation technique without being required to invent one from scratch;
- use breakpoints, variables, call stack, and repeatable test data to debug a collection/parser defect.

## Learn

- [Collections, parsing, and debugging](../Topics/05-collections-memory-parsing-and-debugging.md)
- [File I/O example](../Examples/FileIOExample/)
- [File Round Trip](../Examples/FileRoundTrip/)

## Example progression

1. Start with `int[]` and `List<int>`.
2. Compute count/sum/average.
3. Read the same values from a file.
4. Split one structured text record.
5. Reject one malformed record.
6. Store valid records in a list.
7. Replace manual first-in/first-out shifting with a queue.
8. Treat ring-buffer mechanics as extension material until the behavior is understood.

## Practice

Trace every collection state for a tiny fixed input before using a large file or live device.

## Programming Assignment

Continue [Cross-Language Engineering Model](../ProgrammingAssignments/CrossLanguageEngineeringModel/README.md).

## Assessment

Section 5 practice and graded assessment prioritize ordinary arrays/lists/parsing/debugging. Queue/bounded-buffer questions build from concrete examples.

Next: [Section 6 - Embedded C from Familiar Program Logic](06-Embedded-C-and-Software-State.md)
