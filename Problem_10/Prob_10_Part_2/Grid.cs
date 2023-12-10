using System;
using System.Collections.Generic;

public class Grid
{
    private Tuple<int, int> _startingCoords;
    private List<List<char>> _grid;

    private List<Pipe> _pipesInLoop;
    private List<List<bool>> _isLoopPipe;

    public Grid(List<List<char>> grid)
    {
        Tuple<int, int> startingCoords = null;
        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[i].Count; j++)
            {
                if (grid[i][j] == 'S') startingCoords = new Tuple<int, int>(i, j);
            }
        }

        if (startingCoords == null) throw new NullReferenceException("starting cooridinates shouldn't be null.");

        _startingCoords = startingCoords;
        _grid = grid;

        SetPipesInLoop();
        SetIsLoopPipe();
    }

    private void SetPipesInLoop()
    {
        Tuple<int, int> currentCoords = _startingCoords;

        List<Pipe> pipesInLoop = new List<Pipe>();
        pipesInLoop.Add(GetStartingPipe());

        while (pipesInLoop.Last().ExitDirection != Direction.none)
        {
            switch (pipesInLoop.Last().ExitDirection)
            {
                case Direction.north:
                    currentCoords = new Tuple<int, int>(currentCoords.Item1 - 1, currentCoords.Item2);
                    break;
                case Direction.east:
                    currentCoords = new Tuple<int, int>(currentCoords.Item1, currentCoords.Item2 + 1);
                    break;
                case Direction.south:
                    currentCoords = new Tuple<int, int>(currentCoords.Item1 + 1, currentCoords.Item2);
                    break;
                case Direction.west:
                    currentCoords = new Tuple<int, int>(currentCoords.Item1, currentCoords.Item2 - 1);
                    break;
            }

            pipesInLoop.Add(new Pipe(_grid[currentCoords.Item1][currentCoords.Item2], pipesInLoop.Last().ExitDirection, currentCoords));
        }

        pipesInLoop.RemoveAt(pipesInLoop.Count - 1);
        _pipesInLoop = pipesInLoop;
    }

    private void SetIsLoopPipe()
    {
        List<bool> row = new List<bool>();
        List<List<bool>> grid = new List<List<bool>>();
        for (int i = 0; i < _grid[0].Count; i++) row.Add(false);
        for (int i = 0; i < _grid.Count; i++) grid.Add(new List<bool>(row));

        foreach (Pipe p in _pipesInLoop)
        {
            grid[p.Coordinates.Item1][p.Coordinates.Item2] = true;
        }

        _isLoopPipe = grid;
    }

    private Pipe GetStartingPipe()
    {
        List<char> northConnecting = new List<char>() { '|', 'L', 'J' };
        List<char> southConnecting = new List<char>() { '|', 'F', '7' };
        List<char> eastConnecting = new List<char>() { '-', 'F', 'L' };
        List<char> westConnecting = new List<char>() { '-', '7', 'J' };

        bool onTopBound = _startingCoords.Item1 == 0;
        bool onBottomBound = _startingCoords.Item1 == _grid.Count - 1;
        bool onLeftBound = _startingCoords.Item2 == 0;
        bool onRightBound = _startingCoords.Item2 == _grid[0].Count - 1;

        if (!onBottomBound && !onTopBound && northConnecting.Contains(_grid[_startingCoords.Item1 + 1][_startingCoords.Item2]) && southConnecting.Contains(_grid[_startingCoords.Item1 - 1][_startingCoords.Item2])) 
            return new Pipe('|', Direction.north, _startingCoords);
        if (!onLeftBound && !onRightBound && eastConnecting.Contains(_grid[_startingCoords.Item1][_startingCoords.Item2 - 1]) && westConnecting.Contains(_grid[_startingCoords.Item1][_startingCoords.Item2 + 1]))
            return new Pipe('-', Direction.east, _startingCoords);
        if (!onTopBound && !onRightBound && westConnecting.Contains(_grid[_startingCoords.Item1][_startingCoords.Item2 + 1]) && southConnecting.Contains(_grid[_startingCoords.Item1 - 1][_startingCoords.Item2]))
            return new Pipe('L', Direction.west, _startingCoords);
        if (!onTopBound && !onLeftBound && eastConnecting.Contains(_grid[_startingCoords.Item1][_startingCoords.Item2 - 1]) && southConnecting.Contains(_grid[_startingCoords.Item1 - 1][_startingCoords.Item2]))
            return new Pipe('J', Direction.east, _startingCoords);
        if (!onBottomBound && !onRightBound && westConnecting.Contains(_grid[_startingCoords.Item1][_startingCoords.Item2 + 1]) && northConnecting.Contains(_grid[_startingCoords.Item1 + 1][_startingCoords.Item2]))
            return new Pipe('F', Direction.west, _startingCoords);
        if (!onBottomBound && !onLeftBound && eastConnecting.Contains(_grid[_startingCoords.Item1][_startingCoords.Item2 - 1]) && northConnecting.Contains(_grid[_startingCoords.Item1 + 1][_startingCoords.Item2]))
            return new Pipe('7', Direction.east, _startingCoords);

        return null;
    }

    public int GetNumberOfGroundTilesContainedByLoop()
    {
        try
        {
            return GetNumberOfGroundTilesContainedByLoop(false);
        }
        catch (ArgumentOutOfRangeException e)
        {
            return GetNumberOfGroundTilesContainedByLoop(true);
        }
    }

    private int GetNumberOfGroundTilesContainedByLoop(bool flipSide)
    {
        List<List<bool>> isContainedByLoop = new List<List<bool>>();

        List<bool> gridRow = new List<bool>();
        for (int i = 0; i < _grid[0].Count; i++) gridRow.Add(false);
        for (int i = 0; i < _grid.Count; i++) isContainedByLoop.Add(new List<bool>(gridRow));

        foreach (Pipe p in _pipesInLoop)
        {
            Direction directionEntered = flipSide ? DirectionUtils.ReverseDirection(p.ExitDirection) : p.DirectionEntered;
            Direction exitDirection = flipSide ? DirectionUtils.ReverseDirection(p.DirectionEntered) : p.ExitDirection;

            int row = p.Coordinates.Item1;
            int col = p.Coordinates.Item2;

            switch (directionEntered)
            {
                case Direction.north:
                    switch (exitDirection)
                    {
                        case Direction.north:
                            if (!_isLoopPipe[row][col - 1]) isContainedByLoop[row][col - 1] = true;
                            break;
                        case Direction.east:
                            if (!_isLoopPipe[row - 1][col - 1]) isContainedByLoop[row - 1][col - 1] = true;
                            if (!_isLoopPipe[row][col - 1]) isContainedByLoop[row][col - 1] = true;
                            if (!_isLoopPipe[row - 1][col]) isContainedByLoop[row - 1][col] = true;
                            break;
                        case Direction.west:
                            if (!_isLoopPipe[row + 1][col - 1]) isContainedByLoop[row + 1][col - 1] = true;
                            break;
                        default:
                            throw new Exception("Direction of loop pipe is not valid");
                    }
                    break;
                case Direction.south:
                    switch (exitDirection)
                    {
                        case Direction.south:
                            if (!_isLoopPipe[row][col + 1]) isContainedByLoop[row][col + 1] = true;
                            break;
                        case Direction.east:
                            if (!_isLoopPipe[row - 1][col + 1]) isContainedByLoop[row - 1][col + 1] = true;
                            break;
                        case Direction.west:
                            if (!_isLoopPipe[row + 1][col + 1]) isContainedByLoop[row + 1][col + 1] = true;
                            if (!_isLoopPipe[row][col + 1]) isContainedByLoop[row][col + 1] = true;
                            if (!_isLoopPipe[row + 1][col]) isContainedByLoop[row + 1][col] = true;
                            break;
                        default:
                            throw new Exception("Direction of loop pipe is not valid");
                    }
                    break;
                case Direction.east:
                    switch (exitDirection)
                    {
                        case Direction.north:
                            if (!_isLoopPipe[row - 1][col - 1]) isContainedByLoop[row - 1][col - 1] = true;
                            break;
                        case Direction.south:
                            if (!_isLoopPipe[row - 1][col + 1]) isContainedByLoop[row - 1][col + 1] = true;
                            if (!_isLoopPipe[row][col + 1]) isContainedByLoop[row][col + 1] = true;
                            if (!_isLoopPipe[row - 1][col]) isContainedByLoop[row - 1][col] = true;
                            break;
                        case Direction.east:
                            if (!_isLoopPipe[row - 1][col]) isContainedByLoop[row - 1][col] = true;
                            break;
                        default:
                            throw new Exception("Direction of loop pipe is not valid");
                    }
                    break;
                case Direction.west:
                    switch (exitDirection)
                    {
                        case Direction.north:
                            if (!_isLoopPipe[row + 1][col - 1]) isContainedByLoop[row + 1][col - 1] = true;
                            if (!_isLoopPipe[row][col - 1]) isContainedByLoop[row][col - 1] = true;
                            if (!_isLoopPipe[row + 1][col]) isContainedByLoop[row + 1][col] = true;
                            break;
                        case Direction.south:
                            if (!_isLoopPipe[row + 1][col + 1]) isContainedByLoop[row + 1][col + 1] = true;
                            break;
                        case Direction.west:
                            if (!_isLoopPipe[row + 1][col]) isContainedByLoop[row + 1][col] = true;
                            break;
                        default:
                            throw new Exception("Direction of loop pipe is not valid");
                    }
                    break;
                default:
                    throw new Exception("Direction of loop pipe should not be \"none\"");
            }
        }

        int addedThisLoop;
        do
        {
            addedThisLoop = 0;
            for (int i = 0; i < isContainedByLoop.Count; i++)
            {
                for (int j = 0; j < isContainedByLoop[i].Count; j++)
                {
                    if (!isContainedByLoop[i][j]) continue;

                    if (!isContainedByLoop[i - 1][j] && !_isLoopPipe[i - 1][j])
                    {
                        isContainedByLoop[i - 1][j] = true;
                        addedThisLoop++;
                    }
                    if (!isContainedByLoop[i + 1][j] && !_isLoopPipe[i + 1][j])
                    {
                        isContainedByLoop[i + 1][j] = true;
                        addedThisLoop++;
                    }
                    if (!isContainedByLoop[i][j - 1] && !_isLoopPipe[i][j - 1])
                    {
                        isContainedByLoop[i][j - 1] = true;
                        addedThisLoop++;
                    }
                    if (!isContainedByLoop[i][j + 1] && !_isLoopPipe[i][j + 1])
                    {
                        isContainedByLoop[i][j + 1] = true;
                        addedThisLoop++;
                    }
                }
            }
        } while (addedThisLoop > 0);

        for (int i = 0; i < isContainedByLoop.Count; i++)
        {
            for (int j = 0; j < isContainedByLoop[i].Count; j++)
            {
                if (_isLoopPipe[i][j])
                {
                    Console.Write(_grid[i][j]);
                }
                else
                {
                    Console.Write(isContainedByLoop[i][j] ? "I" : "O");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        int containedGround = 0;
        for (int i = 0; i < isContainedByLoop.Count; i++)
        {
            for (int j = 0; j < isContainedByLoop[i].Count; j++)
            {
                if (isContainedByLoop[i][j]) containedGround++;
            }
        }

        return containedGround;
    }
}
