using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectStepUp.Character;
using ProjectStepUp.UI;
using Stride.Core;
using Stride.Engine;
using Stride.Engine.Events;
using Stride.Physics;
using InputMgr = ProjectStepUp.Input.InputManager;

namespace ProjectStepUp
{
    public class LevelSceneManager : StartupScript
    {
        public static EventKey<int> LoadLevelRequest = new EventKey<int>("LevelScene", nameof(LoadLevelRequest));
        public static EventKey ClearEvent = new EventKey("LevelScene", nameof(ClearEvent));
        public static EventKey ResetLevelEvent = new EventKey("LevelScene", nameof(ResetLevelEvent));

        private Level? currentLevel;
        private Entity currentLevelParent;
        private List<Entity> globalEntities;

        private Entity heavyCharacter;
        private Entity lightCharacter;

        private LevelUIUpdater levelUIUpdater;
        private InputMgr inputManager;

        public Prefab GlobalEntities { get; set; }

        public List<Level> Levels { get; set; } = new();

        public override void Start()
        {
            globalEntities = GlobalEntities.Instantiate();

            heavyCharacter = globalEntities.First(e => e.Get<CharacterController>()?.Energy.Type == EnergyType.Heavy);
            lightCharacter = globalEntities.First(e => e.Get<CharacterController>()?.Energy.Type == EnergyType.Light);
            levelUIUpdater = globalEntities.First(e => e.Get<LevelUIUpdater>() is not null).Get<LevelUIUpdater>();
            inputManager = globalEntities.First(e => e.Get<InputMgr>() is not null).Get<InputMgr>();

            Entity.Scene.Entities.AddRange(globalEntities);

            Script.AddTask(LoadNewLevel);
            Script.AddTask(Clear);
            Script.AddTask(OnFailure);
            Script.AddTask(ReloadLevel);
        }

        public async Task LoadNewLevel()
        {
            var loadLevelReceiver = new EventReceiver<int>(LoadLevelRequest);
            while (Game.IsRunning)
            {
                var levelIndex = await loadLevelReceiver.ReceiveAsync();

                var newLevel = Levels[levelIndex];
                var newLevelEntity = new Entity($"Level {levelIndex} '{newLevel.Name}'");

                foreach (var entity in newLevel.Entities.Instantiate())
                {
                    newLevelEntity.AddChild(entity);
                }

                Entity.Scene.Entities.Add(newLevelEntity);

                if (currentLevel is not null)
                {
                    // TODO: peform the transition, such that at the end new level is at 0,0,0

                    Entity.Scene.Entities.Remove(currentLevelParent);
                }

                currentLevel = newLevel;
                currentLevelParent = newLevelEntity;

                levelUIUpdater.SetLevelName(newLevel.Name);

                ResetCharacters();
            }
        }

        private void ResetCharacters()
        {
            var heavyStartingPoint = currentLevelParent.GetChildren()
                                .First(e => e.Get<CharacterPlaceholder>()?.Type == EnergyType.Heavy)
                                .Transform.Position; // because level parent is at 0,0,0 this position is same as in world space
            var lightStartingPoint = currentLevelParent.GetChildren()
                .First(e => e.Get<CharacterPlaceholder>()?.Type == EnergyType.Light)
                .Transform.Position;

            heavyCharacter.Get<CharacterController>().Energy.Value = CharacterEnergy.MAX_ENERGY;
            lightCharacter.Get<CharacterController>().Energy.Value = CharacterEnergy.MAX_ENERGY;

            heavyCharacter.Get<CharacterComponent>().Teleport(heavyStartingPoint);
            lightCharacter.Get<CharacterComponent>().Teleport(lightStartingPoint);
            
            inputManager.Enabled = true;
        }

        public async Task ReloadLevel()
        {
            var reloadReceiver = new EventReceiver(ResetLevelEvent);
            while (Game.IsRunning)
            {
                await reloadReceiver.ReceiveAsync();

                if (currentLevel == null) continue;

                // remove level entities
                foreach (var child in currentLevelParent.GetChildren())
                    child.Scene = null;

                // and add them back with fresh state
                foreach (var entity in currentLevel.Value.Entities.Instantiate())
                    currentLevelParent.AddChild(entity);

                ResetCharacters();
            }
        }

        public async Task Clear()
        {
            var clearReceiver = new EventReceiver(ClearEvent);
            await clearReceiver.ReceiveAsync();

            if (currentLevel is not null)
            {
                Entity.Scene.Entities.Remove(currentLevelParent);
            }

            foreach (var entity in globalEntities)
            {
                Entity.Scene.Entities.Remove(entity);
            }
        }

        public async Task OnFailure()
        {
            var failReceiver = new EventReceiver<EnergyType>(CharacterEnergy.NoEnergy);

            while (Game.IsRunning)
            {
                _ = await failReceiver.ReceiveAsync();

                inputManager.Enabled = false;
                levelUIUpdater.ShowFailureModal();
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
