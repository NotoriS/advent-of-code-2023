
public class Beam
{
    public int Row { get; private set; }
    public int Col { get; private set; }

    public Vector CurrentDirection { get; set; }

    public Beam(int row, int col, Vector startingDirection)
    {
        Row = row;
        Col = col;

        CurrentDirection = startingDirection;
    }

    public void Move()
    {
        switch (CurrentDirection)
        {
            case Vector.up:
                Row--;
                break;
            case Vector.down:
                Row++;
                break;
            case Vector.left:
                Col--;
                break;
            case Vector.right:
                Col++;
                break;
            default:
                throw new ArgumentException("Invalid direction for beam to move.");
        }
    }

    public Beam Split()
    {
        switch (CurrentDirection)
        {
            case Vector.leftRight:
                CurrentDirection = Vector.left;
                return new Beam(Row, Col, Vector.right);
            case Vector.upDown:
                CurrentDirection = Vector.up;
                return new Beam(Row, Col, Vector.down);
            default:
                throw new InvalidOperationException("This beam cannot be split.");
        }
    }
}
