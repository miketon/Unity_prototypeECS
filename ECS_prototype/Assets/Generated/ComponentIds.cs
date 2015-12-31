public static class ComponentIds {
    public const int InputPress = 0;
    public const int InputRelease = 1;
    public const int Position = 2;

    public const int TotalComponents = 3;

    public static readonly string[] componentNames = {
        "InputPress",
        "InputRelease",
        "Position"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(InputPressComponent),
        typeof(InputReleaseComponent),
        typeof(PositionComponent)
    };
}