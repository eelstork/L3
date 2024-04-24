namespace Activ.Util{
public class Range{

    float min, max;

    public Range(float a, float b){ min = a; max = b; }

    public static implicit operator Range(float dist)
    => new Range(dist, dist);

}}
