using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Audio;
using Stride.Engine;

namespace ProjectStepUp
{
    public static class SoundUtils
    {
        public static async Task FadeOutSound(this ScriptComponent component, SoundInstance soundInstance, TimeSpan duration)
        {
            var targetTime = duration;
            while(targetTime > TimeSpan.Zero)
            {
                await component.Script.NextFrame();
                targetTime -= component.Game.UpdateTime.Elapsed;

                soundInstance.Volume = 1.0f * targetTime.Ticks / duration.Ticks;
            }

            soundInstance.Volume = 0;
            soundInstance.Pause();
        }
    }
}
