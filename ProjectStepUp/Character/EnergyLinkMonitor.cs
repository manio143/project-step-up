using Stride.Engine;
using Stride.Particles.Components;

namespace ProjectStepUp.Character
{
    public class EnergyLinkMonitor : SyncScript
    {
        public CharacterController HeavyCharacter { get; set; }

        public CharacterController LightCharacter { get; set; }

        public ParticleSystemComponent Link { get; set; }

        public float MaxDistance { get; set; }


        public override void Update()
        {
            var apart = (LightCharacter.Entity.Transform.Position - HeavyCharacter.Entity.Transform.Position).Length() > MaxDistance;
            var state = apart ? CharacterLinkState.NotLinked : CharacterLinkState.Linked;
            
            HeavyCharacter.LinkState = state;
            LightCharacter.LinkState = state;

            Link.ParticleSystem.Emitters[0].Spawners[0].Enabled = !apart;
        }
    }
}
