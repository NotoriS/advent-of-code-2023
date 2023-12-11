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
                sum += Math.Abs(_galaxies[j].Coordinates.Item1 - _galaxies[i].Coordinates.Item1);
                sum += Math.Abs(_galaxies[j].Coordinates.Item2 - _galaxies[i].Coordinates.Item2);
            }
        }

        return sum;
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
                if (_grid[i][j] != '.')
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
                if (_grid[i][j] != '.')
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
            list.Add('.');
        }
        _grid.Insert(row, list);
    }

    private void ExpandColumn(int col) 
    {
        foreach (List<char> row in _grid)
        {
            row.Insert(col, '.');
        }
    }
}