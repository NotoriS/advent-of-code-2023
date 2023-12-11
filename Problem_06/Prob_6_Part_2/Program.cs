using System.IO;
using System.Collections;

public class Problem
{
    public static void Main(string[] args)
    {
        long time = 46828479;
        long distanceRecord = 347152214061471;
        long waysToBeatRecord = 0;

        for (int i = 0; i <= time; i++)
        {
            if (i * (time - i) > distanceRecord) waysToBeatRecord++;
        }

        Console.WriteLine(waysToBeatRecord);
    }
}