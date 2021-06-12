using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Physics;

namespace ConstraintsDemo
{
    public class Thrower : SyncScript
    {
        public Prefab bullet;
        public float force;
        private CameraComponent camera;

        public override void Start()
        {
            camera = this.Entity.Get<CameraComponent>();
        }

        public override void Update()
        {
            if (Input.Mouse.IsButtonPressed(MouseButton.Left))
            {
                var b = bullet.Instantiate()[0];
                b.Transform.Position = Entity.Transform.Position;
                Entity.Scene.Entities.Add(b);
                b.Get<RigidbodyComponent>().ApplyForce(Matrix.Invert(camera.ViewMatrix).Forward * force);
            }
        }
    }
}
