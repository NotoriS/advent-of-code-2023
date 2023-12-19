﻿
public class Vertex
{
    public int Row { get; private set; }
    public int Col { get; private set; }
    public int Cost { get; private set; }
    public int StraightMoves { get; private set; }
    public Vector Direction { get; private set; }

    public int ShortestPath { get; set; }
    public Vertex Previous { get; set; }

    public List<Vertex> Neighbors { get; private set; }
    public bool NeighborsAreSet { get; private set; }

    public bool Visited { get; set; }

    public Vertex(int row, int col, int cost, int straightMoves, Vector direction)
    {
        Neighbors = new List<Vertex>();
        NeighborsAreSet = false;

        Row = row; 
        Col = col;
        Cost = cost;
        StraightMoves = straightMoves;
        Direction = direction;

        ShortestPath = int.MaxValue;
        Visited = false;
    }

    public void SetNeighbors(List<List<List<Dictionary<Vector, Vertex>>>> neighborLookup)
    {
        Vertex? leftNeighbor = null;
        Vertex? rightNeighbor = null;
        Vertex? straightNeighbor = null;

        switch (Direction)
        {
            case Vector.Up:
                if (StraightMoves >= 4 && Col - 4 >= 0) leftNeighbor = neighborLookup[Row][Col - 1][0][Vector.Left];
                if (StraightMoves >= 4 && Col + 4 < neighborLookup[Row].Count) rightNeighbor = neighborLookup[Row][Col + 1][0][Vector.Right];
                if (StraightMoves < 10 && Row - 1 >= 0) straightNeighbor = neighborLookup[Row - 1][Col][StraightMoves][Vector.Up];
                break;
            case Vector.Down:
                if (StraightMoves >= 4 && Col + 4 < neighborLookup[Row].Count) leftNeighbor = neighborLookup[Row][Col + 1][0][Vector.Right];
                if (StraightMoves >= 4 && Col - 4 >= 0) rightNeighbor = neighborLookup[Row][Col - 1][0][Vector.Left];
                if (StraightMoves < 10 && Row + 1 < neighborLookup.Count) straightNeighbor = neighborLookup[Row + 1][Col][StraightMoves][Vector.Down];
                break;
            case Vector.Left:
                if (StraightMoves >= 4 && Row + 4 < neighborLookup.Count) leftNeighbor = neighborLookup[Row + 1][Col][0][Vector.Down];
                if (StraightMoves >= 4 && Row - 4 >= 0) rightNeighbor = neighborLookup[Row - 1][Col][0][Vector.Up];
                if (StraightMoves < 10 && Col - 1 >= 0) straightNeighbor = neighborLookup[Row][Col - 1][StraightMoves][Vector.Left];
                break;
            case Vector.Right:
                if (StraightMoves >= 4 && Row - 4 >= 0) leftNeighbor = neighborLookup[Row - 1][Col][0][Vector.Up];
                if (StraightMoves >= 4 && Row + 4 < neighborLookup.Count) rightNeighbor = neighborLookup[Row + 1][Col][0][Vector.Down];
                if (StraightMoves < 10 && Col + 1 < neighborLookup[Row].Count) straightNeighbor = neighborLookup[Row][Col + 1][StraightMoves][Vector.Right];
                break;
        }

        if (leftNeighbor != null) Neighbors.Add(leftNeighbor);
        if (rightNeighbor != null) Neighbors.Add(rightNeighbor);
        if (straightNeighbor != null) Neighbors.Add(straightNeighbor);

        NeighborsAreSet = true;
    }

    public override string ToString()
    {
        return $"{Row},{Col},{StraightMoves}{Direction}";
    }
}
