
public abstract class Tile
{
    protected HashSet<Vector> _beams;

    public bool IsEnergized
    {
        get 
        {
            return _beams.Count > 0;
        }
    }

    protected Tile() 
    {
        _beams = new HashSet<Vector>();
    }

    public abstract Vector GetNextDirection(Vector currentDirection);
}
