
public class ConjunctionModule : Module
{
    private Dictionary<string, PulseType> _inputMemory = new Dictionary<string, PulseType>();
    private Dictionary<string, int> _cycleMemory = new Dictionary<string, int>();

    public ConjunctionModule(string name, List<string> destinations) : base(name, destinations)
    {
        StreamReader reader = new StreamReader("../../../input.txt");

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();

            string left = line.Split(" -> ")[0];
            List<string> right = line.Split(" -> ")[1].Split(", ").ToList();

            if (right.Contains(name))
            {
                if (left == "broadcaster")
                {
                    _inputMemory.Add(left, PulseType.Low);
                }
                else
                {
                    _inputMemory.Add(left.Substring(1), PulseType.Low);
                }
            }
        }

        reader.Close();

        foreach (string label in _inputMemory.Keys)
        {
            _cycleMemory.Add(label, -1);
        }
    }

    public override void RecievePulse(Pulse pulse)
    {
        if (_name == "zh" && pulse.Type == PulseType.High)
        {
            if (_cycleMemory[pulse.Source] == -1)
            {
                _cycleMemory[pulse.Source] = (int)PulseManager.Instance.ButtonPressed;
            }
            if (!_cycleMemory.Values.Contains(-1))
            {
                Console.WriteLine(Utils.LeastCommonMultiple(_cycleMemory.Values));
                Environment.Exit(0);
            }
        }

        _inputMemory[pulse.Source] = pulse.Type;

        foreach (string destination in _destinations)
        {
            if (_inputMemory.Values.All(type => type == PulseType.High))
            {
                Pulse lowPulse = new Pulse(PulseType.Low, _name, destination);
                PulseManager.Instance.SendPulse(lowPulse);
            }
            else
            {
                Pulse highPulse = new Pulse(PulseType.High, _name, destination);
                PulseManager.Instance.SendPulse(highPulse);
            }
        }
    }
}
