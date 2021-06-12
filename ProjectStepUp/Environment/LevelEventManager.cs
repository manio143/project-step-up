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
    public partial class LevelEventManager : AsyncScript
    {
        public List<string> EntityTriggerNames = new();
        public string DoorName = "";
        protected Dictionary<string,bool> conditions = new();
        public static readonly EventKey<string> OpenDoor = new("General");
        public override async Task Execute()
        {
            AddConditions();
            Script.AddTask(ListenForTriggerButtons);

            while (Game.IsRunning)
            {
                if (conditions.Values.All(x => x))
                {
                    OpenDoor.Broadcast(DoorName);
                    break;
                }

                await Script.NextFrame();
            }
        }

        public async Task ListenForTriggerButtons()
        {
            var receiver = new EventReceiver<(string, SwitchState)>(TriggerButton.SwitchStateChange);
            while (Game.IsRunning)
            {
                var (name, state) = await receiver.ReceiveAsync();
                if (conditions.Keys.Contains(name))
                {
                    conditions[name] = (state == SwitchState.ON);
                }
            }
        }


        public void AddConditions() {
            EntityTriggerNames.ForEach(
                x => conditions.Add(x,false)
            );
        }
    }
}
