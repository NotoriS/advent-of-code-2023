using System;
using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");

        List<string> strings = new List<string>();
        foreach (string s in reader.ReadLine().Split(',')) strings.Add(s);

        long sum = 0;
        foreach (string s in strings) 
        {
            long val = 0;
            foreach (char c in s) 
            {
                val += c;
                val *= 17;
                val %= 256;
            }
            sum += val;
        }

        Console.WriteLine(sum);
    }
}