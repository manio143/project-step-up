using System.Threading.Tasks;
using Stride.Engine;

namespace ProjectStepUp
{
    public class NextLevelTrigger : AsyncScript
    {
        public int NextLevelIndex { get; set; }

        public override async Task Execute()
        {
            var physics = Entity.Get<PhysicsComponent>();

            _ = await physics.NewCollision();

            LevelSceneManager.LoadLevelRequest.Broadcast(NextLevelIndex);
        }
    }
}
