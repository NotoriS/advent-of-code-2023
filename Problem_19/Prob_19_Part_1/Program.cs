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

        while (reader.Peek() != -1)
        {
            parts.Add(new Part(reader.ReadLine()));
        }
    }
}
