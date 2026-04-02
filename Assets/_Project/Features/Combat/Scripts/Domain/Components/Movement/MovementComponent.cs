using UnityEngine;

namespace Features.Combat
{
    public class MovementComponent
    {
        public Vector3 CurrentPosition { get; private set; } 
        public Vector3 CurrentRotation { get; private set; }
        private readonly float _speed;

        public MovementComponent(float speed)
        {
            _speed = speed;
        }

        public void MoveTo(Vector3 direction, float deltaTime)
        {
            CurrentPosition += _speed * deltaTime * direction;
            UpdateFacingDirection(direction);
        }

        private void UpdateFacingDirection(Vector3 direction)
        {
            if (direction.x >= 0)
            {
                CurrentRotation = Vector3.zero;
            }
            else if (direction.x < 0)
            {
                CurrentRotation = new(0, 180, 0);
            }
        }

        public void SetCurrentPosition(Vector3 newPosition)
        {
            CurrentPosition = newPosition;   
        }

        public void Reset(Vector3 newPosition)
        {
            CurrentPosition = newPosition;
        }
    }
}
