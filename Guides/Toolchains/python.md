# Python Toolchain Guide

[Toolchain setup index](README.md)

## Goal

Install Python and VS Code, run a first script, use a breakpoint, and create a project virtual environment.

## Course baseline

- Python 3.14.x
- standard CPython distribution
- Visual Studio Code
- Microsoft Python extension for VS Code

## 1. Install Python on Windows

Official download:

https://www.python.org/downloads/

For current Python 3.14 on Windows, Python.org may direct you through the **Python Install Manager**. Install it using the official instructions.

After installation, open a new terminal and verify:

```text
python --version
py -3.14 --version
```

At least one command must clearly identify Python 3.14.x.

To see which interpreter is running:

```text
py -3.14 -c "import sys; print(sys.executable)"
```

### Linux note

Use your distribution's supported Python 3.14 package when available. Do not replace the distribution's system Python with a random installer. Verify with:

```text
python3 --version
python3 -c "import sys; print(sys.executable)"
```

## 2. Install Visual Studio Code

Download:

https://code.visualstudio.com/Download

Install with the normal options for your operating system.

Launch VS Code.

## 3. Install the Python extension

1. Open the Extensions view in VS Code.
2. Search for **Python** published by Microsoft.
3. Install it.
4. Open the Command Palette.
5. Run **Python: Select Interpreter**.
6. Choose the Python 3.14 interpreter you verified above.

## 4. Create your first Python program

Create a new folder named `hello3371-python`.

Open that folder in VS Code.

Create `hello.py`:

```python
course = "RCET 3371"
section = 2

print(f"Hello, {course}!")
print(f"2 + 3 = {2 + 3}")
print(f"Section = {section}")
```

Run from the integrated terminal:

Windows:

```text
py -3.14 hello.py
```

or, when `python` resolves to the verified interpreter:

```text
python hello.py
```

Expected output matches the C# example:

```text
Hello, RCET 3371!
2 + 3 = 5
Section = 2
```

Compare the behavior first. Then compare the syntax.

## 5. Use the debugger once

1. Click beside a `print` line to create a breakpoint.
2. Choose **Run and Debug**.
3. Select Python if asked.
4. Inspect `course` and `section`.
5. Step over one statement.
6. Stop.

## 6. Create a virtual environment

From the project folder:

Windows:

```text
py -3.14 -m venv .venv
.venv\Scripts\activate
python -c "import sys; print(sys.executable)"
```

PowerShell may require:

```text
.\.venv\Scripts\Activate.ps1
```

Linux/macOS:

```text
python3 -m venv .venv
source .venv/bin/activate
python -c "import sys; print(sys.executable)"
```

After activation, select the `.venv` interpreter in VS Code.

## 7. Install a package later

When serial work begins:

```text
python -m pip install pyserial
```

Using `python -m pip` ties the package operation to the interpreter you are actually running.

## 8. Troubleshooting

### `python` opens the wrong version

Use `py -3.14` on Windows and inspect `sys.executable`.

### VS Code runs a different Python than the terminal

Run **Python: Select Interpreter** and choose the same interpreter/virtual environment.

### Module is installed but import fails

Run:

```text
python -m pip --version
python -c "import sys; print(sys.executable)"
```

Confirm both point to the same environment.

## References

- Python downloads: https://www.python.org/downloads/
- Python on Windows: https://docs.python.org/3/using/windows.html
- venv: https://docs.python.org/3/library/venv.html
- VS Code: https://code.visualstudio.com/
- VS Code Python: https://code.visualstudio.com/docs/languages/python
