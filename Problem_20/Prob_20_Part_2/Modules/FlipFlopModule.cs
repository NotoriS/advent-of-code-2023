
public class FlipFlopModule : Module
{
    private bool _isOn;

    public FlipFlopModule(string name, List<string> destinations) : base(name, destinations)
    {
        _isOn = false;
    }

    public override void RecievePulse(Pulse pulse)
    {
        if (pulse.Type == PulseType.High) return;

        _isOn = !_isOn;

        foreach (string destination in _destinations)
        {
            if (_isOn)
            {
                Pulse highPulse = new Pulse(PulseType.High, _name, destination);
                PulseManager.Instance.SendPulse(highPulse);
            }
            else
            {
                Pulse lowPulse = new Pulse(PulseType.Low, _name, destination);
                PulseManager.Instance.SendPulse(lowPulse);
            }
        }
    }
}
