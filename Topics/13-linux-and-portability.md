# RCET 3371 — Linux Systems and Portability

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 13](../LearningPath/13-Linux-and-Portability.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. Linux execution and portability
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

Engineering software often moves between development machines, lab systems, servers, embedded hosts, and automation environments. A program that only works because of one user's directory layout or one IDE launch configuration is not truly portable.

Linux gives you direct exposure to processes, permissions, environment, devices, and shell composition.

## 2. Outcomes

You should be able to:

- navigate the Linux filesystem;
- distinguish absolute and relative paths;
- reason about current working directory;
- inspect users, groups, permissions, processes, and environment variables;
- use shell pipelines and redirection;
- understand PATH and command discovery;
- use package-management concepts without confusing system packages with language packages;
- use SSH for remote shell access appropriately;
- inspect serial devices under /dev;
- build/run prior C# and Python course work on Linux;
- identify and remove avoidable portability assumptions.

## 3. Prerequisites

Sections 1-12, especially Git, file paths, toolchains, host serial, and collaboration.

## 4. Core model

A Linux process runs with an execution context:

- executable/interpreter;
- arguments;
- working directory;
- environment;
- user/group identity;
- open file descriptors;
- permissions;
- parent/child process relationships.

When a command behaves differently from another machine or launch method, compare context before rewriting code.

## 5. Linux execution and portability

### Filesystem and paths

Absolute:

    /home/student/project/data/log.csv

Relative:

    data/log.csv

The relative path is interpreted from the process current working directory.

Linux filenames are normally case-sensitive:

    Data.csv
    data.csv

are distinct names.

### Permissions

Traditional permission classes:

- user/owner;
- group;
- others.

Capabilities include read, write, execute.

Do not respond to a permission problem by granting global write/execute access blindly. Identify the required access and ownership first.

### Processes

Useful concepts/commands:

    ps
    pgrep
    kill
    jobs
    fg
    bg

A process exit code communicates success/failure to its caller or shell.

### Pipelines

A pipeline connects one command's standard output to another command's standard input:

    producer | filter | consumer

This is a compositional model, not merely terminal syntax.

### Redirection

Typical forms:

    command > output.txt
    command >> output.txt
    command < input.txt
    command 2> errors.txt

Order matters for more complex descriptor redirection.

### Environment and PATH

PATH is a search list used to locate commands. It is not the same thing as the current working directory.

Environment variables let a parent process pass configuration into a child process.

### Serial devices

Linux commonly exposes serial ports as device nodes such as:

    /dev/ttyUSB0
    /dev/ttyACM0

Actual names depend on driver/device. Permissions commonly depend on device ownership/group rules.

### Portability traps

Common traps:

- hard-coded Windows drive letters;
- backslash-only path construction;
- case-insensitive filename assumptions;
- GUI-only launch assumptions;
- fixed COM port names;
- current-directory assumptions;
- newline assumptions;
- shell-specific commands in generic scripts.

Use language path APIs rather than manual separators when practical.

## 6. Worked examples

### Example 1: working-directory bug

Program expects:

    ./config.json

Running from project root works.

Running executable from another directory fails.

Diagnosis: relative path depends on launch directory. Fix the contract: either require a documented working directory or resolve config from an explicit location.

### Example 2: pipeline

Suppose an application writes one status record per line.

A shell pipeline can select failures:

    dotnet run | grep FAULT

The program does not need a special "grep integration." Standard output is the interface.

### Example 3: serial permissions

Port exists at /dev/ttyUSB0 but open fails with permission denied.

Do not modify application protocol code. Inspect:

- device permissions;
- user/group membership;
- whether another process owns the device.

## 7. Apply, verify, and troubleshoot

Portability verification:

1. clean clone on second environment;
2. install documented dependencies only;
3. build;
4. run deterministic examples;
5. run tests;
6. verify file paths;
7. verify environment/configuration behavior;
8. verify serial path with fake first;
9. document genuine OS-specific differences.

Shell troubleshooting:

- use pwd;
- use command -v;
- inspect environment;
- inspect permissions;
- capture exit status;
- avoid guessing.

## 8. Practice

1. Why can a relative path change meaning without code changing?
2. What is the difference between PATH and current working directory?
3. What does a pipeline connect?
4. Why is chmod 777 usually a poor first fix?
5. Name four Windows-specific assumptions that can break Linux portability.
6. A serial device exists but opening it returns permission denied. Which layer should you inspect first?

## 9. Answer key

1. It is resolved from the process working directory, which depends on launch context.
2. PATH is a command-search list; working directory is the process's current filesystem location.
3. Standard output of one process to standard input of the next.
4. It grants far broader permissions than usually needed and hides the ownership/access-model problem.
5. Examples: drive letters, backslash separators, case-insensitive filenames, COM names, WinForms dependency, Windows-only shell commands.
6. OS/device permissions and ownership, before parser/protocol/application logic.

## 10. Explain without notes

Explain:

- absolute versus relative path;
- working directory;
- user/group/permission;
- process and exit status;
- pipeline/redirection;
- PATH/environment;
- serial device node;
- clean-clone portability test.

## 11. References

- GNU Bash Reference Manual — https://www.gnu.org/software/bash/manual/bash.html
- GNU Bash pipelines — https://www.gnu.org/software/bash/manual/html_node/Pipelines.html
- GNU Bash redirections — https://www.gnu.org/software/bash/manual/html_node/Redirections.html
- .NET on Linux — https://learn.microsoft.com/en-us/dotnet/core/install/linux
- Python on Unix platforms — https://docs.python.org/3/using/unix.html
