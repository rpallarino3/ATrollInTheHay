﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;

namespace ATrollInTheHay.Common.GameObjects.Weapons
{
    public class TestBluntWeapon : BluntingWeapon
    {

        public TestBluntWeapon(RegionNames region, List<int> imageIndexes, Layer layer, Vector2 anchorPoint) :
            base(region, imageIndexes, Layer.MidMidground, anchorPoint, 10, 3, new Vector2(300, 200), 100, new Vector2(20, 30))
        {
            base.FillOffsets();
        }
    }
}
