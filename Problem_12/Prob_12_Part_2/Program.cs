using System.IO;
using System.Text;
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

            string springRecord = line.Split(' ')[0];
            string expandedSpringRecord = springRecord;
            for (int i = 0; i < 4; i++) expandedSpringRecord = string.Concat(expandedSpringRecord + "?", springRecord);
            springRecords.Add(expandedSpringRecord);

            List<int> springGroup = line.Split(' ')[1].Split(',').Select(n => int.Parse(n)).ToList();
            List<int> expandedSpringGroup = new List<int>(springGroup);
            for (int i = 0; i < 4; i++) expandedSpringGroup.AddRange(springGroup);
            springGroups.Add(expandedSpringGroup);
        }

        long sum = 0;
        for (int i = 0; i < springRecords.Count; i++)
        {
            sum += FindDifferentArrangements(springRecords[i], springGroups[i]);
        }

        Console.WriteLine(sum);
    }

    private static long FindDifferentArrangements(string record, List<int> groups)
    {
        Dictionary<string, long> memoization = new Dictionary<string, long>();
        return FindDifferentArrangementsRec(0, record, groups, memoization);
    }

    private static long FindDifferentArrangementsRec(int strIndex, string record, List<int> groups, Dictionary<string, long> memoization)
    {
        // Create clones
        groups = new List<int>(groups);

        if (groups.Count == 0)
        {
            if (strIndex - 1 < record.Length && record.Substring(strIndex - 1).Contains('#')) return 0;
            return 1;
        }

        long sum = 0;
        for (; strIndex + groups[0] - 1 < record.Length; strIndex++)
        {
            CacheData cacheData = new CacheData(strIndex, record, groups);
            string cacheDataStr = cacheData.ToString();
            if (memoization.ContainsKey(cacheDataStr))
            {
                sum += memoization[cacheDataStr];
                if (record[strIndex] == '#') break;
                continue;
            }

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

            long nextAdded = FindDifferentArrangementsRec(strIndex + groups[0] + 1, record, nextGroups, memoization);
            sum += nextAdded;
            memoization.Add(cacheDataStr, nextAdded);

            if (record[strIndex] == '#') break;
        }

        return sum;
    }

    private class CacheData
    {
        private int _strIndex;
        private string _record;
        private List<int> _groups;

        public CacheData(int strIndex, string record, List<int> groups)
        {
            _strIndex = strIndex;
            _record = record;
            _groups = new List<int>(groups);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_strIndex);
            sb.Append(_record);
            foreach (int n in _groups)
            {
                sb.Append(n);
                sb.Append(',');
            }

            return sb.ToString();
        }
    }
}