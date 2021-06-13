using System;
using ProjectStepUp.Character;
using Stride.Engine;
using Stride.UI.Controls;

namespace ProjectStepUp.UI
{
    public class LevelUIUpdater : SyncScript
    {
        private Slider lightSlider;
        private Slider heavySlider;
        private TextBlock lightEnergyText;
        private TextBlock heavyEnergyText;
        private TextBlock levelNameLabel;

        public CharacterController LightCharacter { get; set; }
        public CharacterController HeavyCharacter { get; set; }

        public LevelUIUpdater() => Priority = 50;

        public override void Start()
        {
            var page = Entity.Get<UIComponent>().Page;

            lightSlider = page.RootElement.FindName("LightSlider") as Slider;
            heavySlider = page.RootElement.FindName("HeavySlider") as Slider;
            lightEnergyText = page.RootElement.FindName("LightTextEnergy") as TextBlock;
            heavyEnergyText = page.RootElement.FindName("HeavyTextEnergy") as TextBlock;
            levelNameLabel = page.RootElement.FindName("LevelName") as TextBlock;
        }
        public override void Update()
        {
            lightSlider.Value = LightCharacter.Energy.Value;
            heavySlider.Value = HeavyCharacter.Energy.Value;
            lightEnergyText.Text = Math.Round(LightCharacter.Energy.Value).ToString("0");
            heavyEnergyText.Text = Math.Round(HeavyCharacter.Energy.Value).ToString("0");
        }

        public void SetLevelName(string name) => levelNameLabel.Text = name;
    }
}
