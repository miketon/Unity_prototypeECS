public static class ComponentIds {
    public const int ButtonEvent = 0;
    public const int CharacterController = 1;
    public const int DpadEvent = 2;
    public const int Force = 3;
    public const int GameObject = 4;
    public const int Gravity = 5;
    public const int IO_OnFirstPress = 6;
    public const int IOControl = 7;
    public const int IORelease = 8;
    public const int _CONSTANT = 9;
    public const int OnCollisionEnter = 10;
    public const int Player = 11;
    public const int Position = 12;
    public const int PowerUpAttributes = 13;
    public const int rbodyEvent = 14;
    public const int Resource = 15;
    public const int RigidBody = 16;
    public const int stateFacing = 17;
    public const int stateHMotion = 18;
    public const int stateVMotion = 19;
    public const int Velocity = 20;
    public const int View = 21;

    public const int TotalComponents = 22;

    public static readonly string[] componentNames = {
        "ButtonEvent",
        "CharacterController",
        "DpadEvent",
        "Force",
        "GameObject",
        "Gravity",
        "IO_OnFirstPress",
        "IOControl",
        "IORelease",
        "_CONSTANT",
        "OnCollisionEnter",
        "Player",
        "Position",
        "PowerUpAttributes",
        "rbodyEvent",
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
        typeof(CharacterControllerComponent),
        typeof(DpadEventComponent),
        typeof(ForceComponent),
        typeof(GameObjectComponent),
        typeof(GravityComponent),
        typeof(IO_OnFirstPressComponent),
        typeof(IOControlComponent),
        typeof(IOReleaseComponent),
        typeof(MTON._CONSTANTComponent),
        typeof(OnCollisionEnterComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(PowerUpAttributesComponent),
        typeof(rbodyEventComponent),
        typeof(ResourceComponent),
        typeof(RigidBodyComponent),
        typeof(stateFacingComponent),
        typeof(stateHMotionComponent),
        typeof(stateVMotionComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent)
    };
}