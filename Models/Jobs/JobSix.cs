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
            BaseIncome = 21.81f;
            BaseXP = 104175;
            NextLevel = 104175;
            JobType = JobTypes.Basics;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.JobFive.Level > 49 && engine.GameData.ResearchTwo.Level > 99)
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
            return engine.GameData.JobFive.Name + ": " + engine.GameData.JobFive.Level + "/50   " + engine.GameData.ResearchOne.Name + ": " + engine.GameData.ResearchOne.Level + "/100";
        }
    }
}
