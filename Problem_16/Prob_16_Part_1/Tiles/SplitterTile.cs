
public class SplitterTile : Tile
{
    private char _splitterType;

    public SplitterTile(char splitterType) : base() 
    {
        _splitterType = splitterType;
    }

    public override Vector GetNextDirection(Vector currentDirection)
    {
        Vector nextDirection = Vector.zero;

        if (_splitterType == '-')
        {
            switch (currentDirection)
            {
                case Vector.up:
                case Vector.down:
                    nextDirection = Vector.leftRight;
                    break;
                default:
                    nextDirection = currentDirection;
                    break;
            }
        }

        if (_splitterType == '|')
        {
            switch (currentDirection)
            {
                case Vector.left:
                case Vector.right:
                    nextDirection = Vector.upDown;
                    break;
                default:
                    nextDirection = currentDirection;
                    break;
            }
        }

        if (_beams.Contains(nextDirection)) return Vector.zero;

        _beams.Add(nextDirection);
        return nextDirection;
    }
}
