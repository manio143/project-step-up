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
        public static readonly EventKey<string> ConditionMet = new("General");
        public override async Task Execute()
        {
            AddConditions();
            var condEv = new EventReceiver<string>(ConditionMet);
            while(Game.IsRunning)
            {
                if(conditions.Values.All(x => x))
                {
                    OpenDoor.Broadcast(DoorName);
                }
                var tmp = await condEv.ReceiveAsync();
                if(conditions.Keys.Contains(tmp))
                {
                    conditions[tmp] = true;
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
