public static class ComponentIds {
    public const int ButtonEvent = 0;
    public const int DpadEvent = 1;
    public const int Force = 2;
    public const int GameObject = 3;
    public const int Gravity = 4;
    public const int IO_OnFirstPress = 5;
    public const int IOControl = 6;
    public const int IOGamePad = 7;
    public const int IORelease = 8;
    public const int OnCollisionEnter = 9;
    public const int Player = 10;
    public const int Position = 11;
    public const int PowerUpAttributes = 12;
    public const int Resource = 13;
    public const int RigidBody = 14;
    public const int stateFacing = 15;
    public const int stateHMotion = 16;
    public const int stateVMotion = 17;
    public const int Velocity = 18;
    public const int View = 19;

    public const int TotalComponents = 20;

    public static readonly string[] componentNames = {
        "ButtonEvent",
        "DpadEvent",
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
        "RigidBody",
        "stateFacing",
        "stateHMotion",
        "stateVMotion",
        "Velocity",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(ButtonEventComponent),
        typeof(DpadEventComponent),
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
        typeof(RigidBodyComponent),
        typeof(stateFacingComponent),
        typeof(stateHMotionComponent),
        typeof(stateVMotionComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent)
    };
}