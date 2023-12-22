
public class Utils
{
    public static long LeastCommonMultiple(IEnumerable<int> nums)
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

    public static List<int> PrimeFactorize(int n)
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

    public static bool IsPrime(int number)
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
