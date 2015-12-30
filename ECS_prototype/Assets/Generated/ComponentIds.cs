public static class ComponentIds {
    public const int Input = 0;
    public const int Position = 1;

    public const int TotalComponents = 2;

    public static readonly string[] componentNames = {
        "Input",
        "Position"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(InputComponent),
        typeof(PositionComponent)
    };
}