using System;
using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");

        List<List<char>> input = new List<List<char>>();
        while (reader.Peek() != -1)
        {
            input.Add(reader.ReadLine().ToCharArray().ToList());
        }

        Platform platform = new Platform(input);

        const int ITERATIONS = 1000000000;

        // Find cycle
        List<string> checkedPlatforms = new List<string>();
        List<int> firstCycleIndices = new List<int>();
        for (int i = 0; i < ITERATIONS;  i++) 
        {
            platform.TiltNorth();
            platform.TiltWest();
            platform.TiltSouth();
            platform.TiltEast();

            string platformString = platform.ToString();
            if (checkedPlatforms.Contains(platformString))
            {
                if (firstCycleIndices.Contains(checkedPlatforms.IndexOf(platformString))) break;
                firstCycleIndices.Add(checkedPlatforms.IndexOf(platformString));
            }
            else
            {
                checkedPlatforms.Add(platformString);
            }
        }

        int equivalentIndex = firstCycleIndices[0] + (ITERATIONS - firstCycleIndices[0]) % firstCycleIndices.Count;

        platform = new Platform(input);

        // Get to equivalent state
        for (int i = 0; i < equivalentIndex; i++)
        {
            platform.TiltNorth();
            platform.TiltWest();
            platform.TiltSouth();
            platform.TiltEast();
        }

        Console.WriteLine(platform.GetNorthLoad());
    }
}
