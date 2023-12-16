
public class MirrorTile : Tile
{
    private char _mirrorType;

    public MirrorTile(char mirrorType) : base()
    {
        _mirrorType = mirrorType;
    }

    public override Vector GetNextDirection(Vector currentDirection)
    {
        Vector nextDirection = Vector.zero;

        if (_mirrorType == '/')
        {
            switch (currentDirection)
            {
                case Vector.up:
                    nextDirection = Vector.right;
                    break;
                case Vector.down:
                    nextDirection = Vector.left;
                    break;
                case Vector.left:
                    nextDirection = Vector.down;
                    break;
                case Vector.right:
                    nextDirection = Vector.up;
                    break;
                default:
                    nextDirection = Vector.zero;
                    break;
            }
        }

        if (_mirrorType == '\\')
        {
            switch (currentDirection)
            {
                case Vector.up:
                    nextDirection = Vector.left;
                    break;
                case Vector.down:
                    nextDirection = Vector.right;
                    break;
                case Vector.left:
                    nextDirection = Vector.up;
                    break;
                case Vector.right:
                    nextDirection = Vector.down;
                    break;
                default:
                    nextDirection = Vector.zero;
                    break;
            }
        }

        if (_beams.Contains(nextDirection)) return Vector.zero;

        _beams.Add(nextDirection);
        return nextDirection;
    }
}
