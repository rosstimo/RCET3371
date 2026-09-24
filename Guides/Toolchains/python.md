# Python Toolchain Guide

[Guides index](../README.md)

## Course baseline

Reference environment:

- Python 3.14.x
- standard CPython distribution
- virtual environment for course projects that use third-party packages
- pySerial for serial-host work

A newer supported Python 3 release is acceptable only after the course examples/tests are verified.

## Verify interpreter

Windows:

    py --version
    py -3.14 --version

Linux/macOS:

    python3 --version

Also check the interpreter path when environments are confusing:

    python3 -c "import sys; print(sys.executable)"

## Virtual environment

Create:

    python3 -m venv .venv

Activate according to OS/shell, then verify:

    python -c "import sys; print(sys.executable)"

Install required packages:

    python -m pip install pyserial

Use the interpreter to invoke pip so the package is installed into the environment you are actually using.

## Run a program

    python program.py

## Debugging

Use the IDE/editor debugger when useful, but preserve a command-line run path.

Also use deliberate prints/logs when they express domain evidence:

- exact input;
- state transition;
- parsed record;
- error context.

Do not replace structured debugging with random print statements.

## Modules

Use modules to separate responsibilities.

Keep executable startup behind:

    if __name__ == "__main__":
        main()

when a file should also be importable for testing/reuse.

## Testing

The standard library unittest framework is sufficient for many course exercises. pytest may be used only when the assignment/environment explicitly adopts it.

## References

- Python downloads: https://www.python.org/downloads/
- Python tutorial: https://docs.python.org/3/tutorial/
- venv: https://docs.python.org/3/library/venv.html
- unittest: https://docs.python.org/3/library/unittest.html
- pySerial: https://pyserial.readthedocs.io/en/latest/
