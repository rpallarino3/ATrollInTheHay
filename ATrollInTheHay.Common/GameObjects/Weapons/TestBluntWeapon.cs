using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;

namespace ATrollInTheHay.Common.GameObjects.Weapons
{
    public class TestBluntWeapon : BluntingWeapon
    {

        public TestBluntWeapon(RegionNames region, Layer layer, Vector2 anchorPoint) :
            base(region, new List<int>() { (int)WeaponNames.TestBlunt }, Layer.MidMidground, anchorPoint, 10, 3, new Vector2(300, 200), 100, new Vector2(20, 30))
        {
            _name = WeaponNames.TestBlunt;
            base.FillOffsets();
        }
    }
}
