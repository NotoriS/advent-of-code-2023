
public class Problem
{
    public static void Main(string[] args)
    {
        for (int i = 0; i < 1000; i++)
        {
            PulseManager.Instance.PushButton();
        }

        long result = PulseManager.Instance.HighPulseCount * PulseManager.Instance.LowPulseCount;
        Console.WriteLine(result);
    }
}
