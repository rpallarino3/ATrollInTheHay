﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;

namespace ATrollInTheHay.Common.GameObjects.Weapons
{
    public class TestPierceWeapon : PiercingWeapon
    {

        public TestPierceWeapon(RegionNames region, List<int> imageIndexes, Layer layer, Vector2 anchorPoint) :
            base(region, imageIndexes, Layer.MidMidground, anchorPoint, 7, 2, new Vector2(200, 200), 100, new Vector2(30, 20))
        {
            base.FillOffsets();
        }
    }
}
