using Godot;
using ReGame.Scripts;

/// <summary>
/// Represent the player
/// </summary>
// ReSharper disable once CheckNamespace
public class Player : KinematicBody
{
    /// <summary>
    /// The default move backward input
    /// </summary>
    [Export] public readonly string MoveBackwardInput = "move_backward";

    /// <summary>
    /// The default move forward input
    /// </summary>
    [Export] public readonly string MoveForwardInput = "move_forward";

    /// <summary>
    /// The default move left input
    /// </summary>
    [Export] public readonly string TurnLeftInput = "turn_left";

    /// <summary>
    /// The default move right input
    /// </summary>
    [Export] public readonly string TurnRightInput = "turn_right";

    /// <summary>
    /// The default run input
    /// </summary>
    [Export] public readonly string RunInput = "run";

    /// <summary>
    /// The defaut action input
    /// </summary>
    [Export] private readonly string _actionInput = "action";

    /// <summary>
    /// The default turn speed
    /// </summary>
    [Export] public readonly float TurnSpeed = 0.05f;

    /// <summary>
    /// The default run speed
    /// </summary>
    [Export] public readonly float RunSpeed = 3.0f;

    /// <summary>
    /// The default walk speed
    /// </summary>
    [Export] public readonly float WalkSpeed = 1.5f;

    /// <summary>
    /// The default backward speed
    /// </summary>
    [Export] public readonly float BackwardSpeed = 0.5f;

    /// <summary>
    /// The default rempage speed
    /// </summary>
    [Export] public float RampageSpeed = 1.5f;

    /// <summary>
    /// The default action raycast reach
    /// </summary>
    [Export] private readonly float _actionRaycastReach = 0.425f;

    /// <summary>
    /// The default name of idle enter animation
    /// </summary>
    [Export] private readonly string _animationNameIdle = "Idle";

    /// <summary>
    /// The default name of walk enter animation
    /// </summary>
    [Export] public readonly string AnimationNameWalk = "Walk";

    /// <summary>
    /// The default name of run enter animation
    /// </summary>
    [Export] public readonly string AnimationNameRun = "Run";

    /// <summary>
    /// The default name of rampage enter animation
    /// </summary>
    [Export] public readonly string AnimationNameRampageEnter = "RampageBegin";

    /// <summary>
    /// The default name of in progress rampage animation
    /// </summary>
    [Export] public readonly string AnimationNameRampageInProgress = "Rampage";

    /// <summary>
    /// The default name of rampage exit animation
    /// </summary>
    [Export] public readonly string AnimationNameRampageExit = "RampageEnd";

    /// <summary>
    /// The group name of rampage point nodes
    /// </summary>
    [Export] private readonly string _rampagePointGroupNode = "rampage_point";

    /// <summary>
    /// The animation player node
    /// </summary>
    public AnimationPlayer AnimationPlayer;

    /// <summary>
    /// The collision shape
    /// </summary>
    private CollisionShape _collision;

    /// <summary>
    /// The collision box shape
    /// </summary>
    public BoxShape CollisionShape;

    /// <summary>
    /// The action raycast node
    /// </summary>
    public RayCast ActionRaycast;

    /// <summary>
    /// The input manager
    /// </summary>
    public InputManager InputManager;

    /// <summary>
    /// The basic movements
    /// </summary>
    public BasicMovements BasicMovements;

    /// <summary>
    /// The rampage
    /// </summary>
    private RampageMovements _rampageMovements;

    /// <summary>
    /// Executed when ready
    /// </summary>
    public override void _Ready()
    {
        _collision = GetNode<CollisionShape>("./CollisionShape");
        AnimationPlayer = GetNode<AnimationPlayer>("./Player/AnimationPlayer");
        CollisionShape = (BoxShape) _collision.Shape;
        ActionRaycast = GetNode<RayCast>("./ActionRaycast");
        ActionRaycast.CastTo = new Vector3(0.0f, -_actionRaycastReach, 0.0f);

        InputManager = new InputManager();
        BasicMovements = new BasicMovements(this);
        _rampageMovements = new RampageMovements(this, _rampagePointGroupNode);
    }

    /// <summary>
    /// Executed on every physics process.
    /// </summary>
    /// <param name="delta">The delta in seconds</param>
    public override void _PhysicsProcess(float delta)
    {
        ManageRampage();
        ManageBasicMovements();
    }

    /// <summary>
    /// Manage the basic movements
    /// </summary>
    private void ManageBasicMovements()
    {
        if (!BasicMovements.MoveFromInputs() && !_rampageMovements.IsActive())
        {
            AnimationPlayer.Play(_animationNameIdle);
        }

        BasicMovements.RotateFromInputs();
    }


    /// <summary>
    /// Manage rampage movements
    /// </summary>
    private void ManageRampage()
    {
        if (!_rampageMovements.IsActive() || _rampageMovements.CheckInProgress()) return;

        InputManager.IsActive = true;

        AnimationPlayer.Play(_animationNameIdle);
    }

    /// <summary>
    /// Executed when input event is detected
    /// </summary>
    /// <param name="event">The event</param>
    public override void _Input(InputEvent @event)
    {
        if (InputManager.IsJustPressed(@event, _actionInput))
        {
            InputManager.IsActive = !_rampageMovements.Enter();
        }
    }
}