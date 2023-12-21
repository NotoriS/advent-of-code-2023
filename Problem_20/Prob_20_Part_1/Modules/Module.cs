
public abstract class Module
{
    protected string _name;
    protected List<string> _destinations;

    protected Module(string name, List<string> destinations)
    {
        _name = name;
        _destinations = new List<string>(destinations);
    }

    public abstract void RecievePulse(Pulse pulse);
}
