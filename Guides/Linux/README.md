# Linux Systems and Portability Guide

[Guides index](../README.md)

Task-oriented companion to [Linux Systems and Portability](../../Topics/13-linux-and-portability.md).

## Baseline commands

```bash
pwd
ls -la
cd path
mkdir name
cp source destination
mv source destination
rm file
find . -maxdepth 2 -type f
file path
cat file
less file
grep pattern file
ps
pgrep name
command -v program
```

Use `man command` or `command --help` for local documentation.

## Pipes and redirection

```bash
program > output.txt
program >> output.txt
program 2> errors.txt
program | grep pattern
program | tee output.txt
```

## Environment

```bash
env
echo "$PATH"
command -v python
python --version
dotnet --info
```

When behavior differs between shells, compare executable resolution and environment before changing code.

## Permissions and processes

Inspect with `ls -l`, `id`, `groups`, `ps`, and `pgrep`. Do not use `sudo` or world-writable permissions as generic debugging fixes.

## Serial devices

Common names include `/dev/ttyUSB*` and `/dev/ttyACM*`. Device names are resources, not identities. Use device/protocol identity when possible.

## C# on Linux

```bash
dotnet restore
dotnet build
dotnet run
```

WinForms is Windows-specific. Keep domain logic outside WinForms so console/library portions remain portable.

## Python on Linux

```bash
python -m venv .venv
source .venv/bin/activate
python -m pip install --upgrade pip
python app.py
```

## Portability checklist

- no hard-coded Windows separators;
- no hard-coded COM identity in domain logic;
- documented dependencies;
- explicit file encoding/format;
- deliberate relative paths;
- OS-specific behavior isolated behind a boundary;
- clean-clone build/run tested.
