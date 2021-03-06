﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATrollInTheHay.Common.Enumerations;

namespace ATrollInTheHay.Common
{
    public abstract class LoadMessage
    {
    }

    public class RegionLoadMessage : LoadMessage
    {
        public List<RegionNames> RegionsToLoad { get; set; }
        public List<RegionNames> RegionsToUnload { get; set; }

        public bool SwapOutCharacterHandler { get; set; }
        public CharacterTheme CharacterThemeToLoad { get; set; }
    }

    public class SaveLoadMessage : LoadMessage
    {
    }
}
