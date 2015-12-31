public static class ComponentIds {
    public const int Gravity = 0;
    public const int InputPress = 1;
    public const int InputRelease = 2;
    public const int Player = 3;
    public const int Position = 4;
    public const int Resource = 5;
    public const int View = 6;

    public const int TotalComponents = 7;

    public static readonly string[] componentNames = {
        "Gravity",
        "InputPress",
        "InputRelease",
        "Player",
        "Position",
        "Resource",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Gravity),
        typeof(InputPressComponent),
        typeof(InputReleaseComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(ResourceComponent),
        typeof(ViewComponent)
    };
}