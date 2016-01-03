public static class ComponentIds {
    public const int Force = 0;
    public const int GameObject = 1;
    public const int Gravity = 2;
    public const int IOControl = 3;
    public const int IOGamePad = 4;
    public const int IORelease = 5;
    public const int Player = 6;
    public const int Position = 7;
    public const int Resource = 8;
    public const int Velocity = 9;
    public const int View = 10;

    public const int TotalComponents = 11;

    public static readonly string[] componentNames = {
        "Force",
        "GameObject",
        "Gravity",
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