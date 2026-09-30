# RCET 3371 — Networking, HTTP, and JSON

*Optional self-learning guide*

[Topics index](README.md)

Networking changes the transport boundary, but it does not remove the need for explicit contracts. Endpoints, message schemas, timeout behavior, validation, and errors must still be defined just as they are for serial and file interfaces.

## Core model

A bounded HTTP/JSON interface should define:

- endpoint and method;
- request/response schema;
- field types/units;
- legal status/error responses;
- timeout behavior;
- retry policy if any;
- serialization/versioning rules.

Treat JSON as a representation, not as the domain model itself.

## Worked example

Suppose a working local controller needs to expose temperature:

```json
{
  "temperature_c": 22.75,
  "status": "OK"
}
```

The contract still needs answers:

- Is `temperature_c` required?
- What numeric range is valid?
- Which status strings are legal?
- What happens on timeout?
- What HTTP status represents invalid input?
- Can the schema change without a version rule?

Moving from serial/file to HTTP does not make these questions disappear.

## Apply and verify

For this optional Topic, use the same engineering frame:

1. state the problem;
2. preserve the known-good baseline;
3. add one bounded mechanism;
4. identify new failure modes;
5. define evidence before claiming improvement.

## Practice

1. Why must an HTTP/JSON interface still have a protocol contract?
2. What is the difference between transport success and semantic validity?
3. Why should retry behavior be specified instead of added automatically?
4. Give one validation rule for the example JSON payload.

## Answer reasoning

1. HTTP/JSON supplies transport/representation mechanisms, not application meaning.
2. A request can arrive successfully while containing invalid/missing/out-of-range domain data.
3. Retries can duplicate actions, change timing, and hide faults unless the operation and failure model support them.
4. For example, require `status` from a known set and `temperature_c` within the defined engineering range.

## Ready to continue when

You can define a bounded HTTP/JSON contract with schema, validation, timeout, and error behavior rather than treating JSON/networking as self-defining.

## References

- Microsoft Learn, HttpClient — https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient
- Microsoft Learn, System.Text.Json — https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview
- Python Software Foundation, json — https://docs.python.org/3/library/json.html
- [Serial Communication and Protocol Design](serial-communication-and-protocol-design.md)
