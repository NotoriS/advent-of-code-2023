
public abstract class ComparisonRule : Rule
{
    protected string _comparisonLetter;
    protected int _comparisonValue;

    protected ComparisonRule(string letter, int value, string destination) : base(destination)
    {
        _comparisonLetter = letter;
        _comparisonValue = value;
    }
}
