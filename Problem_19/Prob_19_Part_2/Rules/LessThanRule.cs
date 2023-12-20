
public class LessThanRule : ComparisonRule
{
    public LessThanRule(string letter, int value, string destination) : base(letter, value, destination)
    {
    }

    public override PartRange Test(PartRange range)
    {
        PartRange newRange = new PartRange();

        switch (_comparisonLetter)
        {
            case "x":
                newRange.MaxX = _comparisonValue - 1;
                break;
            case "m":
                newRange.MaxM = _comparisonValue - 1;
                break;
            case "a":
                newRange.MaxA = _comparisonValue - 1;
                break;
            case "s":
                newRange.MaxS = _comparisonValue - 1;
                break;
            default:
                throw new InvalidDataException("Invalid comparison letter.");
        }

        PartRange overlap = range.Combine(newRange);
        overlap.Destination = Destination;

        return overlap;
    }
}
