using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;

namespace ATrollInTheHay.Common.GameObjects
{
    public abstract class DestructableTerrainObject : TerrainObject
    {
        public DestructableTerrainObject(RegionNames region, List<int> imageIndex, Layer layer, Vector2 anchorPoint, float gravitationalConstant)
            : base(region, imageIndex, layer, anchorPoint, gravitationalConstant)
        {
        }
    }
}
