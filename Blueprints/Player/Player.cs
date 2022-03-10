using Godot;

/// <summary>
/// Represent the player
/// </summary>
// ReSharper disable once CheckNamespace
public class Player : KinematicBody
{
    /// <summary>
    /// The default move backward input
    /// </summary>
    [Export] private string _moveBackwardInput = "move_backward";

    /// <summary>
    /// The default move forward input
    /// </summary>
    [Export] private string _moveForwardInput = "move_forward";

    /// <summary>
    /// The current movement speed
    /// </summary>
    private float _movementSpeed;

    /// <summary>
    /// The default run speed
    /// </summary>
    [Export] private float _runSpeed = 4.0f;

    /// <summary>
    /// The default move left input
    /// </summary>
    [Export] private string _turnLeftInput = "turn_left";

    /// <summary>
    /// The default move right input
    /// </summary>
    [Export] private string _turnRightInput = "turn_right";

    /// <summary>
    /// The default turn speed
    /// </summary>
    [Export] private float _turnSpeed = 0.05f;

    /// <summary>
    /// The default walk speed
    /// </summary>
    [Export] private float _walkSpeed = 2.0f;

    /// <summary>
    /// Create a new player
    /// </summary>
    public Player()
    {
        _movementSpeed = _walkSpeed;
    }

    /// <summary>
    /// Executed on every physics process.
    /// </summary>
    /// <param name="delta">The delta in seconds</param>
    public override void _PhysicsProcess(float delta)
    {
        var forwardValue = Input.GetActionStrength("move_forward");
        var backwardValue = Input.GetActionStrength("move_backward");
        var leftValue = Input.GetActionStrength("turn_left");
        var rightValue = Input.GetActionStrength("turn_right");

        if (forwardValue > 0 || backwardValue > 0)
        {
            var movement = GlobalTransform.basis.z * _movementSpeed;

            if (backwardValue > 0)
            {
                movement = -movement;
            }

            MoveAndSlide(movement, Vector3.Up);
        }

        if (leftValue > 0)
        {
            RotateY(leftValue * _turnSpeed);
        }
        else if (rightValue > 0)
        {
            RotateY(-rightValue * _turnSpeed);
        }
    }

    /// <summary>
    /// Executed when input event is detected
    /// </summary>
    /// <param name="event">The event</param>
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionPressed("run"))
        {
            _movementSpeed = _runSpeed;
        }
        else if (@event.IsActionReleased("run"))
        {
            _movementSpeed = _walkSpeed;
        }
    }
}