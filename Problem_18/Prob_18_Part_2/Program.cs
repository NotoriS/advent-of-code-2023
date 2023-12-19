using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");

        Queue<Instruction> instructions = new Queue<Instruction>();
        Queue<string> nextDirection = new Queue<string>();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            string hexNumber = line.Split(' ')[2];

            string directionIndicator = hexNumber.Substring(7, 1);

            string distanceStr = hexNumber.Substring(2, 5);
            int distance = int.Parse(distanceStr, System.Globalization.NumberStyles.HexNumber);

            instructions.Enqueue(new Instruction(directionIndicator, distance));
        }

        List<Vertex> vertices = new List<Vertex>();
        foreach (Instruction instruction in instructions)
        {
            instruction.Execute(vertices);
        }

        CleanUpFirstAndLastVertex(vertices);

        List<Vertex> flipSideVertices = new List<Vertex>();
        foreach (Vertex vertex in vertices) 
        {
            flipSideVertices.Add(new Vertex(vertex));
        }

        ShiftVertices(vertices, false);
        ShiftVertices(flipSideVertices, true);

        double area = Shoelace(vertices);
        double flipedArea = Shoelace(flipSideVertices);

        Console.WriteLine(Math.Max(area, flipedArea));
    }

    private static double Shoelace(List<Vertex> vertices)
    {
        double prod1Sum = 0;
        double prod2Sum = 0;
        for (int i = 0; i < vertices.Count; i++)
        {
            int i1 = (i + 1) % vertices.Count;

            double prod1 = vertices[i].X * vertices[i1].Y;
            double prod2 = vertices[i1].X * vertices[i].Y;
            prod1Sum += prod1;
            prod2Sum += prod2;
        }
        return Math.Abs(prod1Sum - prod2Sum) / 2f;
    }

    // Shifts the border to the outer edge of the shape
    private static void ShiftVertices(List<Vertex> vertices, bool flipSide)
    {
        int flipSideModifier = flipSide ? -1 : 1;

        foreach (Vertex v in vertices) 
        {
            switch (v.EnterDirection) 
            {
                case Direction.Up:
                    switch (v.ExitDirection)
                    {
                        case Direction.Left:
                            v.X -= 0.5 * flipSideModifier;
                            v.Y += 0.5 * flipSideModifier;
                            break;
                        case Direction.Right:
                            v.X -= 0.5 * flipSideModifier;
                            v.Y -= 0.5 * flipSideModifier;
                            break;
                    }
                    break;
                case Direction.Down:
                    switch (v.ExitDirection)
                    {
                        case Direction.Right:
                            v.X += 0.5 * flipSideModifier;
                            v.Y -= 0.5 * flipSideModifier;
                            break;
                        case Direction.Left:
                            v.X += 0.5 * flipSideModifier;
                            v.Y += 0.5 * flipSideModifier;
                            break;
                    }
                    break;
                case Direction.Left:
                    switch (v.ExitDirection)
                    {
                        case Direction.Down:
                            v.X += 0.5 * flipSideModifier;
                            v.Y += 0.5 * flipSideModifier;
                            break;
                        case Direction.Up:
                            v.X -= 0.5 * flipSideModifier;
                            v.Y += 0.5 * flipSideModifier;
                            break;
                    }
                    break;
                case Direction.Right:
                    switch (v.ExitDirection)
                    {
                        case Direction.Up:
                            v.X -= 0.5 * flipSideModifier;
                            v.Y -= 0.5 * flipSideModifier;
                            break;
                        case Direction.Down:
                            v.X += 0.5 * flipSideModifier;
                            v.Y -= 0.5 * flipSideModifier;
                            break;
                    }
                    break;
            }
        }
    }

    private static void CleanUpFirstAndLastVertex(List<Vertex> vertices)
    {
        Vertex first = vertices[0];
        Vertex second = vertices[1];

        Vertex last = vertices[vertices.Count - 1];
        Vertex secondLast = vertices[vertices.Count - 2];

        // Set exit direction of last vertex and enter direction of first vertex
        if (first.X == last.X)
        {
            if (first.Y < last.Y)
            {
                last.ExitDirection = Direction.Up;
                first.EnterDirection = Direction.Up;
            }
            else if (first.Y > last.Y)
            {
                last.ExitDirection = Direction.Down;
                first.EnterDirection = Direction.Down;
            }
            else
            {
                throw new InvalidDataException("First and last vertex should not be equal.");
            }
        }
        else if (first.Y == last.Y)
        {
            if (first.X < last.X)
            {
                last.ExitDirection = Direction.Left;
                first.EnterDirection = Direction.Left;
            }
            else if (first.X > last.X)
            {
                last.ExitDirection = Direction.Right;
                first.EnterDirection = Direction.Right;
            }
            else
            {
                throw new InvalidDataException("First and last vertex should not be equal.");
            }
        }
        else 
        {
            throw new InvalidDataException("First in last vertex should align on one axis.");
        }

        // Remove non-corner vertex
        if (first.Completes90DegreeAngle(second, secondLast))
        {
            vertices.Remove(last);
        }
        else if (last.Completes90DegreeAngle(second, secondLast))
        {
            vertices.Remove(first);
        }
        else 
        {
            throw new InvalidDataException("Either the first or last point should be removable without changing the shape.");
        }
    }
}
