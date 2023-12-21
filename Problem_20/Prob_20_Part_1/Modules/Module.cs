
public abstract class Module
{
    private List<string> _destinations;

    protected Module(List<string> destinations)
    {
        _destinations = new List<string>(destinations);
    }

    public abstract void RecievePulse(Pulse pulse);
}
