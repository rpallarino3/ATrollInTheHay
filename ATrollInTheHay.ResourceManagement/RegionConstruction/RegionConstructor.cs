using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATrollInTheHay.Common.Enumerations;
using ATrollInTheHay.Common.GameObjects;
using ATrollInTheHay.ResourceManagement.RegionConstruction.RegionFactories;

namespace ATrollInTheHay.ResourceManagement.RegionConstruction
{
    public class RegionConstructor
    {

        private Dictionary<RegionNames, RegionFactory> _regionFactories;

        public RegionConstructor()
        {
            _regionFactories = new Dictionary<RegionNames, RegionFactory>();
            _regionFactories.Add(RegionNames.Test1, new Test1RegionFactory());
            _regionFactories.Add(RegionNames.Test2, new Test2RegionFactory());
            _regionFactories.Add(RegionNames.Test3, new Test3RegionFactory());
        }

        public void ConstructRegion(RegionNames regionName, List<GameObject> gameObjects, List<GameObject> cameraCollisionObjects,
            List<GameObject> backgroundGameObjects, List<GameObject> foregroundGameObjects)
        {
            _regionFactories[regionName].ConstructRegion(gameObjects, cameraCollisionObjects, backgroundGameObjects, foregroundGameObjects);
        }
    }
}
