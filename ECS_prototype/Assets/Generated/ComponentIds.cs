public static class ComponentIds {
    public const int GameObject = 0;
    public const int Gravity = 1;
    public const int IOControl = 2;
    public const int IOGamePad = 3;
    public const int Move = 4;
    public const int Player = 5;
    public const int Position = 6;
    public const int Resource = 7;
    public const int Velocity = 8;
    public const int View = 9;

    public const int TotalComponents = 10;

    public static readonly string[] componentNames = {
        "GameObject",
        "Gravity",
        "IOControl",
        "IOGamePad",
        "Move",
        "Player",
        "Position",
        "Resource",
        "Velocity",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(GameObjectComponent),
        typeof(GravityComponent),
        typeof(IOControlComponent),
        typeof(IOGamePadComponent),
        typeof(MoveComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(ResourceComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent)
    };
}