
public class Instruction
{
    private Direction _direction;
    private int _distance;

    public Instruction(string direction, int distance)
    {
        switch (direction)
        {
            case "3":
                _direction = Direction.Up;
                break;
            case "1":
                _direction = Direction.Down;
                break;
            case "2":
                _direction = Direction.Left;
                break;
            case "0":
                _direction = Direction.Right;
                break;
            default:
                throw new Exception("Invalid direction.");
        }
        _distance = distance;
    }

    public void Execute(List<Vertex> vertices)
    {
        Vertex start;
        Vertex end;

        int offset = 0;
        if (vertices.Count == 0)
        {
            start = new Vertex(0, 0);
            vertices.Add(start);
            offset = -1;
        }
        else
        {
            start = vertices.Last();
        }

        start.ExitDirection = _direction;

        switch (_direction)
        {
            case Direction.Up:
                end = new Vertex(start.X, start.Y - _distance - offset, _direction);
                break;
            case Direction.Down:
                end = new Vertex(start.X, start.Y + _distance + offset, _direction);
                break;
            case Direction.Left:
                end = new Vertex(start.X - _distance - offset, start.Y, _direction);
                break;
            case Direction.Right:
                end = new Vertex(start.X + _distance + offset, start.Y, _direction);
                break;
            default:
                throw new Exception("Invalid direction.");
        }

        vertices.Add(end);
    }
}
