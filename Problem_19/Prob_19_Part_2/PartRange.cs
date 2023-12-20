
public class PartRange
{
    public int MinX { get; set; }
    public int MaxX { get; set; }

    public int MinM { get; set; }
    public int MaxM { get; set; }

    public int MinA { get; set; }
    public int MaxA { get; set; }

    public int MinS { get; set; }
    public int MaxS { get; set; }

    public string Destination { get; set; }

    public bool HasValues
    {
        get { return MinX <= MaxX && MinM <= MaxM && MinA <= MaxA && MinS <= MaxS; }
    }

    public PartRange()
    {
        MinX = 1;
        MaxX = 4000;

        MinM = 1;
        MaxM = 4000;

        MinA = 1;
        MaxA = 4000;

        MinS = 1;
        MaxS = 4000;

        Destination = "";
    }

    public PartRange Combine(PartRange other)
    {
        PartRange newRange = new PartRange();

        newRange.MinX = Math.Max(MinX, other.MinX);
        newRange.MaxX = Math.Min(MaxX, other.MaxX);

        newRange.MinM = Math.Max(MinM, other.MinM);
        newRange.MaxM = Math.Min(MaxM, other.MaxM);

        newRange.MinA = Math.Max(MinA, other.MinA);
        newRange.MaxA = Math.Min(MaxA, other.MaxA);

        newRange.MinS = Math.Max(MinS, other.MinS);
        newRange.MaxS = Math.Min(MaxS, other.MaxS);

        return newRange;
    }
}
