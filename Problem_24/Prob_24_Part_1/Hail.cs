
public class Hail
{
    public long PX { get; private set; }
    public long PY { get; private set; }
    public long PZ { get; private set; }

    public long VX { get; private set; }
    public long VY { get; private set; }
    public long VZ { get; private set; }

    public Hail(long px, long py, long pz, long vx, long vy, long vz)
    {
        PX = px;
        PY = py;
        PZ = pz;
        VX = vx;
        VY = vy;
        VZ = vz;
    }
}
