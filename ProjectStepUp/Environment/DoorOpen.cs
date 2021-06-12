using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Engine.Events;

namespace ProjectStepUp
{
    public class DoorOpen : AsyncScript
    {
        // Declared public member fields and properties will show in the game studio

        public override async Task Execute()
        {
            var doorEv = new EventReceiver<string>(LevelEventManager.OpenDoor);
            while(Game.IsRunning)
            {
                // Do stuff every new frame
                var door = await doorEv.ReceiveAsync();
                if(door == Entity.GetParent().Name)
                {
                    Entity.Transform.Position.Y += 2;   
                }
                break;
            }
        }
    }
}
