
public class Vertex : IEquatable<Vertex>
{
    public double X { get; set; }
    public double Y { get; set; }

    public Direction EnterDirection { get; set; }
    public Direction ExitDirection { get; set; }

    public Vertex(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Vertex(double x, double y, Direction enterDirection)
    {
        X = x;
        Y = y;
        EnterDirection = enterDirection;
    }

    public Vertex(Vertex other) 
    {
        X = other.X;
        Y = other.Y;
        EnterDirection = other.EnterDirection;
        ExitDirection = other.ExitDirection;
    }

    public bool Equals(Vertex? other)
    {
        if (other == null) return false;
        return X == other.X && Y == other.Y;
    }

    public bool Completes90DegreeAngle(Vertex v1, Vertex v2)
    {
        return (this.X == v1.X && this.Y != v1.Y && this.X != v2.X && this.Y == v2.Y)
            || (this.X == v2.X && this.Y != v2.Y && this.X != v1.X && this.Y == v1.Y);
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}
