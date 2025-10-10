namespace WarringStates.Map;

public struct HexagonCoordinate
{
    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public HexagonCoordinate(int x, int z)
    {
        X = x;
        Z = z;
        Y = 0 - (X + Z);
    }

    public static HexagonCoordinate FromOffsetCoordinate(int x, int z)
    {
        return new HexagonCoordinate(x, z);
    }

    public override string ToString()
    {
        return $"x:{X}\ny:{Y}\nz:{Z}";
    }
}
