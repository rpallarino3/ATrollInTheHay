﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;

namespace ATrollInTheHay.Common.GameObjects
{
    public abstract class StationaryTerrainObject : TerrainObject
    {
        public StationaryTerrainObject(RegionNames region, List<int> imageIndex, Layer layer, Vector2 anchorPoint, float friction)
            : base(region, imageIndex, layer, anchorPoint, friction)
        {

        }
    }
}
