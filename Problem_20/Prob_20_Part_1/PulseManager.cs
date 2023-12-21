
public class PulseManager
{
    private Dictionary<string, Module> _modules;

    private Queue<Pulse> _pulseQueue;

    private static PulseManager _instance;

    public long LowPulseCount { get; private set; }
    public long HighPulseCount { get; private set; }

    public static PulseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PulseManager();
            }
            return _instance;
        }
    }

    private PulseManager()
    {
        _modules = new Dictionary<string, Module>();
        _pulseQueue = new Queue<Pulse>();
        LowPulseCount = 0;
        HighPulseCount = 0;

        StreamReader reader = new StreamReader("../../../input.txt");

        while (reader.Peek() != -1)
        {
            CreateModule(reader.ReadLine());
        }

        reader.Close();
    }

    public void PushButton()
    {
        Pulse firstPulse = new Pulse(PulseType.Low, "button", "broadcaster");
        _pulseQueue.Enqueue(firstPulse);
        ProcessPulses();
    }

    private void ProcessPulses()
    {
        while (_pulseQueue.Count > 0)
        {
            Pulse pulse = _pulseQueue.Dequeue();

            if (pulse.Type == PulseType.Low) LowPulseCount++;
            if (pulse.Type == PulseType.High) HighPulseCount++;

            _modules[pulse.Destination].RecievePulse(pulse);
        }
    }

    public void SendPulse(Pulse pulse)
    {
        _pulseQueue.Enqueue(pulse);
    }

    private Module CreateModule(string line)
    {
        string identifier = line.Split(" -> ")[0];
        List<string> destinations = line.Split(" -> ")[1].Split(", ").ToList();

        if (identifier.StartsWith('%'))
        {
            // TODO: Create flip-flip module and add to dictionary.
        }
        else if (identifier.StartsWith('&'))
        {
            // TODO: Create conjunction module and add to dictionary.
        }
        else
        {
            // TODO: Create broadcaster module and add to dictionary.
        }
        return null;
    }
}
