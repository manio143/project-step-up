using ProjectStepUp.Character;
using ProjectStepUp.Link;
using Stride.Engine;
using Stride.Input;

namespace ProjectStepUp
{
    public class InputManager : SyncScript
    {
        public CharacterController LightCharacter { get; set; }
        public CharacterController HeavyCharacter { get; set; }
        public LinkManager LinkManager { get; set; }

        public override void Update()
        {
            // TODO: allow customization
            if(Input.IsKeyDown(Keys.L))
            {
                HeavyCharacter.Move(CharacterMovementDirection.Right);
            }
            else if (Input.IsKeyDown(Keys.J))
            {
                HeavyCharacter.Move(CharacterMovementDirection.Left);
            }
            else
            {
                HeavyCharacter.Move(CharacterMovementDirection.None);
            }

            if (Input.IsKeyDown(Keys.D))
            {
                LightCharacter.Move(CharacterMovementDirection.Right);
            }
            else if (Input.IsKeyDown(Keys.A))
            {
                LightCharacter.Move(CharacterMovementDirection.Left);
            }
            else
            {
                LightCharacter.Move(CharacterMovementDirection.None);
            }

            if (Input.IsKeyDown(Keys.I)) HeavyCharacter.Jump();
            if (Input.IsKeyDown(Keys.W)) LightCharacter.Jump();

            if (Input.IsKeyPressed(Keys.B)) LinkManager.ToggleConnect();
        }
    }
}
