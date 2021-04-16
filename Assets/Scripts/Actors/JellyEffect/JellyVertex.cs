using UnityEngine;

namespace Actors.JellyEffect
{
    public class JellyVertex
    {
        public readonly int ID;
        public Vector3 position;
        private Vector3 _velocity;
        private Vector3 _force;

        public JellyVertex(int identifier, Vector3 inPosition)
        {
            ID = identifier;
            position = inPosition;
        }

        public void Shake(Vector3 inTarget, float inMass, float inStiffness, float inDamping)
        {
            _force = (inTarget - position) * inStiffness;
            _velocity = (_velocity + _force / inMass) * inDamping;
            position += _velocity;
            if ((_velocity + _force + _force / inMass).magnitude < 0.001f)
                position = inTarget;
        }
    }
}