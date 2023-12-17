
using System.Text;

public class Crucible
{
    public int Row { get; private set; }
    public int Col { get; private set; }

    public Vector CurrentDirection { get; private set; }
    public int ConsecutiveStraightMoves { get; private set; }

    public Crucible(int startingRow, int startingCol, Vector startingDirection)
    {
        Row = startingRow;
        Col = startingCol;
        CurrentDirection = startingDirection;
        ConsecutiveStraightMoves = 0;
    }

    public Crucible(Crucible original)
    {
        Row = original.Row;
        Col = original.Col;
        CurrentDirection = original.CurrentDirection;
        ConsecutiveStraightMoves = original.ConsecutiveStraightMoves;
    }

    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                switch (CurrentDirection)
                {
                    case Vector.Up:
                        Col--;
                        CurrentDirection = Vector.Left;
                        break;
                    case Vector.Down:
                        Col++;
                        CurrentDirection = Vector.Right;
                        break;
                    case Vector.Left:
                        Row++;
                        CurrentDirection = Vector.Down;
                        break;
                    case Vector.Right:
                        Row--;
                        CurrentDirection = Vector.Up;
                        break;
                }
                ConsecutiveStraightMoves = 1;
                break;
            case Direction.Right:
                switch (CurrentDirection)
                {
                    case Vector.Up:
                        Col++;
                        CurrentDirection = Vector.Right;
                        break;
                    case Vector.Down:
                        Col--;
                        CurrentDirection = Vector.Left;
                        break;
                    case Vector.Left:
                        Row--;
                        CurrentDirection = Vector.Up;
                        break;
                    case Vector.Right:
                        Row++;
                        CurrentDirection = Vector.Down;
                        break;
                }
                ConsecutiveStraightMoves = 1;
                break;
            case Direction.Straight:
                switch (CurrentDirection)
                {
                    case Vector.Up:
                        Row--;
                        break;
                    case Vector.Down:
                        Row++;
                        break;
                    case Vector.Left:
                        Col--;
                        break;
                    case Vector.Right:
                        Col++;
                        break;
                }
                ConsecutiveStraightMoves++;
                break;
        }
    }

    public override string ToString()
    {
        return Row + "," + Col + (ConsecutiveStraightMoves > 0 ? ",CanMoveStraight," : ",") + CurrentDirection;
    }
}
