using Godot;

// ReSharper disable once CheckNamespace
// ReSharper disable once UnusedType.Global
public class RampagePoint : Godot.Area
{
    /// <summary>
    /// Is the end point of rampage flag
    /// </summary>
    [Export] public bool IsEnd;

    /// <summary>
    /// The disabled (for enter only) flag
    /// </summary>
    [Export] public bool Disabled;

    /// <summary>
    /// The enter label node
    /// </summary>
    [Export] private Spatial _enterLabel;

    /// <summary>
    /// Executed when ready
    /// </summary>
    public override void _Ready()
    {
        _enterLabel = GetNode<Spatial>("./CollisionShape/EnterLabel");

        // Hide the label because it only used developers
        _enterLabel.Hide();
    }
}