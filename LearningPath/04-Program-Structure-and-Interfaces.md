# Section 4 - From Methods and Classes to Program Structure

The goal is to grow the method/class skills from RCET 2265 into larger programs without jumping immediately into software-architecture terminology.

## Start from what you already know

You already know:

- methods with parameters and return values;
- local variables and scope;
- ordinary classes, properties, and constructors;
- more than one C# source file.

We will first make those ideas cleaner and more deliberate. Modules and interfaces come after the familiar version works.

## Outcomes

You should be able to:

- describe a method in terms of inputs, output, and side effects;
- decide whether a value should be local or retained as object/program state;
- split a C# program into files by responsibility;
- translate a small method into a Python function and a C function;
- explain the basic purpose of a Python module and a C header/source pair;
- trace a function call and return;
- recognize an interface as a later tool for separating a caller from a replaceable implementation.

Interfaces are introduced here as a **supported pattern**, not assumed prior knowledge and not the first way students must solve a decomposition problem.

## Learn

- [Program structure and interfaces](../Topics/function-contracts-scope-retained-state.md)
- [Program decomposition, modules, and interfaces](../Topics/program-decomposition-modules-interfaces.md)
- [Cross-language comparison](../References/csharp-python-c-picas-comparison.md)

## Example progression

1. Start with one RCET 2265-style C# program containing `Main` and two helper methods.
2. Move a data model into its own class/file.
3. Move one calculation into a second class/file.
4. Verify the behavior did not change.
5. Translate the small calculation method to Python.
6. Translate it to C.
7. Only after those boundaries are clear, preview why an interface can be useful.

## Practice

**Follow:** Reproduce the multi-file C# example.

**Modify:** Add one field or calculation without mixing responsibilities back together.

**Translate:** Move one already-tested method into Python and C.

**Repair:** Find a scope/lifetime mistake where a needed value disappears between calls.

## Programming Assignment

Continue [Cross-Language Engineering Model](../ProgrammingAssignments/CrossLanguageEngineeringModel/README.md).

## Assessment

Section 4 assessment emphasizes methods/functions, scope/lifetime, state ownership, file/module organization, and simple cross-language translation. Interface questions should test recognition and purpose, not framework-level design.

Next: [Section 5 - Collections, Parsing, and Debugging](05-Collections-Memory-Parsing-and-Debugging.md)
