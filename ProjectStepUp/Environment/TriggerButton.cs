using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Physics;
using Stride.Engine.Events;

namespace ProjectStepUp
{
    public class TriggerButton : AsyncScript
    {
        public static readonly EventKey<(string,SwitchState)> SwitchStateChange = new("General");
        private SwitchState state = SwitchState.OFF;

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
                    state = SwitchState.ON;
                    SwitchStateChange.Broadcast((Entity.GetParent()?.Name, state));

                    while(await rbc.CollisionEnded() != c) {}

                    state = SwitchState.OFF;
                    SwitchStateChange.Broadcast((Entity.GetParent()?.Name, state));
                    Entity.Transform.Position.Y += 0.2f;
                }
            }
        }
    }
}
