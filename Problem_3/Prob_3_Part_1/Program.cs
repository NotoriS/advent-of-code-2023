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
            List<char> numChars = new List<char>();
            int numStartIndex = -1;

            for (int j = 0; j <= charLayout[i].Count; j++)
            {
                if (j != charLayout[i].Count && CharIsDigit(charLayout[i][j]))
                {
                    if (numStartIndex == -1) numStartIndex = j;
                    numChars.Add(charLayout[i][j]);
                }
                else if (numStartIndex != -1)
                {
                    if (NumberShouldBeAdded(i, numStartIndex, numChars.Count, charLayout))
                    {
                        sum += int.Parse(new string(numChars.ToArray()));
                    }
                    numChars = new List<char>();
                    numStartIndex = -1;
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
