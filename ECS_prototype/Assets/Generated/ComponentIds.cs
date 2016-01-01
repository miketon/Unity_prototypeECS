public static class ComponentIds {
    public const int GameObject = 0;
    public const int Gravity = 1;
    public const int InputButton = 2;
    public const int Player = 3;
    public const int Position = 4;
    public const int Resource = 5;
    public const int Velocity = 6;
    public const int View = 7;

    public const int TotalComponents = 8;

    public static readonly string[] componentNames = {
        "GameObject",
        "Gravity",
        "InputButton",
        "Player",
        "Position",
        "Resource",
        "Velocity",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(GameObjectComponent),
        typeof(GravityComponent),
        typeof(InputButtonComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(ResourceComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent)
    };
}