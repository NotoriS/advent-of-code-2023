
public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<Brick> bricks = new List<Brick>();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            int[] left = line.Split('~')[0].Split(',').Select(e => int.Parse(e)).ToArray();
            int[] right = line.Split('~')[1].Split(',').Select(e => int.Parse(e)).ToArray();

            bricks.Add(new Brick(left[0], left[1], left[2], right[0], right[1], right[2]));
        }

        FallArea fallArea = new FallArea(bricks);
        fallArea.SimulateFall();

        Console.WriteLine(fallArea.GetCountOfBricksSafeToDisintegrate());
    }
}
