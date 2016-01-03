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
    public const int PowerUpAttributes = 9;
    public const int Resource = 10;
    public const int Velocity = 11;
    public const int View = 12;

    public const int TotalComponents = 13;

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
        "PowerUpAttributes",
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
        typeof(PowerUpAttributesComponent),
        typeof(ResourceComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent)
    };
}