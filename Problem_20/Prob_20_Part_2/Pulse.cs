
public class Pulse
{
    public PulseType Type { get; private set; }
    public string Source { get; private set; }
    public string Destination { get; private set; }

    public Pulse(PulseType type, string source, string destination)
    {
        Type = type;
        Source = source;
        Destination = destination;
    }
}
