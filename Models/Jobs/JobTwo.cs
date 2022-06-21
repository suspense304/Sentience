﻿using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Jobs
{
    public class JobTwo : Job
    {
        public JobTwo(GameEngine engine)
        {
            Name = "Odd Or Even";
            Active = false;
            Unlocked = false;
            BaseIncome = .36f;
            BaseXP = 750;
            NextLevel = 750;
            JobType = JobTypes.Basics;
        }
        public bool CanUnlock(GameEngine engine)
        {
            return (engine.JobOne.Level > 9) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.JobOne.Name + ": " + engine.JobOne.Level + "/10";
        }
    }
}
