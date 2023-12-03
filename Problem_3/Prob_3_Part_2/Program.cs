using System.IO;
using System.Collections;

public class Problem
{
    public static void Main(string[] args)
    {
        List<List<char>> charLayout = GetInputAs2DCharLayout();
        int sum = 0;

        for (int i = 0; i < charLayout.Count; i++)
        {
            for (int j = 0; j < charLayout[i].Count; j++)
            {
                if (charLayout[i][j] == '*' && IsGear(i, j, charLayout))
                {
                    sum += GetGearRatio(i, j, charLayout);
                }
            }
        }

        Console.WriteLine(sum);
    }

    private static List<List<char>> GetInputAs2DCharLayout()
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<List<char>> charLayout = new List<List<char>>();

        while (reader.Peek() != -1)
        {
            charLayout.Add(reader.ReadLine().ToCharArray().ToList());
        }

        reader.Close();
        return charLayout;
    }

    private static bool IsGear(int row, int col, List<List<char>> charLayout)
    {
        int adjacentNums = 0;

        if (CharIsDigit(charLayout[Math.Max(row - 1, 0)][col]))
        {
            adjacentNums++;
        }
        else
        {
            if (CharIsDigit(charLayout[Math.Max(row - 1, 0)][Math.Max(col - 1, 0)])) adjacentNums++;
            if (CharIsDigit(charLayout[Math.Max(row - 1, 0)][Math.Min(col + 1, charLayout[row].Count - 1)])) adjacentNums++;
        }
        if (CharIsDigit(charLayout[Math.Min(row + 1, charLayout.Count - 1)][col]))
        {
            adjacentNums++;
        }
        else
        {
            if (CharIsDigit(charLayout[Math.Min(row + 1, charLayout.Count - 1)][Math.Max(col - 1, 0)])) adjacentNums++;
            if (CharIsDigit(charLayout[Math.Min(row + 1, charLayout.Count - 1)][Math.Min(col + 1, charLayout[row].Count - 1)])) adjacentNums++;
        }
        if (CharIsDigit(charLayout[row][Math.Max(col - 1, 0)])) adjacentNums++;
        if (CharIsDigit(charLayout[row][Math.Min(col + 1, charLayout[row].Count - 1)])) adjacentNums++;

        return adjacentNums == 2;
    }

    private static int GetGearRatio(int row, int col, List<List<char>> charLayout)
    {
        if (!IsGear(row, col, charLayout)) return -1;

        int gearRatio = 1;

        if (CharIsDigit(charLayout[Math.Max(row - 1, 0)][col]))
        {
            gearRatio *= GetFullNumFromSingleDigitInGrid(Math.Max(row - 1, 0), col, charLayout);
        }
        else
        {
            if (CharIsDigit(charLayout[Math.Max(row - 1, 0)][Math.Max(col - 1, 0)]))
            {
                gearRatio *= GetFullNumFromSingleDigitInGrid(Math.Max(row - 1, 0), Math.Max(col - 1, 0), charLayout);
            }
            if (CharIsDigit(charLayout[Math.Max(row - 1, 0)][Math.Min(col + 1, charLayout[row].Count - 1)]))
            {
                gearRatio *= GetFullNumFromSingleDigitInGrid(Math.Max(row - 1, 0), Math.Min(col + 1, charLayout[row].Count - 1), charLayout);
            }
        }
        if (CharIsDigit(charLayout[Math.Min(row + 1, charLayout.Count - 1)][col]))
        {
            gearRatio *= GetFullNumFromSingleDigitInGrid(Math.Min(row + 1, charLayout.Count - 1), col, charLayout);
        }
        else
        {
            if (CharIsDigit(charLayout[Math.Min(row + 1, charLayout.Count - 1)][Math.Max(col - 1, 0)]))
            {
                gearRatio *= GetFullNumFromSingleDigitInGrid(Math.Min(row + 1, charLayout.Count - 1), Math.Max(col - 1, 0), charLayout);
            }
            if (CharIsDigit(charLayout[Math.Min(row + 1, charLayout.Count - 1)][Math.Min(col + 1, charLayout[row].Count - 1)]))
            {
                gearRatio *= GetFullNumFromSingleDigitInGrid(Math.Min(row + 1, charLayout.Count - 1), Math.Min(col + 1, charLayout[row].Count - 1), charLayout);
            }
        }
        if (CharIsDigit(charLayout[row][Math.Max(col - 1, 0)]))
        {
            gearRatio *= GetFullNumFromSingleDigitInGrid(row, Math.Max(col - 1, 0), charLayout);
        }
        if (CharIsDigit(charLayout[row][Math.Min(col + 1, charLayout[row].Count - 1)]))
        {
            gearRatio *= GetFullNumFromSingleDigitInGrid(row, Math.Min(col + 1, charLayout[row].Count - 1), charLayout);
        }

        return gearRatio;
    }

    private static int GetFullNumFromSingleDigitInGrid(int row, int col, List<List<char>> charLayout)
    {
        while (col > 0 && CharIsDigit(charLayout[row][col - 1])) col--;

        List<char> digits = new List<char>();
        while (col < charLayout[row].Count && CharIsDigit(charLayout[row][col]))
        {
            digits.Add(charLayout[row][col]);
            col++;
        }

        return int.Parse(new string(digits.ToArray()));
    }

    private static bool CharIsDigit(char c)
    {
        return c >= '0' && c <= '9';
    }

    private static bool NumberShouldBeAdded(int row, int startCol, int length, List<List<char>> charLayout)
    {
        for (int i = Math.Max(row - 1, 0); i <= row + 1 && i < charLayout.Count; i++)
        {
            for (int j = Math.Max(startCol - 1, 0); j <= startCol + length && j < charLayout[i].Count; j++)
            {
                if (!CharIsDigit(charLayout[i][j]) && charLayout[i][j] != '.') return true;
            }
        }

        return false;
    }
}
