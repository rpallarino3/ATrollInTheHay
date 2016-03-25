using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;
using ATrollInTheHay.Common.Animations;
using ATrollInTheHay.Common.Animations.AnimationNames;
using ATrollInTheHay.Common.CollisionBoxes;
using ATrollInTheHay.Common.HelperClasses;

namespace ATrollInTheHay.Common.GameObjects.Weapons
{
    public abstract class BluntingWeapon : Weapon
    {
        protected bool _rectangular;
        protected float _length;
        protected Vector2 _bulbSize;
        protected float _bulbRadius;

        public BluntingWeapon(RegionNames region, List<int> imageIndexes, Layer layer, Vector2 anchorPoint, int damage, int swingSpeed, Vector2 weaponImageSize, float length, Vector2 bulbSize) :
            base(region, imageIndexes, layer, anchorPoint, damage, swingSpeed, weaponImageSize)
        {
            _type = WeaponType.Blunting;
            _animator = new Animator(ConstructAnimations(), 0);

            _rectangular = true;
            _length = length;
            _bulbSize = bulbSize;
        }

        public BluntingWeapon(RegionNames region, List<int> imageIndexes, Layer layer, Vector2 anchorPoint, int damage, int swingSpeed, Vector2 weaponImageSize, float length, float bulbRadius) :
            base(region, imageIndexes, layer, anchorPoint, damage, swingSpeed, weaponImageSize)
        {
            _type = WeaponType.Blunting;
            _animator = new Animator(ConstructAnimations(), 0);

            _rectangular = false;
            _length = length;
            _bulbRadius = bulbRadius;
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
            // this is the offset from the top left corner of player sprite (it's location?) to the top left corner of the weapon sprite (it's location?)
            // we actually might need to know this for drawing? do we need it for this? only need this for drawing
            var defaultOffset = new Vector2(GameConstants.PLAYER_SPRITE_SIZE.X / 2 - _weaponSize.X / 2, -_weaponSize.Y / 2);
            // this is the offset from player topleft to where the weapon is held (center of player)
            var distanceToAnchor = new Vector2(GameConstants.PLAYER_SPRITE_SIZE.X / 2, GameConstants.PLAYER_SPRITE_SIZE.Y * 2 / 3);
            var playerOffsets = new List<Vector2>();
            playerOffsets.Add(distanceToAnchor);
            playerOffsets.Add(distanceToAnchor);
            playerOffsets.Add(distanceToAnchor + new Vector2(3, 0));
            playerOffsets.Add(distanceToAnchor + new Vector2(7, 0));
            playerOffsets.Add(distanceToAnchor + new Vector2(12, 0));

            float bulbCenter;

            if (_rectangular)
                bulbCenter = -_length + _bulbSize.X / 2;
            else
                bulbCenter = -_length + _bulbRadius;
            
            var firstFrameCenter  = new Vector2(bulbCenter, 0) + playerOffsets[0];
            var secondFrameCenter = new Vector2(bulbCenter * MathConstants.COS_45, bulbCenter * MathConstants.SIN_45) + playerOffsets[1];
            var thirdFrameCenter  = new Vector2(0, bulbCenter) + playerOffsets[2];
            var fourthFrameCenter = new Vector2(-bulbCenter * MathConstants.COS_45, -bulbCenter * MathConstants.SIN_45) + playerOffsets[3];
            var fifthFrameCenter  = new Vector2(-bulbCenter, 0) + playerOffsets[4];

            _attackLeftBoxOffsets     = new Dictionary<CollisionBox, List<Vector2>>();
            _attackRightBoxOffsets    = new Dictionary<CollisionBox, List<Vector2>>();
            _airAttackLeftBoxOffsets  = new Dictionary<CollisionBox, List<Vector2>>();
            _airAttackRightBoxOffsets = new Dictionary<CollisionBox, List<Vector2>>();

            if (_rectangular)
            {
                var collisionBoxes = new List<CollisionBox>();
                var boxOffsetsFromCenter = new List<Vector2>();
                float radius;

                if (_bulbSize.X > _bulbSize.Y) // width is greater than height, i think this is opposite way
                {
                    boxOffsetsFromCenter.Add(new Vector2(-_bulbSize.X / 3, -_bulbSize.Y / 4));
                    boxOffsetsFromCenter.Add(new Vector2(-_bulbSize.X / 3, _bulbSize.Y / 4));
                    boxOffsetsFromCenter.Add(new Vector2(0, -_bulbSize.Y / 4));
                    boxOffsetsFromCenter.Add(new Vector2(0, _bulbSize.Y / 4));
                    boxOffsetsFromCenter.Add(new Vector2(_bulbSize.X / 3, -_bulbSize.Y / 4));
                    boxOffsetsFromCenter.Add(new Vector2(_bulbSize.X / 3, _bulbSize.Y / 4));
                    radius = (_bulbSize.X / 6) / MathConstants.COS_45;
                }
                else
                {
                    boxOffsetsFromCenter.Add(new Vector2(-_bulbSize.X / 4, -_bulbSize.Y / 3));
                    boxOffsetsFromCenter.Add(new Vector2(-_bulbSize.X / 4, 0));
                    boxOffsetsFromCenter.Add(new Vector2(-_bulbSize.X / 4, _bulbSize.Y / 3));
                    boxOffsetsFromCenter.Add(new Vector2(_bulbSize.X / 4, -_bulbSize.Y / 3));
                    boxOffsetsFromCenter.Add(new Vector2(_bulbSize.X / 4, 0));
                    boxOffsetsFromCenter.Add(new Vector2(_bulbSize.X / 4, _bulbSize.Y / 3));
                    radius = (_bulbSize.Y / 6) / MathConstants.COS_45;
                }

                collisionBoxes.Add(new CircleCollisionBox(this, firstFrameCenter + boxOffsetsFromCenter[0], radius, GameConstants.STANDARD_COLLISION_MASK + GameConstants.TERRAIN_COLLISION_MASK));
                collisionBoxes.Add(new CircleCollisionBox(this, firstFrameCenter + boxOffsetsFromCenter[1], radius, GameConstants.STANDARD_COLLISION_MASK + GameConstants.TERRAIN_COLLISION_MASK));
                collisionBoxes.Add(new CircleCollisionBox(this, firstFrameCenter + boxOffsetsFromCenter[2], radius, GameConstants.STANDARD_COLLISION_MASK + GameConstants.TERRAIN_COLLISION_MASK));
                collisionBoxes.Add(new CircleCollisionBox(this, firstFrameCenter + boxOffsetsFromCenter[3], radius, GameConstants.STANDARD_COLLISION_MASK + GameConstants.TERRAIN_COLLISION_MASK));
                collisionBoxes.Add(new CircleCollisionBox(this, firstFrameCenter + boxOffsetsFromCenter[4], radius, GameConstants.STANDARD_COLLISION_MASK + GameConstants.TERRAIN_COLLISION_MASK));
                collisionBoxes.Add(new CircleCollisionBox(this, firstFrameCenter + boxOffsetsFromCenter[5], radius, GameConstants.STANDARD_COLLISION_MASK + GameConstants.TERRAIN_COLLISION_MASK));

                for (int i = 0; i < collisionBoxes.Count; i++)
                {
                    var box = collisionBoxes[i];
                    _attackLeftBoxOffsets.Add(box, new List<Vector2>()
                    {
                        -firstFrameCenter + boxOffsetsFromCenter[i],
                        -secondFrameCenter + boxOffsetsFromCenter[i],
                        -thirdFrameCenter + boxOffsetsFromCenter[i],
                        -fourthFrameCenter + boxOffsetsFromCenter[i],
                        -fifthFrameCenter + boxOffsetsFromCenter[i]
                    });
                    _attackRightBoxOffsets.Add(box, new List<Vector2>()
                    {
                        firstFrameCenter + boxOffsetsFromCenter[i],
                        secondFrameCenter + boxOffsetsFromCenter[i],
                        thirdFrameCenter + boxOffsetsFromCenter[i],
                        fourthFrameCenter + boxOffsetsFromCenter[i],
                        fifthFrameCenter + boxOffsetsFromCenter[i]
                    });
                    _airAttackLeftBoxOffsets.Add(box, new List<Vector2>()
                    {
                        -secondFrameCenter + boxOffsetsFromCenter[i],
                        -thirdFrameCenter + boxOffsetsFromCenter[i],
                        -fourthFrameCenter + boxOffsetsFromCenter[i],
                        -fifthFrameCenter + boxOffsetsFromCenter[i]
                    });
                    _airAttackRightBoxOffsets.Add(box, new List<Vector2>()
                    {
                        secondFrameCenter + boxOffsetsFromCenter[i],
                        thirdFrameCenter + boxOffsetsFromCenter[i],
                        fourthFrameCenter + boxOffsetsFromCenter[i],
                        fifthFrameCenter + boxOffsetsFromCenter[i]
                    });
                }
            }
            else
            {
                var collisionBox = new CircleCollisionBox(this, firstFrameCenter, _bulbRadius, GameConstants.STANDARD_COLLISION_MASK + GameConstants.TERRAIN_COLLISION_MASK);
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
}
