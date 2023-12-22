
public class Problem
{
    public const int STEPS = 64;

    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<string> input = new List<string>();

        while (reader.Peek() != -1)
        {
            input.Add(reader.ReadLine());
        }

        List<List<Plot>> plotGrid = new List<List<Plot>>();
        for (int i = 0; i < input.Count; i++)
        {
            List<Plot> row = new List<Plot>();
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == '.')
                {
                    row.Add(new Plot(false));
                }
                else if (input[i][j] == 'S')
                {
                    row.Add(new Plot(true));
                }
                else 
                {
                    row.Add(null);
                }
            }
            plotGrid.Add(row);
        }

        List<Plot> plots = new List<Plot>();
        for (int i = 0; i < plotGrid.Count; i++)
        {
            for (int j = 0; j < plotGrid[i].Count; j++)
            {
                if (plotGrid[i][j] == null) continue;

                Plot plot = plotGrid[i][j];
                List<Plot> neighbors = new List<Plot>();

                if (i - 1 >= 0 && plotGrid[i - 1][j] != null) neighbors.Add(plotGrid[i - 1][j]);
                if (i + 1 < plotGrid.Count && plotGrid[i + 1][j] != null) neighbors.Add(plotGrid[i + 1][j]);
                if (j - 1 >= 0 && plotGrid[i][j - 1] != null) neighbors.Add(plotGrid[i][j - 1]);
                if (j + 1 < plotGrid[i].Count && plotGrid[i][j + 1] != null) neighbors.Add(plotGrid[i][j + 1]);

                plot.SetNeighbors(neighbors);

                plots.Add(plot);
            }
        }

        for (int i = 0; i < STEPS; i++)
        {
            foreach (Plot plot in plots) plot.Step();
            foreach (Plot plot in plots) plot.UpdateAfterStep();
        }

        PrintGrid(input, plotGrid);

        int possiblePlots = plots.Where(plot => plot.IsOn).Count();
        Console.WriteLine(possiblePlots);
    }

    private static void PrintGrid(List<string> input, List<List<Plot>> plotGrid)
    {
        for (int i = 0; i < plotGrid.Count; i++)
        {
            for (int j = 0; j < plotGrid[i].Count; j++)
            {
                if (plotGrid[i][j] != null && plotGrid[i][j].IsOn)
                {
                    Console.Write('O');
                }
                else 
                {
                    Console.Write(input[i][j]);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
