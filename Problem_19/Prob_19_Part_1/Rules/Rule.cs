
public abstract class Rule
{
    public string Destination { get; private set; }

    protected Rule(string destination)
    {
        Destination = destination;
    }

    public abstract bool Test(Part part);
}
