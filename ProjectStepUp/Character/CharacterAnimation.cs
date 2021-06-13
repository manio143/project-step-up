using System;
using Stride.Animations;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Rendering;

namespace ProjectStepUp.Character
{
    [DataContract]
    public class CharacterAnimation
    {
       //private bool generated;
        public TimeSpan AnimationCrossfadeTime { get; set; }

        public AnimationComponent Animation { get; set; }

        //public bool GenerateColorAnimations { get; set; }

        public void Update(CharacterMovementState state, CharacterMovementDirection direction)
        {
            //if (!generated && GenerateColorAnimations) GenerateColorAnimationsInternal();

            if (direction != CharacterMovementDirection.None && state != CharacterMovementState.StandBy)
            {
                Animation.Entity.Transform.Rotation = Quaternion.RotationY(-MathUtil.PiOverTwo + (MathUtil.PiOverTwo - 0.25f)* (direction == CharacterMovementDirection.Right ? 1 : -1));
            }

            if (state == CharacterMovementState.StandBy)
            {
                Animation.Crossfade("Idle", AnimationCrossfadeTime / 2);
            }
            else if (state == CharacterMovementState.Walking)
            {
                Animation.Crossfade("Run", AnimationCrossfadeTime / 2);
            }
            else if (state == CharacterMovementState.Jumping)
            {
                Animation.Crossfade("Jump", AnimationCrossfadeTime / 2);
            }
        }
        /*
        private void GenerateColorAnimationsInternal()
        {
            var clip = new AnimationClip { Duration = TimeSpan.FromSeconds(1) };
            "[ModelComponent.Key].Materials[0].Passes[0].DiffuseColor"
        }

        private AnimationCurve MakeColor(Color color) => new KeyFrameData<T>((CompressedTimeSpan)TimeSpan.Zero, value);
        */
    }
}
