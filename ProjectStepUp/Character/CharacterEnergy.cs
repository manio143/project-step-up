using Stride.Core;
using Stride.Core.Annotations;
using Stride.Engine.Events;

namespace ProjectStepUp.Character
{
    [DataContract]
    public class CharacterEnergy
    {
        public const float MAX_ENERGY = 100;
        public const float MIN_ENERGY = 0;

        public static EventKey<EnergyType> NoEnergy = new("General", nameof(NoEnergy));

        public EnergyType Type { get; set; }

        [DataMemberRange(MIN_ENERGY, MAX_ENERGY, 5, 20, 0)]
        public float Value { get; set; } = MAX_ENERGY;
    }
}
