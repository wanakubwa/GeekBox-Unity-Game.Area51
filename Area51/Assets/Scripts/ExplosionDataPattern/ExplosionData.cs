using UnityEngine;
using System.Collections;

namespace ExplosionDataPattern
{
    public class ExplosionData
    {
        private static ExplosionData _instance = new ExplosionData();

        public float ExplosionForce { private set; get; }
        public float ExplosionBounceForce { private set; get; }

        // Constructor
        private ExplosionData() { }

        // Methods
        public static ExplosionData getInstance()
        {
            return _instance;
        }

        public void setExplosionForces(float explosionForce, float explosionBounceForce)
        {
            this.ExplosionForce = explosionForce;
            this.ExplosionBounceForce = explosionBounceForce;
        }
    }
}