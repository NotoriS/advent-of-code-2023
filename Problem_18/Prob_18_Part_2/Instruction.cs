
public class Instruction
{
    private string _direction;
    private int _distance;

    public Instruction(string direction, int distance)
    {
        _direction = direction;
        _distance = distance;
    }

    public void Execute(List<Tuple<Tuple<int, int>, Tuple<int, int>>> edges)
    {
        Tuple<int, int> start;
        Tuple<int, int> end;

        int offset = 0;
        if (edges.Count == 0)
        {
            start = Tuple.Create(0, 0);
            offset = -1;
        }
        else
        {
            start = edges.Last().Item2;
        }

        switch (_direction)
        {
            case "U":
                end = Tuple.Create(start.Item1 - _distance - offset, start.Item2);
                break;
            case "D":
                end = Tuple.Create(start.Item1 + _distance + offset, start.Item2);
                break;
            case "L":
                end = Tuple.Create(start.Item1, start.Item2 - _distance - offset);
                break;
            case "R":
                end = Tuple.Create(start.Item1, start.Item2 + _distance + offset);
                break;
            default:
                throw new Exception("Invalid direction.");
        }

        edges.Add(Tuple.Create(start, end));
    }
}
