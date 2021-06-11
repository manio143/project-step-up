using Stride.Core.Diagnostics;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;

namespace ProjectStepUp.Character
{
    public class CharacterController : SyncScript
    {
        private static Logger log = GlobalLogger.GetLogger(nameof(CharacterController));
        private const float EnergyDecreasePerSecond = 100 / 15;
        private const float EnergyIncreasePerSecond = 100 / 5;

        private CharacterComponent physicsCharacter;

        public CharacterState State { get; set; }

        public CharacterMovement Movement { get; set; } = new CharacterMovement();

        public CharacterEnergy Energy { get; set; } = new CharacterEnergy();

        public override void Start()
        {
            physicsCharacter = Entity.Get<CharacterComponent>();
            if (physicsCharacter == null) log.Error("CharacterController requires a CharacterComponent!");
            
            Movement.PhysicsCharacter = physicsCharacter;

            State = CharacterState.StandBy;
        }

        public override void Update()
        {
            UpdateEnergy();
        }

        public void Jump() => Movement.Jump();

        /// <summary>
        /// Calculates character movement - must be called each frame by the input manager.
        /// </summary>
        /// <param name="direction">Direction to move the player or <see cref="CharacterMovementDirection.None"/> to stop.</param>
        public void Move(CharacterMovementDirection direction) => Movement.Move(direction);

        private void UpdateEnergy()
        {
            double deltaTime = Game.UpdateTime.Elapsed.TotalSeconds;
            float newEnergyValue;
            
            if (State.HasFlag(CharacterState.Linked))
            {
                newEnergyValue = Energy.Value + (float)(deltaTime * EnergyIncreasePerSecond);
            }
            else
            {
                newEnergyValue = Energy.Value + (float)(deltaTime * EnergyDecreasePerSecond);
            }

            Energy.Value = MathUtil.Clamp(newEnergyValue, CharacterEnergy.MIN_ENERGY, CharacterEnergy.MAX_ENERGY);
        }
    }
}
