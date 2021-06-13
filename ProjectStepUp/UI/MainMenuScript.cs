using System;
using System.Threading.Tasks;
using Stride.Audio;
using Stride.Engine;
using Stride.Engine.Events;
using Stride.Rendering.Compositing;
using Stride.UI.Controls;
using Stride.UI.Events;

namespace ProjectStepUp.UI
{
    public class MainMenuScript : StartupScript
    {
        public static EventKey ShowMainMenu = new EventKey("General", nameof(ShowMainMenu));

        private SoundInstance menuMusicInstance;
        private UIPage page;

        public Sound Music { get; set; }

        public Scene LevelLoaderScene { get; set; }

        public CameraComponent MenuCamera { get; set; }

        public override void Start()
        {
            page = Entity.Get<UIComponent>().Page;

            (page.RootElement.FindName("StartButton") as Button).Click += StartButton_Click;
            (page.RootElement.FindName("OptionsButton") as Button).Click += OptionsButton_Click;

            menuMusicInstance = Music.CreateInstance();
            menuMusicInstance.IsLooping = true;
            menuMusicInstance.Play();

            Script.AddTask(ListenToShowMenu);
        }

        public void Reset()
        {
            Entity.Scene.Children.Remove(LevelLoaderScene);

            page.RootElement.Visibility = Stride.UI.Visibility.Visible;

            MenuCamera.Enabled = true;

            menuMusicInstance.Volume = 1;
            menuMusicInstance.Play();
        }

        private void StartButton_Click(object sender, RoutedEventArgs args)
        {
            Script.AddTask(() => this.FadeOutSound(menuMusicInstance, TimeSpan.FromSeconds(1)));
            Entity.Scene.Children.Add(LevelLoaderScene);

            MenuCamera.Enabled = false;

            page.RootElement.Visibility = Stride.UI.Visibility.Collapsed;

            Script.AddTask(async () =>
            {
                // wait for the LevelSceneManager to be fully initialized
                // - if you send a message before receiver is created it's not going to be received
                await Script.NextFrame();
                await Script.NextFrame();

                LevelSceneManager.LoadLevelRequest.Broadcast(0);
            });
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs args)
        {
        }

        private async Task ListenToShowMenu()
        {
            var receiver = new EventReceiver(ShowMainMenu);
            while(Game.IsRunning)
            {
                await receiver.ReceiveAsync();
                Reset();
            }
        }
    }
}
