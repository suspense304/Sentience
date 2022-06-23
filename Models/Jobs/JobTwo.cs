using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
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
            BaseIncome = .35f;
            BaseXP = 250;
            NextLevel = 250;
            JobType = JobTypes.Basics;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.JobOne.Level > 9)
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
            return engine.GameData.JobOne.Name + ": " + engine.GameData.JobOne.Level + "/10";
        }
    }
}
