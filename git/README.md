# RCET Git and Repository Workflow

This directory is the canonical shared student-facing entry point for Git and repository workflow across the RCET courses.

- [Git Simplified](GitSimplified.md) - start here for the normal individual workflow: setup, repositories, staging, commits, history, branches, merging, and everyday commands.
- [Git Fancy](GitFancy.md) - use when the basic workflow is comfortable and you need deeper history inspection, recovery, branching, cleanup, or more advanced repository operations.

Course-specific project-setup and submission instructions remain authoritative for that course. For example, a course may require a particular IDE project structure, repository name, remote, branch, or submission path even though the underlying Git concepts come from these shared guides.

Use the smallest workflow that safely preserves useful history:

1. inspect the current state with `git status`;
2. make one coherent change;
3. review what changed;
4. stage the intended files;
5. commit with a useful message;
6. push when the remote copy should be updated.

When something goes wrong, inspect the repository state and history before trying random recovery commands. The advanced guide exists for that reason.
