public static class ComponentIds {
    public const int GameObject = 0;
    public const int Gravity = 1;
    public const int InputPress = 2;
    public const int InputRelease = 3;
    public const int Player = 4;
    public const int Position = 5;
    public const int Resource = 6;
    public const int Velocity = 7;
    public const int View = 8;

    public const int TotalComponents = 9;

    public static readonly string[] componentNames = {
        "GameObject",
        "Gravity",
        "InputPress",
        "InputRelease",
        "Player",
        "Position",
        "Resource",
        "Velocity",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(GameObjectComponent),
        typeof(GravityComponent),
        typeof(InputPressComponent),
        typeof(InputReleaseComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(ResourceComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent)
    };
}