var controller = new Controller();
controller.Enable(0.0, 23.0);
Console.WriteLine(controller.State);
controller.Update(4.9, 25.2);
Console.WriteLine(controller.State);
controller.Update(5.0, 25.2);
Console.WriteLine(controller.State);

enum ControllerState { Idle, Heating, Cooling, Fault }

sealed class Controller
{
    public ControllerState State { get; private set; } = ControllerState.Idle;
    private double _enteredAt;

    public void Enable(double now, double temperature)
    {
        if (temperature <= 24.0)
        {
            State = ControllerState.Heating;
            _enteredAt = now;
        }
    }

    public void Update(double now, double temperature)
    {
        if (State == ControllerState.Heating &&
            now - _enteredAt >= 5.0 &&
            temperature >= 25.0)
        {
            State = ControllerState.Idle;
            _enteredAt = now;
        }
    }
}
