﻿using Blazored.Toast.Services;

namespace Sentience.Models.Jobs
{
    public class BeginnerJobOne : Job
    {
        public BeginnerJobOne(GameEngine engine)
        {
            Name = "Cookie Clicker";
            Active = false;
            Unlocked = false;
            BaseIncome = 250M;
            BaseXP = 7930070;
            NextLevel = 7930070;
            JobType = JobTypes.Beginner;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.ResearchOne.Level > 299 && engine.GameData.ResearchTwo.Level > 299 && engine.GameData.ResearchThree.Level > 299)
            {
                if (!Unlocked)
                {
                    Unlocked = true;
                    engine.ShowToast(this.Name + " has been unlocked!", "Job Unlocked", ToastLevel.Success);
                }
                return true;
            }
            return false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.GameData.ResearchOne.Name + ": " + engine.GameData.ResearchThree.Level + "/300  " + engine.GameData.ResearchTwo.Name + ": " + engine.GameData.ResearchTwo.Level + "/300  " + engine.GameData.ResearchThree.Name + ": " + engine.GameData.ResearchThree.Level + "/300";
        }
    }
}
