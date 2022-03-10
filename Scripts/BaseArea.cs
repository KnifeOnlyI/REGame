using Godot;

namespace ReGame.Scripts
{
    /// <summary>
    /// The base class for all area
    /// </summary>
    public class BaseArea : Godot.Area
    {
        /// <summary>
        /// The camera to view the area
        /// </summary>
        private Camera _camera;

        /// <summary>
        /// Executed when ready
        /// </summary>
        public override void _Ready()
        {
            _camera = GetNode<Camera>("./Camera");
        }

        /// <summary>
        /// Executed when an other node enter in the area
        /// </summary>
        /// <param name="node">The entered node</param>
        // ReSharper disable once UnusedMember.Global
        public void OnEnter(Node node)
        {
            if (!(node is Player)) return;

            _camera.MakeCurrent();
        }
    }
}