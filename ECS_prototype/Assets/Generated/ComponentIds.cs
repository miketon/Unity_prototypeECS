public static class ComponentIds {
    public const int InputPress = 0;
    public const int InputRelease = 1;
    public const int Position = 2;
    public const int Resource = 3;
    public const int View = 4;

    public const int TotalComponents = 5;

    public static readonly string[] componentNames = {
        "InputPress",
        "InputRelease",
        "Position",
        "Resource",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(InputPressComponent),
        typeof(InputReleaseComponent),
        typeof(PositionComponent),
        typeof(ResourceComponent),
        typeof(ViewComponent)
    };
}