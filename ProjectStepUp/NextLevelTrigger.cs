using System.Threading.Tasks;
using ProjectStepUp.Character;
using Stride.Engine;
using Stride.Physics;

namespace ProjectStepUp
{
    public class NextLevelTrigger : AsyncScript
    {
        private bool lightPassed;
        private bool heavyPassed;
        public int NextLevelIndex { get; set; }

        public override async Task Execute()
        {
            var physics = Entity.Get<PhysicsComponent>();

            do
            {
                var collision = await physics.NewCollision();
                var character = collision.ColliderA is CharacterComponent ? collision.ColliderA.Entity : collision.ColliderB.Entity;
                var energyType = character.Get<CharacterController>().Energy.Type;

                if (energyType == EnergyType.Light) lightPassed = true;
                if (energyType == EnergyType.Heavy) heavyPassed = true;

                if (Check())
                {
                    LevelSceneManager.LoadLevelRequest.Broadcast(NextLevelIndex);
                }
            } while (!Check());
        }

        private bool Check() => lightPassed && heavyPassed;
    }
}
