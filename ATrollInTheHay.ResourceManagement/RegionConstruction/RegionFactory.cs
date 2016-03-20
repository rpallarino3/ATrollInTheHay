using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATrollInTheHay.Common.GameObjects;

namespace ATrollInTheHay.ResourceManagement.RegionConstruction
{
    public abstract class RegionFactory
    {

        public abstract void ConstructRegion(List<GameObject> gameObjects, List<GameObject> cameraCollisionObjects,
            List<GameObject> backgroundGameObjects, List<GameObject> foregroundGameObjects);
    }
}
