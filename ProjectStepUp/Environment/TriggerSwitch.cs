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
    public class TriggerSwitch : SyncScript
    {
        public static readonly EventKey<(string, SwitchState)> SwitchStateChange = new("General");
        public static readonly EventKey SwitchToggle = new("General", "SwitchToggle");

        private StaticColliderComponent sc;
        private Entity lever;
        private SwitchState state = SwitchState.OFF;
        private EventReceiver toggleEvent = new EventReceiver(SwitchToggle);
        private bool StateBool {get {return state == SwitchState.ON;}}

        public override void Start()
        {
            sc = Entity.Get<StaticColliderComponent>();
            lever = Entity.GetChild(0);
        }

        public override void Update()
        {
            if (sc.Collisions.Any(x => x.ColliderA is CharacterComponent || x.ColliderB is CharacterComponent))
            {
                if (toggleEvent.TryReceive())
                {
                    state = state == SwitchState.ON ? SwitchState.OFF : SwitchState.OFF;
                    SwitchStateChange.Broadcast((Entity.GetParent()?.Name,state));
                    LevelEventManager.ConditionMet.Broadcast(Entity.GetParent()?.Name);
                    lever.Transform.Rotation *= Quaternion.RotationZ((int)state * 15);
                }
            }
        }
    }
}
