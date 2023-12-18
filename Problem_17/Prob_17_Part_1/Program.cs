using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<List<int>> grid = new List<List<int>>();

        while (reader.Peek() != -1)
        {
            grid.Add(reader.ReadLine().ToCharArray().ToList().Select(e => int.Parse(e.ToString())).ToList());
        }

        List<List<List<Dictionary<Vector, Vertex>>>> vertexLookup = new List<List<List<Dictionary<Vector, Vertex>>>>();
        for (int row = 0; row < grid.Count; row++) 
        {
            List<List<Dictionary<Vector, Vertex>>> rowList = new List<List<Dictionary<Vector, Vertex>>>();
            for (int col = 0; col < grid[row].Count; col++)
            {
                List<Dictionary<Vector, Vertex>> colList = new List<Dictionary<Vector, Vertex>>();
                for (int straightMoves = 1; straightMoves <= 3;  straightMoves++) 
                {
                    Dictionary<Vector, Vertex> smDict = new Dictionary<Vector, Vertex>();

                    Vertex upOrientedVert = new Vertex(row, col, grid[row][col], straightMoves, Vector.Up);
                    smDict[Vector.Up] = upOrientedVert;

                    Vertex downOrientedVert = new Vertex(row, col, grid[row][col], straightMoves, Vector.Down);
                    smDict[Vector.Down] = downOrientedVert;

                    Vertex leftOrientedVert = new Vertex(row, col, grid[row][col], straightMoves, Vector.Left);
                    smDict[Vector.Left] = leftOrientedVert;

                    Vertex rightOrientedVert = new Vertex(row, col, grid[row][col], straightMoves, Vector.Right);
                    smDict[Vector.Right] = rightOrientedVert;

                    colList.Add(smDict);
                }
                rowList.Add(colList);
            }
            vertexLookup.Add(rowList);
        }

        PriorityQueue<Vertex, int> pq = new PriorityQueue<Vertex, int>();
        List<Vertex> visisted = new List<Vertex>();

        Vertex start1 = vertexLookup[0][1][0][Vector.Right];
        start1.ShortestPath = start1.Cost;
        pq.Enqueue(start1, start1.ShortestPath);

        Vertex start2 = vertexLookup[1][0][0][Vector.Down];
        start2.ShortestPath = start2.Cost;
        pq.Enqueue(start2, start2.ShortestPath);

        int maxShortestPath = 0;
        while (pq.Peek() != null && !(pq.Peek().Row == grid.Count - 1 && pq.Peek().Col == grid[0].Count - 1))
        {
            Vertex current = pq.Dequeue();

            if (!current.NeighborsAreSet)
            {
                current.SetNeighbors(vertexLookup);
            }

            if (current.ShortestPath > maxShortestPath) 
            {
                maxShortestPath = current.ShortestPath;
                Console.WriteLine(maxShortestPath + "...");
            }

            foreach (Vertex neighbor in current.Neighbors.Except(visisted))
            {
                if (current.ShortestPath + neighbor.Cost < neighbor.ShortestPath)
                {
                    neighbor.ShortestPath = current.ShortestPath + neighbor.Cost;
                    pq.Enqueue(neighbor, neighbor.ShortestPath);
                }
            }

            visisted.Add(current);
        }

        Console.WriteLine(pq.Peek().ShortestPath);
    }
}
