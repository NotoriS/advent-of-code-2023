using System.IO;
using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<char> lrInstrucitons = new List<char>();

        while (true)
        {
            string line = reader.ReadLine();
            if (line == "") break;

            foreach (char c in line)
            {
                lrInstrucitons.Add(c);
            }
        }

        Dictionary<string, Tuple<string, string>> map = new Dictionary<string, Tuple<string, string>>();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();

            string start = line.Substring(0, 3);
            string left = line.Substring(7, 3);
            string right = line.Substring(12, 3);

            map.Add(start, Tuple.Create(left, right));
        }

        List<string> nodesEndingInA = map.Keys.Where(node => node.EndsWith('A')).ToList();
        HashSet<int> cyclePoints = new HashSet<int>();

        for (int i = 0; i < nodesEndingInA.Count; i++)
        {
            for (int j = 1; ; j++)
            {
                if (lrInstrucitons[(j - 1) % lrInstrucitons.Count] == 'L')
                {
                    nodesEndingInA[i] = map[nodesEndingInA[i]].Item1;
                }
                else if (lrInstrucitons[(j - 1) % lrInstrucitons.Count] == 'R')
                {
                    nodesEndingInA[i] = map[nodesEndingInA[i]].Item2;
                }
                else
                {
                    throw new InvalidDataException("lrInstructions should only ever contain L or R.");
                }

                if (nodesEndingInA[i].EndsWith('Z'))
                {
                    cyclePoints.Add(j);
                    break;
                }
            }
        }

        Console.WriteLine(LeastCommonMultiple(cyclePoints));
    }

    private static long LeastCommonMultiple(IEnumerable<int> nums)
    {
        List<List<int>> primeFactorizations = new List<List<int>>();
        foreach (int num in nums) 
        {
            primeFactorizations.Add(PrimeFactorize(num));
        }

        Dictionary<int, int> maxOccurences = new Dictionary<int, int>();

        foreach (List<int> factorization in primeFactorizations)
        {
            Dictionary<int, int> occurences = new Dictionary<int, int>();

            foreach (int factor in factorization)
            {
                if (occurences.ContainsKey(factor))
                {
                    occurences[factor]++;
                }
                else 
                {
                    occurences.Add(factor, 1);
                }
            }

            foreach (int factor in occurences.Keys) 
            {
                if (!maxOccurences.ContainsKey(factor) || maxOccurences[factor] < occurences[factor])
                {
                    maxOccurences[factor] = occurences[factor];
                }
            }
        }

        long result = 1;
        foreach (int n in maxOccurences.Keys)
        {
            result *= n * maxOccurences[n];
        }

        return result;
    }

    private static List<int> PrimeFactorize(int n)
    {
        List<int> primeFactors = new List<int>();

        for (int i = 2; n > 1; i++)
        {
            if (n % i == 0)
            {
                if (!IsPrime(i)) continue;

                while (n % i == 0)
                {
                    n /= i;
                    primeFactors.Add(i);
                }
            }
        }

        return primeFactors;
    }

    private static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        int boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2)
        {
            if (number % i == 0) return false;
        }
        
        return true;
    }
}