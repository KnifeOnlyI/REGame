using Godot;

namespace ReGame.Scripts
{
    /// <summary>
    /// Represent an input manager
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// The flag to indicates if the input manager is active or not
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Create a new input manager
        /// </summary>
        public InputManager()
        {
        }

        /// <summary>
        /// Get the strength of the specified action. Always return 0 if the input manager is not active
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>The strength</returns>
        public float GetStrength(string action)
        {
            return IsActive ? Input.GetActionStrength(action) : 0.0f;
        }

        /// <summary>
        /// Check if the specified action is pressed. Always return false if the input manager is not active
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>TRUE if the specified action is pressed, FALSE otherwise</returns>
        public bool IsPressed(string action)
        {
            return IsActive && Input.IsActionPressed(action);
        }

        /// <summary>
        /// Check if the specified action is pressed. Always return false if the input manager is not active
        /// </summary>
        /// <param name="inputEvent">The input event</param>
        /// <param name="action">The action</param>
        /// <returns>TRUE if the specified action is pressed, FALSE otherwise</returns>
        public bool IsJustPressed(InputEvent inputEvent, string action)
        {
            return IsActive && inputEvent.IsActionPressed(action);
        }

        /// <summary>
        /// Check if the specified action is released. Always return false if the input manager is not active
        /// </summary>
        /// <param name="inputEvent">The input event</param>
        /// <param name="action">The action</param>
        /// <returns>TRUE if the specified action is released, FALSE otherwise</returns>
        public bool IsReleased(InputEvent inputEvent, string action)
        {
            return IsActive && inputEvent.IsActionReleased(action);
        }
    }
}