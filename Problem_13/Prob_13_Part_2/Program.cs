using System.IO;
using System.Collections;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<Valley> valleys = new List<Valley>();

        List<string> rows = new List<string>();
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (line == "")
            {
                valleys.Add(new Valley(rows));
                rows = new List<string>();
                continue;
            }
            rows.Add(line);
        }
        valleys.Add(new Valley(rows));
        rows = new List<string>();

        long sum = 0;
        foreach (Valley valley in valleys)
        {
            int columnsLeft = valley.GetColumnsLeftReflectionPoint();
            sum += columnsLeft;
            Console.WriteLine("Cols: " + columnsLeft);

            int rowsAbove = valley.GetRowsAboveReflectionPoint();
            sum += rowsAbove * 100;
            Console.WriteLine("Rows: " + rowsAbove + "\n");
        }

        Console.WriteLine(sum);
    }
}