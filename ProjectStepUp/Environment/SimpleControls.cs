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
    public class SimpleControls : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        private CharacterComponent c;
        public override void Start()
        {
            // Initialization of the script.
            c = Entity.Get<CharacterComponent>();
        }

        public override void Update()
        {
            // Do stuff every new frame
            var velocity = Vector3.Zero;
            if(Input.IsKeyDown(Keys.Right))
                velocity += Vector3.UnitX * 5;
            if(Input.IsKeyDown(Keys.Left))
                velocity -= Vector3.UnitX * 5;
            c.SetVelocity(velocity);
            if(Input.IsKeyPressed(Keys.Space))
                c.Jump(Vector3.UnitY * 5);
            if(Input.IsKeyPressed(Keys.T))
                c.Teleport(Vector3.UnitY);

        }
    }
}
