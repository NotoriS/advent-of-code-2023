
public class GreaterThanRule : ComparisonRule
{
    public GreaterThanRule(string letter, int value, string destination) : base(letter, value, destination)
    {
    }

    public override Mask Test()
    {
        Mask result = new Mask();
        for (int i = 0; i < _comparisonValue; i++) result.Valid[i] = false;
        result.Letter = _comparisonLetter;
        return result;
    }
}
