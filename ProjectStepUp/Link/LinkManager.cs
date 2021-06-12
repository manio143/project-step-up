using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;

namespace ProjectStepUp.Link
{
    public class LinkManager : ScriptComponent
    {
        private Constraint constraint;

        public LinkPhysics Link1 { get; set; }
        public LinkPhysics Link2 { get; set; }

        public TransformComponent CharacterLinkAnchor1 { get; set; }
        public TransformComponent CharacterLinkAnchor2 { get; set; }

        public float MaxConnectionLength { get; set; }

        public void ToggleConnect()
        {
            if (constraint == null)
            {
                if (!CanBeConnected) return;

                constraint = Simulation.CreateConstraint(
                    ConstraintTypes.Point2Point,
                    Link1.EndBody,
                    Link2.EndBody,
                    Matrix.Translation(new Vector3()),
                    Matrix.Translation(new Vector3()));
                this.GetSimulation().AddConstraint(constraint);
                return;
            }

            if (constraint.Enabled)
            {
                constraint.Enabled = false;
            }
            else
            {
                if (CanBeConnected)
                {
                    constraint.Enabled = true;
                }
                else
                {
                    // TODO: make sound so that user gets notified
                }
            }
        }

        private bool CanBeConnected => (CharacterLinkAnchor1.Position - CharacterLinkAnchor2.Position).LengthSquared() <= MaxConnectionLength * MaxConnectionLength;
    }
}
