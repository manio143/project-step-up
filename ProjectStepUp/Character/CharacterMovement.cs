using System;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Physics;

namespace ProjectStepUp.Character
{
    [DataContract]
    public class CharacterMovement
    {
        public const float MovementEpsilon = 0.0001f;
        private const float StepUpBase = 16f;

        private int movementStepUpRight = 0;
        private int movementStepUpLeft = 0;

        public float MaxWalkVelocity { get; set; }

        [DataMemberIgnore]
        public CharacterComponent PhysicsCharacter { get; set; }
        
        [DataMemberIgnore]
        public float Velocity { get; set; }

        public CharacterMovementDirection Direction
            => Velocity > MovementEpsilon ? CharacterMovementDirection.Right
             : Velocity < MovementEpsilon ? CharacterMovementDirection.Left
             : CharacterMovementDirection.None;

        public void Jump()
        {
            if (PhysicsCharacter.IsGrounded)
            {
                PhysicsCharacter.Jump();
            }
        }

        public void Move(CharacterMovementDirection direction)
        {
            ApplySmoothMovementStepUp(direction);

            Velocity = MathUtil.SmoothStep(movementStepUpLeft / StepUpBase) * (-MaxWalkVelocity)
                + MathUtil.SmoothStep(movementStepUpRight / StepUpBase) * MaxWalkVelocity;

            PhysicsCharacter.SetVelocity(new Vector3(Velocity, 0, 0));
        }

        private void ApplySmoothMovementStepUp(CharacterMovementDirection direction)
        {
            // We want the movement to start slowly and increase over a few frames.
            // We start with left and right at 0.
            // Moving right increases the right step up and decreases the left step up. Same with left.
            // Stopping decreases either.

            if (direction == CharacterMovementDirection.None)
            {
                movementStepUpLeft = Math.Max(0, movementStepUpLeft - 1);
                movementStepUpRight = Math.Max(0, movementStepUpRight - 1);
            }
            else if (direction == CharacterMovementDirection.Left)
            {
                movementStepUpLeft = Math.Min((int)StepUpBase, movementStepUpLeft + 1);
                movementStepUpRight = Math.Max(0, movementStepUpRight - 1);
            }
            else if (direction == CharacterMovementDirection.Right)
            {
                movementStepUpLeft = Math.Max(0, movementStepUpLeft - 1);
                movementStepUpRight = Math.Min((int)StepUpBase, movementStepUpRight + 1);
            }
        }
    }
}
