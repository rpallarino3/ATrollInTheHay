﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ATrollInTheHay.Common.HelperClasses
{
    public static class GameConstants
    {
        public static readonly int TERRAIN_COLLISION_MASK = 4;
        public static readonly int STANDARD_COLLISION_MASK = 2;
        public static readonly int PHANTOM_COLLISION_MASK = 1;

        public static readonly float MAX_RUN_VELOCITY_IN_X = 5;
        public static readonly float RUN_THRESHOLD_IN_X = 3;
        public static readonly float MAX_RUN_VELOCITY_IN_Y = 10;
        public static readonly float DASH_THRESHOLD = 20;
        public static readonly float DASH_VELOCITY = 10;
        public static readonly Vector2 MAX_TEXTURE_SIZE = new Vector2(2000, 2000);
        public static readonly Vector2 SCREEN_SIZE_IN_GAME_UNITS = new Vector2(1280, 720);
        public static readonly float BG_SCROLL_RATIO = 5f;
        public static readonly float NUM_LAYERS = 6f;
        public static readonly Vector2 GRAVITATIONAL_PULL = new Vector2(0, 0.4f);
        public static readonly int MAX_COLLISION_TRIES = 5;
        public static readonly Vector2 PLAYER_SPRITE_SIZE = new Vector2(50, 100);

        public static readonly int PIERCE_ANIMATION_LENGTH = 5;
        public static readonly int BLUNT_ANIMATION_LENGTH = 5;
        public static readonly int SLASH_ANIMATION_LENGTH = 5;
        public static readonly int AIR_PIERCE_ANIMATION_LENGTH = 4;
        public static readonly int AIR_BLUNT_ANIMATION_LENGTH = 4;
        public static readonly int AIR_SLASH_ANIMATION_LENGTH = 4;
    }
}
