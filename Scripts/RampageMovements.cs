using Godot;

namespace ReGame.Scripts
{
    /// <summary>
    /// Represent rampage movements
    /// </summary>
    public class RampageMovements
    {
        /// <summary>
        /// The managed player
        /// </summary>
        private readonly Player _player;

        /// <summary>
        /// The needed group in the enter and exit nodes
        /// </summary>
        private readonly string _nodeGroup;

        /// <summary>
        /// Create a new rampage movements
        /// </summary>
        /// <param name="player">The managed player</param>
        /// <param name="nodeGroup">The node group of rampage nodes</param>
        public RampageMovements(Player player, string nodeGroup)
        {
            _player = player;
            _nodeGroup = nodeGroup;
        }

        /// <summary>
        /// Check if the rampage is active
        /// </summary>
        /// <returns>TRUE if the rampage is active, FALSE otherwise</returns>
        public bool IsActive()
        {
            return _player.AnimationPlayer.AssignedAnimation == _player.AnimationNameRampageEnter ||
                   _player.AnimationPlayer.AssignedAnimation == _player.AnimationNameRampageInProgress ||
                   _player.AnimationPlayer.AssignedAnimation == _player.AnimationNameRampageExit;
        }

        /// <summary>
        /// Check if the rampage must be started
        /// </summary>
        public bool Enter()
        {
            if (!(_player.ActionRaycast.GetCollider() is Node node) || !node.IsInGroup(_nodeGroup)) return false;

            var rampagePoint = (RampagePoint) node;

            if (rampagePoint.Disabled) return false;

            _player.Transform = rampagePoint.Transform;
            _player.BasicMovements.MovementSpeed = _player.RampageSpeed;
            _player.CollisionShape.Extents = new Vector3(0.05f, 0.05f, 0.05f);

            _player.AnimationPlayer.Play(_player.AnimationNameRampageEnter);

            return true;
        }

        /// <summary>
        /// Manage the current rampage
        /// </summary>
        public bool CheckInProgress()
        {
            if (!IsActive()) return false;

            var value = true;

            if (_player.AnimationPlayer.AssignedAnimation == _player.AnimationNameRampageEnter &&
                !_player.AnimationPlayer.IsPlaying())
            {
                _player.AnimationPlayer.Play(_player.AnimationNameRampageInProgress);
            }
            else if (_player.AnimationPlayer.AssignedAnimation == _player.AnimationNameRampageInProgress)
            {
                if (_player.AnimationPlayer.IsPlaying())
                {
                    _player.MoveAndSlide(
                        _player.GlobalTransform.basis.z * _player.BasicMovements.MovementSpeed,
                        Vector3.Up
                    );
                }
                else
                {
                    _player.AnimationPlayer.Play(_player.AnimationNameRampageInProgress);
                }

                if (_player.ActionRaycast.GetCollider() is Node node && node.IsInGroup(_nodeGroup))
                {
                    var rampagePoint = (RampagePoint) node;

                    _player.Transform = rampagePoint.Transform;

                    _player.AnimationPlayer.Play(_player.AnimationNameRampageExit);

                    _player.RotateY(3.14159f);
                }
            }
            else if (_player.AnimationPlayer.AssignedAnimation == _player.AnimationNameRampageExit &&
                     !_player.AnimationPlayer.IsPlaying())
            {
                End();
                value = false;
            }

            return value;
        }

        /// <summary>
        /// End the current rampage
        /// </summary>
        private void End()
        {
            _player.BasicMovements.MovementSpeed = _player.WalkSpeed;
            _player.CollisionShape.Extents = new Vector3(0.4f, 0.95f, 0.4f);

            _player.MoveAndSlide(_player.GlobalTransform.basis.z, Vector3.Up);
        }
    }
}