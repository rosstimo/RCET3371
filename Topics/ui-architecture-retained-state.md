# RCET 3371 — UI Architecture and Retained Presentation State

*Self-learning guide*

[Topics index](README.md)

A UI should present system state, not own the system. Retain meaningful data/model state independently of pixels, keep device/protocol/control work out of drawing callbacks, and redraw presentation from current retained state whenever the framework asks.









### Choose the smallest useful interface

Use CLI when:

- automation matters;
- interaction is simple;
- stdout/stderr are valuable.

Use GUI when:

- visual state/trends matter;
- interactive controls are justified;
- persistent visualization improves use.

Do not add GUI complexity merely because the course historically used GUI assignments.

### Repaint

When Paint/redraw occurs:

1. get current viewport;
2. derive transforms;
3. iterate retained state;
4. draw current view.

Do not depend on drawings made by old callbacks still being present.

### Diagnostics

Useful engineering UI state can include:

- connection state;
- identified device;
- last message time;
- parser/fault status;
- current log file;
- buffer occupancy;
- controller mode.

### Bridge example: move from a Label value to one plotted point

Start with a value you already know how to display:

```text
sample = 2.5 V
range  = 0.0 V to 5.0 V
```

Normalize it:

```text
normalized = (2.5 - 0.0) / (5.0 - 0.0) = 0.5
```

If the graph area is 200 pixels high, that value is halfway through the available range.

Work one point by hand before writing a plotting loop. Verify the top/bottom coordinate direction in the UI framework. Then plot two points, then a collection.

The graph is a presentation of model data. The pixels are not the only copy of the measurement.

### Example 2: bad coupling

Serial DataReceived callback:

- parses message;
- updates thermostat state;
- writes CSV;
- draws graph;
- changes button color.

This mixes transport, parser, control, persistence, and presentation.

Better: callback feeds transport/parser; resulting model event reaches dedicated components.

### Example 3: raw pixels versus normalized state

If a drawing application stores only screen pixels, resize may distort meaning.

If it stores domain coordinates or normalized coordinates, the display can be regenerated for a new viewport.

## Practice

1. When is a CLI a better interface than a GUI?
2. Why should a graph be reconstructible after minimize/restore or resize?
3. What should `Invalidate()` mean in a retained-state design?
4. Why is serial DataReceived -> parse -> control -> CSV -> drawing in one callback weak architecture?
5. Name useful engineering state a UI can expose without owning it.

## Answer reasoning

1. When automation/simple interaction/stdout-stderr behavior matters more than visual interaction.
2. Pixels are presentation, not the only copy of the domain data.
3. It requests repaint; the retained model remains the source of truth.
4. It couples transport, parser, domain control, persistence, and presentation so each becomes hard to test/change independently.
5. Connection/identity, parser/fault state, log file, buffer occupancy, controller mode, last-message time, etc.

## Ready to continue when

You can keep UI/presentation separate from domain state, explain retained-state repaint behavior, choose an appropriate interface style, and identify coupling that makes an engineering UI difficult to test.

## References

- Microsoft, Windows Forms `Control.Paint` — https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.paint
- Microsoft, `PaintEventArgs` — https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.painteventargs
- Microsoft, `Control.ClientRectangle` — https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.clientrectangle
