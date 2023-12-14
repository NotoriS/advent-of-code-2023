using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        return FindDifferentArrangementsRec(record.ToCharArray(), GroupListToRegex(groups));
    }

    private static int FindDifferentArrangementsRec(char[] record, Regex pattern)
    {
        if (!record.Contains('?'))
        {
            string recordStr = new string(record);
            if (pattern.IsMatch(recordStr))
            {
                Console.WriteLine(recordStr);
                return 1;
            }
            return 0;
        }

        for (int i = 0; i < record.Length; i++)
        {
            if (record[i] == '?')
            {
                char[] replacedWithPeriod = new char[record.Length];
                record.CopyTo(replacedWithPeriod, 0);
                replacedWithPeriod[i] = '.';

                char[] replacedWithHash = new char[record.Length];
                record.CopyTo(replacedWithHash, 0);
                replacedWithHash[i] = '#';

                return FindDifferentArrangementsRec(replacedWithPeriod, pattern) + FindDifferentArrangementsRec(replacedWithHash, pattern);
            }
        }

        return 0;
    }

    private static Regex GroupListToRegex(List<int> groups)
    {
        groups = new List<int>(groups); // Clone list

        string regex = "^\\.*";
        while (groups.Count > 0)
        {
            int groupSize = groups[0];
            groups.RemoveAt(0);

            string regexSection = "#{" + groupSize + "}" + (groups.Count > 0 ? "\\.+" : "\\.*$");
            regex = string.Concat(regex, regexSection);
        }

        return new Regex(regex);
    }
}
