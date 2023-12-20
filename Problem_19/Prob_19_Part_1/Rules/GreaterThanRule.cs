
public class GreaterThanRule : ComparisonRule
{
    public GreaterThanRule(string letter, int value, string destination) : base(letter, value, destination)
    {
    }

    public override bool Test(Part part)
    {
        switch (_comparisonLetter)
        {
            case "x":
                return part.X > _comparisonValue;
            case "m":
                return part.M > _comparisonValue;
            case "a":
                return part.A > _comparisonValue;
            case "s":
                return part.S > _comparisonValue;
            default:
                throw new InvalidDataException("Invalid comparison letter.");
        }
    }
}
