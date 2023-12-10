public enum Direction
{
    none,
    north,
    east,
    south,
    west,
}

public class DirectionUtils
{
    public static Direction ReverseDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.north:
                return Direction.south;
                break;
            case Direction.south:
                return Direction.north;
                break;
            case Direction.east:
                return Direction.west;
                break;
            case Direction.west:
                return Direction.east;
                break;
            default:
                return Direction.none;
        }
    }
}
