using System.Collections.Generic;
using ProjectStepUp.Character;
using Stride.Engine;
using Stride.Input;

namespace ProjectStepUp.Input
{
    public class InputManager : SyncScript
    {
        public CharacterController LightCharacter { get; set; }
        public CharacterController HeavyCharacter { get; set; }

        public Dictionary<InputAction, Keys> InputConfiguration { get; set; } = new();

        public override void Update()
        {
            if(Input.IsKeyDown(InputConfiguration[InputAction.HeavyCharacterRight]))
                HeavyCharacter.Move(CharacterMovementDirection.Right);
            else if (Input.IsKeyDown(InputConfiguration[InputAction.HeavyCharacterLeft]))
                HeavyCharacter.Move(CharacterMovementDirection.Left);
            else
                HeavyCharacter.Move(CharacterMovementDirection.None);

            if (Input.IsKeyDown(InputConfiguration[InputAction.LightCharacterRight]))
                LightCharacter.Move(CharacterMovementDirection.Right);
            else if (Input.IsKeyDown(InputConfiguration[InputAction.LightCharacterLeft]))
                LightCharacter.Move(CharacterMovementDirection.Left);
            else
                LightCharacter.Move(CharacterMovementDirection.None);

            if (Input.IsKeyDown(InputConfiguration[InputAction.HeavyCharacterJump])) HeavyCharacter.Jump();
            if (Input.IsKeyDown(InputConfiguration[InputAction.LightCharacterJump])) LightCharacter.Jump();

            if (Input.IsKeyPressed(InputConfiguration[InputAction.ToggleSwitch]))
                TriggerSwitch.SwitchToggle.Broadcast();
        }
    }
}
