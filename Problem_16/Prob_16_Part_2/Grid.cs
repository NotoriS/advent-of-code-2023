
public class Grid
{
    private List<List<Tile>> _grid;

    public Grid(List<List<char>> grid)
    {
        _grid = new List<List<Tile>>();

        foreach (List<char> row in grid)
        {
            List<Tile> tileRow = new List<Tile>();
            foreach (char c in row)
            {
                tileRow.Add(createTile(c));
            }
            _grid.Add(tileRow);
        }

        List<Tile> upperBoundary = new List<Tile>();
        for (int i = 0; i < _grid[0].Count; i++) upperBoundary.Add(createTile('#'));
        List<Tile> lowerBoundary = new List<Tile>();
        for (int i = 0; i < _grid[0].Count; i++) lowerBoundary.Add(createTile('#'));

        _grid.Insert(0, upperBoundary);
        _grid.Add(lowerBoundary);

        foreach (List<Tile> row in _grid)
        {
            row.Insert(0, createTile('#'));
            row.Add(createTile('#'));
        }
    }

    public void CastBeam(int startingRow, int startingCol, Vector startingDirection)
    {
        List<Beam> beams = new List<Beam>();
        beams.Add(new Beam(startingRow, startingCol, startingDirection));

        while (beams.Count > 0)
        {
            List<Beam> addAfterUpdate = new List<Beam>();
            List<Beam> removeAfterUpdate = new List<Beam>();

            foreach (Beam beam in beams)
            {
                beam.Move();
                beam.CurrentDirection = _grid[beam.Row][beam.Col].GetNextDirection(beam.CurrentDirection);

                switch (beam.CurrentDirection)
                {
                    case Vector.zero:
                        removeAfterUpdate.Add(beam);
                        break;
                    case Vector.leftRight:
                    case Vector.upDown:
                        addAfterUpdate.Add(beam.Split());
                        break;
                }
            }

            beams.AddRange(addAfterUpdate);
            foreach (Beam beam in removeAfterUpdate) beams.Remove(beam);
        }
    }

    public int GetEnergizedTileCount()
    {
        int sum = 0;
        foreach (List<Tile> row in _grid)
        {
            foreach (Tile tile in row)
            {
                if (tile.IsEnergized) sum++;
            }
        }
        return sum;
    }

    private Tile createTile(char c)
    {
        switch (c)
        {
            case '.':
                return new EmptyTile();
            case '#':
                return new BorderTile();
            case '/':
            case '\\':
                return new MirrorTile(c);
            case '-':
            case '|':
                return new SplitterTile(c);
        }

        return null;
    }
}
