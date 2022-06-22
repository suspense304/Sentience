﻿using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Jobs
{
    public class JobOne : Job
    {
        public JobOne(GameEngine engine)
        {
            Name = "Hello World";
            Active = true;
            Unlocked = true;
            BaseIncome = 0.04f;
            BaseXP = 40;
            NextLevel = 40;
            JobType = JobTypes.Basics;
        }
    }
}