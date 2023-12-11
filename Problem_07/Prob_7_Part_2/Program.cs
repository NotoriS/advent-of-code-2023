using System.IO;
using System.Collections;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<Hand> hands = new List<Hand>();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            string hand = line.Split(' ')[0];
            string bid = line.Split(' ')[1];

            hands.Add(new Hand(hand.ToCharArray().ToList(), int.Parse(bid)));
        }

        reader.Close();

        hands.Sort();

        long sum = 0;
        for (int i = 1; i <= hands.Count; i++)
        {
            sum += hands[i - 1].Bid * i;
        }

        Console.WriteLine(sum);
    }
}
