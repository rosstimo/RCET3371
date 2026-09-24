# RCET 3371 - Collections, Parsing, and Debugging

*Self-learning guide*

[Topics index](README.md) · [Learning Path Section 5](../LearningPath/05-Collections-Memory-Parsing-and-Debugging.md)

## 1. Start with arrays and lists you already know

RCET 2265 already used arrays/lists, loops, files, conversions, and debugging.

Start with a small set of measurements:

```csharp
int[] samples = { 10, 20, 30, 40 };

int sum = 0;

foreach (int sample in samples)
{
    sum += sample;
}

double average = (double)sum / samples.Length;

Console.WriteLine($"Average = {average}");
```

Before adding a new collection type, make sure you can trace this program by hand.

## 2. Why new collection types appear

A collection should make the required behavior easier to express.

Use a familiar array/list while it fits.

Introduce something else only when the requirement changes.

Examples:

- fixed number of samples -> array may fit;
- changing number of samples -> list may fit;
- first item in must be first item out -> queue may fit;
- fixed-size continuously updated history -> bounded/ring structure may fit.

The data structure is a tool for a behavior, not a vocabulary contest.

## 3. Worked example: List instead of fixed array

```csharp
List<int> samples = new();

samples.Add(10);
samples.Add(20);
samples.Add(30);

foreach (int sample in samples)
{
    Console.WriteLine(sample);
}
```

New idea:

The number of elements can grow while the program runs.

Try:

1. add 40;
2. remove 20;
3. predict the contents before running.

## 4. Worked example: read measurements from a file

Suppose `samples.txt` contains:

```text
10
20
30
40
```

A direct version:

```csharp
List<int> samples = new();

foreach (string line in File.ReadLines("samples.txt"))
{
    samples.Add(int.Parse(line));
}

Console.WriteLine($"Count = {samples.Count}");
```

This assumes every line is valid.

That is a useful first version because it separates the new file step from later validation.

## 5. Add validation one step later

Now use `TryParse`:

```csharp
List<int> samples = new();

foreach (string line in File.ReadLines("samples.txt"))
{
    if (int.TryParse(line, out int sample))
    {
        samples.Add(sample);
    }
    else
    {
        Console.WriteLine($"Invalid sample: {line}");
    }
}
```

Test it with:

```text
10
20
bad
40
```

Expected valid list:

```text
10, 20, 40
```

The important debugging habit is to keep the exact failing input, in this case `bad`.

## 6. Structured text: one small CSV record

Input:

```text
21.5,OK
```

First split it:

```csharp
string line = "21.5,OK";
string[] fields = line.Split(',');

Console.WriteLine(fields[0]);
Console.WriteLine(fields[1]);
```

Then convert the first field:

```csharp
if (double.TryParse(fields[0], out double temperature))
{
    string status = fields[1];

    Console.WriteLine($"Temperature = {temperature}");
    Console.WriteLine($"Status = {status}");
}
```

Only after this works should you add:

- missing-field checks;
- extra-field checks;
- range validation;
- timestamp parsing;
- larger files.

## 7. Turn a parsed record into an object

```csharp
class Measurement
{
    public double Temperature { get; }
    public string Status { get; }

    public Measurement(double temperature, string status)
    {
        Temperature = temperature;
        Status = status;
    }
}
```

Then:

```csharp
Measurement sample = new(temperature, status);
```

This connects directly to the ordinary classes from RCET 2265.

## 8. Queue: introduce it from a requirement

Requirement:

> Process commands in the same order they arrive.

That is first-in, first-out.

```csharp
Queue<string> commands = new();

commands.Enqueue("START");
commands.Enqueue("READ");
commands.Enqueue("STOP");

Console.WriteLine(commands.Dequeue()); // START
Console.WriteLine(commands.Dequeue()); // READ
```

Trace the queue after every operation.

Do not start by memorizing queue terminology. Start from the ordering requirement.

## 9. Rolling window

Requirement:

> Keep only the newest three samples.

Input sequence:

```text
10, 20, 30, 40
```

Expected states:

```text
[10]
[10, 20]
[10, 20, 30]
[20, 30, 40]
```

A simple list/queue implementation is fine while learning the behavior.

A ring buffer is a more specialized bounded implementation that avoids shifting/copying data unnecessarily. You should understand **why** it exists before being asked to implement its indexing.

## 10. Ring buffer as extension

A ring buffer typically tracks:

- fixed storage;
- current write/read index;
- count/full status.

Its advantage is predictable bounded storage and no need to shift all elements on every new sample.

At this point, be able to trace a supplied ring-buffer example. Inventing a correct generic ring buffer from scratch is not the first learning target.

## 11. Debugging with deterministic input

Suppose the parser fails only with this line:

```text
22.4,FAULT,EXTRA
```

Do not immediately edit the parser.

Capture:

- exact input;
- expected behavior;
- actual behavior/error;
- field count;
- values before conversion;
- stack trace or breakpoint state.

Then reduce the problem.

For example:

```csharp
string[] fields = line.Split(',');
Console.WriteLine(fields.Length);
```

Now the failure is reproducible.

## 12. Debugger workflow

A useful sequence:

1. set a breakpoint immediately before the suspected operation;
2. run with one known input;
3. inspect collection contents;
4. Step Over one operation;
5. compare expected state with actual state;
6. repeat with the smallest failing case.

Do not change five things at once while debugging.

## 13. Practice

1. Trace the array-average example after each loop iteration.
2. Change the list example to hold 10, 20, 30, 40, then remove 20.
3. What should the file parser do with `bad`?
4. Split `"21.5,OK"` by hand before running code.
5. Why is a queue clearer than manually shifting an array for FIFO behavior?
6. Trace a three-sample rolling window for inputs 5, 10, 15, 20, 25.
7. What evidence should you preserve before fixing a malformed-record bug?
8. Why is a ring buffer not the first collection students should learn here?

## 14. Answer reasoning

1. Track `sum` after each element: 10, 30, 60, 100.
2. Final list: 10, 30, 40.
3. Reject/report it while preserving valid values, according to the program contract.
4. Two fields: `21.5` and `OK`.
5. Queue operations directly express arrival/removal order.
6. `[5]`, `[5,10]`, `[5,10,15]`, `[10,15,20]`, `[15,20,25]`.
7. Exact failing input, expected result, actual result/error, and relevant program state.
8. Its indexing is extra complexity. Students should first understand the bounded-window behavior it is meant to implement efficiently.

## 15. Ready to continue when

Explain and demonstrate:

- array versus list;
- simple file read;
- split -> convert -> validate;
- object from parsed data;
- FIFO queue behavior;
- rolling-window behavior;
- why deterministic failing input helps debugging;
- why ring buffers are introduced from a need rather than as isolated syntax.

## 16. References

- .NET collections: https://learn.microsoft.com/en-us/dotnet/standard/collections/
- C# Queue: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1
- C# file APIs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/
- Visual Studio debugger: https://learn.microsoft.com/en-us/visualstudio/debugger/
