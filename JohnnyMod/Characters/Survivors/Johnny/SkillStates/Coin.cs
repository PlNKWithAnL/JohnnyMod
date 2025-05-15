using EntityStates;
using JohnnyMod.Survivors.Johnny;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace JohnnyMod.Survivors.Johnny.SkillStates
{
    public class Coin : BaseSkillState
    {
        public static float BaseDuration = 0.65f;

        public float duration = 0.65f;

        private bool hasFired = false;
        private Ray aimRay;

        private ChildLocator childLoc;

        public override void OnEnter()
        {
            base.OnEnter();

            duration = BaseDuration / attackSpeedStat;
            aimRay = GetAimRay();

            Util.PlaySound("PlayMistFinerLvlUp", gameObject);

            childLoc = GetModelChildLocator();
            childLoc.FindChild("GhostHilt").gameObject.SetActive(true);
            childLoc.FindChild("KatanaHilt").gameObject.SetActive(false);
            childLoc.FindChild("KatanaBlade").gameObject.SetActive(false);
            childLoc.FindChild("SwordSimp").gameObject.SetActive(false);

            PlayAnimation("Gesture, Override", "Deal", "Deal.playbackRate", this.duration);
            //this is only existing so i can do the ammend thing
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!hasFired)
            {
                Fire();
            }

            if (isAuthority && fixedAge >= duration)
                outer.SetNextStateToMain();
        }

        private void Fire()
        {
            hasFired = true;
            if (isAuthority)
            {
                Ray aimRay = GetAimRay();
                aimRay.direction = Util.ApplySpread(aimRay.direction, 0, 0, 1f, 1f, 0f, -10);

                //A straight ray to compare the angle
                Vector3 straight = aimRay.direction;
                straight.y *= 0;

                Vector3 flickDirection = aimRay.direction;
                flickDirection *= Mathf.Clamp(base.rigidbody.velocity.magnitude, 1f, 20f);
                flickDirection.y += Mathf.Max(base.rigidbody.velocity.y, 0);

                Quaternion rotationOffset = Quaternion.Euler(0, 0, 0);

                //If we are looking more the 60 degrees down roughly
                if(straight.y - flickDirection.y > 0.60f)
                {
                    rotationOffset.eulerAngles = Vector3.up;
                }
                /*if(straight.y - flickDirection.y > 0.60f)
                {
                    rotationOffset = Quaternion.Euler(0, 0, 0);
                }*/

                flickDirection += rotationOffset.eulerAngles;

                ProjectileManager.instance.FireProjectile(new FireProjectileInfo
                {
                    owner = gameObject,
                    damage = characterBody.damage * JohnnyStaticValues.coinDamageCoeffecient,
                    projectilePrefab = JohnnyAssets.coinProjectile,
                    position = aimRay.origin,
                    rotation = Util.QuaternionSafeLookRotation(flickDirection),
                    speedOverride = 32
                });
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}