# Foundations Readiness Rubric

Total: **100 points**

| Area | Points | Full-credit evidence |
| --- | ---: | --- |
| Repository structure and .gitignore | 10 | Clear layout; generated output excluded; clean-clone usable |
| Git history | 15 | Meaningful development commits; no manufactured single final history |
| C# baseline | 15 | .NET 10 build/run, argument behavior, version output, nonzero missing-arg exit |
| Python baseline | 15 | Python 3.14 behavior, version output, documented venv, nonzero missing-arg exit |
| Desktop retained-state model | 20 | State outside Paint/click locals; repaint/resize reconstructs correctly |
| Conflict/recovery exercise | 15 | Real conflict, correct integrated result, command/history evidence, explanation |
| README/reproducibility | 10 | Clean instructions, requirements, verification, limitations |

## Major deductions

- Cannot build/run from submitted repository: affected component receives no functional points.
- Generated build/cache directories committed without justification: up to 10-point deduction.
- Missing meaningful Git history: Git-history section receives zero.
- Drawing exists only as transient pixels and disappears on repaint: retained-state section receives at most 5/20.
