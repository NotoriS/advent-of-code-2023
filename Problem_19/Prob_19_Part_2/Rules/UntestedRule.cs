
public class UntestedRule : Rule
{
    public UntestedRule(string destination) : base(destination)
    {
    }

    public override PartRange Test(PartRange range)
    {
        PartRange overlap = range.Combine(new PartRange());
        overlap.Destination = Destination;

        return overlap;
    }
}
