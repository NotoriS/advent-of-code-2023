using System.IO;
using System.Collections;
using System.Net.NetworkInformation;

public class Solution
{
    public static void Main(string[] args)
    {
        List<string> lines = new List<string>();
        StreamReader fileReader = new StreamReader("../../../input.txt");

        while (fileReader.Peek() != -1)
        {
            lines.Add(fileReader.ReadLine());
        }

        Console.WriteLine(SumFirstAndLastDigitOfEachLine(lines));
    }

    public static int SumFirstAndLastDigitOfEachLine(List<String> lines)
    {
        int sum = 0;

        foreach (String line in lines)
        {
            sum += GetNumberForString(line);
        }

        return sum;
    }

    public static int GetNumberForString(string line)
    {
        List<int> allDigits = new List<int>();

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] >= '0' && line[i] <= '9')
            {
                allDigits.Add(line[i] - '0');
                continue;
            }

            if (StartsWithDigitString(line.Substring(i)))
            {
                allDigits.Add(GetStartingDigitString(line.Substring(i)));
            }
        }

        return allDigits[0] * 10 + allDigits[allDigits.Count - 1];
    }

    public static bool StartsWithDigitString(string s)
    {
        return s.StartsWith("zero")
            || s.StartsWith("one")
            || s.StartsWith("two")
            || s.StartsWith("three")
            || s.StartsWith("four")
            || s.StartsWith("five")
            || s.StartsWith("six")
            || s.StartsWith("seven")
            || s.StartsWith("eight")
            || s.StartsWith("nine");
    }

    public static int GetStartingDigitString(string s)
    {
        if (s.StartsWith("zero")) return 0;
        if (s.StartsWith("one")) return 1;
        if (s.StartsWith("two")) return 2;
        if (s.StartsWith("three")) return 3;
        if (s.StartsWith("four")) return 4;
        if (s.StartsWith("five")) return 5;
        if (s.StartsWith("six")) return 6;
        if (s.StartsWith("seven")) return 7;
        if (s.StartsWith("eight")) return 8;
        if (s.StartsWith("nine")) return 9;

        return -1;
    }
}
