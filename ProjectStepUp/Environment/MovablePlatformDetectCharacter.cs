using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Engine;
using Stride.Physics;

namespace ProjectStepUp
{
    public class MovablePlatformDetectCharacter : SyncScript
    {
        private MovablePlatformAnimation animation;
        private PhysicsComponent physics;

        public MovablePlatformDetectCharacter() => Priority = 10;

        public override void Start()
        {
            animation = Entity.Get<MovablePlatformAnimation>();
            physics = Entity.Get<PhysicsComponent>();
        }
        public override void Update()
        {
            var collisions = physics.Collisions
                .Where(x => x.ColliderA is CharacterComponent || x.ColliderB is CharacterComponent)
                .Select(x => x.ColliderA is CharacterComponent ? x.ColliderA : x.ColliderB)
                .ToList();
            if (collisions.Count > 0 &&
                collisions.Any(ch => ch.Entity.Transform.Position.Y < this.Entity.Transform.Position.Y))
            {
                animation.PauseAnimation();
            }
            else
            {
                animation.ContinueAnimation();
            }
        }
    }
}
