using System;
using ProjectStepUp.Character;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;
using Stride.UI.Events;

namespace ProjectStepUp.UI
{
    public class LevelUIUpdater : SyncScript
    {
        private Slider lightSlider;
        private Slider heavySlider;
        private TextBlock lightEnergyText;
        private TextBlock heavyEnergyText;
        private TextBlock levelNameLabel;
        private ModalElement failureModal;

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
            failureModal = page.RootElement.FindName("FailureModal") as ModalElement;

            (page.RootElement.FindName("RetryButton") as Button).Click += RetryLevelButton_Click;
            (page.RootElement.FindName("BackToMenuButton") as Button).Click += BackToMenuButton_Click;
        }
        public override void Update()
        {
            lightSlider.Value = LightCharacter.Energy.Value;
            heavySlider.Value = HeavyCharacter.Energy.Value;
            lightEnergyText.Text = Math.Round(LightCharacter.Energy.Value).ToString("0");
            heavyEnergyText.Text = Math.Round(HeavyCharacter.Energy.Value).ToString("0");
        }

        public void SetLevelName(string name) => levelNameLabel.Text = name;

        public void ShowFailureModal() => failureModal.Visibility = Visibility.Visible;

        private void RetryLevelButton_Click(object sender, RoutedEventArgs args)
        {
            failureModal.Visibility = Visibility.Hidden;
            LevelSceneManager.ResetLevelEvent.Broadcast();
        }

        private void BackToMenuButton_Click(object sender, RoutedEventArgs args)
        {
            failureModal.Visibility = Visibility.Hidden;
            LevelSceneManager.ClearEvent.Broadcast();
            MainMenuScript.ShowMainMenu.Broadcast();
        }
    }
}
