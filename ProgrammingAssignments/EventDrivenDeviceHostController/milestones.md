# Event-Driven Device/Host Controller Milestones

## Milestone 1 — Model and transition tests

- enum/state model defined;
- transition table documented;
- controlled clock available;
- fake device boundary available;
- boundary tests written before full controller completion.

## Milestone 2 — Normal heating/cooling timing

- Idle/Heating/Cooling transitions pass;
- 5 s minimum-run boundary passes;
- 2 s deadtime boundary passes;
- interlock proven.

## Milestone 3 — Fault/recovery

- disconnect priority;
- sensor-fault priority;
- reset conditions;
- transition logging;
- repeated-event behavior.

## Milestone 4 — Integration and handoff

- configuration validation;
- presentation/CLI separated;
- optional live adapter if hardware available;
- architecture/evidence complete;
- clean-clone build/test verified.
