using System.IO;
using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");

        List<string> lines = new List<string>();
        List<string> springRecords = new List<string>();
        List<List<int>> springGroups = new List<List<int>>();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            lines.Add(line);
            springRecords.Add(line.Split(' ')[0]);
            springGroups.Add(line.Split(' ')[1].Split(',').Select(n => int.Parse(n)).ToList());
        }

        int sum = 0;
        for (int i = 0; i < springRecords.Count; i++)
        {
            Console.WriteLine(lines[i] + ":"); // Debug print
            int next = FindDifferentArrangements(springRecords[i], springGroups[i]);
            sum += next;
            Console.WriteLine(next + "\n"); // Debug print
        }

        Console.WriteLine(sum);
    }

    private static int FindDifferentArrangements(string record, List<int> groups)
    {
        return FindDifferentArrangementsRec(0, record, groups, new List<Tuple<int, int>>());
    }

    private static int FindDifferentArrangementsRec(int strIndex, string record, List<int> groups, List<Tuple<int, int>> debugHelper)
    {
        // Create clones
        groups = new List<int>(groups);
        debugHelper = new List<Tuple<int, int>>(debugHelper);

        if (groups.Count == 0)
        {
            if (strIndex - 1 < record.Length && record.Substring(strIndex - 1).Contains('#')) return 0;

            // Debug print
            for (int i = 0; i < record.Length; i++)
            {
                if (debugHelper.Count > 0 && i == debugHelper[0].Item1)
                {
                    for (; i < debugHelper[0].Item1 + debugHelper[0].Item2; i++) Console.Write('#');
                    i--;
                    debugHelper.RemoveAt(0);
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();

            return 1;
        }

        int sum = 0;
        for (; strIndex + groups[0] - 1 < record.Length; strIndex++)
        {
            bool subStrCantBeThisGroup =
                record.Substring(strIndex, groups[0]).Contains('.')
                || (strIndex + groups[0] < record.Length && record[strIndex + groups[0]] == '#');
            if (subStrCantBeThisGroup) continue;

            List<int> nextGroups = new List<int>(groups);
            nextGroups.RemoveAt(0);

            debugHelper.Add(Tuple.Create(strIndex, groups[0]));
            sum += FindDifferentArrangementsRec(strIndex + groups[0] + 1, record, nextGroups, debugHelper);
            debugHelper.RemoveAt(debugHelper.Count - 1);

            if (record[strIndex] == '#') break;
        }

        return sum;
    }
}
