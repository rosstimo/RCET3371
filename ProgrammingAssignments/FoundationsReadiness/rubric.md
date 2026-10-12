# Foundations Readiness Rubric

Total: **100 points**

| Area | Points | Full-credit evidence |
| --- | ---: | --- |
| Repository structure and .gitignore | 10 | Clear layout; generated output excluded; clean clone understandable |
| Git development history | 15 | Multiple meaningful commits plus status/diff/staged-diff/history evidence |
| C# familiar baseline | 15 | .NET 10 program builds/runs; measurements, helper method, and result are correct |
| Python translation | 15 | Same behavior as C#; Python 3.14 interpreter and venv documented |
| Windows Forms event review | 15 | Button event updates retained counter and label correctly |
| XC8 first build | 10 | PIC16F883 C source and successful-build evidence with tool version |
| pic-as first build | 10 | PIC16F883 assembly source and successful-build evidence with tool version |
| README and setup evidence | 10 | Another student/grader can reproduce the environment checkpoints |

## Major deductions

- A component cannot be built/run or has no credible successful-build evidence: affected component receives no functional points.
- Generated build/cache directories committed without justification: up to 10-point deduction.
- One manufactured final commit instead of development history: Git-history section receives little or no credit.
- Python interpreter/environment is ambiguous: Python section cannot receive full credit.
- Embedded project claims physical verification when only a build/simulator was performed: setup-evidence section loses credit.
