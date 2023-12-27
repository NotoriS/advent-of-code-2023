
public class Path
{
    public int Row { get; private set; }
    public int Col { get; private set; }

    public int TotalMoves { get; private set; }

    private List<Tuple<int, int>> _positionHistory;

    public Path(int row, int col)
    {
        Row = row;
        Col = col;
        TotalMoves = 0;

        _positionHistory = new List<Tuple<int, int>>();
        _positionHistory.Add(Tuple.Create(row, col));
    }

    public Path(Path original)
    {
        Row = original.Row;
        Col = original.Col;
        TotalMoves = original.TotalMoves;

        _positionHistory = new List<Tuple<int, int>>(original._positionHistory);
    }

    public bool CanMove(Direction direction)
    {
        Tuple<int, int> nextPosition;
        switch (direction)
        {
            case Direction.Up:
                nextPosition = Tuple.Create(Row - 1, Col);
                break;
            case Direction.Down:
                nextPosition = Tuple.Create(Row + 1, Col);
                break;
            case Direction.Left:
                nextPosition = Tuple.Create(Row, Col - 1);
                break;
            case Direction.Right:
                nextPosition = Tuple.Create(Row, Col + 1);
                break;
            default:
                throw new Exception("And unknown direction was used.");
        }
        return !_positionHistory.Contains(nextPosition);
    }

    public void Move(Direction direction)
    {
        _positionHistory.Add(Tuple.Create(Row, Col));
        switch (direction)
        {
            case Direction.Up:
                Row--;
                break;
            case Direction.Down:
                Row++;
                break;
            case Direction.Left:
                Col--;
                break;
            case Direction.Right:
                Col++;
                break;
            default:
                throw new Exception("And unknown direction was used.");
        }
        TotalMoves++;
    }

    public override string ToString()
    {
        return $"{Row},{Col}";
    }
}
