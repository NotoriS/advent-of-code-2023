
public class BroadcastModule : Module
{
    public BroadcastModule(string name, List<string> destinations) : base(name, destinations)
    {
    }

    public override void RecievePulse(Pulse pulse)
    {
        foreach (string destination in _destinations)
        {
            Pulse nextPulse = new Pulse(pulse.Type, _name, destination);
            PulseManager.Instance.SendPulse(nextPulse);
        }
    }
}
