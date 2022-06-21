using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Jobs
{
    public class JobOne : Job
    {
        public JobOne(GameEngine engine)
        {
            Name = "Hello World";
            Active = false;
            Unlocked = true;
            BaseIncome = 0.04f;
            BaseXP = 120;
            NextLevel = 120;
            JobType = JobTypes.Basics;
        }
    }
}
