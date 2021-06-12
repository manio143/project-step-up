using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Physics;

namespace ProjectStepUp
{
    public class TriggerButton : AsyncScript
    {
        // Declared public member fields and properties will show in the game studio

        public override async Task Execute()
        {
            var rbc = Entity.Get<RigidbodyComponent>();
            while(Game.IsRunning)
            {
                // Do stuff every new frame
                var c = await rbc.NewCollision();
                if(c.ColliderA is CharacterComponent || c.ColliderB is CharacterComponent)
                {
                    Entity.Transform.Position.Y -= 0.2f;
                    await rbc.CollisionEnded();
                    Entity.Transform.Position.Y += 0.2f;
                }
                

            }
        }
    }
}
