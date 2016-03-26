using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;

namespace ATrollInTheHay.Common.GameObjects.Weapons
{
    public class TestSlashWeapon : SlashingWeapon
    {

        public TestSlashWeapon(RegionNames region, Layer layer, Vector2 anchorPoint) :
            base(region, new List<int>() { (int)WeaponNames.TestSlash }, Layer.MidMidground, anchorPoint, 5, 1, new Vector2(150, 150))
        {
            _name = WeaponNames.TestSlash;
            FillBoxOffsets();
        }

        private void FillBoxOffsets()
        {
        }
    }
}
