public static class ComponentIds {
    public const int _CharacterController = 0;
    public const int _Gravity = 1;
    public const int _OnStart = 2;
    public const int _RigidBody = 3;
    public const int event_IO_OnFirstPress = 4;
    public const int event_IO_OnFirstRelease = 5;
    public const int event_IO_OnRelease = 6;
    public const int eventButton = 7;
    public const int eventCrouch = 8;
    public const int eventDpad = 9;
    public const int eventFacing = 10;
    public const int eventGamePad = 11;
    public const int eventHMotion = 12;
    public const int eventOnCollision = 13;
    public const int eventVMotion = 14;
    public const int IO_Controllable = 15;
    public const int _CONSTANT = 16;
    public const int Player = 17;
    public const int Position = 18;
    public const int PowerUpAttributes = 19;
    public const int Rotation = 20;
    public const int Scale = 21;
    public const int stateButton = 22;
    public const int stateCrouch = 23;
    public const int stateDpad = 24;
    public const int stateFacing = 25;
    public const int stateHMotion = 26;
    public const int stateVMotion = 27;
    public const int Velocity = 28;
    public const int View = 29;
    public const int ViewResource = 30;

    public const int TotalComponents = 31;

    public static readonly string[] componentNames = {
        "_CharacterController",
        "_Gravity",
        "_OnStart",
        "_RigidBody",
        "event_IO_OnFirstPress",
        "event_IO_OnFirstRelease",
        "event_IO_OnRelease",
        "eventButton",
        "eventCrouch",
        "eventDpad",
        "eventFacing",
        "eventGamePad",
        "eventHMotion",
        "eventOnCollision",
        "eventVMotion",
        "IO_Controllable",
        "_CONSTANT",
        "Player",
        "Position",
        "PowerUpAttributes",
        "Rotation",
        "Scale",
        "stateButton",
        "stateCrouch",
        "stateDpad",
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
        typeof(eventCrouchComponent),
        typeof(eventDpadComponent),
        typeof(eventFacingComponent),
        typeof(eventGamePadComponent),
        typeof(eventHMotionComponent),
        typeof(eventOnCollisionComponent),
        typeof(eventVMotionComponent),
        typeof(IO_ControllableComponent),
        typeof(MTON._CONSTANTComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(PowerUpAttributesComponent),
        typeof(RotationComponent),
        typeof(ScaleComponent),
        typeof(stateButtonComponent),
        typeof(stateCrouchComponent),
        typeof(stateDpadComponent),
        typeof(stateFacingComponent),
        typeof(stateHMotionComponent),
        typeof(stateVMotionComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent),
        typeof(ViewResourceComponent)
    };
}