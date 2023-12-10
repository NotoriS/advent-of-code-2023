using System;
using System.Collections.Generic;

public class Pipe
{
    private char _pipeType;
    private Direction _directionEntered;

    public Direction ExitDirection
    {
        get 
        {
            switch (_pipeType)
            {
                case '|':
                    return _directionEntered == Direction.north ? Direction.north : Direction.south;
                case '-':
                    return _directionEntered == Direction.west ? Direction.west : Direction.east;
                case 'L':
                    return _directionEntered == Direction.south ? Direction.east : Direction.north;
                case 'J':
                    return _directionEntered == Direction.south ? Direction.west : Direction.north;
                case '7':
                    return _directionEntered == Direction.north ? Direction.west : Direction.south;
                case 'F':
                    return _directionEntered == Direction.north ? Direction.east : Direction.south;
                default:
                    return Direction.none;
            }
        }
    }

    public Pipe(char pipeType, Direction directionEntered)
    {
        _pipeType = pipeType;
        _directionEntered = directionEntered;
    }
}

public enum Direction
{
    none,
    north,
    east,
    south,
    west,
}
