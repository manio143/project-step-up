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
    public class TriggerSwitch : AsyncScript
    {
        // Declared public member fields and properties will show in the game studio

        public override async Task Execute()
        {
            var rbc = Entity.Get<StaticColliderComponent>();
            var lever = Entity.GetChild(0);
            int state = -1;
            while(Game.IsRunning)
            {
                // Do stuff every new frame
                var c = await rbc.NewCollision();
                if(c.ColliderA is CharacterComponent || c.ColliderB is CharacterComponent)
                {
                    if(Input.IsKeyPressed(Keys.U))
                    {
                        state *= -1;
                        lever.Transform.Rotation *= Quaternion.RotationZ(state * 60);
                    }    
                }
            }
        }
    }
}
