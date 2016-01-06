public static class ComponentIds {
    public const int Force = 0;
    public const int GameObject = 1;
    public const int Gravity = 2;
    public const int IO_OnFirstPress = 3;
    public const int IOControl = 4;
    public const int IOGamePad = 5;
    public const int IORelease = 6;
    public const int OnCollisionEnter = 7;
    public const int Player = 8;
    public const int Position = 9;
    public const int PowerUpAttributes = 10;
    public const int Resource = 11;
    public const int Velocity = 12;
    public const int View = 13;

    public const int TotalComponents = 14;

    public static readonly string[] componentNames = {
        "Force",
        "GameObject",
        "Gravity",
        "IO_OnFirstPress",
        "IOControl",
        "IOGamePad",
        "IORelease",
        "OnCollisionEnter",
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
        typeof(OnCollisionEnterComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(PowerUpAttributesComponent),
        typeof(ResourceComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent)
    };
}