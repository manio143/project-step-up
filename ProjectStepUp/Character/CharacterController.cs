using System;
using Stride.Core.Diagnostics;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;

namespace ProjectStepUp.Character
{
    public class CharacterController : SyncScript
    {
        private static Logger log = GlobalLogger.GetLogger(nameof(CharacterController));
        private const float EnergyDecreasePerSecond = 100 / 10;
        private const float EnergyIncreasePerSecond = 100 / 5;

        private CharacterComponent physicsCharacter;

        public CharacterMovementState MovementState { get; set; }

        public CharacterLinkState LinkState { get; set; }

        public CharacterMovement Movement { get; set; } = new CharacterMovement();

        public CharacterAnimation Animation { get; set; } = new CharacterAnimation();

        public CharacterEnergy Energy { get; set; } = new CharacterEnergy();

        public override void Start()
        {
            physicsCharacter = Entity.Get<CharacterComponent>();
            if (physicsCharacter == null) log.Error("CharacterController requires a CharacterComponent!");

            Movement.PhysicsCharacter = physicsCharacter;
        }

        public override void Update()
        {
            UpdateEnergy();
            UpdateMovementState();
            UpdateAnimation();

            DebugText.Print($"Energy: {Energy.Value:0}", new Int2(10, 10));
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
            float previousEnergyValue = Energy.Value;
            float newEnergyValue;

            if (LinkState == CharacterLinkState.Linked)
            {
                newEnergyValue = previousEnergyValue + (float)(deltaTime * EnergyIncreasePerSecond);
            }
            else
            {
                newEnergyValue = previousEnergyValue - (float)(deltaTime * EnergyDecreasePerSecond);
            }

            Energy.Value = MathUtil.Clamp(newEnergyValue, CharacterEnergy.MIN_ENERGY, CharacterEnergy.MAX_ENERGY);
            
            if (Energy.Value == CharacterEnergy.MIN_ENERGY && Energy.Value != previousEnergyValue)
            {
                // there was a change in energy levels and now we're at zero
                CharacterEnergy.NoEnergy.Broadcast(Energy.Type);
            }
        }

        private void UpdateMovementState()
        {
            if (!Movement.PhysicsCharacter.IsGrounded)
            {
                MovementState = CharacterMovementState.Jumping;
            }
            else if (MathF.Abs(Movement.Velocity) < CharacterMovement.MovementEpsilon)
            {
                MovementState = CharacterMovementState.StandBy;
            }
            else
            {
                MovementState = CharacterMovementState.Walking;
            }
        }

        private void UpdateAnimation() => Animation.Update(MovementState, Movement.Direction);
    }
}
