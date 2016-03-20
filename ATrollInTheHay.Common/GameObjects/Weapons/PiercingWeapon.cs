﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;
using ATrollInTheHay.Common.Animations;
using ATrollInTheHay.Common.Animations.AnimationNames;

namespace ATrollInTheHay.Common.GameObjects.Weapons
{
    public abstract class PiercingWeapon : Weapon
    {

        public PiercingWeapon(RegionNames region, List<int> imageIndexes, Layer layer, Vector2 anchorPoint, int damage, int swingSpeed, Vector2 weaponSize) :
            base(region, imageIndexes, layer, anchorPoint, damage, swingSpeed, weaponSize)
        {
            _type = WeaponType.Piercing;
            _animator = new Animator(ConstructAnimations(), WeaponAnimations.HIDDEN);
        }

        private Dictionary<int, Animation> ConstructAnimations() 
        {
            var animations = new Dictionary<int, Animation>();

            animations.Add(WeaponAnimations.HIDDEN, new Animation(WeaponAnimations.HIDDEN, 1, 1, _weaponSize));
            animations.Add(WeaponAnimations.STATIONARY_LEFT, new Animation(WeaponAnimations.STATIONARY_LEFT, 1, 1, _weaponSize));
            animations.Add(WeaponAnimations.STATIONARY_RIGHT, new Animation(WeaponAnimations.STATIONARY_RIGHT, 1, 1, _weaponSize));
            animations.Add(WeaponAnimations.ATTACK_LEFT, new Animation(WeaponAnimations.ATTACK_LEFT, 4, _swingSpeed, _weaponSize));
            animations.Add(WeaponAnimations.ATTACK_RIGHT, new Animation(WeaponAnimations.ATTACK_RIGHT, 4, _swingSpeed, _weaponSize));
            animations.Add(WeaponAnimations.AERIAL_ATTACK_LEFT, new Animation(WeaponAnimations.AERIAL_ATTACK_LEFT, 5, _swingSpeed, _weaponSize));
            animations.Add(WeaponAnimations.AERIAL_ATTACK_RIGHT, new Animation(WeaponAnimations.AERIAL_ATTACK_RIGHT, 5, _swingSpeed, _weaponSize));

            return animations;
        }
    }
}