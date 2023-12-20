
public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        Dictionary<string, Workflow> workflows = new Dictionary<string, Workflow>();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (line == "") break;

            string workflowLabel = line.Split('{')[0];
            List<string> rules = line.Split('{')[1].Remove(line.Split('{')[1].Length - 1).Split(',').ToList();

            workflows.Add(workflowLabel, new Workflow(rules));
        }

        reader.Close();

        List<PartRange> acceptedRanges = new List<PartRange>();
        Stack<PartRange> rangeStack = new Stack<PartRange>();

        PartRange start = new PartRange();
        start.Destination = "in";
        rangeStack.Push(start);

        while (rangeStack.Count > 0)
        {
            PartRange next = rangeStack.Pop();

            if (next.Destination == "A")
            {
                acceptedRanges.Add(next);
                continue;
            }

            if (next.Destination == "R") continue;

            List<PartRange> nextRanges = workflows[next.Destination].Execute(next);
            for (int i = nextRanges.Count - 1; i >= 0; i--)
            {
                rangeStack.Push(nextRanges[i]);
            }
        }

        long sum = 0;
        foreach (PartRange range in acceptedRanges) 
        {
            long val = range.GetTotalInRange();
            sum += val;
        }

        Console.WriteLine(sum);
    }
}
