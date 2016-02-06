public static class ComponentIds {
    public const int _CharacterController = 0;
    public const int _Gravity = 1;
    public const int _OnStart = 2;
    public const int _RigidBody = 3;
    public const int event_IO_OnFirstPress = 4;
    public const int event_IO_OnFirstRelease = 5;
    public const int event_IO_OnRelease = 6;
    public const int eventButton = 7;
    public const int eventDpad = 8;
    public const int eventGamePad = 9;
    public const int eventHMotion = 10;
    public const int eventOnCollision = 11;
    public const int eventVMotion = 12;
    public const int IO_Controllable = 13;
    public const int _CONSTANT = 14;
    public const int Player = 15;
    public const int Position = 16;
    public const int PowerUpAttributes = 17;
    public const int stateFacing = 18;
    public const int stateHMotion = 19;
    public const int stateVMotion = 20;
    public const int Velocity = 21;
    public const int View = 22;
    public const int ViewResource = 23;

    public const int TotalComponents = 24;

    public static readonly string[] componentNames = {
        "_CharacterController",
        "_Gravity",
        "_OnStart",
        "_RigidBody",
        "event_IO_OnFirstPress",
        "event_IO_OnFirstRelease",
        "event_IO_OnRelease",
        "eventButton",
        "eventDpad",
        "eventGamePad",
        "eventHMotion",
        "eventOnCollision",
        "eventVMotion",
        "IO_Controllable",
        "_CONSTANT",
        "Player",
        "Position",
        "PowerUpAttributes",
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
        typeof(event_IO_OnFirstPressComponent),
        typeof(event_IO_OnFirstReleaseComponent),
        typeof(event_IO_OnReleaseComponent),
        typeof(eventButtonComponent),
        typeof(eventDpadComponent),
        typeof(eventGamePadComponent),
        typeof(eventHMotionComponent),
        typeof(eventOnCollisionComponent),
        typeof(eventVMotionComponent),
        typeof(IO_ControllableComponent),
        typeof(MTON._CONSTANTComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(PowerUpAttributesComponent),
        typeof(stateFacingComponent),
        typeof(stateHMotionComponent),
        typeof(stateVMotionComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent),
        typeof(ViewResourceComponent)
    };
}