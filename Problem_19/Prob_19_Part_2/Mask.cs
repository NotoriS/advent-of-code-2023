
public class Mask
{
    public bool[] Valid { get; private set; }
    public string Letter { get; set; }

    public Mask()
    {
        Valid = new bool[PartRange.MAX_RANGE_SIZE];
        Array.Copy(PartRange.TrueArray, Valid, PartRange.MAX_RANGE_SIZE);
    }

    public Mask Opposite()
    {
        Mask result = new Mask();
        result.Valid = Valid.Select(e => !e).ToArray();
        result.Letter = Letter;
        return result;
    }
}
