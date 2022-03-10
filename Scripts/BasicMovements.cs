using System;
using Godot;

namespace ReGame.Scripts
{
    /// <summary>
    /// Represent basic movements
    /// </summary>
    public class BasicMovements
    {
        /// <summary>
        /// The managed player
        /// </summary>
        private readonly Player _player;

        /// <summary>
        /// The current movement speed
        /// </summary>
        public float MovementSpeed;

        /// <summary>
        /// Create a basic movements manager
        /// </summary>
        /// <param name="player">The manager player</param>
        public BasicMovements(Player player)
        {
            _player = player;

            MovementSpeed = player.WalkSpeed;
        }

        /// <summary>
        /// Perform a move from user inputs
        /// <returns>TRUE if a movement has been performed, FALSE otherwise</returns>
        /// </summary>
        public bool MoveFromInputs()
        {
            var movement = false;

            if (_player.InputManager.GetStrength(_player.MoveForwardInput) > 0)
            {
                MovementSpeed = _player.InputManager.IsPressed(_player.RunInput) ? _player.RunSpeed : _player.WalkSpeed;

                if (IsWalking())
                {
                    _player.AnimationPlayer.Play(_player.AnimationNameWalk);
                }
                else if (IsRunning())
                {
                    _player.AnimationPlayer.Play(_player.AnimationNameRun);
                }

                _player.MoveAndSlide(_player.GlobalTransform.basis.z * MovementSpeed, Vector3.Up);

                movement = true;
            }

            if (_player.InputManager.GetStrength(_player.MoveBackwardInput) > 0)
            {
                _player.MoveAndSlide(-(_player.GlobalTransform.basis.z * _player.BackwardSpeed), Vector3.Up);

                movement = true;
            }

            return movement;
        }

        /// <summary>
        /// Perform a rotation from user inputs
        /// </summary>
        public void RotateFromInputs()
        {
            var leftValue = _player.InputManager.GetStrength(_player.TurnLeftInput);
            var rightValue = _player.InputManager.GetStrength(_player.TurnRightInput);

            if (leftValue > 0)
            {
                _player.RotateY(leftValue * _player.TurnSpeed);
            }
            else if (rightValue > 0)
            {
                _player.RotateY(-rightValue * _player.TurnSpeed);
            }
        }

        /// <summary>
        /// Check if the player walking
        /// </summary>
        /// <returns>TRUE if walking, FALSE otherwise</returns>
        private bool IsWalking()
        {
            return Math.Abs(MovementSpeed - _player.WalkSpeed) < 0.01;
        }

        /// <summary>
        /// Check if the player running
        /// </summary>
        /// <returns>TRUE if running, FALSE otherwise</returns>
        private bool IsRunning()
        {
            return Math.Abs(MovementSpeed - _player.RunSpeed) < 0.01;
        }
    }
}