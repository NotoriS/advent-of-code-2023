
public class Workflow
{
    private List<Rule> _rules;

    public Workflow(List<string> rules) 
    {
        _rules = new List<Rule>();
        foreach (var rule in rules)
        {
            _rules.Add(CreateRule(rule));
        }
    }

    public List<PartRange> Execute(PartRange range) 
    {
        List<PartRange> result = new List<PartRange>();
        foreach (Rule rule in _rules)
        {
            Mask mask = rule.Test();
            Mask reverseMask = mask.Opposite();

            PartRange added = range.ApplyMask(mask);
            added.Destination = rule.Destination;
            result.Add(added);

            range = range.ApplyMask(reverseMask);
        }

        return result;
    }

    private Rule CreateRule(string rule)
    {
        if (rule.Contains(':'))
        {
            string test = rule.Split(':')[0];
            string destination = rule.Split(':')[1];

            string letter = test.Split('<', '>')[0];
            int value = int.Parse(test.Split('<', '>')[1]);

            if (test.Contains('<'))
            {
                return new LessThanRule(letter, value, destination);
            }
            else if (test.Contains('>'))
            {
                return new GreaterThanRule(letter, value, destination);
            }
            else
            {
                throw new InvalidDataException("Unknown test type.");
            }
        }

        return new UntestedRule(rule);
    }
}
