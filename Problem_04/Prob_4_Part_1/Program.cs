using System.IO;
using System.Collections;

public class Problem
{
    public static void Main(string[] args)
    {
        List<string> lines = GetInputAsStringList();
        Dictionary<int, int> cardInstances = new Dictionary<int, int>();

        for (int i = 0; i < lines.Count; i++)
        {
            cardInstances[i] = 1;
        }

        for (int i = 0; i < lines.Count; i++)
        {
            HashSet<string> winningNumbers = lines[i].Split(':', '|')[1].Split(' ', '\t').ToHashSet();
            winningNumbers.Remove("");
            winningNumbers.Remove("\t");
            HashSet<string> myNumbers = lines[i].Split(':', '|')[2].Split(' ', '\t').ToHashSet();
            myNumbers.Remove("");
            myNumbers.Remove("\t");

            HashSet<string> common = myNumbers.Where(x => winningNumbers.Contains(x)).ToHashSet();

            for (int j = 1; j <= common.Count && i + j < lines.Count; j++)
            {
                cardInstances[i + j] += cardInstances[i];
            }
        }

        int finalCardCount = 0;
        foreach (int instances in cardInstances.Values) finalCardCount += instances;

        Console.WriteLine(finalCardCount);
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
