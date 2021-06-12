using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;

namespace ProjectStepUp.Link
{
    public class CharacterLinkAttachment : StartupScript
    {
        public LinkPhysics Link { get; set; }

        public CharacterLinkAttachment() => Priority = 10;

        public override void Start()
        {
            var physics = Entity.Get<RigidbodyComponent>();
            var constraint = Simulation.CreateConstraint(
                ConstraintTypes.Point2Point,
                physics,
                Link.StartBody,
                Matrix.Translation(new Vector3()),
                Matrix.Translation(new Vector3()));
            physics.Simulation.AddConstraint(constraint, disableCollisionsBetweenLinkedBodies: true);
        }
    }
}
