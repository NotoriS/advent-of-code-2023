using MathNet.Numerics;
using System.Text;

public class Problem
{
    public const double STEPS = 26501365;

    public static void Main(string[] args)
    {
        double[] inputs = new double[4];
        double[] outputs = new double[4];

        inputs[0] = 65;
        outputs[0] = (double)CalculateReachablePlots(65);

        inputs[1] = 65 + 131;
        outputs[1] = (double)CalculateReachablePlots(65 + 131);

        inputs[2] = 65 + 131 * 2;
        outputs[2] = (double)CalculateReachablePlots(65 + 131 * 2);

        inputs[3] = 65 + 131 * 3;
        outputs[3] = (double)CalculateReachablePlots(65 + 131 * 3);

        double[] coefficients = Fit.Polynomial(inputs, outputs, 2);

        double result = Polynomial.Evaluate(STEPS, coefficients);
        Console.WriteLine(result);
    }

    private static int CalculateReachablePlots(int steps)
    {
        StreamReader lineCouter = new StreamReader("../../../input.txt");
        int gridWidth = lineCouter.ReadLine().Length;
        lineCouter.Close();

        int gridWidths = (steps + gridWidth - 1) / gridWidth * 2 + 1;

        List<string> input = new List<string>();
        for (int i = 0; i < gridWidths; i++)
        {
            StreamReader reader = new StreamReader("../../../input.txt");
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string lineNoS = line.Replace('S', '.');

                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < gridWidths / 2; j++)
                {
                    sb.Append(lineNoS);
                }
                if (i == gridWidths / 2 + 1)
                {
                    sb.Append(line);
                }
                else
                {
                    sb.Append(lineNoS);
                }
                for (int j = 0; j < gridWidths / 2; j++)
                {
                    sb.Append(lineNoS);
                }

                input.Add(sb.ToString());
            }
            reader.Close();
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

        for (int i = 0; i < steps; i++)
        {
            foreach (Plot plot in plots) plot.Step();
            foreach (Plot plot in plots) plot.UpdateAfterStep();
        }

        int possiblePlots = plots.Where(plot => plot.IsOn).Count();
        return possiblePlots;
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

    private static int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
