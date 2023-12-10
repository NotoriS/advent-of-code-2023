using System;
using System.Collections.Generic;

public class Pipe
{
    private char _pipeType;

    public Tuple<int, int> Coordinates { get; private set; }
    public Direction DirectionEntered { get; private set; }

    public Direction ExitDirection
    {
        get 
        {
            switch (_pipeType)
            {
                case '|':
                    return DirectionEntered == Direction.north ? Direction.north : Direction.south;
                case '-':
                    return DirectionEntered == Direction.west ? Direction.west : Direction.east;
                case 'L':
                    return DirectionEntered == Direction.south ? Direction.east : Direction.north;
                case 'J':
                    return DirectionEntered == Direction.south ? Direction.west : Direction.north;
                case '7':
                    return DirectionEntered == Direction.north ? Direction.west : Direction.south;
                case 'F':
                    return DirectionEntered == Direction.north ? Direction.east : Direction.south;
                default:
                    return Direction.none;
            }
        }
    }

    public Pipe(char pipeType, Direction directionEntered, Tuple<int, int> coordinates)
    {
        _pipeType = pipeType;
        DirectionEntered = directionEntered;
        Coordinates = coordinates;
    }
}
