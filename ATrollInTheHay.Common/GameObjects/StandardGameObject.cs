using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;
using ATrollInTheHay.Common.HelperClasses;

namespace ATrollInTheHay.Common.GameObjects
{
    public abstract class StandardGameObject : GameObject
    {
        public StandardGameObject(RegionNames region, List<int> imageIndex, Layer layer, Vector2 anchorPoint, float gravitationalConstant)
            : base(region, imageIndex, layer, anchorPoint, gravitationalConstant)
        {
            _maskType = GameConstants.STANDARD_COLLISION_MASK;
        }
    }
}
