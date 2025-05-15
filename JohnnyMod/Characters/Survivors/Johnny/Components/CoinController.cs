using HG;
using RoR2;
using RoR2.Orbs;
using RoR2.Projectile;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using R2API;

namespace JohnnyMod.Survivors.Johnny.Components
{
    public class CoinController : NetworkBehaviour, IOnIncomingDamageServerReceiver, IProjectileImpactBehavior
    {
        public HealthComponent projectileHealthComponent;
        public JohnnyTensionController JohnnyStandee;
        public HurtBox targetHurtbox;

        private float upCount = 0;
        //we make it invulnerable and ignore all the hits in the first 10th of a second to avoid the coins from instantly dying on splitting
        private float spawnInvuln = 0.2f;
        private float coinFlash = 0.45f;
        private float flashWindow = 0.3f;
        //private bool isChildCoin = false;

        private ProjectileSimple projSimp;
        //BlastAttack coinBomb = null;

        public static List<HurtBox> cardHurtBoxList = new List<HurtBox>();
        ProjectileController projCTRL;

        private void Start()
        {
            projSimp = this.GetComponent<ProjectileSimple>();
            projCTRL = GetComponent<ProjectileController>();
        }

        private void OnEnable()
        {
            cardHurtBoxList.Add(this.targetHurtbox);
        }

        private void OnDisable()
        {
            cardHurtBoxList.Remove(this.targetHurtbox);
        }

        private void FixedUpdate()
        {
            if (this.projSimp)
                this.projSimp.SetLifetime(projSimp.lifetime);

            upCount += Time.deltaTime;
        }

        public void PopCoin(DamageInfo damageInfo)
        {
            Vector3 position = damageInfo.position;

            FireProjectileInfo info = new FireProjectileInfo()
            {
                owner = projCTRL.owner,
                damage = JohnnyStaticValues.coinDamageCoeffecient,
                force = 0,
                position = new Vector3(position.x, position.y + 2, position.z),
                rotation = Quaternion.Euler(45, Random.Range(-360, 360), 0),
                projectilePrefab = JohnnyAssets.coinProjectile,
                speedOverride = 32,
            };
            FireProjectileInfo info2 = new FireProjectileInfo()
            {
                owner = projCTRL.owner,
                damage = JohnnyStaticValues.coinDamageCoeffecient,
                force = 0,
                position = new Vector3(position.x, position.y + 2, position.z),
                rotation = Quaternion.Euler(45, Random.Range(-360, 360), 0),
                projectilePrefab = JohnnyAssets.coinProjectile,
                speedOverride = 32,
            };

            ProjectileManager.instance.FireProjectile(info);
            ProjectileManager.instance.FireProjectile(info2);

            if (projectileHealthComponent)
                projectileHealthComponent.Suicide();

            Destroy(gameObject);
        }

        public void OnIncomingDamageServer(DamageInfo damageInfo)
        {
            // filter out non-johnny, and only accept certain inflictors so things like fireworks/frost relic dont pop it
            if (upCount > spawnInvuln && damageInfo.attacker && damageInfo.attacker.GetComponent<JohnnyTensionController>() && damageInfo.inflictor != this.gameObject &&
               (!damageInfo.inflictor || damageInfo.inflictor == damageInfo.attacker || damageInfo.inflictor.GetComponent<CoinController>()))
            {
                if (upCount >= coinFlash && upCount <= coinFlash + flashWindow)
                {
                    PopCoin(damageInfo);
                }
            }

            damageInfo.rejected = true;
        }

        public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
        {

            // apply hurt state, its like stun but mostly helps with attack interruption good
            var body = Util.HurtBoxColliderToBody(impactInfo.collider);
            if (body && body.TryGetComponent<SetStateOnHurt>(out var setStateOnHurt))
                setStateOnHurt.SetPain();

            //if we actually hit something level up mist finer
            if (body.healthComponent && body.teamComponent.teamIndex != projCTRL.teamFilter.teamIndex)
            {
                JohnnyStandee.AddMistFiner();
                Chat.AddMessage($"Leveled up mistfiner to {JohnnyStandee.mfLevel}");
            }

            //if we landed on the ground
            if (!body.healthComponent)
            {
                BlastAttack blastAttack = new BlastAttack
                {
                    baseDamage = JohnnyStaticValues.coinDamageCoeffecient * 10,
                    radius = 1.5f,
                    baseForce = 0f,
                    crit = false,
                    procCoefficient = 1f,
                    attacker = projCTRL.owner,
                    inflictor = projCTRL.owner,
                    damageType = DamageType.Stun1s,
                    damageColorIndex = DamageColorIndex.WeakPoint,
                    teamIndex = projCTRL.teamFilter.teamIndex,
                    falloffModel = BlastAttack.FalloffModel.SweetSpot,
                    position = transform.position,
                };

                blastAttack.Fire();
            }

            if (projectileHealthComponent)
                projectileHealthComponent.Suicide();

            Destroy(gameObject);
        }
    }
}