
public class PartRange
{
    public const int MAX_RANGE_SIZE = 4000;
    public static bool[] TrueArray = Enumerable.Repeat(true, MAX_RANGE_SIZE).ToArray();

    public bool[] ValidX { get; private set; }
    public bool[] ValidM { get; private set; }
    public bool[] ValidA { get; private set; }
    public bool[] ValidS { get; private set; }

    public string Destination { get; set; }

    public PartRange()
    {
        ValidX = new bool[MAX_RANGE_SIZE];
        Array.Copy(TrueArray, ValidX, MAX_RANGE_SIZE);

        ValidM = new bool[MAX_RANGE_SIZE];
        Array.Copy(TrueArray, ValidM, MAX_RANGE_SIZE);

        ValidA = new bool[MAX_RANGE_SIZE];
        Array.Copy(TrueArray, ValidA, MAX_RANGE_SIZE);

        ValidS = new bool[MAX_RANGE_SIZE];
        Array.Copy(TrueArray, ValidS, MAX_RANGE_SIZE);

        Destination = "";
    }

    public PartRange(PartRange original)
    {
        ValidX = new bool[MAX_RANGE_SIZE];
        Array.Copy(original.ValidX, ValidX, MAX_RANGE_SIZE);

        ValidM = new bool[MAX_RANGE_SIZE];
        Array.Copy(original.ValidM, ValidM, MAX_RANGE_SIZE);

        ValidA = new bool[MAX_RANGE_SIZE];
        Array.Copy(original.ValidA, ValidA, MAX_RANGE_SIZE);

        ValidS = new bool[MAX_RANGE_SIZE];
        Array.Copy(original.ValidS, ValidS, MAX_RANGE_SIZE);

        Destination = "";
    }

    public PartRange ApplyMask(Mask mask) 
    {
        PartRange result = new PartRange(this);

        switch (mask.Letter)
        {
            case "x":
                for (int i = 0; i < MAX_RANGE_SIZE; i++)
                {
                    result.ValidX[i] = ValidX[i] && mask.Valid[i];
                }
                break;
            case "m":
                for (int i = 0; i < MAX_RANGE_SIZE; i++)
                {
                    result.ValidM[i] = ValidM[i] && mask.Valid[i];
                }
                break;
            case "a":
                for (int i = 0; i < MAX_RANGE_SIZE; i++)
                {
                    result.ValidA[i] = ValidA[i] && mask.Valid[i];
                }
                break;
            case "s":
                for (int i = 0; i < MAX_RANGE_SIZE; i++)
                {
                    result.ValidS[i] = ValidS[i] && mask.Valid[i];
                }
                break;
            default:
                break;
        }

        return result;
    }

    public long GetTotalInRange()
    {
        long xCount = ValidX.Where(e => e).Count();
        long mCount = ValidM.Where(e => e).Count();
        long aCount = ValidA.Where(e => e).Count();
        long sCount = ValidS.Where(e => e).Count();

        return xCount * mCount * aCount * sCount;
    }

    public override string ToString()
    {
        int xCount = ValidX.Where(e => e).Count();
        int mCount = ValidM.Where(e => e).Count();
        int aCount = ValidA.Where(e => e).Count();
        int sCount = ValidS.Where(e => e).Count();

        return $"x={xCount},m={mCount},a={aCount},s={sCount}";
    }
}
