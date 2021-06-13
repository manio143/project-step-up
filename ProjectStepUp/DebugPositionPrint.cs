using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Engine;

namespace ProjectStepUp
{
    public class DebugPositionPrint : SyncScript
    {
        public int PrintLinePosition;
        public override void Update()
        {
            DebugText.Print($"Position: {Entity.Transform.Position}", new Stride.Core.Mathematics.Int2(10, PrintLinePosition));
        }
    }
}
