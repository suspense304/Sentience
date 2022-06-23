using Blazored.Toast.Services;
using Sentience.Models.Research;

namespace Sentience.Models.Jobs
{
    public class JobSix: Job
    {
        public JobSix(GameEngine engine)
        {
            Name = "Pong";
            Active = false;
            Unlocked = false;
            BaseIncome = 94.13f;
            BaseXP = 382354;
            NextLevel = 382354;
            JobType = JobTypes.Basics;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.JobFive.Level > 99 && engine.GameData.ResearchTwo.Level > 199)
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
            return engine.GameData.JobFive.Name + ": " + engine.GameData.JobFive.Level + "/100   " + engine.GameData.ResearchTwo.Name + ": " + engine.GameData.ResearchTwo.Level + "/200";
        }
    }
}
