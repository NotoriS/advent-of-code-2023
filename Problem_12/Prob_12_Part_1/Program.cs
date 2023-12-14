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
            int next = FindDifferentArrangements(springRecords[i], springGroups[i]);
            sum += next;
        }

        Console.WriteLine(sum);
    }

    private static int FindDifferentArrangements(string record, List<int> groups)
    {
        return FindDifferentArrangementsRec(0, record, groups);
    }

    private static int FindDifferentArrangementsRec(int strIndex, string record, List<int> groups)
    {
        // Create clones
        groups = new List<int>(groups);

        if (groups.Count == 0)
        {
            if (strIndex - 1 < record.Length && record.Substring(strIndex - 1).Contains('#')) return 0;

            return 1;
        }

        int sum = 0;
        for (; strIndex + groups[0] - 1 < record.Length; strIndex++)
        {
            bool subStrCantBeThisGroup =
                record.Substring(strIndex, groups[0]).Contains('.')
                || (strIndex + groups[0] < record.Length && record[strIndex + groups[0]] == '#');
            if (subStrCantBeThisGroup)
            {
                if (record[strIndex] == '#') break;
                continue;
            }

            List<int> nextGroups = new List<int>(groups);
            nextGroups.RemoveAt(0);

            sum += FindDifferentArrangementsRec(strIndex + groups[0] + 1, record, nextGroups);

            if (record[strIndex] == '#') break;
        }

        return sum;
    }
}