using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace ProjectStepUp
{
    public class CameraPositionScript : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        public Entity Robot1;
        public Entity Robot2;
        
        public override void Start()
        {
            Entity.Transform.Position = (Robot1.Transform.Position + Robot2.Transform.Position)/2;
        }

        public override void Update()
        {
            var pos1 = Robot1.Transform.Position;
            var pos2 = Robot2.Transform.Position;
            Entity.Transform.Position = (pos1 + pos2)/2;
            Entity.Transform.Position.Y += 1;
            Entity.Transform.Position.Z = Math.Max((pos2 - pos1).Length(),10);
        }
    }
}
