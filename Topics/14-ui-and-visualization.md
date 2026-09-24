# RCET 3371 — UI and Visualization

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 14](../LearningPath/14-UI-and-Visualization.md)

## Contents

1. Why this matters
2. Outcomes
3. Prerequisites
4. Core model
5. Presentation and visualization
6. Worked examples
7. Apply, verify, and troubleshoot
8. Practice
9. Answer key
10. Explain without notes
11. References

## 1. Why this matters

A user interface should expose a working engineering system, not become the system.

Visualization is useful when it helps a user understand state, trend, fault, timing, or comparison. Poor UI architecture mixes device I/O and domain logic into callbacks, making the program difficult to test and repaint correctly.

## 2. Outcomes

You should be able to:

- choose among CLI, TUI, GUI, and generated/report output based on need;
- keep presentation separate from domain/protocol state;
- explain event-driven UI behavior;
- retain data required for repaint;
- convert between domain coordinates and screen coordinates;
- scale and translate data into a viewport;
- implement a rolling plot conceptually;
- preserve behavior during resize/redraw;
- expose connection, logging, buffer, and fault state;
- identify common flicker/transient-drawing problems.

## 3. Prerequisites

Sections 2, 8-13, especially retained state, host architecture, logging, and system integration.

## 4. Core model

Use three layers:

    domain state
       ↓
    view model / presentation data
       ↓
    rendering

The domain should not need to know pixel coordinates.

A graph should be reconstructible from retained domain/presentation data whenever the UI asks for repaint.

## 5. Presentation and visualization

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

### Coordinate transform

Suppose domain x spans xmin..xmax and screen x spans left..right.

Normalized position:

    u = (x - xmin) / (xmax - xmin)

Screen:

    px = left + u * (right - left)

For screen y, remember many GUI coordinate systems increase downward, so a vertical inversion is often required.

### Rolling data

Keep domain samples in a collection. Decide:

- fixed count window;
- fixed time window;
- x-axis semantics;
- rescale policy;
- what happens when no data exists.

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

## 6. Worked examples

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


### Example 1: resize-safe plot

Store:

    [(time, temperature), ...]

On each Paint:

- determine current width/height;
- choose domain range;
- transform each sample;
- draw axes/line.

Resizing changes transforms, not the data.

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

## 7. Apply, verify, and troubleshoot

UI test questions:

- if window is covered/uncovered, is display restored?
- if resized, is data preserved?
- if no device is connected, is state clear?
- if logging fails, is failure visible?
- can core behavior be tested without UI?
- does UI thread own UI updates?

For visualization:

- test min/max/equal values;
- empty collection;
- one sample;
- constant range where xmax == xmin or ymax == ymin;
- very large/small values;
- resize.

## 8. Practice

1. Why should the domain model avoid pixel coordinates?
2. What causes transient drawings to disappear?
3. How do you map a domain value into a screen interval?
4. Why is an empty dataset an important plotting test?
5. Name four diagnostics worth exposing in a device host UI.
6. What is wrong with performing protocol parsing directly in every button/event callback?

## 9. Answer key

1. Pixels are presentation-specific; domain state should survive different view sizes/interfaces.
2. Repaint reconstructs the control; drawings not derived from retained state may be lost.
3. Normalize within domain range then scale/translate into viewport.
4. Many range/loop assumptions fail with zero samples; UI must define a valid empty state.
5. Connection, device identity, last message, fault/parser state, log file, buffer state, controller mode.
6. It duplicates responsibility and couples presentation to protocol, making testing and reuse difficult.

## 10. Explain without notes

Explain:

- domain versus presentation state;
- retained state and repaint;
- coordinate normalization;
- rolling window;
- resize behavior;
- diagnostic UI;
- why UI should be added after core system behavior works.

## 11. References

- Microsoft Learn, Windows Forms control events — https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/events
- Microsoft Learn, custom painting and drawing — https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/custom-painting-drawing
- Microsoft Learn, Control.Paint — https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.paint
