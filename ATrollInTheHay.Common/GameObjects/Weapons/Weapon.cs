using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common.Enumerations;
using ATrollInTheHay.Common.Interfaces;
using ATrollInTheHay.Common.HelperClasses;
using ATrollInTheHay.Common.Animations;
using ATrollInTheHay.Common.Animations.AnimationNames;
using ATrollInTheHay.Common.CollisionBoxes;

namespace ATrollInTheHay.Common.GameObjects.Weapons
{
    public abstract class Weapon : StandardGameObject, IDamaging
    {
        protected WeaponNames _name;
        protected WeaponType _type;
        protected int _damage;
        protected int _swingSpeed;
        protected Vector2 _weaponSize;

        protected Dictionary<CollisionBox, List<Vector2>> _attackLeftBoxOffsets;
        protected Dictionary<CollisionBox, List<Vector2>> _attackRightBoxOffsets;
        protected Dictionary<CollisionBox, List<Vector2>> _airAttackLeftBoxOffsets;
        protected Dictionary<CollisionBox, List<Vector2>> _airAttackRightBoxOffsets;

        protected Dictionary<CollisionBox, List<Vector2>> _attackDictionary;
        protected bool _attackFinished;

        public Weapon(RegionNames region, List<int> imageIndexes, Layer layer, Vector2 anchorPoint, int damage, int swingSpeed, Vector2 weaponSize)
            : base(region, imageIndexes, layer, anchorPoint, 0f)
        {
            _damage = damage;
            _swingSpeed = swingSpeed;

            _maskType = GameConstants.STANDARD_COLLISION_MASK;
            _mask = GameConstants.STANDARD_COLLISION_MASK + GameConstants.TERRAIN_COLLISION_MASK;

            _weaponSize = weaponSize;

            _attackLeftBoxOffsets = new Dictionary<CollisionBox, List<Vector2>>();
            _attackRightBoxOffsets = new Dictionary<CollisionBox, List<Vector2>>();
            _airAttackLeftBoxOffsets = new Dictionary<CollisionBox, List<Vector2>>();
            _airAttackRightBoxOffsets = new Dictionary<CollisionBox, List<Vector2>>();

            _attackFinished = true;
        }

        public override void HandleCollisionWithObject(Vector2 currentObjectOffset, CollisionBox collidingBox, GameObject otherObject, Vector2 otherObjectOffset, CollisionBox otherCollidingBox)
        {
            if (otherObject is IDamageable) // need to make sure we don't damage twice on the other object
            {
                // damage the other object
            }
        }

        public void BeginAttack(bool aerial, Directions dir)
        {
            _attackFinished = false;

            if (aerial)
            {
                if (dir == Directions.Left)
                {
                    _animator.SetNewAnimation(WeaponAnimations.AERIAL_ATTACK_LEFT);
                    _attackDictionary = _airAttackLeftBoxOffsets;
                }
                else
                {
                    _animator.SetNewAnimation(WeaponAnimations.AERIAL_ATTACK_RIGHT);
                    _attackDictionary = _airAttackRightBoxOffsets;
                }
            }
            else
            {
                if (dir == Directions.Left)
                {
                    _animator.SetNewAnimation(WeaponAnimations.ATTACK_LEFT);
                    _attackDictionary = _attackLeftBoxOffsets;
                }
                else
                {
                    _animator.SetNewAnimation(WeaponAnimations.ATTACK_RIGHT);
                    _attackDictionary = _attackRightBoxOffsets;
                }
            }

            SetBoxOffsetBasedOnAnimationCounter();
        }

        public void ContinueAttack()
        {
            _animator.AdvanceAnimation();
            SetBoxOffsetBasedOnAnimationCounter();

            _attackFinished = _animator.AnimationFinished;
        }

        public void FinishAttack()
        {
            _attackFinished = true; // do this here so you can force stop attack if you need to
            _animator.SetNewAnimation(WeaponAnimations.HIDDEN);
            _collisionBoxes.Clear(); // remove all the collision boxes from the weapon
        }

        protected void SetBoxOffsetBasedOnAnimationCounter()
        {
            foreach (var box in _attackDictionary.Keys)
            {
                box.AnchorPoint = _attackDictionary[box][_animator.AnimationCounter];
                _collisionBoxes.Add(box);
            }
        }

        public void UpdateAnchorPosition(Vector2 anchorObjectLocation)
        {
            // this is the offset from the top left corner of player sprite (it's location?) to the top left corner of the weapon sprite (it's location?)
            var defaultOffset = new Vector2(GameConstants.PLAYER_SPRITE_SIZE.X / 2 - _weaponSize.X / 2, -_weaponSize.Y / 2);
            _position = anchorObjectLocation + defaultOffset; // + some offset
        }

        public WeaponNames Name
        {
            get { return _name; }
        }

        public WeaponType Type
        {
            get { return _type; }
        }

        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }

        public int SwingSpeed
        {
            get { return _swingSpeed; }
            set { _swingSpeed = value; }
        }

        public bool AttackFinished
        {
            get { return _attackFinished; }
        }
    }
}
