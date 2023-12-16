
public class EmptyTile : Tile
{
    public EmptyTile() : base() { }

    public override Vector GetNextDirection(Vector currentDirection)
    {
        if (_beams.Contains(currentDirection)) return Vector.zero;

        _beams.Add(currentDirection);
        return currentDirection;
    }
}
