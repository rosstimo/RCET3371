# Protocol Data Logger Milestones

## Milestone 1 — Protocol parser

- protocol constants defined;
- authoritative frames parsed;
- arbitrary chunk-boundary tests pass;
- invalid length/checksum/noise tests pass.

## Milestone 2 — Device API and fake transport

- identity request/response works;
- telemetry maps to structured model;
- fake transport can deliver partial chunks, timeout, and disconnect;
- no live hardware required.

## Milestone 3 — Persistence and replay

- CSV logging contract implemented;
- 100-record rotation test passes;
- replay summary matches known dataset;
- malformed-row behavior documented/tested.

## Milestone 4 — Python + live adapter + final evidence

- Python identify/replay tools complete;
- C# SerialPort adapter complete;
- hardware verification performed when available;
- clean-clone instructions verified;
- failure/recovery evidence complete.
