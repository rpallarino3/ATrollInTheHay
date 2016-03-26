using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;

namespace ATrollInTheHay.Common.GameObjects.Weapons
{
    public class TestPierceWeapon : PiercingWeapon
    {

        public TestPierceWeapon(RegionNames region, Layer layer, Vector2 anchorPoint) :
            base(region, new List<int>() { (int)WeaponNames.TestPierce }, Layer.MidMidground, anchorPoint, 7, 2, new Vector2(200, 200), 100, new Vector2(30, 20))
        {
            _name = WeaponNames.TestPierce;
            base.FillOffsets();
        }
    }
}
