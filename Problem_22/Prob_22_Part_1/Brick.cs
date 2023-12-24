
public class Brick
{
    private HashSet<Brick> _above;
    private HashSet<Brick> _below;

    public int X1 { get; private set; }
    public int Y1 { get; private set; }
    public int Z1 { get; private set; }

    public int X2 { get; private set; }
    public int Y2 { get; private set; }
    public int Z2 { get; private set; }

    public int Width { get { return Math.Abs(X1 - X2) + 1; } }
    public int Length { get { return Math.Abs(Y1 - Y2) + 1; } }
    public int Height { get { return Math.Abs(Z1 - Z2) + 1; } }

    public int LowestZ { get { return Math.Min(Z1, Z2); } }

    public IEnumerable<Tuple<int, int>> XYCoverage { get; private set; }

    public bool CanBeDisintegrated
    {
        get
        {
            foreach (Brick above in _above)
            {
                if (above._below.Count == 1) return false;
            }
            return true;
        }
    }

    public Brick(int x1, int y1, int z1, int x2, int y2, int z2)
    {
        _above = new HashSet<Brick>();
        _below = new HashSet<Brick>();

        X1 = x1;
        Y1 = y1;
        Z1 = z1;
        X2 = x2;
        Y2 = y2;
        Z2 = z2;

        List<Tuple<int, int>> XYCoverageList = new List<Tuple<int, int>>();
        for (int x = Math.Min(X1, X2); x <= Math.Max(X1, X2); x++)
        {
            for (int y = Math.Min(Y1, Y2); y <= Math.Max(Y1, Y2); y++)
            {
                XYCoverageList.Add(Tuple.Create(x, y));
            }
        }
        XYCoverage = XYCoverageList;
    }

    public void AddAbove(Brick brick)
    {
        if (brick != this) _above.Add(brick);
    }

    public void AddBelow(Brick brick)
    {
        if (brick != this) _below.Add(brick);
    }

    //public void FallToHeight(int height)
    //{
    //    int currentX1 = _x1;
    //    int currentX2 = _x2;

    //    if (currentX1 < _x1)
    //    {
    //        _x1 = height;
    //        _x2 = height + currentX2 - currentX1;
    //    }
    //    else
    //    {
    //        _x2 = height;
    //        _x1 = height + currentX1 - currentX2;
    //    }
    //}

    public override string ToString()
    {
        return $"{X1},{Y1},{Z1}~{X2},{Y2},{Z2}";
    }
}
