using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Engine;

namespace ProjectStepUp
{
    public class AnimationTest : StartupScript
    {
        public string AnimationName { get; set; }
        public override void Start()
        {
            Entity.Get<AnimationComponent>().Play(AnimationName);
        }
    }
}
