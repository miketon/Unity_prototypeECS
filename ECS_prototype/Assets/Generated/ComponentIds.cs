public static class ComponentIds {
    public const int Force = 0;
    public const int GameObject = 1;
    public const int Gravity = 2;
    public const int IO_OnFirstPress = 3;
    public const int IOControl = 4;
    public const int IOGamePad = 5;
    public const int IORelease = 6;
    public const int Player = 7;
    public const int Position = 8;
    public const int Resource = 9;
    public const int Velocity = 10;
    public const int View = 11;

    public const int TotalComponents = 12;

    public static readonly string[] componentNames = {
        "Force",
        "GameObject",
        "Gravity",
        "IO_OnFirstPress",
        "IOControl",
        "IOGamePad",
        "IORelease",
        "Player",
        "Position",
        "Resource",
        "Velocity",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(ForceComponent),
        typeof(GameObjectComponent),
        typeof(GravityComponent),
        typeof(IO_OnFirstPressComponent),
        typeof(IOControlComponent),
        typeof(IOGamePadComponent),
        typeof(IOReleaseComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(ResourceComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent)
    };
}