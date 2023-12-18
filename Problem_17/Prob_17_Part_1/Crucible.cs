
using System.Text;

public class Crucible
{
    public int Row { get; private set; }
    public int Col { get; private set; }

    private Vector _currentDirection;
    private int _consecutiveStraightMoves;

    private HashSet<Tuple<int, int>> _visited;
    
    public int HeatLost { get; private set; }

    public Crucible(int startingRow, int startingCol, Vector startingDirection)
    {
        _visited = new HashSet<Tuple<int, int>>();
        
        Row = startingRow;
        Col = startingCol;
        _visited.Add(Tuple.Create(Row, Col));
        _currentDirection = startingDirection;
        _consecutiveStraightMoves = 0;
        HeatLost = 0;
    }

    public Crucible(Crucible original)
    {
        Row = original.Row;
        Col = original.Col;
        _visited = new HashSet<Tuple<int, int>>(original._visited);
        _currentDirection = original._currentDirection;
        _consecutiveStraightMoves = original._consecutiveStraightMoves;
        HeatLost = original.HeatLost;
    }

    public void Move(Direction direction, List<List<int>> grid)
    {
        switch (direction)
        {
            case Direction.Left:
                switch (_currentDirection)
                {
                    case Vector.Up:
                        Col--;
                        _currentDirection = Vector.Left;
                        break;
                    case Vector.Down:
                        Col++;
                        _currentDirection = Vector.Right;
                        break;
                    case Vector.Left:
                        Row++;
                        _currentDirection = Vector.Down;
                        break;
                    case Vector.Right:
                        Row--;
                        _currentDirection = Vector.Up;
                        break;
                }
                _consecutiveStraightMoves = 1;
                break;
            case Direction.Right:
                switch (_currentDirection)
                {
                    case Vector.Up:
                        Col++;
                        _currentDirection = Vector.Right;
                        break;
                    case Vector.Down:
                        Col--;
                        _currentDirection = Vector.Left;
                        break;
                    case Vector.Left:
                        Row--;
                        _currentDirection = Vector.Up;
                        break;
                    case Vector.Right:
                        Row++;
                        _currentDirection = Vector.Down;
                        break;
                }
                _consecutiveStraightMoves = 1;
                break;
            case Direction.Straight:
                if (_consecutiveStraightMoves >= 3)
                {
                    HeatLost = int.MaxValue;
                    return;
                }
                switch (_currentDirection)
                {
                    case Vector.Up:
                        Row--;
                        break;
                    case Vector.Down:
                        Row++;
                        break;
                    case Vector.Left:
                        Col--;
                        break;
                    case Vector.Right:
                        Col++;
                        break;
                }
                _consecutiveStraightMoves++;
                break;
        }

        if (_visited.Any(pos => pos.Item1 == Row && pos.Item2 == Col))
        {
            HeatLost = int.MaxValue;
            return;
        }
        
        if (Row < 0 || Row >= grid.Count || Col < 0 || Col >= grid[0].Count)
        {
            HeatLost = int.MaxValue;
        }
        else
        {
            HeatLost += grid[Row][Col];
            _visited.Add(Tuple.Create(Row, Col));
        }
    }

    public bool IsAtEnd(List<List<int>> grid)
    {
        return Row == grid.Count - 1 && Col == grid[0].Count - 1;
    }

    public void PrintVisited(List<List<int>> grid)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[i].Count; j++)
            {
                if (_visited.Any(pos => pos.Item1 == i && pos.Item2 == j))
                {
                    sb.Append('#');
                }
                else 
                {
                    sb.Append('.');
                }
            }
            sb.Append('\n');
        }

        Console.WriteLine(sb.ToString());
    }

    public override string ToString()
    {
        return Row + "," + Col + "," + _consecutiveStraightMoves + (_consecutiveStraightMoves > 1 ? _currentDirection : "");
    }
}
