
public class Map
{
    private List<string> _map;
    private GraphNode _graph;

    public Map(List<string> input)
    {
        _map = new List<string>(input);
        _graph = ConstructGraph();
    }

    public int FindLongestPath()
    {
        Path initial = new Path(_graph);
        Stack<Path> stack = new Stack<Path>();
        stack.Push(initial);

        Path longest = initial;

        while (stack.Count > 0)
        {
            Path next = stack.Pop();
            if (next.CurrentNode.IsEnd && next.TotalMoves > longest.TotalMoves) longest = next;
            foreach (Path path in next.GetNextPaths()) stack.Push(path);
        }

        return longest.TotalMoves;
    }

    private GraphNode ConstructGraph()
    {
        List<List<GraphNode?>> constructionHelper = BuildConstructionHelper();

        for (int row = 0; row < _map.Count; row++)
        {
            for (int col = 0; col < _map[row].Length; col++)
            {
                GraphNode? node = constructionHelper[row][col];
                if (node == null) continue;

                SetNodeEdges(row, col, constructionHelper);
            }
        }

        return constructionHelper[0][1]!;
    }

    private List<List<GraphNode?>> BuildConstructionHelper()
    {
        List<List<GraphNode?>> constructionHelper = new List<List<GraphNode?>>();
        for (int row = 0; row < _map.Count; row++)
        {
            List<GraphNode?> constructionHelperRow = new List<GraphNode?>();
            for (int col = 0; col < _map[row].Length; col++)
            {
                if (CoordinateIsDecisionPoint(row, col))
                {
                    GraphNode node = new GraphNode();
                    constructionHelperRow.Add(node);
                }
                else
                {
                    constructionHelperRow.Add(null);
                }
            }
            constructionHelper.Add(constructionHelperRow);
        }

        GraphNode start = new GraphNode(true, false);
        constructionHelper[0].RemoveAt(1);
        constructionHelper[0].Insert(1, start);

        GraphNode end = new GraphNode(false, true);
        constructionHelper[constructionHelper.Count - 1].RemoveAt(constructionHelper[constructionHelper.Count - 1].Count - 2);
        constructionHelper[constructionHelper.Count - 1].Insert(constructionHelper[constructionHelper.Count - 1].Count - 1, end);

        return constructionHelper;
    }

    private void SetNodeEdges(int row, int col, List<List<GraphNode?>> constructionHelper)
    {
        GraphNode? node = constructionHelper[row][col];
        if (node == null) throw new Exception("Invalid coordinates");

        Tuple<GraphNode, int>? upPathEdge = FindNodeEdge(row, col, Direction.Up, constructionHelper);
        Tuple<GraphNode, int>? downPathEdge = FindNodeEdge(row, col, Direction.Down, constructionHelper);
        Tuple<GraphNode, int>? leftPathEdge = FindNodeEdge(row, col, Direction.Left, constructionHelper);
        Tuple<GraphNode, int>? rightPathEdge = FindNodeEdge(row, col, Direction.Right, constructionHelper);

        if (upPathEdge != null) node.AddEdge(upPathEdge.Item1, upPathEdge.Item2);
        if (downPathEdge != null) node.AddEdge(downPathEdge.Item1, downPathEdge.Item2);
        if (leftPathEdge != null) node.AddEdge(leftPathEdge.Item1, leftPathEdge.Item2);
        if (rightPathEdge != null) node.AddEdge(rightPathEdge.Item1, rightPathEdge.Item2);
    }

    private Tuple<GraphNode, int>? FindNodeEdge(int row, int col, Direction dir, List<List<GraphNode?>> constructionHelper)
    {
        Direction directionOfPreviousPosition;
        switch (dir)
        {
            case Direction.Up:
                directionOfPreviousPosition = Direction.Down;
                row--;
                break;
            case Direction.Down:
                directionOfPreviousPosition = Direction.Up;
                row++;
                break;
            case Direction.Left:
                directionOfPreviousPosition = Direction.Right;
                col--;
                break;
            case Direction.Right:
                directionOfPreviousPosition = Direction.Left;
                col++;
                break;
            default:
                throw new Exception("And unknown direction was used.");
        }

        int distance = 1;
        while (CoordinateIsWalkable(row, col))
        {
            GraphNode? node = constructionHelper[row][col];
            if (node != null) return Tuple.Create(node, distance);

            if (directionOfPreviousPosition != Direction.Up && CoordinateIsWalkable(row - 1, col))
            {
                row--;
                directionOfPreviousPosition = Direction.Down;
                distance++;
                continue;
            }
            if (directionOfPreviousPosition != Direction.Down && CoordinateIsWalkable(row + 1, col))
            {
                row++;
                directionOfPreviousPosition = Direction.Up;
                distance++;
                continue;
            }
            if (directionOfPreviousPosition != Direction.Left && CoordinateIsWalkable(row, col - 1))
            {
                col--;
                directionOfPreviousPosition = Direction.Right;
                distance++;
                continue;
            }
            if (directionOfPreviousPosition != Direction.Right && CoordinateIsWalkable(row, col + 1))
            {
                col++;
                directionOfPreviousPosition = Direction.Left;
                distance++;
                continue;
            }

            break;
        }

        return null;
    }

    private bool IsValidMove(int row, int col, Direction direction)
    {
        switch (_map[row][col])
        {
            case '#':
                return false;
            case '^':
            case 'v':
            case '<':
            case '>':
            case '.':
                switch (direction)
                {
                    case Direction.Up:
                        return CoordinateIsWalkable(row - 1, col);
                    case Direction.Down:
                        return CoordinateIsWalkable(row + 1, col);
                    case Direction.Left:
                        return CoordinateIsWalkable(row, col - 1);
                    case Direction.Right:
                        return CoordinateIsWalkable(row, col + 1);
                    default:
                        throw new Exception("And unknown direction was used.");
                }
            default:
                throw new Exception("The map contains an invalid character.");
        }
    }

    private bool CoordinateIsDecisionPoint(int row, int col)
    {
        if (!CoordinateIsWalkable(row, col)) return false;

        int movableDirections = 0;
        if (IsValidMove(row, col, Direction.Up)) movableDirections++;
        if (IsValidMove(row, col, Direction.Down)) movableDirections++;
        if (IsValidMove(row, col, Direction.Left)) movableDirections++;
        if (IsValidMove(row, col, Direction.Right)) movableDirections++;

        return movableDirections > 2;
    }

    private bool CoordinateIsWalkable(int row, int col)
    {
        if (row < 0 || row >= _map.Count) return false;
        if (col < 0 || col >= _map[row].Length) return false;

        return _map[row][col] != '#';
    }
}
