using EntityStates;
using JohnnyMod.Survivors.Johnny;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.AddressableAssets;

namespace JohnnyMod.Survivors.Johnny.SkillStates
{
    class Coin : AimThrowableBase
    {
        public float BaseDuration { get; private set; }

        public override void OnEnter()
        {
            var childLoc = GetModelChildLocator();
                childLoc.FindChild("GhostHilt").gameObject.SetActive(true);
                childLoc.FindChild("KatanaHilt").gameObject.SetActive(false);
                childLoc.FindChild("KatanaBlade").gameObject.SetActive(false);
                childLoc.FindChild("SwordSimp").gameObject.SetActive(false);

            this.damageCoefficient = characterBody.damage * JohnnyStaticValues.coinDamageCoeffecient;
            this.projectileBaseSpeed = 32f;
            this.detonationRadius = 2.5f;
            this.projectilePrefab = JohnnyAssets.coinProjectile;
            this.baseMinimumDuration = 0f;
            this.maxDistance = 100;
            this.setFuse = false;
            this.arcVisualizerPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/VFX/BasicThrowableVisualizer.prefab").WaitForCompletion();
            this.endpointVisualizerPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Huntress/HuntressArrowRainIndicator.prefab").WaitForCompletion();
            this.useGravity = true;
            base.OnEnter();
        }
        public override void ModifyProjectile(ref FireProjectileInfo fireProjectileInfo)
        {
            base.ModifyProjectile(ref fireProjectileInfo);
        }

        public override void OnProjectileFiredLocal()
        {
            base.OnProjectileFiredLocal();
            Util.PlaySound("PlayMistFinerLvlUp", gameObject);
            this.PlayAnimation("Gesture, Override", "Deal", "Deal.playbackRate", 0.65f);
            //        //this is only existing so i can do the ammend thing
        }
        //public class Coin : BaseSkillState
        //{
        //    public static float BaseDuration = 0.65f;

        //    public float duration = 0.65f; 

        //    private bool hasFired = false;
        //    private Ray aimRay;

        //    private ChildLocator childLoc;

        //    public override void OnEnter()
        //    {
        //        base.OnEnter();

        //        duration = BaseDuration / attackSpeedStat;
        //        aimRay = GetAimRay();

        //        Util.PlaySound("PlayMistFinerLvlUp", gameObject);

        //        childLoc = GetModelChildLocator();
        //        childLoc.FindChild("GhostHilt").gameObject.SetActive(true);
        //        childLoc.FindChild("KatanaHilt").gameObject.SetActive(false);
        //        childLoc.FindChild("KatanaBlade").gameObject.SetActive(false);
        //        childLoc.FindChild("SwordSimp").gameObject.SetActive(false);

        //        PlayAnimation("Gesture, Override", "Deal", "Deal.playbackRate", this.duration);
        //        //this is only existing so i can do the ammend thing
        //    }

        //    public override void FixedUpdate()
        //    {
        //        base.FixedUpdate();
        //        if (!hasFired)
        //        {
        //            Fire();
        //        }

        //        if (isAuthority && fixedAge >= duration)
        //            outer.SetNextStateToMain();
        //    }

        //    private void Fire()
        //    {
        //        hasFired = true;
        //        if (isAuthority)
        //        {
        //            Ray aimRay = GetAimRay();
        //            aimRay.direction = Util.ApplySpread(aimRay.direction, 0, 0, 1f, 1f, 0f, -10);

        //            //A straight ray to compare the angle
        //            Vector3 straight = aimRay.direction;
        //            straight.y *= 0;

        //            Vector3 flickDirection = aimRay.direction;
        //            flickDirection *= Mathf.Clamp(base.rigidbody.velocity.magnitude, 1f, 20f);
        //            flickDirection.y += Mathf.Max(base.rigidbody.velocity.y, 0);

        //            Quaternion rotationOffset = Quaternion.Euler(0, 0, 0);

        //            //If we are looking more the 60 degrees down roughly
        //            if(straight.y - flickDirection.y > 0.60f)
        //            {
        //                rotationOffset.eulerAngles = Vector3.up;
        //            }
        //            /*if(straight.y - flickDirection.y > 0.60f)
        //            {
        //                rotationOffset = Quaternion.Euler(0, 0, 0);
        //            }*/

        //            flickDirection += rotationOffset.eulerAngles;

        //            ProjectileManager.instance.FireProjectile(new FireProjectileInfo
        //            {
        //                owner = gameObject,
        //                damage = characterBody.damage * JohnnyStaticValues.coinDamageCoeffecient,
        //                projectilePrefab = JohnnyAssets.coinProjectile,
        //                position = aimRay.origin,
        //                rotation = Util.QuaternionSafeLookRotation(flickDirection),
        //                speedOverride = 32
        //            });
        //        }
        //    }

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