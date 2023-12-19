
public class Instruction
{
    private string _direction;
    private int _distance;

    public Instruction(string direction, int distance)
    {
        _direction = direction;
        _distance = distance;
    }

    public void Execute(List<Tuple<int, int>> holeBorder)
    {
        int loopStart = 0;
        if (holeBorder.Count == 0)
        {
            holeBorder.Add(Tuple.Create(0, 0));
            loopStart = 1;
        }

        switch (_direction)
        {
            case "U":
                for (int i = loopStart; i < _distance; i++)
                {
                    Tuple<int, int> last = holeBorder.Last();
                    holeBorder.Add(Tuple.Create(last.Item1 - 1, last.Item2));
                }
                break;
            case "D":
                for (int i = loopStart; i < _distance; i++)
                {
                    Tuple<int, int> last = holeBorder.Last();
                    holeBorder.Add(Tuple.Create(last.Item1 + 1, last.Item2));
                }
                break;
            case "L":
                for (int i = loopStart; i < _distance; i++)
                {
                    Tuple<int, int> last = holeBorder.Last();
                    holeBorder.Add(Tuple.Create(last.Item1, last.Item2 - 1));
                }
                break;
            case "R":
                for (int i = loopStart; i < _distance; i++)
                {
                    Tuple<int, int> last = holeBorder.Last();
                    holeBorder.Add(Tuple.Create(last.Item1, last.Item2 + 1));
                }
                break;
        }
    }
}
