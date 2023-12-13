using System;
using System.Collections.Generic;

public class Valley
{
    List<string> _rows;
    List<string> _cols;

    public Valley(List<string> rows)
    {
        _rows = new List<string>(rows);
        _cols = new List<string>();

        for (int i = 0; i < rows[0].Length; i++)
        {
            List<char> colChars = new List<char>();
            for (int j = 0; j < rows.Count; j++)
            {
                colChars.Add(_rows[j][i]);
            }
            _cols.Add(new String(colChars.ToArray()));
        }
    }

    public int GetColumnsLeftReflectionPoint()
    {
        return GetStringsLeftReflectionPoint(_cols);
    }

    public int GetRowsAboveReflectionPoint()
    {
        return GetStringsLeftReflectionPoint(_rows);
    }

    private int GetStringsLeftReflectionPoint(List<string> list)
    {
        int startIndex = 0;
        int endIndex = list.Count - 1;

        bool reflectionFound = false;

        for (int i = startIndex; i < endIndex; i++)
        {
            if (list[i] == list[endIndex] && VerifyReflection(list, i, endIndex))
            {
                startIndex = i;
                reflectionFound = true;
                break;
            }
        }

        for (int i = endIndex; i > startIndex; i--)
        {
            if (list[i] == list[startIndex] && VerifyReflection(list, startIndex, i))
            {
                endIndex = i;
                reflectionFound = true;
                break;
            }
        }

        return reflectionFound ? (startIndex + endIndex) / 2 + 1 : 0;
    }

    private bool VerifyReflection(List<string> list, int startIndex, int endIndex)
    {
        while (startIndex < endIndex)
        {
            if (list[startIndex] != list[endIndex] || startIndex == endIndex)
            {
                return false;
            }
            startIndex++;
            endIndex--;
        }
        return true;
    }
}
