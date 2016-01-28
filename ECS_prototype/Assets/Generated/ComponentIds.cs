public static class ComponentIds {
    public const int _CharacterController = 0;
    public const int _Gravity = 1;
    public const int _OnStart = 2;
    public const int _RigidBody = 3;
    public const int ButtonEvent = 4;
    public const int DpadEvent = 5;
    public const int GpadEvent = 6;
    public const int IO_OnFirstPress = 7;
    public const int IOControl = 8;
    public const int IORelease = 9;
    public const int _CONSTANT = 10;
    public const int OnCollisionEnter = 11;
    public const int Player = 12;
    public const int Position = 13;
    public const int PowerUpAttributes = 14;
    public const int RbodyEvent = 15;
    public const int stateFacing = 16;
    public const int stateHMotion = 17;
    public const int stateVMotion = 18;
    public const int Velocity = 19;
    public const int View = 20;
    public const int ViewResource = 21;

    public const int TotalComponents = 22;

    public static readonly string[] componentNames = {
        "_CharacterController",
        "_Gravity",
        "_OnStart",
        "_RigidBody",
        "ButtonEvent",
        "DpadEvent",
        "GpadEvent",
        "IO_OnFirstPress",
        "IOControl",
        "IORelease",
        "_CONSTANT",
        "OnCollisionEnter",
        "Player",
        "Position",
        "PowerUpAttributes",
        "RbodyEvent",
        "stateFacing",
        "stateHMotion",
        "stateVMotion",
        "Velocity",
        "View",
        "ViewResource"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(_CharacterControllerComponent),
        typeof(_GravityComponent),
        typeof(_OnStartComponent),
        typeof(_RigidBodyComponent),
        typeof(ButtonEventComponent),
        typeof(DpadEventComponent),
        typeof(GpadEventComponent),
        typeof(IO_OnFirstPressComponent),
        typeof(IOControlComponent),
        typeof(IOReleaseComponent),
        typeof(MTON._CONSTANTComponent),
        typeof(OnCollisionEnterComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(PowerUpAttributesComponent),
        typeof(RbodyEventComponent),
        typeof(stateFacingComponent),
        typeof(stateHMotionComponent),
        typeof(stateVMotionComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent),
        typeof(ViewResourceComponent)
    };
}