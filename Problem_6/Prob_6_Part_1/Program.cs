using System.IO;
using System.Collections;

public class Problem
{
    public static void Main(string[] args)
    {
        List<int> times = new List<int> { 46, 82, 84, 79 };
        List<int> distanceRecords = new List<int> { 347, 1522, 1406, 1471 };

        List<int> waysToBeatRecord = new List<int> { 0, 0, 0, 0 };

        for (int i = 0; i < times.Count; i++)
        {
            for (int j = 0; j <= times[i]; j++)
            {
                if (j * (times[i] - j) > distanceRecords[i]) waysToBeatRecord[i]++;
            }
        }

        int result = 1;
        foreach (int way in waysToBeatRecord)
        {
            result *= way;
        }

        Console.WriteLine(result);
    }
}