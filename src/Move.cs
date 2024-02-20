public struct Move
{
    public Coordinate Start;
    public Coordinate Target;

    public Move(Coordinate start, Coordinate target)
    {
        Start = start;
        Target = target;
    }


    public override string ToString() => $"{Start}{Target}";
}
