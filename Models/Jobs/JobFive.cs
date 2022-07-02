using Blazored.Toast.Services;
using Sentience.Models.Research;

namespace Sentience.Models.Jobs
{
    public class JobFive: Job
    {
        public JobFive(GameEngine engine)
        {
            Name = "Tic-Tac-Toe";
            Active = false;
            Unlocked = false;
            BaseIncome = 11.23M;
            BaseXP = 402953;
            NextLevel = 402953;
            JobType = JobTypes.Basics;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.JobFour.Level > 24 && engine.GameData.ResearchTwo.Level > 99)
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
            return engine.GameData.JobFour.Name + ": " + engine.GameData.JobFour.Level + "/25   " + engine.GameData.ResearchTwo.Name + ": " + engine.GameData.ResearchTwo.Level + "/100";
        }
    }
}
