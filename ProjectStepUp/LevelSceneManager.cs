using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectStepUp.Character;
using Stride.Core;
using Stride.Engine;
using Stride.Engine.Events;
using Stride.Physics;

namespace ProjectStepUp
{
    public class LevelSceneManager : StartupScript
    {
        public static EventKey<int> LoadLevelRequest = new EventKey<int>("LevelScene", nameof(LoadLevelRequest));
        public static EventKey ClearEvent = new EventKey("LevelScene", nameof(ClearEvent));

        private Level? currentLevel;
        private Entity currentLevelParent;
        private List<Entity> globalEntities;

        private Entity heavyCharacter;
        private Entity lightCharacter;

        public Prefab GlobalEntities { get; set; }

        public List<Level> Levels { get; set; } = new();

        public override void Start()
        {
            globalEntities = GlobalEntities.Instantiate();

            heavyCharacter = globalEntities.First(e => e.Get<CharacterController>()?.Energy.Type == EnergyType.Heavy);
            lightCharacter = globalEntities.First(e => e.Get<CharacterController>()?.Energy.Type == EnergyType.Light);

            Entity.Scene.Entities.AddRange(globalEntities);

            Script.AddTask(LoadNewLevel);
            Script.AddTask(Clear);
        }

        public async Task LoadNewLevel()
        {
            while (Game.IsRunning)
            {
                var loadLevelReceiver = new EventReceiver<int>(LoadLevelRequest);
                var levelIndex = await loadLevelReceiver.ReceiveAsync();

                var newLevel = Levels[levelIndex];
                var newLevelEntity = new Entity($"Level {levelIndex} '{newLevel.Name}'");

                foreach (var entity in newLevel.Entities.Instantiate())
                {
                    newLevelEntity.AddChild(entity);
                }

                if (currentLevel is not null)
                {
                    // TODO: peform the transition, such that at the end new level is at 0,0,0

                    Entity.Scene.Entities.Remove(currentLevelParent);
                }

                currentLevel = newLevel;
                currentLevelParent = newLevelEntity;
                Entity.Scene.Entities.Add(newLevelEntity);

                var heavyStartingPoint = currentLevelParent.GetChildren()
                    .First(e => e.Get<CharacterPlaceholder>()?.Type == EnergyType.Heavy)
                    .Transform.Position; // because level parent is at 0,0,0 this position is same as in world space
                var lightStartingPoint = currentLevelParent.GetChildren()
                    .First(e => e.Get<CharacterPlaceholder>()?.Type == EnergyType.Light)
                    .Transform.Position;

                heavyCharacter.Get<CharacterComponent>().Teleport(heavyStartingPoint);
                lightCharacter.Get<CharacterComponent>().Teleport(lightStartingPoint);
            }
        }

        public async Task Clear()
        {
            var clearReceiver = new EventReceiver(ClearEvent);
            await clearReceiver.ReceiveAsync();

            if(currentLevel is not null)
            {
                Entity.Scene.Entities.Remove(currentLevelParent);
            }

            foreach (var entity in globalEntities)
            {
                Entity.Scene.Entities.Remove(entity);
            }
        }
    }

    [DataContract]
    public struct Level
    {
        public string Name { get; set; }
        public Prefab Entities { get; set; }
    }
}
