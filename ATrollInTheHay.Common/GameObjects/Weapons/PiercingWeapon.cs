using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;
using ATrollInTheHay.Common.Animations;
using ATrollInTheHay.Common.Animations.AnimationNames;
using ATrollInTheHay.Common.HelperClasses;
using ATrollInTheHay.Common.CollisionBoxes;

namespace ATrollInTheHay.Common.GameObjects.Weapons
{
    public abstract class PiercingWeapon : Weapon
    {
        // length is the distnace from the center of the spear handle to the beginning of the spear head
        protected float _length;
        protected Vector2 _headSize;

        public PiercingWeapon(RegionNames region, List<int> imageIndexes, Layer layer, Vector2 anchorPoint, int damage, int swingSpeed, Vector2 weaponImageSize, float length, Vector2 headSize) :
            base(region, imageIndexes, layer, anchorPoint, damage, swingSpeed, weaponImageSize)
        {
            _type = WeaponType.Piercing;
            _animator = new Animator(ConstructAnimations(), WeaponAnimations.HIDDEN);
            _length = length;
            _headSize = headSize;
        }

        private Dictionary<int, Animation> ConstructAnimations() 
        {
            var animations = new Dictionary<int, Animation>();

            animations.Add(WeaponAnimations.HIDDEN, new Animation(WeaponAnimations.HIDDEN, 1, 1, _weaponSize));
            animations.Add(WeaponAnimations.STATIONARY_LEFT, new Animation(WeaponAnimations.STATIONARY_LEFT, 1, 1, _weaponSize)); // probably get rid of these two
            animations.Add(WeaponAnimations.STATIONARY_RIGHT, new Animation(WeaponAnimations.STATIONARY_RIGHT, 1, 1, _weaponSize));
            animations.Add(WeaponAnimations.ATTACK_LEFT, new Animation(WeaponAnimations.ATTACK_LEFT, GameConstants.PIERCE_ANIMATION_LENGTH, _swingSpeed, _weaponSize));
            animations.Add(WeaponAnimations.ATTACK_RIGHT, new Animation(WeaponAnimations.ATTACK_RIGHT, GameConstants.PIERCE_ANIMATION_LENGTH, _swingSpeed, _weaponSize));
            animations.Add(WeaponAnimations.AERIAL_ATTACK_LEFT, new Animation(WeaponAnimations.AERIAL_ATTACK_LEFT, GameConstants.AIR_PIERCE_ANIMATION_LENGTH, _swingSpeed, _weaponSize));
            animations.Add(WeaponAnimations.AERIAL_ATTACK_RIGHT, new Animation(WeaponAnimations.AERIAL_ATTACK_RIGHT, GameConstants.AIR_PIERCE_ANIMATION_LENGTH, _swingSpeed, _weaponSize));

            return animations;
        }

        protected virtual void FillOffsets()
        {
            // this is the center of the spear handle
            var distanceToAnchor = new Vector2(GameConstants.PLAYER_SPRITE_SIZE.X / 2, GameConstants.PLAYER_SPRITE_SIZE.Y * 2 / 3);

            // this is the front of the spear head
            var firstFrameCenter = distanceToAnchor + new Vector2(_length, -_headSize.Y/2);
            var secondFrameCenter = distanceToAnchor + new Vector2(_length, -_headSize.Y / 2) + new Vector2(_length / 4, 0);
            var thirdFrameCenter = distanceToAnchor + new Vector2(_length, -_headSize.Y / 2) + new Vector2(_length / 2, 0);
            var fourthFrameCenter = distanceToAnchor + new Vector2(_length, -_headSize.Y / 2) + new Vector2(_length * 3 / 4, 0);
            var fifthFrameCenter = distanceToAnchor + new Vector2(_length, -_headSize.Y / 2) + new Vector2(_length, 0);

            _attackLeftBoxOffsets = new Dictionary<CollisionBox, List<Vector2>>();
            _attackRightBoxOffsets = new Dictionary<CollisionBox, List<Vector2>>();
            _airAttackLeftBoxOffsets = new Dictionary<CollisionBox, List<Vector2>>();
            _airAttackRightBoxOffsets = new Dictionary<CollisionBox, List<Vector2>>();

            var collisionBox = new RectangleCollisionBox(this, firstFrameCenter, _headSize.X, _headSize.Y,
                GameConstants.STANDARD_COLLISION_MASK + GameConstants.TERRAIN_COLLISION_MASK);

            _attackLeftBoxOffsets.Add(collisionBox,
                new List<Vector2>()
                {
                        -firstFrameCenter, -secondFrameCenter, -thirdFrameCenter, -fourthFrameCenter, -fifthFrameCenter
                });
            _attackRightBoxOffsets.Add(collisionBox,
                new List<Vector2>()
                {
                        firstFrameCenter, secondFrameCenter, thirdFrameCenter, fourthFrameCenter, fifthFrameCenter
                });

            // might need to add the first frame back to these attacks
            _airAttackLeftBoxOffsets.Add(collisionBox,
                new List<Vector2>()
                {
                        -secondFrameCenter, -thirdFrameCenter, -fourthFrameCenter, -fifthFrameCenter
                });
            _airAttackRightBoxOffsets.Add(collisionBox,
                new List<Vector2>()
                {
                        secondFrameCenter, thirdFrameCenter, fourthFrameCenter, fifthFrameCenter
                });
        }
    }
}
