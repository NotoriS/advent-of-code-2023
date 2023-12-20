using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        Dictionary<string, Workflow> workflows = new Dictionary<string, Workflow>();
        List<Part> parts = new List<Part>();

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
        Queue<PartRange> rangeQ = new Queue<PartRange>();

        PartRange start = new PartRange();
        start.Destination = "in";
        rangeQ.Enqueue(start);

        while (rangeQ.Count > 0)
        {
            PartRange next = rangeQ.Dequeue();

            if (next.Destination == "A")
            {
                if (next.HasValues) acceptedRanges.Add(next);
                continue;
            }

            if (next.Destination == "R") continue;

            List<PartRange> nextRanges = workflows[next.Destination].Execute(next);
            foreach (PartRange range in nextRanges)
            {
                rangeQ.Enqueue(range);
            }
        }

        Console.WriteLine();
    }
}
