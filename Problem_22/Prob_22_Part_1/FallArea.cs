
public class FallArea
{
    private List<Brick> _bricks;
    private List<List<List<Brick?>>> _space;

    public FallArea(List<Brick> bricks)
    {
        _bricks = new List<Brick>(bricks);

        _space = new List<List<List<Brick?>>>();
        AddNewZLayer();
    }

    public void SimulateFall()
    {
        foreach (Brick brick in _bricks.OrderBy(b => b.LowestZ))
        {
            int finalZLevel = 0;
            foreach (Tuple<int, int> point in brick.XYCoverage)
            {
                if (_space.Last()[point.Item2][point.Item1] != null) AddNewZLayer();

                for (int z = _space.Count - 1; z >= 0; z--)
                {
                     if (_space[z][point.Item2][point.Item1] != null) finalZLevel = Math.Max(z + 1, finalZLevel);  
                }
            }

            foreach (Tuple<int, int> point in brick.XYCoverage)
            {
                for (int zOffest = 0; zOffest < brick.Height; zOffest++)
                {
                    if (finalZLevel + zOffest >= _space.Count) AddNewZLayer();
                    _space[finalZLevel + zOffest][point.Item2][point.Item1] = brick;
                }
            }
        }

        for (int z = 0; z < _space.Count; z++)
        {
            for (int y = 0; y < _space[z].Count; y++)
            {
                for (int x = 0; x < _space[z][y].Count; x++)
                {
                    if (_space[z][y][x] == null) continue;

                    if (PointIsInSpace(x, y, z - 1) && _space[z - 1][y][x] != null)
                    {
                        _space[z][y][x].AddBelow(_space[z - 1][y][x]);
                    }

                    if (PointIsInSpace(x, y, z + 1) && _space[z + 1][y][x] != null)
                    {
                        _space[z][y][x].AddAbove(_space[z + 1][y][x]);
                    }
                }
            }
        }
    }

    public int GetCountOfBricksSafeToDisintegrate()
    {
        int sum = 0;
        foreach (Brick brick in _bricks)
        {
            if (brick.CanBeDisintegrated) sum++;
        }

        return sum;
    }

    private void AddNewZLayer()
    {
        int maxY = _bricks.Select(b => Math.Max(b.Y1, b.Y2)).Max();
        int maxX = _bricks.Select(b => Math.Max(b.X1, b.X2)).Max();

        List<List<Brick?>> new2DLayer = new List<List<Brick?>>();
        for (int y = 0; y <= maxY; y++)
        {
            List<Brick?> row = new List<Brick?>();
            for (int x = 0; x <= maxX; x++)
            {
                row.Add(null);
            }
            new2DLayer.Add(row);
        }
        _space.Add(new2DLayer);
    }

    private bool PointIsInSpace(int x, int y, int z)
    {
        return z >= 0 && z < _space.Count
            && y >= 0 && y < _space[z].Count
            && x >= 0 && x < _space[z][y].Count;
    }
}
