# File Round-Trip Example

This example supports Section 9. It starts from ordinary file writing and proves that structured records can be read back into the same model without losing meaning.

## Why round trip?

Writing a file once proves very little.

A stronger check is:

```text
program objects
    ↓ write
file representation
    ↓ read/parse
reconstructed objects
```

Then compare the reconstructed values with the originals.

## Run

```bash
python round_trip.py
```

Expected:

```text
round-trip ok: 3 records
```

## Trace one record first

Imagine one measurement:

```text
timestamp = 2026-09-24T10:00:00
temperature = 21.5
status = OK
```

Its CSV representation might be:

```text
2026-09-24T10:00:00,21.5,OK
```

Before running the full example, identify:

1. what delimiter separates fields;
2. which field needs numeric conversion;
3. what exact representation is stored for the timestamp;
4. how malformed/missing fields should be handled.

## Verification sequence

1. construct three known records in memory;
2. write them;
3. close/reopen the file;
4. parse the saved records;
5. compare the reconstructed records with the originals;
6. report success only if all required values agree.

## Safe modifications

1. Change one temperature and predict the file line.
2. Add a fourth record and verify the count changes.
3. Corrupt one numeric field in the saved file and observe the parser behavior.
4. Remove one field and define the expected failure.
5. Add a field only after deciding how older readers should handle it.

## What this example demonstrates

- file format is a contract between writer and reader;
- serialization and parsing should be tested together;
- known records make verification deterministic;
- persistence should preserve data meaning, not merely produce a file.
