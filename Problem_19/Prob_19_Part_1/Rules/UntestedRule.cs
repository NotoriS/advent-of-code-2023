
public class UntestedRule : Rule
{
    public UntestedRule(string destination) : base(destination)
    {
    }

    public override bool Test(Part part)
    {
        return true;
    }
}
