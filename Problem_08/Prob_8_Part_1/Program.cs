using System.IO;
using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<char> lrInstrucitons = new List<char>();

        while (true)
        {
            string line = reader.ReadLine();
            if (line == "") break;

            foreach (char c in line)
            {
                lrInstrucitons.Add(c);
            }
        }

        Dictionary<string, Tuple<string, string>> map = new Dictionary<string, Tuple<string, string>>();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();

            string start = line.Substring(0, 3);
            string left = line.Substring(7, 3);
            string right = line.Substring(12, 3);
            
            map.Add(start, Tuple.Create(left, right));
        }

        string location = "AAA";
        int steps = 0;

        for (; location != "ZZZ"; steps++)
        {
            if (lrInstrucitons[steps % lrInstrucitons.Count] == 'L')
            {
                location = map[location].Item1;
            }
            else if (lrInstrucitons[steps % lrInstrucitons.Count] == 'R')
            {
                location = map[location].Item2;
            }
            else
            {
                throw new InvalidDataException("lrInstructions should only ever contain L or R.");
            }
        }

        Console.WriteLine(steps);
    }
}