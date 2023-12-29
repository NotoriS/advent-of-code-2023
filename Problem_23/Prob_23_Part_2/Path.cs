
public class Path
{
    public GraphNode CurrentNode { get; private set; }

    public int TotalMoves { get; private set; }

    private HashSet<GraphNode> _visited;

    public Path(GraphNode startNode)
    {
        CurrentNode = startNode;
        TotalMoves = 0;

        _visited = new HashSet<GraphNode>();
    }

    public Path(Path original)
    {
        CurrentNode = original.CurrentNode;
        TotalMoves = original.TotalMoves;

        _visited = new HashSet<GraphNode>(original._visited);
    }

    public IEnumerable<Path> GetNextPaths()
    {
        List<Path> nextPaths = new List<Path>();
        foreach (Tuple<GraphNode, int> edge in CurrentNode.Edges)
        {
            if (_visited.Contains(edge.Item1)) continue;

            Path path = new Path(this);
            path._visited.Add(path.CurrentNode);
            path.TotalMoves += edge.Item2;
            path.CurrentNode = edge.Item1;

            nextPaths.Add(path);
        }

        return nextPaths;
    }
}
