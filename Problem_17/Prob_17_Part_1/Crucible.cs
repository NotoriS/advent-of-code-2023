
using System.Text;

public class Crucible
{
    private int _row;
    private int _col;

    private Vector _currentDirection;
    private int _consecutiveStraightMoves;

    private HashSet<Tuple<int, int>> _visited;
    
    public int HeatLost { get; private set; }

    public Crucible(int startingRow, int startingCol, Vector startingDirection)
    {
        _visited = new HashSet<Tuple<int, int>>();
        
        _row = startingRow;
        _col = startingCol;
        _visited.Add(Tuple.Create(_row, _col));
        _currentDirection = startingDirection;
        _consecutiveStraightMoves = 0;
        HeatLost = 0;
    }

    public Crucible(Crucible original)
    {
        _row = original._row;
        _col = original._col;
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
                        _col--;
                        _currentDirection = Vector.Left;
                        break;
                    case Vector.Down:
                        _col++;
                        _currentDirection = Vector.Right;
                        break;
                    case Vector.Left:
                        _row++;
                        _currentDirection = Vector.Down;
                        break;
                    case Vector.Right:
                        _row--;
                        _currentDirection = Vector.Up;
                        break;
                }
                _consecutiveStraightMoves = 1;
                break;
            case Direction.Right:
                switch (_currentDirection)
                {
                    case Vector.Up:
                        _col++;
                        _currentDirection = Vector.Right;
                        break;
                    case Vector.Down:
                        _col--;
                        _currentDirection = Vector.Left;
                        break;
                    case Vector.Left:
                        _row--;
                        _currentDirection = Vector.Up;
                        break;
                    case Vector.Right:
                        _row++;
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
                        _row--;
                        break;
                    case Vector.Down:
                        _row++;
                        break;
                    case Vector.Left:
                        _col--;
                        break;
                    case Vector.Right:
                        _col++;
                        break;
                }
                _consecutiveStraightMoves++;
                break;
        }

        if (_visited.Any(pos => pos.Item1 == _row && pos.Item2 == _col))
        {
            HeatLost = int.MaxValue;
            return;
        }
        
        if (_row < 0 || _row >= grid.Count || _col < 0 || _col >= grid[0].Count)
        {
            HeatLost = int.MaxValue;
        }
        else
        {
            HeatLost += grid[_row][_col];
            _visited.Add(Tuple.Create(_row, _col));
        }
    }

    public bool IsAtEnd(List<List<int>> grid)
    {
        return _row == grid.Count - 1 && _col == grid[0].Count - 1;
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
        return _row + "," + _col + "," + _consecutiveStraightMoves;
    }
}
