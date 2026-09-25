# RCET 3371 — Persistence and File Contracts

*Self-learning guide*

[Topics index](README.md)

Persistence is a boundary between one program run and a future reader. Treat a file format like any other interface: define path/location, record boundaries, field meaning, units, timestamp representation, conversion rules, and malformed-input behavior.









### Path identity

Do not assume a relative path means "next to my source code."

A relative path is resolved against a working directory chosen by how the process was launched.

When a file belongs to the project/package, locate it deliberately. When a file is user data, choose a deliberate application/user-data location. For coursework, document the required location precisely enough that tests and instructors can reproduce it.

### Append versus overwrite

**Overwrite** is appropriate for current configuration or an explicitly saved snapshot.

**Append** is appropriate for a chronological log when old records must remain.

Make the choice explicit.

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

## Practice

1. Explain why a relative path does not necessarily mean "next to the source file."
2. Define a three-field measurement record including units and timestamp convention.
3. Describe the round-trip test for that record.
4. What should happen when a required configuration field is missing or outside its legal range?
5. Why should malformed persisted input fail near the persistence boundary rather than later in UI/control code?

## Answer reasoning

1. Relative paths resolve from the process working directory, which depends on how the program was launched.
2. The contract must name field order/meaning, units, timestamp representation, and invalid/missing rules.
3. Construct a known domain value, serialize it, deserialize it, then compare the meaningful reconstructed state to the original.
4. Reject it or apply an explicitly documented default; do not silently invent behavior.
5. Early boundary failure preserves context and prevents invalid state from propagating into unrelated components.

## Ready to continue when

You can define a durable record/configuration contract, choose paths deliberately, perform a C#/Python round trip, validate configuration at load time, and report malformed/missing data with useful context.

## References

- Microsoft, .NET file-system guidance — https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/
- Microsoft, `DateTimeOffset` — https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset
- Python, `datetime` — https://docs.python.org/3/library/datetime.html
- Python, `pathlib` — https://docs.python.org/3/library/pathlib.html
