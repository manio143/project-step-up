using Stride.Engine;
using Stride.Input;

namespace ProjectStepUp
{
    public class DebugLevelLoader : SyncScript
    {
        public override void Update()
        {
#if DEBUG
            if (Input.IsKeyPressed(Keys.D0)) LevelSceneManager.LoadLevelRequest.Broadcast(0);
            if (Input.IsKeyPressed(Keys.D1)) LevelSceneManager.LoadLevelRequest.Broadcast(1);
            if (Input.IsKeyPressed(Keys.D2)) LevelSceneManager.LoadLevelRequest.Broadcast(2);
            if (Input.IsKeyPressed(Keys.D3)) LevelSceneManager.LoadLevelRequest.Broadcast(3);
            if (Input.IsKeyPressed(Keys.D4)) LevelSceneManager.LoadLevelRequest.Broadcast(4);
            if (Input.IsKeyPressed(Keys.D5)) LevelSceneManager.LoadLevelRequest.Broadcast(5);
            if (Input.IsKeyPressed(Keys.D6)) LevelSceneManager.LoadLevelRequest.Broadcast(6);
            if (Input.IsKeyPressed(Keys.D7)) LevelSceneManager.LoadLevelRequest.Broadcast(7);
            if (Input.IsKeyPressed(Keys.D8)) LevelSceneManager.LoadLevelRequest.Broadcast(8);
            if (Input.IsKeyPressed(Keys.D9)) LevelSceneManager.LoadLevelRequest.Broadcast(9);
            if (Input.IsKeyPressed(Keys.Delete)) LevelSceneManager.ClearEvent.Broadcast();
#endif
        }
    }
}
