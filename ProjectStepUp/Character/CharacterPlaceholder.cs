using Stride.Core;
using Stride.Engine;

namespace ProjectStepUp.Character
{
    [DataContract]
    public class CharacterPlaceholder : EntityComponent
    {
        public EnergyType Type { get; set; }
    }
}
