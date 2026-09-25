# RCET 3371 — Parsing and Validation

*Self-learning guide*

[Topics index](README.md)

Parsing converts raw representation into structured program data. Validation decides whether the converted data satisfies the application's contract. Keep those steps visible so malformed input fails near the boundary rather than much later in unrelated code.









## Practice

A useful pipeline is:

```text
raw input
  -> split/frame
  -> convert fields
  -> validate range/count/state
  -> construct structured value
```

### Python translation of the same CSV record

```python
line = "21.5,OK"
fields = line.split(",")

if len(fields) != 2:
    raise ValueError("expected two fields")

temperature = float(fields[0])
status = fields[1]

if status not in {"OK", "FAULT"}:
    raise ValueError("invalid status")
```

### Bounded embedded-C example

On a microcontroller, parsing often uses caller-supplied buffers and explicit lengths. Keep the contract small:

```c
#include <stdint.h>

uint8_t parse_two_hex_digits(
    const char *text,
    uint8_t length,
    uint8_t *value);
```

Before converting anything, validate that `length == 2`, both characters are legal hex digits, and `value` is a valid output pointer according to the course's C contract. The bounded length is part of the parser interface rather than an assumption about a terminating character.

## Answer reasoning

1. Split `"21.5,OK"` by hand and identify the type of each resulting field.
2. What should happen to `"21.5,UNKNOWN"` if only OK/FAULT are legal?
3. Why should field count be checked before conversion?
4. Why is exact input length often more explicit in embedded C?
5. What information should a parse failure preserve for troubleshooting?

## Ready to continue when

1. Two fields: temperature text and status text; after conversion they become a numeric value and a constrained status value.
2. Reject it with a deliberate validation failure.
3. It prevents indexing/conversion from operating on a shape the parser contract does not accept.
4. Buffer size and termination cannot safely be assumed; explicit lengths bound what the parser may read.
5. Exact raw input, failure reason/location, and the expected contract.

## References

You can separate splitting/framing, conversion, validation, and object construction; reject malformed field counts/ranges/status values deliberately; and explain why a bounded embedded parser exposes input length explicitly.
