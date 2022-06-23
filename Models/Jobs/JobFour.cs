﻿using Sentience.Models.Research;

namespace Sentience.Models.Jobs
{
    public class JobFour : Job
    {
        public JobFour(GameEngine engine)
        {
            Name = "Random Number Generator";
            Active = false;
            Unlocked = false;
            BaseIncome = 6.21f;
            BaseXP = 9775;
            NextLevel = 9775;
            JobType = JobTypes.Basics;
        }
        public bool CanUnlock(GameEngine engine)
        {
            return (engine.GameData.JobThree.Level > 19 && engine.GameData.ResearchOne.Level > 49) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.GameData.JobThree.Name + ": " + engine.GameData.JobThree.Level + "/20   " + engine.GameData.ResearchOne.Name + ": " + engine.GameData.ResearchOne.Level + "/50";
        }
    }
}
