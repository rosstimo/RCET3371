# RCET 3371 — Engineering Visualization

*Self-learning guide*

[Topics index](README.md)

Visualization maps retained engineering data into a current viewport. Define domain ranges and rolling-window policy first, then derive screen coordinates. Resize/repaint should change the transform, not the meaning of stored measurements.

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

### Example 1: resize-safe plot

Store:

    [(time, temperature), ...]

On each Paint:

- determine current width/height;
- choose domain range;
- transform each sample;
- draw axes/line.

Resizing changes transforms, not the data.

## Practice

### Complete plotting progression

1. Keep samples in domain units such as volts or degrees C.
2. Define x/y domain ranges or rolling-window policy.
3. Normalize each domain value.
4. Map normalized values to the current `ClientRectangle`.
5. Invert y where the GUI coordinate system increases downward.
6. Draw from the retained collection during Paint.
7. Resize the control and verify the same measurements re-render correctly.

Use the retained-state WinForms example in [UI Architecture and Retained Presentation State](ui-architecture-retained-state.md) as the rendering boundary.

## Answer reasoning

1. Map 2.5 V from a 0–5 V range into normalized position.
2. Why is screen y often inverted relative to ordinary Cartesian coordinates?
3. What policy choices belong to a rolling plot?
4. What should change when a plot control doubles in width: stored measurements or the transform?
5. How would you verify that a resize-safe plot is correct beyond "it looks okay"?

## Ready to continue when

1. 0.5.
2. Many GUI coordinate systems increase y downward from the top.
3. Fixed count vs time window, x-axis meaning, rescaling policy, and empty-data behavior.
4. The transform/current pixel positions change; domain data does not.
5. Use known data/ranges, calculate expected positions/bounds, resize, and confirm the same domain relationships are preserved.

## References

You can convert domain values to screen coordinates, define a rolling-data policy, redraw from retained data after resize, and verify visualization with known values rather than visual intuition alone.
