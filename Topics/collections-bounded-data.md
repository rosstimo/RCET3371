# RCET 3371 — Collections and Bounded Data

*Self-learning guide*

[Topics index](README.md)

Choose a collection because it expresses required behavior. This Topic starts from familiar arrays/lists, then introduces FIFO queues, rolling windows, and fixed/bounded storage when those requirements appear.













## Practice

1. Trace the array-average example after each loop iteration.
2. After adding 10, 20, 30, 40 to a rolling window of size 3, what remains?
3. What behavioral requirement suggests a queue?
4. Why can a ring buffer be preferable to shifting an array every sample?
5. Why does embedded C often make collection capacity more explicit than C# or Python?
6. What policy must be defined when a bounded buffer is full?

## Answer reasoning

1. Track the running sum: 10, 30, 60, 100.
2. `[20, 30, 40]`.
3. First-in/first-out processing.
4. It keeps bounded storage without moving every element on each update.
5. Embedded storage is constrained and predictable capacity is often part of the requirement; fixed arrays make capacity and overflow policy explicit.
6. The design must state whether to reject, overwrite oldest, drop newest, block, signal a fault, or use another deliberate behavior.

## Ready to continue when

You can select array/list/queue/bounded storage from the behavior requirement, trace FIFO and rolling-window state, explain ring-buffer motivation, and identify the full-buffer policy as part of the collection contract.

## References

- Microsoft, .NET collections — https://learn.microsoft.com/en-us/dotnet/standard/collections/
- Microsoft, C# `Queue<T>` — https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1
- Python `collections.deque` — https://docs.python.org/3/library/collections.html#collections.deque
