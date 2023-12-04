using System.IO;
using System.Collections;

public class Problem
{
    public static void Main(string[] args)
    {
        List<string> lines = GetInputAsStringList();
        double sum = 0;

        foreach (string line in lines)
        {
            HashSet<string> winningNumbers = line.Split(':', '|')[1].Split(' ', '\t').ToHashSet();
            winningNumbers.Remove("");
            winningNumbers.Remove("\t");
            HashSet<string> myNumbers = line.Split(':', '|')[2].Split(' ', '\t').ToHashSet();
            myNumbers.Remove("");
            myNumbers.Remove("\t");

            HashSet<string> common = myNumbers.Where(x => winningNumbers.Contains(x)).ToHashSet();
            if (common.Count() > 0)
            {
                sum += Math.Pow(2, common.Count - 1);
            }
        }

        Console.WriteLine(sum);
    }

    private static List<string> GetInputAsStringList()
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<string> lines = new List<string>();

        while (reader.Peek() != -1)
        {
            lines.Add(reader.ReadLine());
        }

        reader.Close();
        return lines;
    }
}
