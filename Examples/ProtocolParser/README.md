# Protocol Parser Example

Small C# streaming parser for RCET Telemetry Protocol v1.

Demonstrates pre-frame noise handling, partial state across calls, length/check validation, and multiple frames across uneven chunks.

Run:

```bash
dotnet run --project ProtocolParser.csproj
```

Expected output:

```text
type=0x01 len=0
type=0x10 len=0
```
