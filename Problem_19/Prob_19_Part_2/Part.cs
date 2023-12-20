
public class Part
{
    public int X { get; private set; }
    public int M { get; private set; }
    public int A { get; private set; }
    public int S { get; private set; }

    public int XMAS { get { return X + M + A + S; } }

    public Part(string str)
    {
        str = str.Remove(str.Length - 1);
        str = str.Substring(1);

        X = int.Parse(str.Split(',')[0].Split('=')[1]);
        M = int.Parse(str.Split(',')[1].Split('=')[1]);
        A = int.Parse(str.Split(',')[2].Split('=')[1]);
        S = int.Parse(str.Split(',')[3].Split('=')[1]);
    }

    public Part(int x, int m, int a, int s)
    {
        X = x;
        M = m;
        A = a;
        S = s;
    }

    public override string ToString()
    {
        return $"x={X},m={M},a={A},s={S}";
    }
}