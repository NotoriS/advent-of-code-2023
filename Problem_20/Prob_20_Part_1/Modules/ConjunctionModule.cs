
public class ConjunctionModule : Module
{
    private Dictionary<string, PulseType> _inputMemory = new Dictionary<string, PulseType>();

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
    }

    public override void RecievePulse(Pulse pulse)
    {
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
