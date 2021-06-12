using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Engine.Events;
using Stride.Animations;
using Stride.Core.Collections;

namespace ProjectStepUp
{
    public class DoorOpen : AsyncScript
    {
        // Declared public member fields and properties will show in the game studio
        private AnimationComponent animC;
        public override async Task Execute()
        {
            var doorEv = new EventReceiver<string>(LevelEventManager.OpenDoor);
            var parent = Entity.GetParent();
            animC = Entity.GetOrCreate<AnimationComponent>();
            var clip = new AnimationClip{Duration = TimeSpan.FromSeconds(0.4)};
            var positions = new List<Vector3>
            {
                Entity.Transform.Position,
                Entity.Transform.Position + 2 * Vector3.UnitY * 0.2f,
                Entity.Transform.Position + 2 * Vector3.UnitY * 0.8f,                
                Entity.Transform.Position + 2 * Vector3.UnitY
            };
            clip.RepeatMode = AnimationRepeatMode.PlayOnceHold;
            clip.AddCurve(
                "[TransformComponent.Key].Position",
                CreatePositionCurve(positions,0.4)
            );
            clip.Optimize();
            animC.Animations.Add("MovePlatform",  clip);
            while(Game.IsRunning)
            {
                // Do stuff every new frame
                var door = await doorEv.ReceiveAsync();
                if(door == Entity.GetParent().Name)
                {
                    StartAnimation();
                }
            }
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
                    positions.Select((x,i) => CreateKF(x, duration * i / positions.Count)).ToList()
                )
            };
        }
        public static KeyFrameData<T> CreateKF<T>(T data, double time)
        {
            return new KeyFrameData<T>(CompressedTimeSpan.FromSeconds(time),data);
        }
    }
}
