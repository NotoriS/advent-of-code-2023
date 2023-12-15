using System.Collections.Specialized;

public class Instruction
{
    private string _label;
    private char _operator;

    private int _boxIndex;
    private int _lensLength;

    public Instruction(string input)
    {
        if (input.Contains('-'))
        {
            _operator = '-';
            _label = input.Remove(input.Length - 1);
            _boxIndex = HashLabel(_label);
        }
        else if (input.Contains('=')) 
        {
            _operator = '=';
            _label = input.Split('=')[0];
            _boxIndex = HashLabel(_label);
            _lensLength = int.Parse(input.Split('=')[1]);
        }
    }

    public void Execute(List<OrderedDictionary> boxes)
    {
        if (_operator == '-')
        {
            ExecuteRemoveLens(boxes);
        }
        else if (_operator == '=')
        {
            ExecuteAddLens(boxes);
        }
    }

    private void ExecuteRemoveLens(List<OrderedDictionary> boxes)
    {
        boxes[_boxIndex].Remove(_label);
    }

    private void ExecuteAddLens(List<OrderedDictionary> boxes)
    {
        if (boxes[_boxIndex].Contains(_label))
        {
            boxes[_boxIndex][_label] = _lensLength;
        }
        else 
        {
            boxes[_boxIndex].Add(_label, _lensLength);
        }
    }

    public static int HashLabel(string label)
    {
        int val = 0;
        foreach (char c in label)
        {
            val += c;
            val *= 17;
            val %= 256;
        }

        return val;
    }
}
