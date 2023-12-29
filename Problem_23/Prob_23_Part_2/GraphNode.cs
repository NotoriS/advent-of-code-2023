
public class GraphNode
{
    private List<Tuple<GraphNode, int>> _edges;
    public IEnumerable<Tuple<GraphNode, int>> Edges { get { return _edges; } }

    public bool IsStart { get; private set; }
    public bool IsEnd { get; private set; }

    public GraphNode()
    {
        _edges = new List<Tuple<GraphNode, int>>();
        IsStart = false;
        IsEnd = false;
    }

    public GraphNode(bool isStart, bool isEnd)
    {
        _edges = new List<Tuple<GraphNode, int>>();
        IsStart = isStart;
        IsEnd = isEnd;
    }

    public void AddEdge(GraphNode node, int weight)
    {
        _edges.Add(Tuple.Create(node, weight));
    }
}
