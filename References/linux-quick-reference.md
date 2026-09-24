# Linux Quick Reference

[References index](README.md)

## Location and files

    pwd
    ls -la
    cd path
    mkdir name
    cp source dest
    mv source dest
    rm file

Inspect before destructive operations.

## Find commands/files

    command -v name
    find . -name 'pattern'
    grep -R 'text' .

## Processes

    ps
    ps aux
    pgrep name
    kill PID

## Permissions

    ls -l
    id
    groups
    chmod ...
    chown ...

Understand owner/group/mode before changing access.

## Environment

    env
    printenv
    echo "$PATH"
    export NAME=value

## Redirection

    command > file
    command >> file
    command 2> errors.txt
    command < input.txt

## Pipeline

    producer | filter | consumer

## Serial

Common device names may include:

    /dev/ttyUSB0
    /dev/ttyACM0

Inspect actual device ownership/permissions and never assume the name is stable.

## .NET / Python

    dotnet --info
    dotnet build
    dotnet run
    python3 --version
    python3 program.py

## References

- GNU Bash manual: https://www.gnu.org/software/bash/manual/bash.html
- .NET Linux install docs: https://learn.microsoft.com/en-us/dotnet/core/install/linux
- Python Unix docs: https://docs.python.org/3/using/unix.html
