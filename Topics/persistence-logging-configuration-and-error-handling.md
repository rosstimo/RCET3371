# RCET 3371 — Persistence, Logging, Configuration, and Error Handling

*Self-learning guide*

[Topics index](README.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. File and configuration behavior
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

Data is only useful if another person or another run of the program can interpret it later. Logging, configuration, and replay turn a one-time live interaction into reproducible engineering evidence.

Many file bugs are actually path, lifecycle, or contract bugs: the program writes somewhere unexpected, silently overwrites data, stores ambiguous units, or cannot read what it wrote.

## 2. Outcomes

You should be able to:

- distinguish text and binary storage choices;
- distinguish project/source directory, executable location, and current working directory;
- create an explicit structured-record contract;
- implement append, overwrite, explicit save, naming, rotation, and discovery intentionally;
- store timestamps with an understood representation;
- serialize and deserialize with round-trip tests;
- replay stored data through normal analysis code;
- validate configuration;
- handle missing, partial, malformed, locked, or inaccessible files deliberately.

## 3. Prerequisites

Sections 4-8: program boundaries, parsing, protocol design, and host architecture.

## 4. Core model

Treat persistence as another interface.

A file format needs the same clarity as a wire protocol:

- record boundary;
- field names/order;
- types;
- units;
- timestamp representation;
- missing/invalid rules;
- versioning when needed.

A useful pipeline is:

    domain object
       ↓
    serialize
       ↓
    durable representation
       ↓
    deserialize
       ↓
    equivalent domain object

Round-trip verification checks that the meaningful state survives that cycle.

## 5. File and configuration behavior

### Path identity

Do not assume a relative path means "next to my source code."

A relative path is resolved against a working directory chosen by how the process was launched.

When a file belongs to the project/package, locate it deliberately. When a file is user data, choose a deliberate application/user-data location. For coursework, document the required location precisely enough that tests and instructors can reproduce it.

### Append versus overwrite

**Overwrite** is appropriate for current configuration or an explicitly saved snapshot.

**Append** is appropriate for a chronological log when old records must remain.

Make the choice explicit.

### Log names and rotation

A useful log strategy answers:

- base directory;
- naming convention;
- when a new file starts;
- size/time/session limit;
- how files are discovered later;
- whether writes are flushed frequently enough for the risk.

### Timestamps

A timestamp should state or imply:

- time zone/offset or UTC;
- precision;
- serialization format.

ISO 8601-compatible representations are often useful for interchange because they are explicit and sortable when used carefully.

### Configuration

Validate configuration at startup or load time.

Reject or clearly default:

- missing required fields;
- invalid ranges;
- unknown mode names;
- impossible combinations.

Do not let malformed configuration silently produce unsafe or confusing behavior.

### Exceptions and error state

Catch an exception when you can add useful context, recover, translate it into an expected domain failure, or clean up.

Do not catch every exception merely to hide it.

## 6. Worked examples

### Bridge example: grow one file write into a record contract

Start with familiar output:

```csharp
File.AppendAllText("log.txt", "21.5\n");
```

Then make one record slightly more useful:

```text
2026-09-24T10:00:00,21.5,OK
```

Write three records. Reopen the file. Split and parse them back. Verify the reconstructed values match what was written.

Only after that round trip works should you add configuration files, log rotation, corrupt-record recovery, or richer serialization. The first new idea is simply that a file format is a contract between the writer and the future reader.


### Example 1: structured CSV contract

Header:

    timestamp_utc,temp_c,status

Record:

    2026-09-24T16:30:00Z,22.75,OK

The header names units and timestamp convention, reducing ambiguity during replay.

### Example 2: round trip in C#

Keep the format deliberately small so the file contract stays visible:

```csharp
using System.Globalization;

record Measurement(
    DateTimeOffset Time,
    double TempC,
    string Status);

static string Serialize(Measurement value)
{
    return string.Join(
        ",",
        value.Time.ToString("O"),
        value.TempC.ToString("R", CultureInfo.InvariantCulture),
        value.Status);
}

static Measurement Deserialize(string line)
{
    string[] fields = line.Split(',');

    return new Measurement(
        DateTimeOffset.Parse(fields[0], CultureInfo.InvariantCulture),
        double.Parse(fields[1], CultureInfo.InvariantCulture),
        fields[2]);
}

Measurement original = new(
    DateTimeOffset.Parse("2026-09-24T16:30:00Z"),
    22.75,
    "OK");

string text = Serialize(original);
Measurement restored = Deserialize(text);

Console.WriteLine(text);
Console.WriteLine(restored == original);
```

The important test is not merely "a file was written." It is:

```text
domain value -> serialized representation -> domain value
```

and the reconstructed meaningful state agrees with the original.

### Example 3: the same round trip in Python

```python
from dataclasses import dataclass
from datetime import datetime

@dataclass(frozen=True)
class Measurement:
    time: datetime
    temp_c: float
    status: str

def serialize(value: Measurement) -> str:
    return f"{value.time.isoformat()},{value.temp_c!r},{value.status}"

def deserialize(line: str) -> Measurement:
    timestamp, temp_c, status = line.split(",")
    return Measurement(
        datetime.fromisoformat(timestamp),
        float(temp_c),
        status,
    )

original = Measurement(
    datetime.fromisoformat("2026-09-24T16:30:00+00:00"),
    22.75,
    "OK",
)

text = serialize(original)
restored = deserialize(text)

print(text)
print(restored == original)
```

The language changed. The persistence contract did not: field order, timestamp meaning, numeric representation, status vocabulary, and failure policy still need to be defined.

If floating-point formatting is intentionally lossy, define the acceptable tolerance rather than pretending exact equality is always the requirement.

### Example 4: malformed record

Input:

    2026-09-24T16:30:00Z,not-a-number,OK

A robust parser returns or records a structured failure rather than allowing an unexplained conversion exception to escape from a distant UI event.

### Example 5: configuration is input too

Suppose the host application needs:

```text
port_name = COM4
baud_rate = 115200
log_directory = logs
```

Validate the configuration before starting the serial/logging system:

- port name must be present;
- baud rate must be one of the values the device contract supports;
- log directory must be usable or deliberately creatable;
- unknown modes should not silently become a different mode.

Configuration errors should fail close to configuration loading, with a message that names the invalid field. They should not appear later as an unrelated serial or file error.

## 7. Apply, verify, and troubleshoot

When a file "is missing":

1. print/log the fully resolved path;
2. inspect current working directory;
3. verify create/read permissions;
4. verify filename/case rules on the current OS;
5. verify the program closed/flushed the file;
6. inspect exception details.

When a replay differs from live behavior:

- feed the same domain events into both paths;
- verify units/timestamps;
- verify ordering;
- verify parser version;
- compare exact intermediate states.

## 8. Practice

1. Why can a relative path work in an IDE and fail when the executable is launched another way?
2. When is overwrite appropriate? When is append appropriate?
3. What must a timestamp contract clarify?
4. Why is a serialize/deserialize round trip valuable?
5. A config file says sample_period_ms = -10. What should the program do?
6. What information should be logged when a file cannot be opened?

## 9. Answer key

1. The working directory can differ even when the executable is the same.
2. Overwrite fits current snapshots/configuration; append fits accumulating chronological records. The system specification decides.
3. Representation, time zone/offset or UTC convention, and useful precision.
4. It proves the persisted representation can recreate the intended domain state.
5. Reject it or apply an explicitly documented validation/default policy; do not silently use an impossible negative period.
6. Resolved path, operation, relevant configuration, and exception/error details without leaking unrelated sensitive data.

## 10. Explain without notes

Explain:

- working directory versus executable/source location;
- file-format contract;
- append versus overwrite;
- log rotation;
- timestamp representation;
- round-trip test;
- configuration validation;
- when exception handling adds value.

## 11. References

- Microsoft, .NET file-system guidance — https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/
- Microsoft, `DateTimeOffset` — https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset
  - Used for: explicit timestamp/offset representation in persisted records.
- Python, `datetime` — https://docs.python.org/3/library/datetime.html
  - Used for: Python timestamp serialization/deserialization.
- Python, `pathlib` — https://docs.python.org/3/library/pathlib.html
  - Used for: explicit path handling in Python.


- Microsoft Learn, file and stream I/O — https://learn.microsoft.com/en-us/dotnet/standard/io/
- Microsoft Learn, C# exceptions — https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/
- Python Software Foundation, input and output — https://docs.python.org/3/tutorial/inputoutput.html
- Python Software Foundation, pathlib — https://docs.python.org/3/library/pathlib.html
- Python Software Foundation, json — https://docs.python.org/3/library/json.html
