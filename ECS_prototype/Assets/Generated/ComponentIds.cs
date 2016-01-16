public static class ComponentIds {
    public const int _Gravity = 0;
    public const int _OnStart = 1;
    public const int ButtonEvent = 2;
    public const int CharacterController = 3;
    public const int DpadEvent = 4;
    public const int Force = 5;
    public const int IO_OnFirstPress = 6;
    public const int IOControl = 7;
    public const int IORelease = 8;
    public const int _CONSTANT = 9;
    public const int OnCollisionEnter = 10;
    public const int Player = 11;
    public const int Position = 12;
    public const int PowerUpAttributes = 13;
    public const int rbody = 14;
    public const int rbodyEvent = 15;
    public const int RigidBody = 16;
    public const int stateFacing = 17;
    public const int stateHMotion = 18;
    public const int stateVMotion = 19;
    public const int Velocity = 20;
    public const int View = 21;
    public const int ViewResource = 22;

    public const int TotalComponents = 23;

    public static readonly string[] componentNames = {
        "_Gravity",
        "_OnStart",
        "ButtonEvent",
        "CharacterController",
        "DpadEvent",
        "Force",
        "IO_OnFirstPress",
        "IOControl",
        "IORelease",
        "_CONSTANT",
        "OnCollisionEnter",
        "Player",
        "Position",
        "PowerUpAttributes",
        "rbody",
        "rbodyEvent",
        "RigidBody",
        "stateFacing",
        "stateHMotion",
        "stateVMotion",
        "Velocity",
        "View",
        "ViewResource"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(_GravityComponent),
        typeof(_OnStartComponent),
        typeof(ButtonEventComponent),
        typeof(CharacterControllerComponent),
        typeof(DpadEventComponent),
        typeof(ForceComponent),
        typeof(IO_OnFirstPressComponent),
        typeof(IOControlComponent),
        typeof(IOReleaseComponent),
        typeof(MTON._CONSTANTComponent),
        typeof(OnCollisionEnterComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(PowerUpAttributesComponent),
        typeof(rbodyComponent),
        typeof(rbodyEventComponent),
        typeof(RigidBodyComponent),
        typeof(stateFacingComponent),
        typeof(stateHMotionComponent),
        typeof(stateVMotionComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent),
        typeof(ViewResourceComponent)
    };
}