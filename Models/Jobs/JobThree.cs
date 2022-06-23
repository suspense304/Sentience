﻿using Blazored.Toast.Services;
using Sentience.Models.Research;

namespace Sentience.Models.Jobs
{
    public class JobThree : Job
    {
        public JobThree(GameEngine engine)
        {
            Name = "Mad Libs";
            Active = false;
            Unlocked = false;
            BaseIncome = 1.56f;
            BaseXP = 1563;
            NextLevel = 1563;
            JobType = JobTypes.Basics;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.JobTwo.Level > 19)
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
            return engine.GameData.JobTwo.Name + ": " + engine.GameData.JobTwo.Level + "/20";
        }
    }
}

