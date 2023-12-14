using System;
using System.Collections.Generic;
using System.Text;

public class Platform
{
    List<List<char>> _grid;

    public Platform(List<List<char>> grid)
    {
        _grid = new List<List<char>>();
        foreach (List<char> row in grid)
        {
            _grid.Add(new List<char>(row));
        }

        List<char> upperBorder = new List<char>();
        for (int i = 0; i < _grid[0].Count; i++) upperBorder.Add('%');
        List<char> lowerBorder = new List<char>(upperBorder);
        _grid.Insert(0, upperBorder);
        _grid.Add(lowerBorder);

        foreach (List<char> row in _grid)
        {
            row.Insert(0, '%');
            row.Add('%');
        }
    }

    public void TiltNorth()
    {
        for (int i = 1; i < _grid.Count - 1; i++)
        {
            for (int j = 1;  j < _grid[i].Count - 1; j++) 
            {
                if (_grid[i][j] == 'O' && _grid[i - 1][j] == '.') MoveRockAllTheWayNorth(i, j);
            }
        }
    }

    private void MoveRockAllTheWayNorth(int row, int col)
    {
        while (_grid[row - 1][col] == '.')
        {
            _grid[row][col] = '.';
            _grid[row - 1][col] = 'O';

            row--;
        }
    }

    public int GetNorthLoad() 
    {
        int sum = 0;
        int maxValue = _grid.Count - 2;

        for (int i = 1; i < _grid.Count - 1; i++)
        {
            foreach (char c in _grid[i])
            {
                if (c == 'O') sum += maxValue;
            }
            maxValue--;
        }

        return sum;
    }

    public void TiltWest()
    {
        for (int i = 1; i < _grid[0].Count - 1; i++)
        {
            for (int j = 1; j < _grid.Count - 1; j++)
            {
                if (_grid[j][i] == 'O' && _grid[j][i - 1] == '.') MoveRockAllTheWayWest(j, i);
            }
        }
    }

    private void MoveRockAllTheWayWest(int row, int col)
    {
        while (_grid[row][col - 1] == '.')
        {
            _grid[row][col] = '.';
            _grid[row][col - 1] = 'O';

            col--;
        }
    }

    public void TiltSouth()
    {
        for (int i = _grid.Count - 1; i > 0; i--)
        {
            for (int j = 1; j < _grid[i].Count - 1; j++)
            {
                if (_grid[i][j] == 'O' && _grid[i + 1][j] == '.') MoveRockAllTheWaySouth(i, j);
            }
        }
    }

    private void MoveRockAllTheWaySouth(int row, int col)
    {
        while (_grid[row + 1][col] == '.')
        {
            _grid[row][col] = '.';
            _grid[row + 1][col] = 'O';

            row++;
        }
    }

    public void TiltEast()
    {
        for (int i = _grid[0].Count - 1; i > 0; i--)
        {
            for (int j = 1; j < _grid.Count - 1; j++)
            {
                if (_grid[j][i] == 'O' && _grid[j][i + 1] == '.') MoveRockAllTheWayEast(j, i);
            }
        }
    }

    private void MoveRockAllTheWayEast(int row, int col)
    {
        while (_grid[row][col + 1] == '.')
        {
            _grid[row][col] = '.';
            _grid[row][col + 1] = 'O';

            col++;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (List<char> row in _grid)
        {
            sb.Append(new string(row.ToArray()));
            sb.Append('\n');
        }

        return sb.ToString();
    }
}