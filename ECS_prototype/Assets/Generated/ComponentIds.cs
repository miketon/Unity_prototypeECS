public static class ComponentIds {
    public const int _CharacterController = 0;
    public const int _Gravity = 1;
    public const int _RigidBody = 2;
    public const int event_IO_OnFirstPress = 3;
    public const int event_IO_OnFirstRelease = 4;
    public const int event_IO_OnRelease = 5;
    public const int eventButton = 6;
    public const int eventCrouch = 7;
    public const int eventDpad = 8;
    public const int eventFacing = 9;
    public const int eventGamePad = 10;
    public const int eventHMotion = 11;
    public const int eventOnCollision = 12;
    public const int eventVMotion = 13;
    public const int IO_Controllable = 14;
    public const int _CONSTANT = 15;
    public const int Player = 16;
    public const int Position = 17;
    public const int PowerUpAttributes = 18;
    public const int Rotation = 19;
    public const int Scale = 20;
    public const int stateButton = 21;
    public const int stateCrouch = 22;
    public const int stateDpad = 23;
    public const int stateFacing = 24;
    public const int stateHMotion = 25;
    public const int stateVMotion = 26;
    public const int Velocity = 27;
    public const int View = 28;
    public const int ViewResource = 29;

    public const int TotalComponents = 30;

    public static readonly string[] componentNames = {
        "_CharacterController",
        "_Gravity",
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