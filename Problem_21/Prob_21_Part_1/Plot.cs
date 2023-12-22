
public class Plot
{
    private List<Plot> _neighbors;
    private bool _isOnNextBeforeNextStep;

    public bool IsOn { get; private set; }

    public Plot(bool isOn)
    {
        _neighbors = new List<Plot>();
        _isOnNextBeforeNextStep = false;

        IsOn = isOn;
    }

    public void Step()
    {
        if (!IsOn) return;

        foreach (Plot neighbor in _neighbors)
        {
            neighbor._isOnNextBeforeNextStep = true;
        }
        IsOn = false;

    }

    public void UpdateAfterStep()
    {
        IsOn = _isOnNextBeforeNextStep;
        _isOnNextBeforeNextStep = false;
    }

    public void SetNeighbors(List<Plot> neighbors)
    {
        _neighbors = new List<Plot>(neighbors);
    }
}
