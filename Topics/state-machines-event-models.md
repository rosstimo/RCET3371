# RCET 3371 — State Machines and Event Models

*Self-learning guide*

[Topics index](README.md)

Interactive systems become easier to reason about when persistent state and momentary events are named separately and legal transitions are explicit. Start from familiar event handlers, then make the transition contract independent of UI/framework syntax.









### Events versus state

**State** persists.  
**Event** happens.

"Connected" can be a state.  
"Connection lost" is an event.

### Bridge example: from a button click to explicit state

A familiar Windows Forms handler might be:

```csharp
private int count = 0;

private void addButton_Click(object sender, EventArgs e)
{
    count++;
    countLabel.Text = count.ToString();
}
```

This already contains three useful ideas:

- an event occurs;
- persistent state changes;
- the presentation is updated.

The next step is not a large architecture. Add one second event, such as a timer tick, and write down which event is allowed to change which state.

For example:

```text
Button Click -> request RUNNING
Timer Tick   -> advance elapsed time
Stop Click   -> request IDLE
```

Trace those transitions on paper before implementing a formal state machine.

## Practice

1. Classify each as state or event: Connected, ConnectionLost, Running, StartPressed.
2. Write a transition row for Idle + Start -> Running.
3. Why is a pile of booleans risky when the modes are mutually exclusive?
4. Run the same transition vectors against the C# and embedded-C examples.
5. Why is a pure next-state function easier to test than state changes scattered through callbacks?

## Answer reasoning

1. Connected/Running are states; ConnectionLost/StartPressed are events.
2. Current=Idle, event=Start, action=start required behavior/timer as specified, next=Running.
3. Invalid combinations can exist unless every relationship is enforced manually.
4. The expected next state must match in both languages because the transition contract is the same.
5. Inputs and output are explicit and deterministic, so tests do not need UI/transport side effects.

## Ready to continue when

You can distinguish event from state, construct a transition table, implement the same transition contract in C# and embedded C, and keep framework callbacks from becoming the state model themselves.

## References

- Microsoft, C# enumeration types — https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
- Microchip Technology Inc., *MPLAB XC8 C Compiler User's Guide* — https://onlinedocs.microchip.com/
- [State and timing reference](../References/state-and-timing-reference.md)
