using Stride.Core;
using Stride.Core.Annotations;

namespace ProjectStepUp.Character
{
    [DataContract]
    public class CharacterEnergy
    {
        public const float MAX_ENERGY = 100;
        public const float MIN_ENERGY = 0;

        public EnergyType Type { get; set; }

        [DataMemberRange(MIN_ENERGY, MAX_ENERGY, 5, 20, 0)]
        public float Value { get; set; }
    }
}
