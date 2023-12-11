using System;
using System.Collections.Generic;

public class Galaxy
{
    public Tuple<int, int> Coordinates { get; private set; }

    public Galaxy(int row, int col)
    {
        Coordinates = Tuple.Create(row, col);
    }
}