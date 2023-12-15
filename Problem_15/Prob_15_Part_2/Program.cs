using System.Collections;
using System.Collections.Specialized;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");

        List<string> strings = new List<string>();
        foreach (string s in reader.ReadLine().Split(',')) strings.Add(s);

        List<Instruction> instructions = new List<Instruction>();

        foreach (string s in strings) 
        {
            instructions.Add(new Instruction(s));
        }

        List<OrderedDictionary> boxes = new List<OrderedDictionary>();

        for (int i = 0; i < 256; i++) 
        {
            boxes.Add(new OrderedDictionary());
        }

        foreach (Instruction i in instructions)
        {
            i.Execute(boxes);
        }

        foreach (OrderedDictionary box in boxes)
        {
            if (box.Count > 0)
            {
                Console.Write("Box" + boxes.IndexOf(box) + ": ");
                foreach (DictionaryEntry entry in box) 
                {
                    Console.Write("[" + entry.Key + " " + entry.Value + "] ");
                }
                Console.WriteLine();
            }
        }
        Console.WriteLine();

        long sum = 0;
        for (int i = 0; i < boxes.Count; i++) 
        {
            for (int j = 0; j < boxes[i].Count; j++)
            {
                int lensLength = (int)boxes[i][j];
                int boxFocusPower = lensLength * (i + 1) * (j + 1);
                sum += boxFocusPower;
            }
        }

        Console.WriteLine(sum);
    }
}