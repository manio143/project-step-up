using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;

namespace ProjectStepUp.Link
{
    public class LinkPhysics : StartupScript
    {
        private const float NodeSize = 0.0035f;
        private const float NodeDistanceDelta = 0.001f;
        private List<RigidbodyComponent> nodes = new List<RigidbodyComponent>();
        private List<Constraint> constraints = new List<Constraint>();

        [DataMemberIgnore]
        public RigidbodyComponent StartBody => nodes[0];
        [DataMemberIgnore]
        public RigidbodyComponent EndBody => nodes[nodes.Count - 1];
        public override void Start()
        {
            var skeleton = Entity.Get<ModelComponent>().Skeleton;
            var simulation = this.GetSimulation();

            for (int i = 0; i < skeleton.Nodes.Length; i++)
            {
                if (!skeleton.Nodes[i].Name.StartsWith("joint"))
                    continue;

                var rigidbody = new RigidbodyComponent
                {
                    IsKinematic = false,
                    CanCollideWith = CollisionFilterGroupFlags.AllFilter & ~CollisionFilterGroupFlags.CharacterFilter,
                    NodeName = skeleton.Nodes[i].Name,
                    Mass = 0.05f,
                };

                rigidbody.ColliderShapes.Add(new BoxColliderShapeDesc
                {
                    Size = new Vector3(NodeSize),
                });

                nodes.Add(rigidbody);
                Entity.Add(rigidbody);
            }

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                var constraint = Simulation.CreateConstraint(
                    ConstraintTypes.Point2Point,
                    nodes[i],
                    nodes[i + 1],
                    Matrix.Translation(new Vector3(NodeSize / 2 + NodeDistanceDelta, 0, 0)),
                    Matrix.Translation(new Vector3(-NodeSize / 2 - NodeDistanceDelta, 0, 0)));
                
                constraints.Add(constraint);
                simulation.AddConstraint(constraint);
            }
        }
    }
}
