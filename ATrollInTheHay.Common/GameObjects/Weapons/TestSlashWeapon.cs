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

        public TestSlashWeapon(RegionNames region, List<int> imageIndexes, Layer layer, Vector2 anchorPoint) :
            base(region, imageIndexes, Layer.MidMidground, anchorPoint, 5, 1, new Vector2(150, 150))
        {
            FillBoxOffsets();
        }

        private void FillBoxOffsets()
        {
        }
    }
}
