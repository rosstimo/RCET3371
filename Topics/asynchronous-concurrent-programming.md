# RCET 3371 — Asynchronous and Concurrent Programming

*Optional self-learning guide*

[Topics index](README.md)

Asynchronous/concurrent programming is justified when work must overlap without blocking required responsiveness or throughput. It also introduces ordering, ownership, cancellation, and error-propagation problems that simpler sequential code may avoid.

## Core model

Before adding concurrency, answer:

- what work actually overlaps?
- which mutable state has one clear owner?
- what ordering must be preserved?
- how is cancellation requested?
- where do errors surface?
- what happens during shutdown?

Do not use concurrency merely to make code appear more advanced.

## Worked example

A host UI freezes while waiting for a network request.

A bounded improvement is to move only the waiting I/O onto an asynchronous path while keeping model updates controlled:

```csharp
private async Task<string> LoadStatusAsync(
    HttpClient client,
    CancellationToken token)
{
    return await client.GetStringAsync(
        "status",
        token);
}
```

The new mechanism solves one concrete problem: the caller no longer needs to block the UI thread while waiting for I/O. It does **not** remove the need to define cancellation, errors, ordering, or ownership of shared state.

## Apply and verify

For this optional Topic, use the same engineering frame:

1. state the problem;
2. preserve the known-good baseline;
3. add one bounded mechanism;
4. identify new failure modes;
5. define evidence before claiming improvement.

## Practice

1. When is concurrency unnecessary?
2. Why is shared mutable state a design concern?
3. What should cancellation mean for an in-progress I/O operation?
4. Why can async code still have ordering bugs?

## Answer reasoning

1. When sequential work already meets responsiveness/throughput requirements and overlap adds no useful behavior.
2. Multiple execution paths can observe/update it in incompatible orders unless ownership/synchronization is explicit.
3. The operation should stop through a defined cancellation path and leave the system in a known state.
4. Completion order can differ from start order, and callbacks/tasks can interleave with other events.

## Ready to continue when

You can justify one asynchronous/concurrent boundary from a measurable requirement and explain ownership, ordering, cancellation, and error handling introduced by that choice.

## References

- Microsoft Learn, Task asynchronous programming model — https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/task-asynchronous-programming-model
- Python Software Foundation, asyncio — https://docs.python.org/3/library/asyncio.html
