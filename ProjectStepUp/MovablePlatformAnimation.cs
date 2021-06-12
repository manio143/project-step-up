using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Animations;
using Stride.Core.Collections;

namespace ProjectStepUp
{
    public class MovablePlatformAnimation : StartupScript
    {
        public readonly List<Vector3> Positions = new();
        public double Duration;
        private AnimationComponent animC;

        public override void Start()
        {
            Positions.Add(Positions[0]);
            animC = Entity.GetParent().GetOrCreate<AnimationComponent>();
            var clip = new AnimationClip{Duration = TimeSpan.FromSeconds(Duration)};

            clip.RepeatMode = AnimationRepeatMode.LoopInfinite;
            clip.AddCurve(
                "[TransformComponent.Key].Position",
                CreatePositionCurve(Positions,Duration)
            );
            animC.Animations.Add("MovePlatform",  clip);
            StartAnimation();
        }

        public void StartAnimation()
        {
            animC.Play("MovePlatform");
        }
        public void PauseAnimation()
        {
            animC.PlayingAnimations[0].TimeFactor = 0;
        }

        public void ContinueAnimation()
        {
            animC.PlayingAnimations[0].TimeFactor = 1;
        }
        public static AnimationCurve<Vector3> CreatePositionCurve(List<Vector3> positions, double duration)
        {
            return new AnimationCurve<Vector3>
            {
                InterpolationType = AnimationCurveInterpolationType.Linear,
                KeyFrames = new FastList<KeyFrameData<Vector3>>
                (
                    positions.Select((x,i) => CreateKF(x, duration * i/ positions.Count)).ToList()
                )
            };
        }
        public static KeyFrameData<T> CreateKF<T>(T data, double time)
        {
            return new KeyFrameData<T>(CompressedTimeSpan.FromSeconds(time),data);
        }
    }
}
