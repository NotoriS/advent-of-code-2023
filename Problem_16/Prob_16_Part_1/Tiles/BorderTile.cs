
public class BorderTile : Tile
{
    public BorderTile() : base() { }

    public override Vector GetNextDirection(Vector currentDirection)
    {
        return Vector.zero;
    }
}
