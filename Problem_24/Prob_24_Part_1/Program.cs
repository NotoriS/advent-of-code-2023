
public class Problem
{
    private const long TEST_AREA_MIN = 7;
    private const long TEST_AREA_MAX = 27;

    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<Hail> hail = new List<Hail>();

        while (reader.Peek() != -1)
        {
            long[] line = reader.ReadLine().Split(',', '@').Select(s => long.Parse(s.Trim())).ToArray();
            hail.Add(new Hail(line[0], line[1], line[2], line[3], line[4], line[5]));
        }

        List<Intersection> intersections = new List<Intersection>();
        for (int i = 0; i < hail.Count - 1; i++)
        {
            for (int j = i + 1; j < hail.Count; j++)
            {
                Intersection? intersection = Intersection.Get2DIntersection(hail[i], hail[j]);
                if (intersection != null) intersections.Add(intersection);
            }
        }

        List<Intersection> inRange = new List<Intersection>();
        foreach (Intersection i in intersections)
        {
            if (i.X < TEST_AREA_MIN) continue;
            if (i.Y < TEST_AREA_MIN) continue;
            if (i.X > TEST_AREA_MAX) continue;
            if (i.Y > TEST_AREA_MAX) continue;
            inRange.Add(i);
        }

        Console.WriteLine(inRange.Count);
    }
}
