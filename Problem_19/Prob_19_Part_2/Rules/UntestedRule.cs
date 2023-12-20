
public class UntestedRule : Rule
{
    public UntestedRule(string destination) : base(destination)
    {
    }

    public override Mask Test()
    {
        return new Mask();
    }
}
