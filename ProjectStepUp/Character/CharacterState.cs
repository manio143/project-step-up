using System;

namespace ProjectStepUp.Character
{
    [Flags]
    public enum CharacterState
    {
        StandBy = 1,

        Walking = 2,

        Jumping = 4,

        Linked = 16,
    }
}
