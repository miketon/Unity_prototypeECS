public static class ComponentIds {
    public const int InputPress = 0;
    public const int InputRelease = 1;
    public const int Player = 2;
    public const int Position = 3;
    public const int Resource = 4;
    public const int View = 5;

    public const int TotalComponents = 6;

    public static readonly string[] componentNames = {
        "InputPress",
        "InputRelease",
        "Player",
        "Position",
        "Resource",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(InputPressComponent),
        typeof(InputReleaseComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(ResourceComponent),
        typeof(ViewComponent)
    };
}