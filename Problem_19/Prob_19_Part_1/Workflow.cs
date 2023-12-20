
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

    public string Execute(Part part) 
    {
        foreach (Rule rule in _rules)
        {
            if (rule.Test(part))
            {
                return rule.Destination;
            }
        }

        throw new Exception("At least one rule should always pass.");
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
