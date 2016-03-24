using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;
using ATrollInTheHay.Common.Animations;
using ATrollInTheHay.Common.Animations.AnimationNames;
using ATrollInTheHay.Common.CollisionBoxes;

namespace ATrollInTheHay.Common.GameObjects.Weapons
{
    public abstract class BluntingWeapon : Weapon
    {
        protected int _length;
        protected Vector2 _bulbSize;

        public BluntingWeapon(RegionNames region, List<int> imageIndexes, Layer layer, Vector2 anchorPoint, int damage, int swingSpeed, Vector2 weaponSize) :
            base(region, imageIndexes, layer, anchorPoint, damage, swingSpeed, weaponSize)
        {
            _type = WeaponType.Blunting;
            _animator = new Animator(ConstructAnimations(), 0);
        }

        private Dictionary<int, Animation> ConstructAnimations()
        {
            var animations = new Dictionary<int, Animation>();

            animations.Add(WeaponAnimations.HIDDEN, new Animation(WeaponAnimations.HIDDEN, 1, 1, _weaponSize));
            animations.Add(WeaponAnimations.STATIONARY_LEFT, new Animation(WeaponAnimations.STATIONARY_LEFT, 1, 1, _weaponSize));
            animations.Add(WeaponAnimations.STATIONARY_RIGHT, new Animation(WeaponAnimations.STATIONARY_RIGHT, 1, 1, _weaponSize));
            animations.Add(WeaponAnimations.ATTACK_LEFT, new Animation(WeaponAnimations.ATTACK_LEFT, 5, _swingSpeed, _weaponSize));
            animations.Add(WeaponAnimations.ATTACK_RIGHT, new Animation(WeaponAnimations.ATTACK_RIGHT, 5, _swingSpeed, _weaponSize));
            animations.Add(WeaponAnimations.AERIAL_ATTACK_LEFT, new Animation(WeaponAnimations.AERIAL_ATTACK_LEFT, 5, _swingSpeed, _weaponSize));
            animations.Add(WeaponAnimations.AERIAL_ATTACK_RIGHT, new Animation(WeaponAnimations.AERIAL_ATTACK_RIGHT, 5, _swingSpeed, _weaponSize));

            return animations;
        }

        protected virtual void FillOffsets() // put the offets in here
        {
            _attackLeftBoxOffsets     = new Dictionary<CollisionBox, List<Vector2>>();

            // circular
            // -L+r + offset, 0
            // (-L+r)cos45 + offset, (-L+r)sin45
            // 0 + offset, -L+r
            // (L+r)cos45 + offset, (L+r)sin45
            // L-r+offset, 0

            // rect
            // -L + width/2 + offset, 0
            // (-L + width/2)cos45 + offset, (-L + height/2)sin45
            // 0 + offset, -L + width /2
            // (L - width/2)cos45 + offset, (L - height/2)sin45
            // L-width/2 + offset, 0

            // offsets: 0, 0, 5, 10, 20 or a little less
            _attackRightBoxOffsets    = new Dictionary<CollisionBox, List<Vector2>>();
            _airAttackLeftBoxOffsets  = new Dictionary<CollisionBox, List<Vector2>>();
            _airAttackRightBoxOffsets = new Dictionary<CollisionBox, List<Vector2>>();
        }
    }
}
