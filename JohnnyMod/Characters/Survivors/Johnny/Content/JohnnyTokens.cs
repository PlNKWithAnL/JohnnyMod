using System;
using JohnnyMod.Modules;
using JohnnyMod.Survivors.Johnny.Achievements;

namespace JohnnyMod.Survivors.Johnny
{
    public static class JohnnyTokens
    {
        public static void Init()
        {
            AddJohnnyTokens();

            ////uncomment this to spit out a lanuage file with all the above tokens that people can translate
            ////make sure you set Language.usingLanguageFolder and printingEnabled to true
            //Language.PrintOutput("Johnny.txt");
            ////refer to guide on how to build and distribute your mod with the proper folders
        }

        public static void AddJohnnyTokens()
        {
            string prefix = JohnnySurvivor.Johnny_PREFIX;

            string desc = "Johnny is a skilled fighter who makes use of a wide arsenal of weaponry to take down his foes.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine
             + "< ! > Sword is a good all-rounder while Boxing Gloves are better for laying a beatdown on more powerful foes." + Environment.NewLine + Environment.NewLine
             + "< ! > Pistol is a powerful anti air, with its low cooldown and high damage." + Environment.NewLine + Environment.NewLine
             + "< ! > Roll has a lingering armor buff that helps to use it aggressively." + Environment.NewLine + Environment.NewLine
             + "< ! > Bomb can be used to wipe crowds with ease." + Environment.NewLine + Environment.NewLine;

            string outro = "..and so he left, searching for a new identity.";
            string outroFailure = "..and so he vanished, forever a blank slate.";

            Language.Add(prefix + "NAME", "Johnny");
            Language.Add(prefix + "DESCRIPTION", desc);
            Language.Add(prefix + "SUBTITLE", "Natural-Born Gambler");
            Language.Add(prefix + "LORE", "sample lore");
            Language.Add(prefix + "OUTRO_FLAVOR", outro);
            Language.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            Language.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            #endregion

            #region Passive
            Language.Add(prefix + "PASSIVE_NAME", "Turn Up");
            Language.Add(prefix + "PASSIVE_DESCRIPTION", "Cards from Deal can be hit with Mist Finer to extend your attack.");

            Language.Add(prefix + "KEYWORD_TENSION", "<style=cKeywordName>Tension</style>\nA unique meter resource used for <style=cKeywordName>Roman Cancel</style> Landing successful attacks fills up the meter.");
            Language.Add(prefix + "KEYWORD_STEPDASH", "<style=cKeywordName>Step Dash</style>\nInstead of a traditional dash, Johnny lunges forwards a short distance.");
            Language.Add(prefix + "KEYWORD_RC", $"<style=cKeywordName>Roman Cancel</style>\nPressing <style=cIsUtility>Primary, Secondary, and Special</style> at the same time resets your state of action, preserving your momentum.");
            Language.Add(prefix + "KEYWORD_COIN", $"<style=cKeywordName>Coins</style>\nOn equipping coin, you give up the ability to charge <style=cIsUtility>Mist Finer</style> by holding. Instead you level up <style=cIsUtility>Mist Finer</style> by landing coins.");
            Language.Add(prefix + "KEYWORD_POP", $"<style=cKeywordName>Coin Impact</style>\nWhen a coin hits an enemy, it increases <style=cIsUtility>Mist Finer</style> level by 1. Coins hitting the ground instead explode and stun.");
            Language.Add(prefix + "KEYWORD_STUN", $"<style=cIsUtility>Stunning.</style>");
            #endregion

            #region Primary
            Language.Add(prefix + "PRIMARY_SLASH_NAME", "Sword");
            Language.Add(prefix + "PRIMARY_SLASH_DESCRIPTION", Tokens.agilePrefix + $" Swing forward for <style=cIsDamage>{100f * JohnnyStaticValues.swordDamageCoefficient}% damage</style>.");
            #endregion

            #region Secondary
            Language.Add(prefix + "SECONDARY_MIST_NAME", "Mist Finer");
            Language.Add(prefix + "SECONDARY_MIST_DESCRIPTION", $"Swing your Katana faster than light for <style=cIsDamage>{100f * JohnnyStaticValues.mistFinerDamageCoeffecient}% damage</style>.");
            #endregion

            #region Utility
            Language.Add(prefix + "UTILITY_DASH_NAME", "Step Dash");
            Language.Add(prefix + "UTILITY_DASH_DESCRIPTION", "Dash forward a short distance. You have armor while dashing.");

            Language.Add(prefix + "UTILITY_VAULT_NAME", "Vault");
            Language.Add(prefix + "UTILITY_VAULT_DESCRIPTION", "Leap forwards a significant distance. Can be canceled into Mist Finer to preserve momentum.");
            #endregion

            #region Special
            Language.Add(prefix + "SPECIAL_DEAL_NAME", "Deal");
            Language.Add(prefix + "SPECIAL_DEAL_DESCRIPTION", $"Throw a card that deals no damage, but can be hit by Mist Finer, dealing 1.25x damage.");

            Language.Add(prefix + "SPECIAL_COIN_NAME", "Glitter is Gold");
            Language.Add(prefix + "SPECIAL_COIN_DESCRIPTION", $"Toss a coin into the air, striking this coin at the perfect time splits it into 2.");
        #endregion

        #region Achievements
        Language.Add(Tokens.GetAchievementNameToken(JohnnyMasteryAchievement.identifier), "Johnny: Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(JohnnyMasteryAchievement.identifier), "As Johnny, beat the game or obliterate on Monsoon.");
            #endregion
        }
    }
}
