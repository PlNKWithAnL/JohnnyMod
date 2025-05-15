using JohnnyMod.Survivors.Johnny.SkillStates;

namespace JohnnyMod.Survivors.Johnny
{
    public static class JohnnyStates
    {
        public static void Init()
        {
            Modules.Content.AddEntityState(typeof(SlashCombo));

            Modules.Content.AddEntityState(typeof(MistFiner));

            Modules.Content.AddEntityState(typeof(StepDash));

            Modules.Content.AddEntityState(typeof(AirDash));

            Modules.Content.AddEntityState(typeof(MistFinerDash));

            Modules.Content.AddEntityState(typeof(Vault));

            Modules.Content.AddEntityState(typeof(Deal));

            Modules.Content.AddEntityState(typeof(Coin));

            Modules.Content.AddEntityState(typeof(RomanCancel));

            Modules.Content.AddEntityState(typeof(RomanIdle));
        }
    }
}
