using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.GameObjects;
using ATrollInTheHay.Common.Enumerations;
using ATrollInTheHay.Common.Animations;

namespace ATrollInTheHay.Common.GameObjects.Terrain
{
    public class NormalStationaryTerrainObject : StationaryTerrainObject
    {
        public NormalStationaryTerrainObject(RegionNames region, List<int> imageIndex, Layer layer, Vector2 anchorPoint, Vector2 size)
            : base(region, imageIndex, layer, anchorPoint, 0.5f)
        {
            var animationDictionary = new Dictionary<int, Animation>();
            animationDictionary.Add(0, new Animation(0, 1, 1, size));

            _animator = new Animator(animationDictionary, 0);

        }
    }
}
