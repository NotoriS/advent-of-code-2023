
public class Intersection
{
    public double X { get; private set; }
    public double Y { get; private set; }

    public Intersection(double x, double y)
    {
        X = x;
        Y = y;
    }

    public static Intersection? Get2DIntersection(Hail h1, Hail h2)
    {
        long denominator = h1.VX * h2.VY - h1.VY * h2.VX;

        if (denominator == 0) return null;

        double t1 = ((h2.PX - h1.PX) * h2.VY - (h2.PY - h1.PY) * h2.VX) / (double)denominator;
        double t2 = ((h2.PX - h1.PX) * h1.VY - (h2.PY - h1.PY) * h1.VX) / (double)denominator;

        if (t1 < 0 || t2 < 0) return null;

        double x = h1.PX + t1 * h1.VX;
        double y = h1.PY + t1 * h1.VY;
        return new Intersection(x, y);
    }
}
