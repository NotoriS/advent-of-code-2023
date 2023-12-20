
public class GreaterThanRule : ComparisonRule
{
    public GreaterThanRule(string letter, int value, string destination) : base(letter, value, destination)
    {
    }

    public override PartRange Test(PartRange range)
    {
        PartRange newRange = new PartRange();

        switch (_comparisonLetter)
        {
            case "x":
                newRange.MinX = _comparisonValue + 1;
                break;
            case "m":
                newRange.MinM = _comparisonValue + 1;
                break;
            case "a":
                newRange.MinA = _comparisonValue + 1;
                break;
            case "s":
                newRange.MinS = _comparisonValue + 1;
                break;
            default:
                throw new InvalidDataException("Invalid comparison letter.");
        }

        PartRange overlap = range.Combine(newRange);
        overlap.Destination = Destination;

        return overlap;
    }
}
