public static class ComponentIds {
    public const int _CharacterController = 0;
    public const int _Gravity = 1;
    public const int _OnStart = 2;
    public const int _RigidBody = 3;
    public const int ButtonEvent = 4;
    public const int DpadEvent = 5;
    public const int GpadEvent = 6;
    public const int IO_Controllable = 7;
    public const int IO_OnFirstPress = 8;
    public const int IO_OnFirstRelease = 9;
    public const int IORelease = 10;
    public const int _CONSTANT = 11;
    public const int OnCollisionEnter = 12;
    public const int Player = 13;
    public const int Position = 14;
    public const int PowerUpAttributes = 15;
    public const int stateCBody = 16;
    public const int stateFacing = 17;
    public const int stateHMotion = 18;
    public const int stateVMotion = 19;
    public const int Velocity = 20;
    public const int View = 21;
    public const int ViewResource = 22;
    public const int VstateEvent = 23;

    public const int TotalComponents = 24;

    public static readonly string[] componentNames = {
        "_CharacterController",
        "_Gravity",
        "_OnStart",
        "_RigidBody",
        "ButtonEvent",
        "DpadEvent",
        "GpadEvent",
        "IO_Controllable",
        "IO_OnFirstPress",
        "IO_OnFirstRelease",
        "IORelease",
        "_CONSTANT",
        "OnCollisionEnter",
        "Player",
        "Position",
        "PowerUpAttributes",
        "stateCBody",
        "stateFacing",
        "stateHMotion",
        "stateVMotion",
        "Velocity",
        "View",
        "ViewResource",
        "VstateEvent"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(_CharacterControllerComponent),
        typeof(_GravityComponent),
        typeof(_OnStartComponent),
        typeof(_RigidBodyComponent),
        typeof(ButtonEventComponent),
        typeof(DpadEventComponent),
        typeof(GpadEventComponent),
        typeof(IO_ControllableComponent),
        typeof(IO_OnFirstPressComponent),
        typeof(IO_OnFirstReleaseComponent),
        typeof(IOReleaseComponent),
        typeof(MTON._CONSTANTComponent),
        typeof(OnCollisionEnterComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(PowerUpAttributesComponent),
        typeof(stateCBodyComponent),
        typeof(stateFacingComponent),
        typeof(stateHMotionComponent),
        typeof(stateVMotionComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent),
        typeof(ViewResourceComponent),
        typeof(VstateEventComponent)
    };
}