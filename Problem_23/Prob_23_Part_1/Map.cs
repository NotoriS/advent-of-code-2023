
public class Map
{
    List<string> _map;

    public Map(List<string> input)
    {
        _map = new List<string>(input);
    }

    public int FindLongestPath() 
    {
        Queue<Path> paths = new Queue<Path>();
        paths.Enqueue(new Path(0, 1));

        List<Path> fullPaths = new List<Path>();

        while (paths.Count > 0)
        {
            Path current = paths.Dequeue();

            if (current.Row == _map.Count - 1 && current.Col == _map[current.Row].Length - 2)
            {
                fullPaths.Add(current);
                continue;
            }

            if (IsValidMove(current, Direction.Up) && current.CanMove(Direction.Up))
            {
                Path up = new Path(current);
                up.Move(Direction.Up);
                paths.Enqueue(up);
            }
            if (IsValidMove(current, Direction.Down) && current.CanMove(Direction.Down))
            {
                Path down = new Path(current);
                down.Move(Direction.Down);
                paths.Enqueue(down);
            }
            if (IsValidMove(current, Direction.Left) && current.CanMove(Direction.Left))
            {
                Path left = new Path(current);
                left.Move(Direction.Left);
                paths.Enqueue(left);
            }
            if (IsValidMove(current, Direction.Right) && current.CanMove(Direction.Right))
            {
                Path right = new Path(current);
                right.Move(Direction.Right);
                paths.Enqueue(right);
            }
        }

        return fullPaths.Select(p => p.TotalMoves).Max();
    }

    private bool IsValidMove(Path path, Direction direction)
    {
        switch (_map[path.Row][path.Col])
        {
            case '#':
                return false;
            case '.':
                switch (direction)
                {
                    case Direction.Up:
                        return CoordinateIsWalkable(path.Row - 1, path.Col);
                    case Direction.Down:
                        return CoordinateIsWalkable(path.Row + 1, path.Col);
                    case Direction.Left:
                        return CoordinateIsWalkable(path.Row, path.Col - 1);
                    case Direction.Right:
                        return CoordinateIsWalkable(path.Row, path.Col + 1);
                    default:
                        throw new Exception("And unknown direction was used.");
                }
            case '^':
                if (direction != Direction.Up) return false;
                return CoordinateIsWalkable(path.Row - 1, path.Col);
            case 'v':
                if (direction != Direction.Down) return false;
                return CoordinateIsWalkable(path.Row + 1, path.Col);
            case '<':
                if (direction != Direction.Left) return false;
                return CoordinateIsWalkable(path.Row, path.Col - 1);
            case '>':
                if (direction != Direction.Right) return false;
                return CoordinateIsWalkable(path.Row, path.Col + 1);
            default:
                throw new Exception("The map contains an invalid character.");
        }
    }

    private bool CoordinateIsWalkable(int row, int col)
    {
        if (row < 0 || row >= _map.Count) return false;
        if (col < 0 || col >= _map[row].Length) return false;

        return _map[row][col] != '#';
    }
}
