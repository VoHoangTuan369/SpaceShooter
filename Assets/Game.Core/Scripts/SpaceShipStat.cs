using UnityEngine;

namespace Game.Core.Scripts
{
    [CreateAssetMenu(fileName = "NewSpaceShipStat", menuName = "Galaxy/SpaceShipStat")]
    public class SpaceShipStat : ScriptableObject
    {
        public float accelerationRate = 5f;
        public float decelerationRate = 5f;
        public float thrustForce = 1000f;
        public float brakeForce = 1000f;
        public float pitchForce = 1000f;
        public float yawForce = 1000f;
        public float rollForce = 1000f;
        public float boostForceMultiplier = 250f;
    }
}
