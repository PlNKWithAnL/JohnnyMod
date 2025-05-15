using EntityStates;
using JohnnyMod.Survivors.Johnny.Components;
using RoR2;
using RoR2.Skills;

namespace JohnnyMod.Survivors.Johnny.SkillStates
{
    internal class JohnnyMainState : GenericCharacterMain
    {
        ///@Desc
        /// This main state only really exists to replace Johnny's sprinting with Vault. This then replaces Deal with Ensenga.
        /// Also implements Roman Cancelling and the Instant Kill

        private GenericSkill stepDashSkill;
        private GenericSkill airDashSkill;
        private JohnnyTensionController tensionCTRL;

        public override void OnEnter()
        {
            base.OnEnter();
            stepDashSkill = skillLocator.FindSkill("LOADOUT_SKILL_DASH");
            airDashSkill = skillLocator.FindSkill("LOADOUT_SKILL_AIRDASH");
            tensionCTRL = GetComponent<JohnnyTensionController>();
        }

        public override void HandleMovements()
        {
            bool canExecute = stepDashSkill.CanExecute();
            if (!canExecute)
            {
                sprintInputReceived = false;
            }

            if(!isGrounded && characterMotor.jumpCount >= characterBody.maxJumpCount)
            {
                sprintInputReceived = false;
            }

            //bool canRc = (tensionCTRL.tension >= 50);
            //bool canIK = (tension >= 100);

            base.HandleMovements();

            /*if(canRc && isAuthority && 
                (inputBank.skill1.down) &&
                (inputBank.skill2.down) &&
                (inputBank.skill4.down))
            {
                inputBank.skill1.PushState(false);
                inputBank.skill2.PushState(false);
                inputBank.skill4.PushState(false);
                GetComponent<JohnnyTensionController>().AddTension(-50);
                outer.SetState(new RomanCancel());
            }

            if(canIK && isAuthority && (inputBank.skill1.down && inputBank.skill2.down && inputBank.skill3.down && inputBank.skill4.down))
            {

            }*/

            if (sprintInputReceived && canExecute && isAuthority)
            {
                SkillDef skillDef = stepDashSkill.skillDef;

                if (!isGrounded && characterMotor.jumpCount < characterBody.maxJumpCount)
                {
                    skillDef = airDashSkill.skillDef;

                    stepDashSkill.hasExecutedSuccessfully = true;
                    //ugly avoid CharacterBody.OnSkillActivated cause it was not intended to be used with extra generic skills woops
                    stepDashSkill.stateMachine.SetInterruptState(skillDef.InstantiateNextState(airDashSkill), skillDef.interruptPriority);
                    stepDashSkill.stock -= skillDef.stockToConsume;


                    characterMotor.jumpCount++;
                    return;
                }

                if(!isGrounded && characterMotor.jumpCount >= characterBody.maxJumpCount)
                {
                    return;
                }

                stepDashSkill.hasExecutedSuccessfully = true;
                //ugly avoid CharacterBody.OnSkillActivated cause it was not intended to be used with extra generic skills woops
                stepDashSkill.stateMachine.SetInterruptState(skillDef.InstantiateNextState(stepDashSkill), skillDef.interruptPriority);
                stepDashSkill.stock -= skillDef.stockToConsume;
            }
        }
    }
}
