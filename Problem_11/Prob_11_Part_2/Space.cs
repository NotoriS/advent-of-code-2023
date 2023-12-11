using System;
using System.Collections.Generic;

public class Space
{
    List<List<char>> _grid;
    List<Galaxy> _galaxies;

    public Space(List<List<char>> input)
    {
        _galaxies = new List<Galaxy>();

        _grid = input;
        ExpandGrid();

        FindGalaxies();
    }

    public long GetSumOfShortestPathBetweenEachGalaxy()
    {
        long sum = 0;

        for (int i = 0; i < _galaxies.Count - 1; i++)
        {
            for (int j = i + 1; j < _galaxies.Count; j++)
            {
                sum += GetShortestPathBetweenTwoGalaxies(_galaxies[i], _galaxies[j]);
            }
        }

        return sum;
    }

    
    private long GetShortestPathBetweenTwoGalaxies(Galaxy g1, Galaxy g2)
    {
        int lesserRow = Math.Min(g1.Coordinates.Item1, g2.Coordinates.Item1);
        Galaxy lesserRowGalaxy = g1.Coordinates.Item1 == lesserRow ? g1 : g2;
        int greaterRow = Math.Max(g1.Coordinates.Item1, g2.Coordinates.Item1);
        Galaxy greaterRowGalaxy = g1.Coordinates.Item1 == greaterRow ? g1 : g2;

        int lesserCol = Math.Min(g1.Coordinates.Item2, g2.Coordinates.Item2);
        Galaxy lesserColGalaxy = g1.Coordinates.Item2 == lesserCol ? g1 : g2;
        int greaterCol = Math.Max(g1.Coordinates.Item2, g2.Coordinates.Item2);
        Galaxy greaterColGalaxy = g1.Coordinates.Item2 == greaterCol ? g1 : g2;

        const int BLANK_LINE_VALUE = 1000000;


        long sumDownThenSideways = 0;

        for (int i = lesserRow; i <= greaterRow; i++)
        {
            if (_grid[i][lesserRowGalaxy.Coordinates.Item2] == '%')
            {
                sumDownThenSideways += BLANK_LINE_VALUE;
            }
            else 
            {
                sumDownThenSideways++;
            }
        }

        for (int i = lesserCol; i <= greaterCol; i++)
        {
            if (_grid[greaterColGalaxy.Coordinates.Item1][i] == '%')
            {
                sumDownThenSideways += BLANK_LINE_VALUE;
            }
            else
            {
                sumDownThenSideways++;
            }
        }

        sumDownThenSideways--; // Accounts for first galaxy counting its own space

        sumDownThenSideways -= _grid[greaterColGalaxy.Coordinates.Item1][lesserRowGalaxy.Coordinates.Item2] == '%' ? 1000000 : 1; // Accounts for counting the corner twice;


        long sumSidewaysThenUp = 0;

        for (int i = lesserCol; i <= greaterCol; i++)
        {
            if (_grid[lesserColGalaxy.Coordinates.Item1][i] == '%')
            {
                sumSidewaysThenUp += BLANK_LINE_VALUE;
            }
            else
            {
                sumSidewaysThenUp++;
            }
        }

        for (int i = lesserRow; i <= greaterRow; i++)
        {
            if (_grid[i][greaterRowGalaxy.Coordinates.Item2] == '%')
            {
                sumSidewaysThenUp += BLANK_LINE_VALUE;
            }
            else
            {
                sumSidewaysThenUp++;
            }
        }

        sumSidewaysThenUp--; // Accounts for first galaxy counting its own space

        sumSidewaysThenUp -= _grid[lesserColGalaxy.Coordinates.Item1][greaterRowGalaxy.Coordinates.Item2] == '%' ? 1000000 : 1; // Accounts for counting the corner twice;


        return Math.Min(sumSidewaysThenUp, sumDownThenSideways);
    }

    private void FindGalaxies()
    {
        for (int i = 0; i < _grid.Count; i++)
        {
            for (int j = 0; j < _grid[i].Count; j++)
            {
                if (_grid[i][j] == '#') _galaxies.Add(new Galaxy(i, j));
            }
        }
    }

    private void ExpandGrid()
    {  
        for (int i = 0; i < _grid.Count; i++) 
        {
            bool rowHasNoGalaxies = true;
            for (int j = 0;  j < _grid[i].Count; j++) 
            {
                if (_grid[i][j] != '.' && _grid[i][j] != '%')
                {
                    rowHasNoGalaxies = false;
                    break;
                }
            }

            if (rowHasNoGalaxies)
            {
                ExpandRow(i);
                i++;
            }
        }

        for (int j = 0; j < _grid[0].Count; j++)
        {
            bool columnHasNoGalaxies = true;
            for (int i = 0; i < _grid.Count; i++)
            {
                if (_grid[i][j] != '.' && _grid[i][j] != '%')
                {
                    columnHasNoGalaxies = false;
                    break;
                }
            }

            if (columnHasNoGalaxies) 
            {
                ExpandColumn(j);
                j++;
            }
        }
    }

    private void ExpandRow(int row) 
    {
        List<char> list = new List<char>();
        for (int i = 0; i < _grid[row].Count; i++) 
        {
            list.Add('%');
        }
        _grid.RemoveAt(row);
        _grid.Insert(row, list);
    }

    private void ExpandColumn(int col) 
    {
        foreach (List<char> row in _grid)
        {
            row.RemoveAt(col);
            row.Insert(col, '%');
        }
    }
}