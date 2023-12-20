
public class LessThanRule : ComparisonRule
{
    public LessThanRule(string letter, int value, string destination) : base(letter, value, destination)
    {
    }

    public override Mask Test()
    {
        Mask result = new Mask();
        for (int i = _comparisonValue - 1; i < result.Valid.Length; i++) result.Valid[i] = false;
        result.Letter = _comparisonLetter;
        return result;
    }
}
