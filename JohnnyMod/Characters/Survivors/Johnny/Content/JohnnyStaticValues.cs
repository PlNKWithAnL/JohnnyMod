using RoR2;
using RoR2.Skills;
using System;

namespace JohnnyMod.Survivors.Johnny
{
    public static class JohnnyStaticValues
    {
        public const float swordDamageCoefficient = 2f;

        public const float mistFinerDamageCoeffecient = 5.0f;

        public const float coinDamageCoeffecient = 1.5f;

        // stored skills for replacement
        internal static SkillDef StepDash;
        internal static SkillDef MistFinerDash;
        internal static SkillDef Vault;

        internal static SkillDef Coin;

        internal static GenericSkill Dash;
        internal static GenericSkill AirDash;
    }
}